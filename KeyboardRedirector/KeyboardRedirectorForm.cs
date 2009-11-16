#region Copyright (C) 2009 Nate

/* 
 *	Copyright (C) 2009 Nate
 *	http://nate.dynalias.net
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MS;

namespace KeyboardRedirector
{
    public partial class KeyboardRedirectorForm : MinimizeToTrayForm
    {
        InputDevice _inputDevice;
        List<DeviceInformation> _keyboards;
        List<KeyToHookInformation> _keysToHook;
        Dictionary<string, KeyCombination> _keyCombinations;
        ActionPerformer _actionPerformer;
        IconExtractor.ExecutableImageList _imageList;
        static bool _disableGlobalKeyboardHook = false;

        public static bool DisableGlobalKeyboardHook
        {
            get { return _disableGlobalKeyboardHook; }
            set { _disableGlobalKeyboardHook = value; }
        }

        class DeviceInformation
        {
            public InputDevice.DeviceInfo DeviceInfo = null;

            public DeviceInformation(InputDevice.DeviceInfo DeviceInfo)
            {
                this.DeviceInfo = DeviceInfo;
            }

            public override string ToString()
            {
                return DeviceInfo.Name;
            }
        }

        class KeyToHookInformation
        {
            public KeyCombination KeyCombo;
            public DateTime SeenAt;

            public KeyToHookInformation(KeyCombination KeyCombo)
            {
                this.KeyCombo = new KeyCombination(KeyCombo);
                this.SeenAt = DateTime.Now;
            }
        }

        public KeyboardRedirectorForm()
        {
            Log.MainLog.SetFilename(Settings.SettingsPath + @"main.log");

            _keyboards = new List<DeviceInformation>();
            _keysToHook = new List<KeyToHookInformation>();
            _keyCombinations = new Dictionary<string, KeyCombination>();

            InitializeComponent();

            

            treeViewKeys.Nodes.Clear();
            panelKeyboardProperties.Location = new Point(3, 3);
            panelKeyboardProperties.Size = new Size(panelKeyboardProperties.Parent.Size.Width - 6, panelKeyboardProperties.Parent.Size.Height - 6);
            panelKeyProperties.Location = new Point(3, 3);
            panelKeyProperties.Size = new Size(panelKeyProperties.Parent.Size.Width - 6, panelKeyProperties.Parent.Size.Height - 6);

            _actionPerformer = new ActionPerformer();
            _actionPerformer.StatusMessage += new ActionPerformer.StatusMessageHandler(_actionPerformer_StatusMessage);
            _actionPerformer.StartProcessingThread();

            _imageList = new IconExtractor.ExecutableImageList(imageListApplications);

            if (Settings.Current.Applications.FindByName("Default") == null)
            {
                SettingsApplication app = new SettingsApplication();
                app.Name = "Default";
                Settings.Current.Applications.Add(app);
                Settings.Save();
            }
            listViewApplicationsInFocus.AddColumn("Application in focus", -1, "ApplicationName");

            _inputDevice = new InputDevice(Handle);
            _inputDevice.DeviceEvent += new InputDevice.DeviceEventHandler(InputDevice_DeviceEvent);

            RefreshDevices();

            NotifyIcon.ContextMenuStrip = contextMenuStripNotifyIcon;

            // Only start the keyboard hook if we're not debugging.
            //string exeFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            //if (exeFilename.EndsWith(".vshost.exe") == false)
            {
                KeyboardHookExternal.Current.SetHook(Handle, 0x401, 0x402);
                KeyboardHookExternal.Current.KeyEvent += new KeyHookEventHandler(KeyboardHook_KeyEvent);
                KeyboardHookExternal.Current.KeyEventLowLevel += new KeyHookEventHandler(KeyboardHook_KeyEventLowLevel);
            }

            timerMinimiseOnStart.Start();
        }

        private void KeyboardRedirectorForm_Load(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.checkBoxCaptureLowLevel, "Capture low level keystrokes (not keyboard specific)");
        }

        private void timerMinimiseOnStart_Tick(object sender, EventArgs e)
        {
            timerMinimiseOnStart.Stop();
            if (Settings.Current.MinimizeOnStart)
            {
                checkBoxMinimiseOnStart.Checked = Settings.Current.MinimizeOnStart;
                SendToTray();
            }
        }

        private void KeyboardRedirectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _actionPerformer.StopProcessingThread();
            KeyboardHookExternal.Current.ClearHook();
        }

        private void RefreshDevices()
        {
            if (_inputDevice != null)
            {
                List<string> keyboardsBeforeRefresh = new List<string>();
                foreach (DeviceInformation deviceInformation in _keyboards)
                {
                    keyboardsBeforeRefresh.Add(deviceInformation.DeviceInfo.DeviceName);
                }

                _inputDevice.EnumerateDevices();
                foreach (InputDevice.DeviceInfo info in _inputDevice.DeviceList.Values)
                {
                    if (info.DeviceType != InputDevice.DeviceType.Keyboard)
                        continue;
                    if (info.DeviceHandle == IntPtr.Zero)
                        continue;

                    if (keyboardsBeforeRefresh.Contains(info.DeviceName) == false)
                    {
                        _keyboards.Add(new DeviceInformation(info));

                        SettingsKeyboard keyboard = Settings.Current.Keyboards.FindByDeviceName(info.DeviceName);
                        if (keyboard == null)
                        {
                            keyboard = new SettingsKeyboard();
                            keyboard.Name = info.Name;
                            keyboard.DeviceName = info.DeviceName;
                            Settings.Current.Keyboards.Add(keyboard);
                            Settings.Save();
                            WriteEvent("New Keyboard Added : " + keyboard.Name + Environment.NewLine);
                        }
                        else
                        {
                            WriteEvent("Keyboard Added : " + keyboard.Name + Environment.NewLine);
                        }
                    }
                    else
                    {
                        keyboardsBeforeRefresh.Remove(info.DeviceName);
                    }
                }

                foreach (string deviceName in keyboardsBeforeRefresh)
                {
                    DeviceInformation deviceInformation = FindKeyboardDevice(deviceName);
                    _keyboards.Remove(deviceInformation);

                    WriteEvent("Keyboard Removed : " + deviceInformation.DeviceInfo.Name + Environment.NewLine);
                }

                RefreshTreeView();
            }
        }

        protected override void WndProc(ref Message message)
        {
            //System.Diagnostics.Debug.WriteLine(message.ToString());

            if (_inputDevice != null)
            {
                if (_inputDevice.IsInputMessage(message))
                {
                    _inputDevice.ProcessMessage(message);
                }
            }

            if (KeyboardHookExternal.Current.IsHookMessage(message))
            {
                // If there's a WM_INPUT message in the message queue then we'll process that first
                Message msg;
                Win32.PeekMessage(out msg, this.Handle, (uint)Win32.WM.INPUT, (uint)Win32.WM.INPUT, Win32.PeekMessageRemoveFlag.PM_REMOVE);
                if (msg.Msg != 0)
                {
                    //Log.MainLog.WriteInfo("PeekMessage found WM_INPUT message waiting : " + msg.ToString());
                    Win32.DispatchMessage(msg);
                    //Log.MainLog.WriteInfo("Finished dispatching");
                }
            }

            int result = KeyboardHookExternal.Current.ProcessMessage(message);
            if (result != 0)
            {
                message.Result = new IntPtr(-1);
                return;
            }

            if (message.Msg == (int)Win32.WM.DEVICECHANGE)
            {
                RefreshDevices();
            }
            
            base.WndProc(ref message);
        }

        void KeyboardHook_KeyEvent(object sender, KeyHookEventArgs e)
        {
            bool block = false;
            lock (_keysToHook)
            {
                //Log.MainLog.WriteDebug(" - KeyEvent");
                //Log.MainLog.WriteDebug("   - KeyCombo to match: " + e.KeyCombination.ToString());
                //Log.MainLog.WriteDebug("   - KeysToHook: " + _keysToHook.Count.ToString());
                for (int i = 0; i < _keysToHook.Count; i++)
                {
                    if (_keysToHook[i].KeyCombo.Equals(e.KeyCombination))
                    {
                        //Log.MainLog.WriteDebug("     - Equal: " + _keysToHook[i].KeyCombo.ToString() + i.ToString());
                        block = true;
                        _keysToHook.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        //Log.MainLog.WriteDebug("     - Unequal: " + _keysToHook[i].KeyCombo.UnequalReason(e.KeyCombination) + " : " + _keysToHook[i].KeyCombo.ToString());
                    }
                }

                // Remove any old stale entries
                while ((_keysToHook.Count > 0) && (DateTime.Now.Subtract(_keysToHook[0].SeenAt).TotalMilliseconds > 500))
                {
                    //Log.MainLog.WriteDebug("   - RemovingOldKey: " + _keysToHook[0].KeyCombo.ToString());
                    _keysToHook.RemoveAt(0);
                }
            }

            //DEBUG: Disable blocking for testing purposes.
            //block = false;

            string blockText = "      ";
            if (block)
            {
                blockText = "block ";
                e.Handled = true;
            }

            if (e.KeyCombination.KeyDown)
                WriteHookEvent("Down : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
            else
                WriteHookEvent("Up   : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
        }

        KeyCombination _lastLowLevelKeyCombo = null;
        void KeyboardHook_KeyEventLowLevel(object sender, KeyHookEventArgs e)
        {
            if (_disableGlobalKeyboardHook == true)
                return;

            if ((richTextBoxKeyDetector.Focused) && (checkBoxCaptureLowLevel.Checked))
            {
                e.Handled = true;

                if (e.KeyCombination.TransitionToKeyUp)
                {
                    WriteLowLevelEvent("Adding key" + Environment.NewLine);
                    AddAndSelectKey("LowLevel", _lastLowLevelKeyCombo);
                }
            }

            lock (Settings.Current)
            {
                SettingsKeyboard keyboard = Settings.Current.LowLevelKeyboard;
                if (keyboard != null)
                {
                    SettingsKeyboardKey settingsKey = keyboard.Keys.FindKey(e.KeyCombination);

                    if ((settingsKey != null) && settingsKey.Enabled)
                    {
                        // Intercept key if we need to
                        if (e.KeyCombination.KeyDown && settingsKey.Capture)
                        {
                            e.Handled = true;
                        }

                        _actionPerformer.EnqueueKey(settingsKey, e.KeyCombination.KeyDown);
                    }
                }
            }

            _lastLowLevelKeyCombo = e.KeyCombination;

            string blockText = "      ";
            if (e.Handled)
                blockText = "block ";
            if (e.KeyCombination.KeyDown)
                WriteLowLevelEvent("Down : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
            else
                WriteLowLevelEvent("Up   : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
        }

        void InputDevice_DeviceEvent(object sender, InputDevice.DeviceInfo dInfo, InputDevice.RAWINPUT rawInput)
        {
            if (rawInput.header.dwType == InputDevice.DeviceType.Keyboard)
            {
                Keys key = (Keys)rawInput.data.keyboard.VKey;
                bool keyDown = ((rawInput.data.keyboard.Message == Win32.WM.KEYDOWN) ||
                                (rawInput.data.keyboard.Message == Win32.WM.SYSKEYDOWN));
                bool extended = ((rawInput.data.keyboard.Flags & InputDevice.RawKeyboardFlags.E0) != 0);

                string text = string.Format("{0} 0x{1:x}({2}) makecode:0x{3:x} flags:0x{4:x} extraInfo:{5} ext:{6}",
                    rawInput.header.dwType,
                    rawInput.data.keyboard.VKey,
                    key,
                    rawInput.data.keyboard.MakeCode,
                    rawInput.data.keyboard.Flags,
                    rawInput.data.keyboard.ExtraInformation,
                    extended);
                
                if (rawInput.data.keyboard.VKey == 0xff)
                {
                    WriteWMInputEvent("0x" + dInfo.DeviceHandle.ToInt32().ToString("x8") + " " + rawInput.data.keyboard.Message.ToString().PadRight(10) + " : " + text + " (ignoring VK 0xff)" + Environment.NewLine);
                    return;
                }

                WriteWMInputEvent("0x" + dInfo.DeviceHandle.ToInt32().ToString("x8") + " " + rawInput.data.keyboard.Message.ToString().PadRight(10) + " : " + text + Environment.NewLine);

                if (_keyCombinations.ContainsKey(dInfo.DeviceName) == false)
                {
                    _keyCombinations.Add(dInfo.DeviceName, new KeyCombination());
                }
                KeyCombination keyCombo = _keyCombinations[dInfo.DeviceName];
                KeyCombination lastKeyCombo = new KeyCombination(keyCombo);
                keyCombo.KeyPress(keyDown, key, extended);

                if ((richTextBoxKeyDetector.Focused) && (checkBoxCaptureLowLevel.Checked == false))
                {
                    lock (_keysToHook)
                    {
                        //Log.MainLog.WriteDebug("RawInput-AddingKeyForDetector: " + keyCombo.ToString());
                        _keysToHook.Add(new KeyToHookInformation(keyCombo));
                    }

                    if (keyCombo.TransitionToKeyUp)
                    {
                        AddAndSelectKey(dInfo.DeviceName, lastKeyCombo);
                    }

                    return;
                }

                lock (Settings.Current)
                {
                    SettingsKeyboard keyboard = Settings.Current.Keyboards.FindByDeviceName(dInfo.DeviceName);
                    if (keyboard != null)
                    {
                        SettingsKeyboardKey settingsKey = keyboard.Keys.FindKey(keyCombo);

                        if ((settingsKey != null) && settingsKey.Enabled)
                        {
                            // Intercept key if we need to
                            if (keyboard.CaptureAllKeys || (keyCombo.KeyDown && settingsKey.Capture))
                            {
                                lock (_keysToHook)
                                {
                                    //Log.MainLog.WriteDebug("RawInput-AddingKeyForCapture: " + keyCombo.ToString());
                                    _keysToHook.Add(new KeyToHookInformation(keyCombo));
                                }
                            }

                            _actionPerformer.EnqueueKey(settingsKey, keyDown);
                        }
                        else
                        {
                            if (keyboard.CaptureAllKeys)
                            {
                                lock (_keysToHook)
                                {
                                    //Log.MainLog.WriteDebug("RawInput-AddingKeyForCaptureAll: " + keyCombo.ToString());
                                    _keysToHook.Add(new KeyToHookInformation(keyCombo));
                                }
                            }
                        }

                    }
                }
            }
        }

        void _actionPerformer_StatusMessage(string text)
        {
            this.Invoke(new WriteDelegate(WriteEvent), new object[] { text + Environment.NewLine });
            //WriteEvent(text + Environment.NewLine);
        }

        private delegate void WriteDelegate(string message);
        private void WriteEvent(string message)
        {
            System.Diagnostics.Debug.Write(message);
            richTextBoxEvents.AppendText(message);
            Log.MainLog.WriteInfo(message.TrimEnd('\r', '\n'));
        }
        private void WriteLowLevelEvent(string message)
        {
            System.Diagnostics.Debug.Write("LL " + message);
            richTextBoxKeyEventsLowLevel.AppendText(message);
            Log.MainLog.WriteInfo("LL " + message.TrimEnd('\r', '\n'));
        }
        private void WriteHookEvent(string message)
        {
            System.Diagnostics.Debug.Write(message);
            richTextBoxKeyEventsHook.AppendText(message);
            Log.MainLog.WriteInfo("   " + message.TrimEnd('\r', '\n'));
        }
        private void WriteWMInputEvent(string message)
        {
            System.Diagnostics.Debug.Write(message);
            richTextBoxKeyEventsWMInput.AppendText(message);
            Log.MainLog.WriteInfo(message.TrimEnd('\r', '\n'));
        }

        private DeviceInformation FindKeyboardDevice(string deviceName)
        {
            foreach (DeviceInformation info in _keyboards)
            {
                if (info.DeviceInfo.DeviceName == deviceName)
                    return info;
            }
            return null;
        }




        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBoxKeyDetector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Menu)
                e.Handled = true;
        }

        private void checkBoxMinimiseOnStart_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings.Current.MinimizeOnStart != checkBoxMinimiseOnStart.Checked)
            {
                Settings.Current.MinimizeOnStart = checkBoxMinimiseOnStart.Checked;
                Settings.Save();
            }
        }


        private void RefreshTreeView()
        {
            lock (treeViewKeys)
            {
                treeViewKeys.BeginUpdate();

                // Update the keyboard nodes

                List<TreeNode> staleKeyboardNodes = new List<TreeNode>();
                foreach (TreeNode node in treeViewKeys.Nodes)
                {
                    staleKeyboardNodes.Add(node);
                }

                List<SettingsKeyboard> keyboards = new List<SettingsKeyboard>();
                keyboards.Add(Settings.Current.LowLevelKeyboard);
                keyboards.AddRange(Settings.Current.Keyboards);

                foreach (SettingsKeyboard keyboard in keyboards)
                {
                    // Find existing node and remove it from the stale list
                    TreeNode keyboardNode = FindTreeNode(keyboard, treeViewKeys.Nodes, false);
                    if (keyboardNode != null)
                    {
                        staleKeyboardNodes.Remove(keyboardNode);
                    }
                    else
                    {
                        // No keyboard node. We'll create one.
                        keyboardNode = new TreeNode(keyboard.Name);
                        keyboardNode.Tag = keyboard;

                        treeViewKeys.Nodes.Add(keyboardNode);
                    }

                    // Update node data
                    keyboardNode.Text = keyboard.Name;

                    DeviceInformation deviceInformation = FindKeyboardDevice(keyboard.DeviceName);
                    int imageIndex = 0;
                    if (keyboard == Settings.Current.LowLevelKeyboard)
                        imageIndex = 2;
                    else if (deviceInformation == null)
                        imageIndex = 1;
                    else if (keyboard.CaptureAllKeys)
                        imageIndex = 3;
                    keyboardNode.ImageIndex = imageIndex;
                    keyboardNode.SelectedImageIndex = imageIndex;


                    // Update keys
                    List<TreeNode> staleKeyNodes = new List<TreeNode>();
                    foreach (TreeNode node in keyboardNode.Nodes)
                    {
                        staleKeyNodes.Add(node);
                    }

                    foreach (SettingsKeyboardKey key in keyboard.Keys)
                    {
                        // Find existing node and remove it from the stale list
                        TreeNode keyNode = FindTreeNode(key, keyboardNode.Nodes, false);
                        if (keyNode != null)
                        {
                            staleKeyNodes.Remove(keyNode);
                        }
                        else
                        {
                            // No key node. We'll create one.
                            keyNode = new TreeNode(key.ToString());
                            keyNode.Tag = key;

                            bool expand = (keyboardNode.Nodes.Count == 0);
                            keyboardNode.Nodes.Add(keyNode);
                            if (expand)
                                keyboardNode.Expand();
                        }

                        // Update node data
                        keyNode.Text = key.ToString();
                        imageIndex = 2;
                        if (!key.Enabled)
                            imageIndex = 1;
                        else if (key.Capture)
                            imageIndex = 3;
                        else if (keyboard.CaptureAllKeys)
                            imageIndex = 4;

                        keyNode.ImageIndex = imageIndex;
                        keyNode.SelectedImageIndex = imageIndex;
                    }

                    // Remove stale key nodes.
                    foreach (TreeNode node in staleKeyNodes)
                    {
                        keyboardNode.Nodes.Remove(node);
                    }

                }

                // Remove stale keyboard nodes.
                foreach (TreeNode node in staleKeyboardNodes)
                {
                    treeViewKeys.Nodes.Remove(node);
                }


                treeViewKeys.EndUpdate();
            }
        }

        private TreeNode FindTreeNode(object tag, TreeNodeCollection baseCollection, bool includeChildren)
        {
            lock (treeViewKeys)
            {
                List<TreeNodeCollection> collections = new List<TreeNodeCollection>();
                collections.Add(baseCollection);
                while (collections.Count > 0)
                {
                    foreach (TreeNode node in collections[0])
                    {
                        if (node.Tag.Equals(tag))
                            return node;

                        if (includeChildren && (node.Nodes.Count > 0))
                            collections.Add(node.Nodes);
                    }
                    collections.RemoveAt(0);
                }
            }
            return null;
        }

        private void AddAndSelectKey(string deviceName, KeyCombination keyCombo)
        {
            SettingsKeyboard keyboard;
            if (deviceName == "LowLevel")
                keyboard = Settings.Current.LowLevelKeyboard;
            else
                keyboard = Settings.Current.Keyboards.FindByDeviceName(deviceName);
            if (keyboard == null)
                return;

            lock (treeViewKeys)
            {
                TreeNode keyboardNode = FindTreeNode(keyboard, treeViewKeys.Nodes, false);
                if (keyboardNode == null)
                    return;

                SettingsKeyboardKey key = keyboard.Keys.FindKey(keyCombo);
                if (key != null)
                {
                    WriteEvent("Selecting Key: " + keyCombo.ToString() + Environment.NewLine);

                    TreeNode node = FindTreeNode(key, keyboardNode.Nodes, true);
                    if (node != null)
                        treeViewKeys.SelectedNode = node;
                }
                else
                {
                    WriteEvent("Adding Key: " + keyCombo.ToString() + Environment.NewLine);

                    key = new SettingsKeyboardKey(keyCombo);
                    keyboard.Keys.Add(key);
                    Settings.Save();

                    RefreshTreeView();

                    TreeNode node = FindTreeNode(key, keyboardNode.Nodes, true);
                    if (node != null)
                        treeViewKeys.SelectedNode = node;
                }
            }

        }


        private void treeViewKeys_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SettingsKeyboard keyboard = e.Node.Tag as SettingsKeyboard;
            SettingsKeyboardKey key = e.Node.Tag as SettingsKeyboardKey;

            panelKeyboardProperties.Visible = false;
            panelKeyProperties.Visible = false;

            if (keyboard != null)
            {
                panelKeyboardProperties.Visible = true;

                StringBuilder details = new StringBuilder();
                details.AppendLine("DeviceName: " + keyboard.DeviceName);

                DeviceInformation deviceInformation = FindKeyboardDevice(keyboard.DeviceName);
                if ((deviceInformation == null) || (deviceInformation.DeviceInfo == null))
                {
                    details.Append("Device not present.");
                }
                else
                {
                    details.Append("DeviceDesc: " + deviceInformation.DeviceInfo.DeviceDesc);
                }

                if (keyboard == Settings.Current.LowLevelKeyboard)
                {
                    panelKeyboardProperties.Enabled = false;
                    textBoxKeyboardDetails.Text = "Low level keyboard hook";
                }
                else
                {
                    panelKeyboardProperties.Enabled = true;
                    textBoxKeyboardDetails.Text = details.ToString();
                }
                textBoxKeyboardName.Text = keyboard.Name;

                checkBoxCaptureAllKeys.Checked = keyboard.CaptureAllKeys;

            }
            else if (key != null)
            {
                panelKeyProperties.Visible = true;

                RefreshKeyDetails();
            }
        }

        private void treeViewKeys_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteSelectedTreeViewEvent();
                }
            }
        }

        private void contextMenuStripTreeViewEvents_Opening(object sender, CancelEventArgs e)
        {
            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            //SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (keyboard != null)
            {
                if (keyboard.DeviceName == "LowLevel")
                {
                    deleteToolStripMenuItem.Visible = false;
                    removeAllKeysToolStripMenuItem.Visible = true;
                    executeActionsToolStripMenuItem.Visible = false;
                }
                else
                {
                    deleteToolStripMenuItem.Visible = true;
                    removeAllKeysToolStripMenuItem.Visible = true;
                    executeActionsToolStripMenuItem.Visible = false;
                }
            }
            else
            {
                deleteToolStripMenuItem.Visible = true;
                removeAllKeysToolStripMenuItem.Visible = false;
                executeActionsToolStripMenuItem.Visible = true;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedTreeViewEvent();
        }

        private void removeAllKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllKeysFromTreeViewKeyboard();
        }

        private void executeActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteActionsFromTreeViewKey();
        }


        private SettingsKeyboard GetSelectedKeyboardFromTreeView()
        {
            TreeNode node = treeViewKeys.SelectedNode;
            if (node == null)
                return null;

            return node.Tag as SettingsKeyboard;
        }

        private SettingsKeyboardKey GetSelectedKeyFromTreeView()
        {
            TreeNode node = treeViewKeys.SelectedNode;
            if (node == null)
                return null;

            return node.Tag as SettingsKeyboardKey;
        }

        private void DeleteSelectedTreeViewEvent()
        {
            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (keyboard != null)
            {
                if (keyboard == Settings.Current.LowLevelKeyboard)
                    return;

                DialogResult result = MessageBox.Show(this, "Are you sure you want to delete this keyboard?" + Environment.NewLine + keyboard.Name, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result != DialogResult.Yes)
                    return;
                Settings.Current.Keyboards.Remove(keyboard);
                Settings.Save();
                RefreshTreeView();
            }
            else if (key != null)
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you want to delete this key?" + Environment.NewLine + key.Name, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result != DialogResult.Yes)
                    return;
                TreeNode node = treeViewKeys.SelectedNode;
                keyboard = node.Parent.Tag as SettingsKeyboard;
                keyboard.Keys.Remove(key);
                Settings.Save();
                RefreshTreeView();
            }
        }

        private void RemoveAllKeysFromTreeViewKeyboard()
        {
            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            if (keyboard != null)
            {
                keyboard.Keys.Clear();
                Settings.Save();
                RefreshTreeView();
            }
        }

        private void ExecuteActionsFromTreeViewKey()
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                _actionPerformer.EnqueueKey(key, true);
            }
        }

        private void RefreshKeyDetails()
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
                throw new NullReferenceException("Selected key value missing");

            StringBuilder details = new StringBuilder();
            details.AppendLine("Key: " + key.ToString());

            labelKeyDetails.Text = details.ToString();
            textBoxKeyName.Text = key.Name;
            checkBoxKeyEnabled.Checked = key.Enabled;
            checkBoxKeyCapture.Checked = key.Capture;

            RefreshApplicationsList();
            listViewApplicationsInFocus.SelectedIndex = 0;

            checkBoxKeyCapture.Enabled = key.Enabled;
            labelKeyCapture.Enabled = key.Enabled;
            groupBoxActions.Enabled = key.Enabled;

        }

        private void treeViewKeys_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeViewKeys.GetNodeAt(e.Location);
                treeViewKeys.SelectedNode = node;
            }
        }

        private void checkBoxCaptureAllKeys_CheckedChanged(object sender, EventArgs e)
        {
            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            if (keyboard != null)
            {
                if (keyboard.CaptureAllKeys != checkBoxCaptureAllKeys.Checked)
                {
                    keyboard.CaptureAllKeys = checkBoxCaptureAllKeys.Checked;
                    Settings.Save();
                    RefreshTreeView();
                }
            }

        }

        private void checkBoxKeyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                if (key.Enabled != checkBoxKeyEnabled.Checked)
                {
                    key.Enabled = checkBoxKeyEnabled.Checked;
                    Settings.Save();
                    RefreshTreeView();
                    RefreshKeyDetails();
                }
            }

        }

        private void checkBoxKeyCapture_CheckedChanged(object sender, EventArgs e)
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                key.Capture = checkBoxKeyCapture.Checked;
                Settings.Save();
                RefreshTreeView();
            }
        }


        private void textBoxKeyboardName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxKeyboardName.Text.Length == 0)
                return;

            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            if (keyboard != null)
            {
                keyboard.Name = textBoxKeyboardName.Text;
                Settings.Save();
                RefreshTreeView();
            }

        }

        private void textBoxKeyName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxKeyName.Text.Length == 0)
                return;

            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                key.Name = textBoxKeyName.Text;
                Settings.Save();
                RefreshTreeView();

                labelKeyDetails.Text = "Key: " + key.ToString();
            }

        }

        private void RefreshApplicationsList()
        {
            SettingsKeyboardKeyFocusedApplication previouslySelectedApp = listViewApplicationsInFocus.SelectedItem as SettingsKeyboardKeyFocusedApplication;

            listViewApplicationsInFocus.DataSource = null;

            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
            {
                listViewApplicationsInFocus.Enabled = false;
                return;
            }
            listViewApplicationsInFocus.Enabled = true;

            SettingsKeyboardKeyFocusedApplication defaultFocusedApplication;
            defaultFocusedApplication = key.FocusedApplications.FindByName("Default");
            if (defaultFocusedApplication == null)
            {
                defaultFocusedApplication = new SettingsKeyboardKeyFocusedApplication();
                defaultFocusedApplication.Application = Settings.Current.Applications.FindByName("Default");
                key.FocusedApplications.Insert(0, defaultFocusedApplication);
                Settings.Save();
            }

            listViewApplicationsInFocus.DataSource = key.FocusedApplications;

            int selectIndex = 0;
            foreach (ListViewItem item in listViewApplicationsInFocus.Items)
            {
                SettingsKeyboardKeyFocusedApplication focussedApp = item.Tag as SettingsKeyboardKeyFocusedApplication;

                if (focussedApp.Application.Name == "Default")
                    item.ImageIndex = 0;
                else
                    item.ImageIndex = _imageList.GetExecutableIndex(focussedApp.Application.Executable);

                if (focussedApp == previouslySelectedApp)
                    selectIndex = item.Index;
            }
            listViewApplicationsInFocus.SelectedIndex = selectIndex;

        }

        private void listViewApplicationsInFocus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshActionList();

            // Only enable remove app button if any but the default app are selected
            if ((listViewApplicationsInFocus.SelectedIndices.Count == 1) &&
                (listViewApplicationsInFocus.SelectedIndices[0] > 0))
                buttonRemoveApplication.Enabled = true;
            else
                buttonRemoveApplication.Enabled = false;
        }

        private void buttonAddApplications_Click(object sender, EventArgs e)
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
                throw new NullReferenceException("Selected key value missing");

            EditApplicationsDialog dialog = new EditApplicationsDialog();
            DialogResult result = dialog.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            if (dialog.SelectedApplication == null)
                return;

            SettingsKeyboardKeyFocusedApplication focusedApplication = new SettingsKeyboardKeyFocusedApplication();
            focusedApplication.Application = dialog.SelectedApplication;
            key.FocusedApplications.Add(focusedApplication);
            Settings.Save();

            RefreshApplicationsList();
        }

        private void buttonRemoveApplication_Click(object sender, EventArgs e)
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
                throw new NullReferenceException("Selected key value missing");

            SettingsKeyboardKeyFocusedApplication app = listViewApplicationsInFocus.SelectedItem as SettingsKeyboardKeyFocusedApplication;
            if (app == null)
                throw new NullReferenceException("Focussed application value missing");

            key.FocusedApplications.Remove(app);
            Settings.Save();

            RefreshApplicationsList();
        }

        private void RefreshActionList()
        {
            listViewActions.BeginUpdate();
            listViewActions.Items.Clear();

            SettingsKeyboardKeyFocusedApplication app = listViewApplicationsInFocus.SelectedItem as SettingsKeyboardKeyFocusedApplication;
            buttonAddAction.Enabled = (app != null);
            buttonEditAction.Enabled = false;
            buttonRemoveAction.Enabled = false;
            listViewActions.Enabled = (app != null);
            if (app == null)
            {
                listViewActions.EndUpdate();
                return;
            }

            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                foreach (SettingsKeyboardKeyAction action in app.Actions)
                {
                    SettingsKeyboardKeyTypedAction typedAction = action.CurrentActionType;
                    string[] columns = new string[] { typedAction.GetName(), typedAction.GetDetails() };
                    ListViewItem item = new ListViewItem(columns);
                    item.Tag = action;
                    listViewActions.Items.Add(item);
                }
            }

            listViewActions.EndUpdate();
        }

        private void listViewActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEditAction.Enabled = (listViewActions.SelectedIndices.Count == 1);
            buttonRemoveAction.Enabled = (listViewActions.SelectedIndices.Count == 1);
        }

        private void buttonAddAction_Click(object sender, EventArgs e)
        {
            ActionPropertiesDialog dialog = new ActionPropertiesDialog();
            dialog.Action = new SettingsKeyboardKeyAction();
            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
                if (key == null)
                    throw new NullReferenceException("Selected key value missing");

                SettingsKeyboardKeyFocusedApplication focusedApplication = listViewApplicationsInFocus.SelectedItem as SettingsKeyboardKeyFocusedApplication;
                if (focusedApplication == null)
                    throw new NullReferenceException("Focussed application value missing");

                focusedApplication.Actions.Add(dialog.Action);
                Settings.Save();
                RefreshActionList();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewActions.SelectedIndices.Count != 1)
                return;

            int selectedIndex = listViewActions.SelectedIndices[0];
            SettingsKeyboardKeyAction action = listViewActions.SelectedItems[0].Tag as SettingsKeyboardKeyAction;
            if (action == null)
                throw new NullReferenceException("Action value missing");

            ActionPropertiesDialog dialog = new ActionPropertiesDialog();
            dialog.Action = action;
            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Settings.Save();
                RefreshActionList();
                listViewActions.SelectedIndices.Clear();
                listViewActions.SelectedIndices.Add(selectedIndex);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listViewActions.SelectedIndices.Count != 1)
                return;

            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
                throw new NullReferenceException("Selected key value missing");

            SettingsKeyboardKeyFocusedApplication focusedApplication = listViewApplicationsInFocus.SelectedItem as SettingsKeyboardKeyFocusedApplication;
            if (focusedApplication == null)
                throw new NullReferenceException("Focussed application value missing");

            SettingsKeyboardKeyAction action = listViewActions.SelectedItems[0].Tag as SettingsKeyboardKeyAction;
            if (action == null)
                throw new NullReferenceException("Action value missing");

            focusedApplication.Actions.Remove(action);
            Settings.Save();
            RefreshActionList();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            richTextBoxEvents.Clear();
            richTextBoxKeyEventsLowLevel.Clear();
            richTextBoxKeyEventsHook.Clear();
            richTextBoxKeyEventsWMInput.Clear();
        }

    }
}

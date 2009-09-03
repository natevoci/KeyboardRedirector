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

namespace KeyboardRedirector
{
    public partial class KeyboardRedirectorForm : MinimizeToTrayForm
    {
        InputDevice _inputDevice;
        List<DeviceInformation> _keyboards;
        List<KeyToHookInformation> _keysToHook;
        Dictionary<string, KeyCombination> _keyCombinations;
        ActionPerformer _actionPerformer;
        ExecutableImageList _imageList;

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
            _actionPerformer.StartProcessingThread();

            _imageList = new ExecutableImageList(imageListApplications);

            if (Settings.Current.Applications.FindByName("Default") == null)
            {
                SettingsApplication app = new SettingsApplication();
                app.Name = "Default";
                Settings.Current.Applications.Add(app);
                Settings.Save();
            }
            listViewApplicationsInFocus.AddColumn("Application in focus", -1, "Name");

            _inputDevice = new InputDevice(Handle);
            _inputDevice.DeviceEvent += new InputDevice.DeviceEventHandler(InputDevice_DeviceEvent);

            RefreshDevices();

            NotifyIcon.ContextMenuStrip = contextMenuStripNotifyIcon;

            // Only start the keyboard hook if we're not debugging.
            string exeFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            //if (exeFilename.EndsWith(".vshost.exe") == false)
            {
                KeyboardHookExternal.Current.SetHook(Handle, 0x401, 0x402);
                KeyboardHookExternal.Current.KeyEvent += new KeyHookEventHandler(Current_KeyEvent);
                KeyboardHookExternal.Current.KeyEventLowLevel += new KeyHookEventHandler(Current_KeyEventLowLevel);
            }

            timerMinimiseOnStart.Start();
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
                            richTextBoxEvents.AppendText("New Keyboard Added : " + keyboard.Name + Environment.NewLine);
                        }
                        else
                        {
                            richTextBoxEvents.AppendText("Keyboard Added : " + keyboard.Name + Environment.NewLine);
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

                    SettingsKeyboard keyboard = Settings.Current.Keyboards.FindByDeviceName(deviceName);
                    richTextBoxEvents.AppendText("Keyboard Removed : " + keyboard.Name + Environment.NewLine);
                }

                RefreshTreeView();
            }
        }

        private void DebugWrite(string message)
        {
            System.Diagnostics.Debug.Write(message);
            richTextBoxEvents.AppendText(message);
        }

        protected override void WndProc(ref Message message)
        {
            //System.Diagnostics.Debug.WriteLine(message.ToString());

            if (_inputDevice != null)
            {
                _inputDevice.ProcessMessage(message);
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

        void Current_KeyEvent(object sender, KeyHookEventArgs e)
        {
            bool block = false;
            lock (_keysToHook)
            {
                for (int i = 0; i < _keysToHook.Count; i++)
                {
                    if (_keysToHook[i].KeyCombo.Equals(e.KeyCombination))
                    {
                        block = true;
                        _keysToHook.RemoveAt(i);
                    }
                }

                // Remove any old stale entries
                while ((_keysToHook.Count > 0) && (DateTime.Now.Subtract(_keysToHook[0].SeenAt).TotalMilliseconds > 500))
                {
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
                DebugWrite("Down     : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
            else
                DebugWrite("Up       : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
        }

        void Current_KeyEventLowLevel(object sender, KeyHookEventArgs e)
        {
            string blockText = "      ";
            if (e.KeyCombination.KeyDown)
                DebugWrite("LL Down  : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
            else
                DebugWrite("LL Up    : " + blockText + e.KeyCombination.ToString() + Environment.NewLine);
        }

        void InputDevice_DeviceEvent(object sender, InputDevice.DeviceInfo dInfo, InputDevice.RAWINPUT rawInput)
        {
            if (rawInput.header.dwType == InputDevice.DeviceType.Keyboard)
            {
                Keys key = (Keys)rawInput.data.keyboard.VKey;
                bool keyDown = ((rawInput.data.keyboard.Message == Win32.WM.KEYDOWN) ||
                                (rawInput.data.keyboard.Message == Win32.WM.SYSKEYDOWN));

                string text = string.Format("{0} 0x{1:x}({2}) 0x{3:x} 0x{4:x} 0x{5:x} {6}",
                    rawInput.header.dwType,
                    rawInput.data.keyboard.VKey,
                    key,
                    rawInput.data.keyboard.MakeCode,
                    rawInput.data.keyboard.Flags,
                    rawInput.data.keyboard.ExtraInformation,
                    rawInput.data.keyboard.Message);

                DebugWrite("WM_INPUT: 0x" + dInfo.DeviceHandle.ToInt32().ToString("x8") + " " + text + Environment.NewLine);

                if (_keyCombinations.ContainsKey(dInfo.DeviceName) == false)
                {
                    _keyCombinations.Add(dInfo.DeviceName, new KeyCombination());
                }
                KeyCombination keyCombo = _keyCombinations[dInfo.DeviceName];
                KeyCombination lastKeyCombo = new KeyCombination(keyCombo);
                keyCombo.KeyPress(keyDown, key);

                if (richTextBoxKeyDetector.Focused)
                {
                    lock (_keysToHook)
                    {
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

                        // Intercept key if we need to
                        if (keyboard.CaptureAllKeys)
                        {
                            lock (_keysToHook)
                            {
                                _keysToHook.Add(new KeyToHookInformation(keyCombo));
                            }
                        }
                        else if (keyCombo.KeyDown && (settingsKey != null) && settingsKey.Capture)
                        {
                            lock (_keysToHook)
                            {
                                _keysToHook.Add(new KeyToHookInformation(keyCombo));
                            }
                        }

                        if (settingsKey != null)
                        {
                            _actionPerformer.EnqueueKey(settingsKey, keyDown);
                        }

                    }
                }
            }
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

                foreach (SettingsKeyboard keyboard in Settings.Current.Keyboards)
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
                    if (deviceInformation == null)
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
                        if (key.Capture)
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
            SettingsKeyboard keyboard = Settings.Current.Keyboards.FindByDeviceName(deviceName);
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
                    TreeNode node = FindTreeNode(key, keyboardNode.Nodes, true);
                    if (node != null)
                        treeViewKeys.SelectedNode = node;
                }
                else
                {
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

                textBoxKeyboardDetails.Text = details.ToString();
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedTreeViewEvent();
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

        private void RefreshKeyDetails()
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
                return;

            StringBuilder details = new StringBuilder();
            details.AppendLine("Key: " + key.ToString());

            labelKeyDetails.Text = details.ToString();
            textBoxKeyName.Text = key.Name;
            checkBoxCaptureKey.Checked = key.Capture;

            RefreshApplicationsList();
            listViewApplicationsInFocus.SelectedIndex = 0;
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
                keyboard.CaptureAllKeys = checkBoxCaptureAllKeys.Checked;
                Settings.Save();
                RefreshTreeView();
            }

        }

        private void checkBoxCaptureKey_CheckedChanged(object sender, EventArgs e)
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                key.Capture = checkBoxCaptureKey.Checked;
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

        private void buttonEditApplications_Click(object sender, EventArgs e)
        {
            EditApplicationsDialog dialog = new EditApplicationsDialog();
            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                RefreshApplicationsList();
            }
        }

        private void RefreshApplicationsList()
        {
            listViewApplicationsInFocus.DataSource = null;
            listViewApplicationsInFocus.DataSource = Settings.Current.Applications;

            foreach (ListViewItem item in listViewApplicationsInFocus.Items)
            {
                SettingsApplication application = item.Tag as SettingsApplication;
                if (application.Name == "Default")
                    item.ImageIndex = 0;
                else
                    item.ImageIndex = _imageList.GetExecutableIndex(application.Executable);
            }

        }

        private void listViewApplicationsInFocus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshActionList();
        }

        private void RefreshActionList()
        {
            listViewActions.BeginUpdate();
            listViewActions.Items.Clear();

            SettingsApplication application = listViewApplicationsInFocus.SelectedItem as SettingsApplication;
            buttonAddAction.Enabled = (application != null);
            buttonEditAction.Enabled = (application != null);
            buttonRemoveAction.Enabled = (application != null);
            listViewActions.Enabled = (application != null);
            if (application == null)
            {
                listViewActions.EndUpdate();
                return;
            }

            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                SettingsKeyboardKeyFocusedApplication app = key.FocusedApplications.FindByName(application.Name);
                if (app != null)
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
                SettingsApplication application = listViewApplicationsInFocus.SelectedItem as SettingsApplication;
                if (application == null)
                    return;

                SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
                if (key == null)
                    return;

                SettingsKeyboardKeyFocusedApplication focusedApplication;
                focusedApplication = key.FocusedApplications.FindByName(application.Name);
                if (focusedApplication == null)
                {
                    focusedApplication = new SettingsKeyboardKeyFocusedApplication();
                    focusedApplication.Application = application;
                    key.FocusedApplications.Add(focusedApplication);
                }
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

            SettingsApplication application = listViewApplicationsInFocus.SelectedItem as SettingsApplication;
            if (application == null)
                return;

            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
                return;

            SettingsKeyboardKeyFocusedApplication focusedApplication;
            focusedApplication = key.FocusedApplications.FindByName(application.Name);
            if (focusedApplication == null)
                return;

            SettingsKeyboardKeyAction action = listViewActions.SelectedItems[0].Tag as SettingsKeyboardKeyAction;
            if (action == null)
                return;

            focusedApplication.Actions.Remove(action);
            Settings.Save();
            RefreshActionList();
        }

    }
}
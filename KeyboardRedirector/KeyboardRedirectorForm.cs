#region Copyright (C) 2009,2010 Nate

/* 
 *	Copyright (C) 2009,2010 Nate
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

//#define EXTENDED_LOGGING_FOR_WNDPROC

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
        IntPtr _handle = IntPtr.Zero;
        bool _keyDetectorFocused = false;
        bool _captureLowLevelChecked = false;

        const int HookTimeout = 2000;

        InputDevice _inputDevice;
        List<DeviceInformation> _keyboards;

        KeysToHookList _keysSeen = new KeysToHookList();
        KeysToHookList _keysToHook = new KeysToHookList();

        Dictionary<string, KeyCombination> _keyCombinations;
        ActionPerformer _actionPerformer;
        IconExtractor.ExecutableImageList _imageList;
        static bool _disableGlobalKeyboardHook = false;

        Dictionary<SettingsKeyboardKey, double> _antiRepeatTimes;

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


        public KeyboardRedirectorForm()
        {
            Log.MainLog.SetFilename(Settings.SettingsPath + @"main.log");
            Log.MainLog.WriteInfo("--------");

            _keyboards = new List<DeviceInformation>();

            _keyCombinations = new Dictionary<string, KeyCombination>();
            _antiRepeatTimes = new Dictionary<SettingsKeyboardKey, double>();

            _keysSeen.Timeout = 100;
            _keysSeen.TestModifiers = false;

            InitializeComponent();

            treeViewKeys.Nodes.Clear();
            panelKeyboardProperties.Location = new Point(3, 3);
            panelKeyboardProperties.Size = new Size(panelKeyboardProperties.Parent.Size.Width - 6, panelKeyboardProperties.Parent.Size.Height - 6);
            panelKeyProperties.Location = new Point(3, 3);
            panelKeyProperties.Size = new Size(panelKeyProperties.Parent.Size.Width - 6, panelKeyProperties.Parent.Size.Height - 6);
            panelDevices.Location = new Point(3, 3);
            panelDevices.Size = new Size(panelDevices.Parent.Size.Width - 6, panelDevices.Parent.Size.Height - 6);

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

        }

        private void KeyboardRedirectorForm_Load(object sender, EventArgs e)
        {
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

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.checkBoxCaptureLowLevel, "Capture low level keystrokes (not keyboard specific)");

            LoggingOnOff();
            checkBoxLogging.Checked = Settings.Current.LogOn;
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

                        lock (Settings.Current)
                        {
                            SettingsKeyboard keyboard = null;
                            SettingsKeyboardDevice keyboardDevice = Settings.Current.KeyboardDevices.FindByDeviceName(info.DeviceName);
                            if (keyboardDevice == null)
                            {
                                keyboardDevice = new SettingsKeyboardDevice();
                                keyboardDevice.DeviceName = info.DeviceName;
                                keyboardDevice.Name = info.Name;
                                keyboardDevice.KeyboardId = Settings.Current.KeyboardDevices.MaxId() + 1;

                                keyboard = new SettingsKeyboard();
                                keyboard.Name = info.Name;
                                keyboard.KeyboardId = keyboardDevice.KeyboardId;
                                Settings.Current.Keyboards.Add(keyboard);
                                Settings.Current.KeyboardDevices.Add(keyboardDevice);
                                Settings.Save();
                                WriteEvent("New Keyboard Added : " + keyboard.Name + Environment.NewLine);
                            }
                            else
                            {
                                keyboard = Settings.Current.Keyboards.FindByKeyboardId(keyboardDevice.KeyboardId);
                                if (keyboard == null)
                                    WriteEvent("Keyboard Not Used : " + keyboardDevice.DeviceName + Environment.NewLine);
                                else
                                    WriteEvent("Keyboard Added : " + keyboard.Name + Environment.NewLine);
                            }
                        }                        
                    }
                    else
                    {
                        keyboardsBeforeRefresh.Remove(info.DeviceName);
                    }
                }

                foreach (string deviceName in keyboardsBeforeRefresh)
                {
                    DeviceInformation deviceInformation = FindKeyboardDeviceInformation(deviceName);
                    _keyboards.Remove(deviceInformation);

                    WriteEvent("Keyboard Removed : " + deviceInformation.DeviceInfo.Name + Environment.NewLine);
                }

                RefreshTreeView();
                RefreshDevicesListView();
            }
        }

#if EXTENDED_LOGGING_FOR_WNDPROC
        private int _logCounter = 1;
#endif
        protected override void WndProc(ref Message message)
        {
            if (_inputDevice != null)
            {
                if (_inputDevice.IsInputMessage(message) || KeyboardHookExternal.Current.IsHookMessage(message))
                {
#if EXTENDED_LOGGING_FOR_WNDPROC
                    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                    string logPrefix = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + " " + _logCounter++.ToString();
                    stopwatch.Start();

                    Log.MainLog.WriteInfo("ENTER: " + logPrefix + "  " + message.ToString());
                    Log.MainLog.WriteDebug(StackTrace.GetStackTrace());
#endif
                    _keyDetectorFocused = richTextBoxKeyDetector.Focused;
                    _captureLowLevelChecked = checkBoxCaptureLowLevel.Checked;
                    _handle = this.Handle;

                    try
                    {
                        int result = 0;

                        if (KeyboardHookExternal.Current.IsHookMessage(message))
                        {
                            // If there's a WM_INPUT message in the message queue then we'll process that first
                            Win32.MSG msg;
                            Win32.PeekMessage(out msg, _handle, (uint)Win32.WM.INPUT, (uint)Win32.WM.INPUT, Win32.PeekMessageRemoveFlag.PM_REMOVE);
                            if (msg.msg != 0)
                            {
#if EXTENDED_LOGGING_FOR_WNDPROC
                                Log.MainLog.WriteInfo("  PeekMessage found WM_INPUT message waiting : " + msg.ToString());
#endif
                                _inputDevice.ProcessMessage(msg);
#if EXTENDED_LOGGING_FOR_WNDPROC
                                Log.MainLog.WriteInfo("  EndPeekMessage found WM_INPUT message waiting : " + msg.ToString());
#endif
                            }

                            result = ProcessExternalHookMessage(message);
                            if (result != 0)
                            {
                                message.Result = new IntPtr(-1);
                                return;
                            }
                        }

                        // This has to be after
                        if (_inputDevice.IsInputMessage(message))
                        {
                            _inputDevice.ProcessMessage(message);
                        }

                        if (message.Msg == (int)Win32.WM.DEVICECHANGE)
                        {
                            RefreshDevices();
                        }
                    }
                    catch (Exception e)
                    {
                        Log.LogException(e);
                    }
#if EXTENDED_LOGGING_FOR_WNDPROC
                    finally
                    {
                        stopwatch.Stop();
                        if (_inputDevice != null)
                            if (_inputDevice.IsInputMessage(message) || KeyboardHookExternal.Current.IsHookMessage(message))
                                Log.MainLog.WriteInfo("EXIT : " + logPrefix + "  " + stopwatch.ElapsedMilliseconds.ToString());
                    }
#endif
                }
            }

            base.WndProc(ref message);
        }

        private int ProcessExternalHookMessage(Message message)
        {
            int result = 0;
            if (KeyboardHookExternal.Current.IsHookMessage(message))
            {
                Call.WithTimeout(HookTimeout, false, () =>
                {
                    result = KeyboardHookExternal.Current.ProcessMessage(message);
                });
            }
            return result;
        }

        void KeyboardHook_KeyEvent(object sender, KeyHookEventArgs e)
        {
            System.Threading.Thread.CurrentThread.Name = "Hook";

            WriteHookEvent("Enter " + (e.KeyCombination.KeyDown ? "Down" : "Up  ") + " : " + e.KeyCombination.ToString() + Environment.NewLine);

            var hookTime = new System.Diagnostics.Stopwatch();
            hookTime.Start();

            var waitingTime = new System.Diagnostics.Stopwatch();
            var keysSeen = 0;

            while (true)
            {
                // See if the key has been added by the Raw Input API
                if (_keysSeen.Remove(e.KeyCombination.KeyWithExtended, e.KeyCombination.KeyDown))
                    break;

                // If not, wait until there's a change.
                if (!waitingTime.IsRunning)
                {
                    if (e.KeyCombination.KeyWithExtended.Keys == (Keys.LButton | Keys.OemClear))
                    {
                        WriteHookEvent("  Ignoring remote desktop clear command: " + e.KeyCombination.KeyWithExtended.ToString() + " " + (e.KeyCombination.KeyDown ? "down" : "up") + Environment.NewLine);
                        return;
                    }

                    WriteHookEvent("  Waiting for Raw Input to see key: " + e.KeyCombination.KeyWithExtended.ToString() + " " + (e.KeyCombination.KeyDown ? "down" : "up") + Environment.NewLine);
                    waitingTime.Start();
                }

                if (_keysSeen.Count != keysSeen)
                {
                    keysSeen = _keysSeen.Count;
                    WriteHookEvent("   Keys Seen: " + _keysSeen.GetKeysString());
                }

                if (_keysSeen.ChangedEvent.WaitOne(_keysSeen.Timeout) == false)
                    break; // if we timeout then we'll just continue anyway.
            }

            if (waitingTime.IsRunning)
            {
                waitingTime.Stop();
                WriteHookEvent("  Waited " + waitingTime.ElapsedMilliseconds.ToString() + "ms " + "for Raw Input to see key: " + e.KeyCombination.KeyWithExtended.ToString() + " " + (e.KeyCombination.KeyDown ? "down" : "up") + Environment.NewLine);
            }

            bool block = false;
            block = _keysToHook.Remove(e.KeyCombination.KeyWithExtended, e.KeyCombination.KeyDown);

            //DEBUG: Disable blocking for testing purposes.
            //block = false;

            string blockText = "      ";
            if (block)
            {
                blockText = "block ";
                e.Handled = true;
            }

            WriteHookEvent("Finish " + (e.KeyCombination.KeyDown ? "Down" : "Up  ") + " : " + blockText + e.KeyCombination.ToString() + " (" + hookTime.ElapsedMilliseconds.ToString() + "ms)" + Environment.NewLine);
        }


        KeyCombination _lastLowLevelKeyCombo = null;
        void KeyboardHook_KeyEventLowLevel(object sender, KeyHookEventArgs e)
        {
            if (_disableGlobalKeyboardHook == true)
                return;

            System.Threading.Thread.CurrentThread.Name = "LLHook";

            if (_keyDetectorFocused && _captureLowLevelChecked)
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

                        double currPressTime = Utils.Time.GetTime();
                        double lastPressTime = 0.0;
                        if (_antiRepeatTimes.ContainsKey(settingsKey))
                            lastPressTime = _antiRepeatTimes[settingsKey];

                        if (currPressTime - lastPressTime > settingsKey.AntiRepeatTime)
                        {
                            _antiRepeatTimes[settingsKey] = currPressTime;

                            _actionPerformer.EnqueueKey(settingsKey, e.KeyCombination.KeyDown);
                        }
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

#if EXTENDED_LOGGING_FOR_WNDPROC
        private int _processInputDeviceMessageCounter = 0;
#endif
        void InputDevice_DeviceEvent(object sender, InputDevice.DeviceInfo dInfo, InputDevice.RAWINPUT rawInput)
        {
#if EXTENDED_LOGGING_FOR_WNDPROC
            int processInputDeviceMessageCounter = _processInputDeviceMessageCounter++;
            Log.MainLog.WriteDebug("   Begin InputDevice_DeviceEvent" + processInputDeviceMessageCounter.ToString());
#endif
            Call.WithTimeout(HookTimeout, false, () =>
            {
                System.Threading.Thread.CurrentThread.Name = "RawEvent";
                InputDevice_DeviceEvent_Worker(sender, dInfo, rawInput);
            });
#if EXTENDED_LOGGING_FOR_WNDPROC
            Log.MainLog.WriteDebug("   End  InputDevice_DeviceEvent" + processInputDeviceMessageCounter.ToString());
#endif
        }

        void InputDevice_DeviceEvent_Worker(object sender, InputDevice.DeviceInfo dInfo, InputDevice.RAWINPUT rawInput)
        {
#if EXTENDED_LOGGING_FOR_WNDPROC
            Log.MainLog.WriteDebug("    Begin  InputDevice_DeviceEvent_Worker");
#endif
            if (_keysToHook.KeysAdded >= 5)
            {
                if (_keysToHook.KeysRemovedDueToTimeout >= 5)
                {
                    // found a problem
                    //MessageBox.Show(this, "KeyboardRedirector detected that it's keyboard hooks are not working correctly." + Environment.NewLine + "You may need to restart KeyboardRedirector", "KeyboardRedirector", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    WriteEvent("Detected hooks not working correctly. Restarting hooks." + Environment.NewLine);
                    KeyboardHookExternal.Current.RestartHooks();
                    _keysToHook.Clear();
                }
            }

            if (rawInput.header.dwType == InputDevice.DeviceType.Keyboard)
            {
                Keys key = (Keys)rawInput.data.keyboard.VKey;
                bool keyDown = ((rawInput.data.keyboard.Message == Win32.WM.KEYDOWN) ||
                                (rawInput.data.keyboard.Message == Win32.WM.SYSKEYDOWN));
                bool extended = ((rawInput.data.keyboard.Flags & InputDevice.RawKeyboardFlags.E0) != 0);

                string text = string.Format("0x{0:x}({1}) makecode:0x{2:x} flags:0x{3:x} extraInfo:{4} ext:{5}",
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

                if ((_keyDetectorFocused) && (_captureLowLevelChecked == false))
                {
                    lock (_keysToHook)
                    {
#if EXTENDED_LOGGING_FOR_WNDPROC
                        Log.MainLog.WriteDebug("RawInput-AddingKeyForDetector: " + keyCombo.ToString());
#endif
                        _keysToHook.Add(keyCombo.KeyWithExtended, keyCombo.KeyDown);
                    }

                    if (keyCombo.TransitionToKeyUp)
                    {
                        AddAndSelectKey(dInfo.DeviceName, lastKeyCombo);
                    }

                    //Add to the _keysSeen list so that the keyboard hook can continue
                    _keysSeen.Add(new KeysWithExtended(key, extended), keyDown);
                    return;
                }

                lock (Settings.Current)
                {
                    SettingsKeyboardDevice keyboardDevice = Settings.Current.KeyboardDevices.FindByDeviceName(dInfo.DeviceName);
                    if (keyboardDevice != null)
                    {
                        SettingsKeyboard keyboard = Settings.Current.Keyboards.FindByKeyboardId(keyboardDevice.KeyboardId);
                        if (keyboard != null)
                        {
                            SettingsKeyboardKey settingsKey = keyboard.Keys.FindKey(keyCombo);

                            if ((settingsKey != null) && settingsKey.Enabled)
                            {
                                // Intercept key if we need to
                                if (keyboard.CaptureAllKeys || (keyCombo.KeyDown && settingsKey.Capture))
                                {
#if EXTENDED_LOGGING_FOR_WNDPROC
                                    Log.MainLog.WriteDebug("RawInput-AddingKeyForCapture: " + keyCombo.ToString());
#endif
                                    _keysToHook.Add(keyCombo.KeyWithExtended, keyCombo.KeyDown);
                                }

                                double currPressTime = Utils.Time.GetTime();
                                double lastPressTime = 0.0;
                                if (_antiRepeatTimes.ContainsKey(settingsKey))
                                    lastPressTime = _antiRepeatTimes[settingsKey];

                                if (currPressTime - lastPressTime > settingsKey.AntiRepeatTime)
                                {
                                    _antiRepeatTimes[settingsKey] = currPressTime;

                                    _actionPerformer.EnqueueKey(settingsKey, keyDown);
                                }
                            }
                            else
                            {
                                if (keyboard.CaptureAllKeys)
                                {
#if EXTENDED_LOGGING_FOR_WNDPROC
                                    Log.MainLog.WriteDebug("RawInput-AddingKeyForCaptureAll: " + keyCombo.ToString());
#endif
                                    _keysToHook.Add(keyCombo.KeyWithExtended, keyCombo.KeyDown);
                                }
                            }

                        }
                    }
                }

#if EXTENDED_LOGGING_FOR_WNDPROC
                Log.MainLog.WriteDebug("    Ending InputDevice_DeviceEvent_Worker");
#endif

                //Add to the _keysSeen list so that the keyboard hook can continue
                _keysSeen.Add(new KeysWithExtended(key, extended), keyDown);

            }
        }

        delegate void _actionPerformer_StatusMessageDelegate(string text);
        void _actionPerformer_StatusMessage(string text)
        {
            //Log.MainLog.WriteInfo(text.TrimEnd('\r', '\n'));

            // Note: We can't use this.Invoke here because we can end up with a 
            //       recursive call to WndProc.

            if (this.InvokeRequired)
            {
                _actionPerformer_StatusMessageDelegate function = new _actionPerformer_StatusMessageDelegate(_actionPerformer_StatusMessage_Invoke);
                function.BeginInvoke(text, asyncResult => { function.EndInvoke(asyncResult); }, null);
            }
            else
            {
                WriteEvent(text + Environment.NewLine, false);
            }
        }

        void _actionPerformer_StatusMessage_Invoke(string text)
        {
            if (this.InvokeRequired)
                this.Invoke(new WriteDelegate(WriteEvent), text + Environment.NewLine, false);
            else
                WriteEvent(text + Environment.NewLine, false);
        }

        private delegate void WriteDelegate(string message, bool toLogAsWell);
        private void WriteEvent(string message)
        {
            WriteEvent(message, true);
        }
        private void WriteEvent(string message, bool toLogAsWell)
        {
            string counter = (Utils.Time.GetTime() / 1000.0).ToString("0.000").PadLeft(8);
            message = counter + ":" + message;
            System.Diagnostics.Debug.Write(message);
            Log.MainLog.WriteInfo(message.TrimEnd('\r', '\n'));
            AppendToEventsTextBox(message);
        }
        private void WriteLowLevelEvent(string message)
        {
            string counter = (Utils.Time.GetTime() / 1000.0).ToString("0.000").PadLeft(8);
            message = counter + ":" + " LL" + message;
            System.Diagnostics.Debug.Write(message);
            Log.MainLog.WriteInfo(message.TrimEnd('\r', '\n'));
            AppendToEventsTextBox(message);
        }
        private void WriteHookEvent(string message)
        {
            string counter = (Utils.Time.GetTime() / 1000.0).ToString("0.000").PadLeft(8);
            message = counter + ":" + "   " + message;
            System.Diagnostics.Debug.Write(message);
            Log.MainLog.WriteInfo(message.TrimEnd('\r', '\n'));
            AppendToEventsTextBox(message);
        }
        private void WriteWMInputEvent(string message)
        {
            string counter = (Utils.Time.GetTime() / 1000.0).ToString("0.000").PadLeft(8);
            message = counter + ":" + "  " + message;
            System.Diagnostics.Debug.Write(message);
            Log.MainLog.WriteInfo(message.TrimEnd('\r', '\n'));
            AppendToEventsTextBox(message);
        }

        private DeviceInformation FindKeyboardDeviceInformation(string deviceName)
        {
            foreach (DeviceInformation info in _keyboards)
            {
                if (info.DeviceInfo.DeviceName == deviceName)
                    return info;
            }
            return null;
        }

        private void AppendToEventsTextBox(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (checkBoxDisplayLogMessages.Checked)
                        richTextBoxEvents.AppendText(message);
                }));
            }
            if (checkBoxDisplayLogMessages.Checked)
                richTextBoxEvents.AppendText(message);
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
                if (treeViewKeys.Nodes.Count == 0)
                {
                    TreeNode node = new TreeNode("Settings");
                    node.Tag = "Settings";
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 5;
                    treeViewKeys.Nodes.Add(node);
                }

                List<TreeNode> staleKeyboardNodes = new List<TreeNode>();
                foreach (TreeNode node in treeViewKeys.Nodes)
                {
                    if ((node.Tag as string) == "Settings")
                        continue;
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

                    int imageIndex = 0;
                    if (keyboard == Settings.Current.LowLevelKeyboard)
                        imageIndex = 6;
                    //else if (deviceInformation == null)
                    //    imageIndex = 1;
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
                        if (node.Tag == null)
                            continue;

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
            SettingsKeyboard keyboard = null;
            if (deviceName == "LowLevel")
                keyboard = Settings.Current.LowLevelKeyboard;
            else
            {
                SettingsKeyboardDevice keyboardDevice = Settings.Current.KeyboardDevices.FindByDeviceName(deviceName);
                if (keyboardDevice != null)
                    keyboard = Settings.Current.Keyboards.FindByKeyboardId(keyboardDevice.KeyboardId);
            }
            if (keyboard == null)
                return;

            AddAndSelectKeyInTreeDelegate function = new AddAndSelectKeyInTreeDelegate(AddAndSelectKeyInTree);
            function.BeginInvoke(keyCombo, keyboard, asyncResult => { function.EndInvoke(asyncResult); }, null);
        }

        private delegate void AddAndSelectKeyInTreeDelegate(KeyCombination keyCombo, SettingsKeyboard keyboard);
        private void AddAndSelectKeyInTree(KeyCombination keyCombo, SettingsKeyboard keyboard)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AddAndSelectKeyInTreeDelegate(AddAndSelectKeyInTree), keyCombo, keyboard);
                return;
            }

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
            e.Node.NodeFont = new Font(treeViewKeys.Font, FontStyle.Bold);

            SettingsKeyboard keyboard = e.Node.Tag as SettingsKeyboard;
            SettingsKeyboardKey key = e.Node.Tag as SettingsKeyboardKey;

            panelKeyboardProperties.Visible = false;
            panelKeyProperties.Visible = false;
            panelDevices.Visible = false;

            if (keyboard != null)
            {
                panelKeyboardProperties.Visible = true;

                RefreshKeyboardDetails();
            }
            else if (key != null)
            {
                panelKeyProperties.Visible = true;

                RefreshKeyDetails();
            }
            else if ((e.Node.Tag as string) == "Settings")
            {
                panelDevices.Visible = true;

                RefreshDevicesListView();
            }
        }

        private void treeViewKeys_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (treeViewKeys.SelectedNode != null)
                treeViewKeys.SelectedNode.NodeFont = null;
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
                if (keyboard.KeyboardId == 0)   // Low Level
                {
                    deleteToolStripMenuItem.Visible = false;
                }
                else
                {
                    deleteToolStripMenuItem.Visible = true;
                }
                removeAllKeysToolStripMenuItem.Visible = true;
                executeActionsToolStripMenuItem.Visible = false;

                toolStripMenuItemImportExportSplitter.Visible = true;
                importToolStripMenuItem.Visible = true;
                exportToolStripMenuItem.Visible = true;
            }
            else
            {
                deleteToolStripMenuItem.Visible = true;
                removeAllKeysToolStripMenuItem.Visible = false;
                executeActionsToolStripMenuItem.Visible = true;

                toolStripMenuItemImportExportSplitter.Visible = false;
                importToolStripMenuItem.Visible = false;
                exportToolStripMenuItem.Visible = false;
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

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportSelectedTreeViewKeyboard();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSelectedTreeViewKeyboard();
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

        private void ExportSelectedTreeViewKeyboard()
        {
            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            if (keyboard != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
                sfd.DefaultExt = "xml";
                DialogResult result = sfd.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    XMLFileStore<SettingsKeyboardExport> fileStore = new XMLFileStore<SettingsKeyboardExport>(sfd.FileName);
                    SettingsKeyboardExport export = new SettingsKeyboardExport();
                    export.Keyboard = keyboard;
                    foreach (SettingsKeyboardKey key in keyboard.Keys)
                    {
                        foreach (SettingsKeyboardKeyFocusedApplication focussedApp in key.FocusedApplications)
                        {
                            if (focussedApp.ApplicationName == "Default")
                                continue;
                            SettingsApplication app = export.Applications.FindByName(focussedApp.ApplicationName);
                            if (app == null)
                            {
                                export.Applications.Add(focussedApp.Application);
                            }
                        }
                    }

                    fileStore.SetData(export);
                    fileStore.Save();
                }
            }
        }

        private void ImportSelectedTreeViewKeyboard()
        {
            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            if (keyboard != null)
            {
                DialogResult result;
                result = MessageBox.Show(this, "Warning: Importing a keyboard from a file will replace any existing key definitions", "Keyboard Redirector", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                    return;

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
                ofd.Multiselect = false;
                result = ofd.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    XMLFileStore<SettingsKeyboardExport> fileStore = new XMLFileStore<SettingsKeyboardExport>(ofd.FileName);

                    foreach (SettingsApplication application in fileStore.Data.Applications)
                    {
                        SettingsApplication app = Settings.Current.Applications.FindByName(application.Name);
                        if (app == null)
                        {
                            Settings.Current.Applications.Add(application);
                        }
                    }

                    if (keyboard.KeyboardId != 0)
                        keyboard.Name = fileStore.Data.Keyboard.Name;
                    keyboard.CaptureAllKeys = fileStore.Data.Keyboard.CaptureAllKeys;
                    keyboard.Keys.Clear();
                    keyboard.Keys.AddRange(fileStore.Data.Keyboard.Keys);
                    Settings.Save();
                    RefreshTreeView();
                    RefreshKeyboardDetails();
                }

            }
        }

        private void RefreshKeyboardDetails()
        {
            SettingsKeyboard keyboard = GetSelectedKeyboardFromTreeView();
            if (keyboard == null)
                return;

            StringBuilder details = new StringBuilder();

            if (keyboard == Settings.Current.LowLevelKeyboard)
            {
                panelKeyboardProperties.Enabled = false;
                textBoxKeyboardDetails.Text = "Low level keyboard hook";
            }
            else
            {
                List<SettingsKeyboardDevice> keyboardDevices = Settings.Current.KeyboardDevices.FindByKeyboardId(keyboard.KeyboardId);
                foreach (SettingsKeyboardDevice keyboardDevice in keyboardDevices)
                {
                    DeviceInformation deviceInformation = FindKeyboardDeviceInformation(keyboardDevice.DeviceName);
                    if (deviceInformation == null)
                    {
                        details.AppendLine("Device not present: " + keyboardDevice.DeviceName);
                    }
                    else
                    {
                        details.AppendLine("Device: " + keyboardDevice.DeviceName);
                    }
                }
                if (keyboardDevices.Count == 0)
                {
                    details.AppendLine("No devices attached to this keyboard.");
                }
                details.Remove(details.Length - 2, 2);

                panelKeyboardProperties.Enabled = true;
                textBoxKeyboardDetails.Text = details.ToString();
            }
            textBoxKeyboardName.Text = keyboard.Name;

            checkBoxCaptureAllKeys.Checked = keyboard.CaptureAllKeys;
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
            numericUpDownAntiRepeat.Value = key.AntiRepeatTime;

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

        private void numericUpDownAntiRepeat_ValueChanged(object sender, EventArgs e)
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key != null)
            {
                key.AntiRepeatTime = (int)numericUpDownAntiRepeat.Value;
                Settings.Save();
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
                {
                    item.ImageIndex = 0;
                }
                else
                {
                    if (focussedApp.Application.ExecutableImage.Image != null)
                        _imageList.AddImage(focussedApp.Application.Executable, focussedApp.Application.ExecutableImage.Image);
                    item.ImageIndex = _imageList.GetExecutableIndex(focussedApp.Application.Executable);
                }

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
            {
                buttonEditApplication.Enabled = true;
                buttonRemoveApplication.Enabled = true;
            }
            else
            {
                buttonEditApplication.Enabled = false;
                buttonRemoveApplication.Enabled = false;
            }
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

        private void buttonEditApplication_Click(object sender, EventArgs e)
        {
            SettingsKeyboardKey key = GetSelectedKeyFromTreeView();
            if (key == null)
                throw new NullReferenceException("Selected key value missing");

            SettingsKeyboardKeyFocusedApplication app = listViewApplicationsInFocus.SelectedItem as SettingsKeyboardKeyFocusedApplication;
            if (app == null)
                throw new NullReferenceException("Focussed application value missing");

            EditApplicationsDialog dialog = new EditApplicationsDialog();
            dialog.SelectedApplication = app.Application;
            DialogResult result = dialog.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            app.Application = dialog.SelectedApplication;

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
            richTextBoxEvents.SelectionStart = 0;
            richTextBoxEvents.Clear();
            //richTextBoxKeyEventsLowLevel.Clear();
            //richTextBoxKeyEventsHook.Clear();
            //richTextBoxKeyEventsWMInput.Clear();
        }


        private void RefreshDevicesListView()
        {
            listViewDevices.BeginUpdate();
            listViewDevices.Items.Clear();
            foreach (SettingsKeyboardDevice keyboardDevice in Settings.Current.KeyboardDevices)
            {
                ListViewItem item = new ListViewItem(new string[] { keyboardDevice.Name, keyboardDevice.DeviceName });
                item.Tag = keyboardDevice;

                int imageIndex = 0;
                DeviceInformation deviceInformation = FindKeyboardDeviceInformation(keyboardDevice.DeviceName);
                if (deviceInformation == null)
                    imageIndex = 1;

                item.ImageIndex = imageIndex;
                item.StateImageIndex = imageIndex;

                listViewDevices.Items.Add(item);
            }
            listViewDevices.EndUpdate();

            BindListviewDevicesKeyboard();
        }

        private void listViewDevices_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            listViewDevicesKeyboard.Items.Clear();

            if (e.IsSelected == false)
                return;

            BindListviewDevicesKeyboard();
        }

        private void BindListviewDevicesKeyboard()
        {
            listViewDevicesKeyboard.BeginUpdate();
            listViewDevicesKeyboard.Items.Clear();


            if (listViewDevices.SelectedItems.Count > 0)
            {
                SettingsKeyboardDevice keyboardDevice = listViewDevices.SelectedItems[0].Tag as SettingsKeyboardDevice;

                foreach (SettingsKeyboard keyboard in Settings.Current.Keyboards)
                {
                    ListViewItem item = new ListViewItem(new string[] { keyboard.Name });
                    item.Tag = keyboard;

                    listViewDevicesKeyboard.Items.Add(item);

                    if (keyboard.KeyboardId == keyboardDevice.KeyboardId)
                        item.Checked = true;
                }
            }
            listViewDevicesKeyboard.EndUpdate();
        }

        private void listViewDevicesKeyboard_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (listViewDevices.SelectedItems.Count == 0)
                return;

            SettingsKeyboardDevice keyboardDevice = listViewDevices.SelectedItems[0].Tag as SettingsKeyboardDevice;
            if (keyboardDevice == null)
                return;

            SettingsKeyboard keyboard = listViewDevicesKeyboard.Items[e.Index].Tag as SettingsKeyboard;
            if (keyboard == null)
                return;

            if ((e.CurrentValue != CheckState.Checked) && (e.NewValue == CheckState.Checked))
            {
                if (keyboardDevice.KeyboardId != keyboard.KeyboardId)
                {
                    keyboardDevice.KeyboardId = keyboard.KeyboardId;
                    Settings.Save();
                    BindListviewDevicesKeyboard();
                }
            }
            else if ((e.CurrentValue == CheckState.Checked) && (e.NewValue != CheckState.Checked))
            {
                if (keyboardDevice.KeyboardId != -1)
                {
                    keyboardDevice.KeyboardId = -1;
                    Settings.Save();
                    BindListviewDevicesKeyboard();
                }
            }
        }

        private void buttonEditApplications_Click(object sender, EventArgs e)
        {
            EditApplicationsDialog dialog = new EditApplicationsDialog();
            DialogResult result = dialog.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
        }

        private void openSettingsFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.SettingsPath);
        }

        private void CheckBoxLogging_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings.Current.LogOn != checkBoxLogging.Checked)
            {
                Settings.Current.LogOn = checkBoxLogging.Checked;
                Settings.Save();
                LoggingOnOff();
            }
        }

        private void LoggingOnOff()
        {
            if (Settings.Current.LogOn) Log.MainLog.LogOn();
            else Log.MainLog.LogOff();
        }
    }
}

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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MS;

namespace KeyboardRedirector
{
    class ActionPerformer
    {
        class KeyInformation
        {
            public SettingsKeyboardKey Key;
            public bool KeyDown;
        }

        private List<KeyInformation> _keyQueue = new List<KeyInformation>();
        private Thread _processingThread;
        private AutoResetEvent _stopProcessing;

        public delegate void StatusMessageHandler(string text);
        public event StatusMessageHandler StatusMessage;

        public object KeyboardSendInputLock = new object();

        private void WriteStatusMessage(string text)
        {
            if (StatusMessage != null)
                StatusMessage(text);
        }

        public void EnqueueKey(SettingsKeyboardKey key, bool keyDown)
        {
            KeyInformation info = new KeyInformation();
            info.Key = key;
            info.KeyDown = keyDown;
            lock (_keyQueue)
            {
                _keyQueue.Clear();
                _keyQueue.Add(info);
                if (info.KeyDown)
                {
                    WriteStatusMessage("   Queueing keystroke: " + key.ToString());
                }
            }
            //ProcessKeyInfo(info);
        }

        public void StartProcessingThread()
        {
            _stopProcessing = new AutoResetEvent(false);
            _processingThread = new Thread(KeyProcessingThreadProc);
            _processingThread.IsBackground = true;
            _processingThread.Name = "Key Processsing";
            _processingThread.Start();
        }

        public void StopProcessingThread()
        {
            if (_processingThread == null)
                return;
            if (_processingThread.IsAlive == false)
                return;
            _stopProcessing.Set();
            _processingThread.Join();
            _processingThread = null;
        }

        public void KeyProcessingThreadProc()
        {
            while (_stopProcessing.WaitOne(10, true) == false)
            {
                try
                {
                    KeyInformation info = null;
                    lock (_keyQueue)
                    {
                        if (_keyQueue.Count > 0)
                        {
                            info = _keyQueue[0];
                            _keyQueue.RemoveAt(0);
                        }
                    }
                    ProcessKeyInfo(info);
                }
                catch (Exception e)
                {
                    Log.LogException(e);
                }
            }
        }

        private void ProcessKeyInfo(KeyInformation info)
        {
            if (info == null)
                return;

            if (info.KeyDown)
            {
                string focussedWindowTitle;
                string focussedExecutable;
                GetFocussedExecutable(out focussedWindowTitle, out focussedExecutable);

                SettingsKeyboardKeyFocusedApplication application;
                application = info.Key.FocusedApplications.FindByExecutable(focussedWindowTitle, focussedExecutable);
                if (application == null)
                {
                    if (info.Key.FocusedApplications.Count == 0)
                        return;
                    application = info.Key.FocusedApplications[0];
                }

                foreach (SettingsKeyboardKeyAction action in application.Actions)
                {
                    try
                    {
                        ProcessAction(action);
                    }
                    catch (Exception e)
                    {
                        WriteStatusMessage("Unexpected error: " + action.ToString() + " : " + e.Message);
                    }
                }
            }
        }

        private void ProcessAction(SettingsKeyboardKeyAction action)
        {
            ProcessAction(action, action.ActionType);
        }
        private void ProcessAction(SettingsKeyboardKeyAction action, SettingsKeyboardKeyActionType actionType)
        {
            switch (actionType)
            {
                case SettingsKeyboardKeyActionType.LaunchApplication:
                    ExecuteLaunchApplication(action);
                    break;

                case SettingsKeyboardKeyActionType.Keyboard:
                    ExecuteKeyboard(action);
                    break;

                case SettingsKeyboardKeyActionType.WindowMessage:
                    ExecuteWindowMessage(action);
                    break;
            }
        }

        private Dictionary<uint, string> _cachedExecutableNames = new Dictionary<uint, string>();
        private void GetFocussedExecutable(out string windowTitle, out string executable)
        {
            windowTitle = null;
            executable = null;

            IntPtr hwnd = Win32.GetForegroundWindow();
            if (hwnd == IntPtr.Zero)
                return;

            hwnd = Win32.GetAncestor(hwnd, Win32.GA.ROOT);
            if (hwnd == IntPtr.Zero)
                return;

            StringBuilder windowTitleString = new StringBuilder(Win32.GETWINDOWTEXT_MAXLENGTH);
            Win32.GetWindowText(hwnd, windowTitleString, windowTitleString.Capacity);
            windowTitle = windowTitleString.ToString();

            uint processId = 0;
            Win32.GetWindowThreadProcessId(hwnd, out processId);

            if (_cachedExecutableNames.ContainsKey(processId))
            {
                executable = _cachedExecutableNames[processId];
            }
            else
            {
                executable = Win32.GetProcessExecutableName((int)processId);
                _cachedExecutableNames.Add(processId, executable);
            }
        }


        private void ExecuteLaunchApplication(SettingsKeyboardKeyAction action)
        {
            SettingsKeyboardKeyTypedActionLaunchApplication launchApplication = action.LaunchApplication;

            WriteStatusMessage("Launching application: " + launchApplication.Command);

            string exe = launchApplication.Command.Trim();
            string args = "";

            if (exe[0] == '"')
            {
                int endOfExeIndex = exe.IndexOf("\" ", 1);
                if (endOfExeIndex != -1)
                {
                    endOfExeIndex++;
                    args = exe.Substring(endOfExeIndex + 1).TrimStart();
                    exe = exe.Substring(0, endOfExeIndex);
                }
            }
            else
            {
                int endOfExeIndex = exe.IndexOf(" ");
                if (endOfExeIndex != -1)
                {
                    args = exe.Substring(endOfExeIndex + 1).TrimStart();
                    exe = exe.Substring(0, endOfExeIndex);
                }
            }

            Process process = System.Diagnostics.Process.Start(exe, args);

            if (launchApplication.WaitForInputIdle)
                process.WaitForInputIdle(10000);
            else if (launchApplication.WaitForExit)
                process.WaitForExit(10000);
        }

        private void ExecuteKeyboard(SettingsKeyboardKeyAction action)
        {
            SettingsKeyboardKeyTypedActionKeyboard keyboard = action.Keyboard;

            int keysDownCount = KeyboardHookExternal.Current.KeysDownCount();
            if (keysDownCount > 0)
            {
                Log.MainLog.WriteDebug("   Waiting for all keys to be up before sending new keystroke : keys down = " + keysDownCount.ToString());
                if (KeyboardHookExternal.Current.AllKeysUpEvent.WaitOne(1000))
                {
                    Log.MainLog.WriteDebug("   All keys are up");
                }
                else
                {
                    WriteStatusMessage("   Timeout waiting for all keys to be up");
                }
            }

            WriteStatusMessage("Sending keystroke: " + keyboard.GetDetails());

            for (int repeat = 0; repeat < keyboard.RepeatCount; repeat++)
            {
                //lock (KeyboardSendInputLock)
                {
                    List<Win32.INPUT> inputList = new List<Win32.INPUT>();

                    if (repeat == 0)
                    {
                        List<KeysWithExtended> keysDown = new List<KeysWithExtended>();
                        keysDown.AddRange(KeyboardHookExternal.Current.KeyStateLowLevel.Modifiers);
                        if (KeyboardHookExternal.Current.KeyStateLowLevel.KeyDown)
                            keysDown.Add(KeyboardHookExternal.Current.KeyStateLowLevel.KeyWithExtended);
                        bool controlDown = false;
                        bool shiftDown = false;
                        bool altDown = false;
                        bool lWinDown = false;
                        bool rWinDown = false;
                        foreach (KeysWithExtended key in keysDown)
                        {
                            controlDown |= key.IsControlKey;
                            shiftDown |= key.IsShiftKey;
                            altDown |= key.IsAltKey;
                            lWinDown |= key.IsLWinKey;
                            rWinDown |= key.IsRWinKey;
                        }

                        if (controlDown)
                        {
                            inputList.Add(CreateInputStruct((ushort)Keys.ControlKey, false));
                        }

                        if (shiftDown)
                        {
                            inputList.Add(CreateInputStruct((ushort)Keys.ShiftKey, false));
                        }

                        if (altDown)
                        {
                            inputList.Add(CreateInputStruct((ushort)Keys.Menu, false));
                        }

                        if (lWinDown)
                        {
                            inputList.Add(CreateInputStruct((ushort)Keys.LWin, false));
                        }

                        if (rWinDown)
                        {
                            inputList.Add(CreateInputStruct((ushort)Keys.RWin, false));
                        }
                    }

                    if (keyboard.Control)
                        inputList.Add(CreateInputStruct((ushort)Keys.ControlKey, true));

                    if (keyboard.Shift)
                        inputList.Add(CreateInputStruct((ushort)Keys.ShiftKey, true));

                    if (keyboard.Alt)
                        inputList.Add(CreateInputStruct((ushort)Keys.Menu, true));

                    if (keyboard.LWin)
                        inputList.Add(CreateInputStruct((ushort)Keys.LWin, true));

                    if (keyboard.RWin)
                        inputList.Add(CreateInputStruct((ushort)Keys.RWin, true));

                    inputList.Add(CreateInputStruct((ushort)keyboard.VirtualKeyCode, true));

                    inputList.Add(CreateInputStruct((ushort)keyboard.VirtualKeyCode, false));

                    if (keyboard.RWin)
                        inputList.Add(CreateInputStruct((ushort)Keys.RWin, false));

                    if (keyboard.LWin)
                        inputList.Add(CreateInputStruct((ushort)Keys.LWin, false));

                    if (keyboard.Alt)
                        inputList.Add(CreateInputStruct((ushort)Keys.Menu, false));

                    if (keyboard.Shift)
                        inputList.Add(CreateInputStruct((ushort)Keys.ShiftKey, false));

                    if (keyboard.Control)
                        inputList.Add(CreateInputStruct((ushort)Keys.ControlKey, false));

                    KeyboardRedirectorForm.DisableGlobalKeyboardHook = true;

                    Win32.INPUT[] input = inputList.ToArray();
                    uint result = Win32.SendInput(input.Length, input, Marshal.SizeOf(input[0]));

                    KeyboardRedirectorForm.DisableGlobalKeyboardHook = false;
                }
            }
        }

        private void ExecuteWindowMessage(SettingsKeyboardKeyAction action)
        {
            SettingsKeyboardKeyTypedActionWindowMessage windowMessage = action.WindowMessage;

            WriteStatusMessage("Sending window message: " + windowMessage.GetDetails());


            var processIds = new HashSet<uint>();

            if (!string.IsNullOrEmpty(windowMessage.ProcessName))
            {
                var processes = Process.GetProcessesByName(windowMessage.ProcessName);
                foreach (var p in processes)
                {
                    processIds.Add((uint)p.Id);
                }
            }

            var windowHandles = new List<IntPtr>();

            Win32.EnumWindowsProc proc = new Win32.EnumWindowsProc((IntPtr hwnd, int lParam) =>
            {
                if (processIds.Count > 0)
                {
                    uint pid;
                    Win32.GetWindowThreadProcessId(hwnd, out pid);
                    if (!processIds.Contains(pid))
                        return true;
                }

                if (!string.IsNullOrEmpty(windowMessage.WindowClass))
                {
                    StringBuilder windowClass = new StringBuilder(Win32.GETWINDOWTEXT_MAXLENGTH);
                    Win32.GetClassName(hwnd, windowClass, windowClass.Capacity);
                    if (!windowMessage.WindowClass.Equals(windowClass.ToString(), StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                if (!string.IsNullOrEmpty(windowMessage.WindowName))
                {
                    StringBuilder windowTitle = new StringBuilder(Win32.GETWINDOWTEXT_MAXLENGTH);
                    Win32.GetWindowText(hwnd, windowTitle, windowTitle.Capacity);
                    if (!windowMessage.WindowName.Equals(windowTitle.ToString(), StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                windowHandles.Add(hwnd);
                return true;
            });
            Win32.EnumWindows(proc, 0);

            if (windowHandles.Count == 0)
            {
                if (windowMessage.NotFoundAction != SettingsKeyboardKeyActionType.WindowMessage)
                {
                    ProcessAction(action, windowMessage.NotFoundAction);
                }
                return;
            }

            foreach (var hwnd in windowHandles)
            {
                System.Diagnostics.Debug.WriteLine("SendMessage(" +
                    "0x" + ((uint)hwnd.ToInt32()).ToString("x") +
                    ", 0x" + windowMessage.Message.ToString("x") +
                    ", 0x" + windowMessage.WParam.ToString("x") +
                    ", 0x" + windowMessage.LParam.ToString("x") + ")");
                var result = Win32.SendMessage(hwnd, windowMessage.Message, new IntPtr(windowMessage.WParam), new IntPtr(windowMessage.LParam));
                WriteStatusMessage("SendMessage result: 0x" + ((uint)result.ToInt32()).ToString("x"));
            }
        }


        private Win32.INPUT CreateInputStruct(ushort virtualKeyCode, bool keyDown)
        {
            Win32.INPUT input = new Win32.INPUT();
            //input.header.dwType = Win32.INPUTTYPE.KEYBOARD;
            input.header.dwTypePtr = new IntPtr((int)Win32.INPUTTYPE.KEYBOARD);

            input.data.keyboard.VKey = virtualKeyCode;

            if ((virtualKeyCode >= 0xE0) && (virtualKeyCode < 0xE2))
                input.data.keyboard.dwFlags = Win32.KEYEVENTF.EXTENDEDKEY;
            if (keyDown == false)
                input.data.keyboard.dwFlags |= Win32.KEYEVENTF.KEYUP;

            return input;
        }

    }
}

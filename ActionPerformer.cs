using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

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
                _keyQueue.Add(info);
            }
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
            while (_stopProcessing.WaitOne(10) == false)
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

                if (info == null)
                    continue;

                if (info.KeyDown)
                {
                    string focussedExe = GetFocussedExecutable();

                    SettingsKeyboardKeyFocusedApplication application;
                    application = info.Key.FocusedApplications.FindByExecutable(focussedExe);
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
                            switch (action.ActionType)
                            {
                                case SettingsKeyboardKeyActionType.LaunchApplication:
                                    LaunchApplication(action.LaunchApplication);
                                    break;

                                case SettingsKeyboardKeyActionType.Keyboard:
                                    Keyboard(action.Keyboard);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            WriteStatusMessage("Unexpected error: " + action.ToString() + " : " + e.Message);
                        }
                    }

                }

            }
        }

        private string GetFocussedExecutable()
        {
            IntPtr hwnd = Win32.GetFocus();
            if (hwnd == IntPtr.Zero)
                return "";

            hwnd = Win32.GetAncestor(hwnd, Win32.GA.ROOT);
            if (hwnd == IntPtr.Zero)
                return "";

            uint processId = 0;
            Win32.GetWindowThreadProcessId(hwnd, out processId);

            return Win32.GetProcessExecutableName((int)processId);
        }


        private void LaunchApplication(SettingsKeyboardKeyTypedActionLaunchApplication launchApplication)
        {
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

        private void Keyboard(SettingsKeyboardKeyTypedActionKeyboard keyboard)
        {
            WriteStatusMessage("Sending keystroke: " + keyboard.GetDetails());

            for (int repeat = 0; repeat < keyboard.RepeatCount; repeat++)
            {
                List<Win32.INPUT> inputList = new List<Win32.INPUT>();

                if (keyboard.Control)
                    inputList.Add(CreateInputStruct((ushort)Keys.ControlKey, true));

                if (keyboard.Shift)
                    inputList.Add(CreateInputStruct((ushort)Keys.ShiftKey, true));

                if (keyboard.Alt)
                    inputList.Add(CreateInputStruct((ushort)Keys.Menu, true));

                inputList.Add(CreateInputStruct((ushort)keyboard.VirtualKeyCode, true));

                inputList.Add(CreateInputStruct((ushort)keyboard.VirtualKeyCode, false));

                if (keyboard.Alt)
                    inputList.Add(CreateInputStruct((ushort)Keys.Menu, false));

                if (keyboard.Shift)
                    inputList.Add(CreateInputStruct((ushort)Keys.ShiftKey, false));

                if (keyboard.Control)
                    inputList.Add(CreateInputStruct((ushort)Keys.ControlKey, false));

                Win32.INPUT[] input = inputList.ToArray();
                Win32.SendInput(input.Length, input, Marshal.SizeOf(input[0]));
            }
        }

        private Win32.INPUT CreateInputStruct(ushort virtualKeyCode, bool keyDown)
        {
            Win32.INPUT input = new Win32.INPUT();
            input.header.dwType = Win32.INPUTTYPE.KEYBOARD;

            input.data.keyboard.VKey = virtualKeyCode;

            if ((virtualKeyCode >= 0xE0) && (virtualKeyCode < 0xE2))
                input.data.keyboard.dwFlags = Win32.KEYEVENTF.EXTENDEDKEY;
            if (keyDown == false)
                input.data.keyboard.dwFlags |= Win32.KEYEVENTF.KEYUP;

            return input;
        }

    }
}

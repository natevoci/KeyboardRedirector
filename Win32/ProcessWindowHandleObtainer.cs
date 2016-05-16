using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace MS
{
    public class ProcessWindow
    {
        public enum Action
        {
            None,
            Activate,
            Hide
        }

        public static bool IsThereAnInstanceOfThisProgramAlreadyRunning(Action actionOnThePreviousInstance = Action.None, string windowName = null)
        {
            var thisProcess = Process.GetCurrentProcess();
            string processName = thisProcess.ProcessName;
            List<Process> processList = new List<Process>();
            processList.AddRange(Process.GetProcessesByName(processName));

            if (processName.EndsWith(".vshost"))
            {
                processName = processName.Substring(0, processName.Length - 7);
                processList.AddRange(Process.GetProcessesByName(processName));
            }

            if (processList.Count == 1)
                return false; // There's just the current process.

            if (actionOnThePreviousInstance != Action.None)
            {
                foreach (var process in processList)
                {
                    if (process.Id != thisProcess.Id)
                    {
                        // Activate the previous instance.
                        IntPtr windowHandle = IntPtr.Zero;
                        List<IntPtr> windowHandles = ProcessWindowHandleObtainer.GetWindowHandle(process.Id);
                        foreach (IntPtr handle in windowHandles)
                        {
                            if (!string.IsNullOrEmpty(windowName))
                            {
                                StringBuilder windowText = new StringBuilder(260);
                                Win32.GetWindowText(handle, windowText, 260);
                                if (windowText.ToString() == windowName)
                                    windowHandle = handle;
                            }
                            else
                                windowHandle = handle;
                            break;
                        }

                        if (windowHandle != IntPtr.Zero)
                        {
                            switch (actionOnThePreviousInstance)
                            {
                                case Action.Activate:
                                    Win32.ShowWindow(windowHandle, Win32.SW.RESTORE);
                                    Win32.SetForegroundWindow(windowHandle);
                                break;
                                case Action.Hide:
                                    Win32.ShowWindow(windowHandle, Win32.SW.HIDE);
                                break;
                            }
                        }
                    }
                }
            }

            return true;
        }

    }

    class ProcessWindowHandleObtainer
    {
        public static List<IntPtr> GetWindowHandle(int processId)
        {
            ProcessWindowHandleObtainer obtainer = new ProcessWindowHandleObtainer();
            obtainer._processId = (uint)processId;
            Win32.EnumWindowsProc proc = new Win32.EnumWindowsProc(obtainer.EnumWindowsCallback);
            Win32.EnumWindows(proc, 0);
            return obtainer._windowHandles;
        }

        uint _processId = 0;
        List<IntPtr> _windowHandles = new List<IntPtr>();

        private bool EnumWindowsCallback(IntPtr hwnd, int lParam)
        {
            uint pid;
            Win32.GetWindowThreadProcessId(hwnd, out pid);
            if (pid == _processId)
            {
                _windowHandles.Add(hwnd);
            }
            return true;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
using MS;

namespace ApplicationLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (IsThereAnInstanceOfThisProgramAlreadyRunning(true))
                {
                    Application.Exit();
                    return;
                }

                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ApplicationLauncherForm());
            }
            catch (Exception e)
            {
                LogException(e);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogException(e.Exception);
        }

        static void LogException(Exception e)
        {
            Log.LogException(e);

            string message = "";
            message += "An exception has occurred in ApplicationLauncher." + Environment.NewLine;
            message += Environment.NewLine;
            message += e.ToString();
            MessageBox.Show(message, "ApplicationLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            Log.LogException(ex);
        }

        static bool IsThereAnInstanceOfThisProgramAlreadyRunning(bool activateThePreviousInstance)
        {
            Process thisProcess = Process.GetCurrentProcess();
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

            if (activateThePreviousInstance)
            {
                foreach (Process process in processList)
                {
                    if (process.Id != thisProcess.Id)
                    {
                        // Activate the previous instance.
                        IntPtr windowHandle = IntPtr.Zero;
                        List<IntPtr> windowHandles = ProcessWindowHandleObtainer.GetWindowHandle(process.Id);
                        foreach (IntPtr handle in windowHandles)
                        {
                            //StringBuilder windowText = new StringBuilder(260);
                            //Win32.GetWindowText(handle, windowText, 260);

                            windowHandle = handle;
                            break;
                        }

                        if (windowHandle != IntPtr.Zero)
                        {
                            Win32.ShowWindow(windowHandle, Win32.SW.RESTORE);
                            Win32.SetForegroundWindow(windowHandle);
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

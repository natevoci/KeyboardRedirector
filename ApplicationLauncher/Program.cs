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
                if (MS.ProcessWindow.IsThereAnInstanceOfThisProgramAlreadyRunning(MS.ProcessWindow.Action.Activate))
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
    }
}

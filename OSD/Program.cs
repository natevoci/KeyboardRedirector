using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace OSD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MS.ProcessWindow.IsThereAnInstanceOfThisProgramAlreadyRunning(MS.ProcessWindow.Action.Hide);

            var args = Environment.GetCommandLineArgs();
            List<OSD> windows = new List<OSD>();

            for (int i = 1; i < args.Length; i++)
            {
                var text = args[i];
                windows.Add(OSD.ShowOSD(text));
            }

            while (true)
            {
                if ((from w in windows
                     where w.Handle != IntPtr.Zero
                     select w).FirstOrDefault() == null)
                    return;

                Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}

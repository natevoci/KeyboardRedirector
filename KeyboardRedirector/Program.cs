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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text;
using MS;

namespace KeyboardRedirector
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
                if (MS.ProcessWindow.IsThereAnInstanceOfThisProgramAlreadyRunning(MS.ProcessWindow.Action.Activate, "Keyboard Redirector"))
                {
                    Application.Exit();
                    return;
                }

                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new KeyboardRedirectorForm());
            }
            catch (Exception e)
            {
                LogException(e);

                string message = "";
                message += "An exception has occurred in Keyboard Redirector." + Environment.NewLine;
                message += Environment.NewLine;
                message += e.ToString();
                MessageBox.Show(message, "Keyboard Redirector", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogException(e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogException(e.ExceptionObject as Exception);
        }

        static void LogException(Exception e)
        {
            Log.LogException(e);

            string message = "";
            message += "An exception has occurred in Keyboard Redirector." + Environment.NewLine;
            message += Environment.NewLine;
            message += e.ToString();
            MessageBox.Show(message, "Keyboard Redirector", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace kXToggle
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

            var result = Toggle();
            windows.Add(OSD.ShowOSD(result));

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

        static string Toggle()
        {
            var kXToggleAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\kXToggle\";

            if (!Directory.Exists(kXToggleAppDataFolder))
                Directory.CreateDirectory(kXToggleAppDataFolder);

            string lastSetValue = "";
            var lastSetFilename = kXToggleAppDataFolder + "lastset.txt";
            if (File.Exists(lastSetFilename))
                lastSetValue = File.ReadAllText(lastSetFilename);

            var settingsDir = new DirectoryInfo(kXToggle.Properties.Settings.Default.kXSettings);
            if (!settingsDir.Exists)
            {
                MessageBox.Show("kXSettings path not found", "kXToggle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            FileInfo firstItem = null;
            bool found = false;
            FileInfo nextItem = null;

            var settingsFiles = from s in settingsDir.GetFiles("*.kx")
                                orderby Path.GetFileNameWithoutExtension(s.Name)
                                select s;

            foreach (var fileInfo in settingsFiles)
            {
                if (firstItem == null)
                {
                    firstItem = fileInfo;
                    if (string.IsNullOrEmpty(lastSetValue))
                        break;
                }

                if (fileInfo.Name == lastSetValue)
                {
                    found = true;
                    continue;
                }

                if (found)
                {
                    nextItem = fileInfo;
                    break;
                }
            }

            if (nextItem == null)
                nextItem = firstItem;

            if (nextItem == null)
            {
                MessageBox.Show("No .kx files found in kXSettings path", "kXToggle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            var kXMixer = kXToggle.Properties.Settings.Default.kXPath;
            if (!kXMixer.EndsWith(@"\"))
                kXMixer += @"\";
            kXMixer += @"kxmixer.exe";

            try
            {
                var process = System.Diagnostics.Process.Start(kXMixer, "--shell --load-settings " + nextItem.FullName);
                process.WaitForExit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "kXToggle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            File.WriteAllText(lastSetFilename, nextItem.Name);

            return Path.GetFileNameWithoutExtension(nextItem.Name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Text.RegularExpressions;

namespace ApplicationLauncher
{
    public partial class ApplicationLauncherForm : Form
    {
        private DesktopWindows _windows;
        private IconExtractor.ExecutableImageList _imageList;
        private Rectangle _normalBounds;

        private FormWindowState _previousWindowState = FormWindowState.Normal;

        public ApplicationLauncherForm()
        {
            string exeFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(exeFilename);

            InitializeComponent();

            buttonListControlRunning.ItemActivate += new ButtonListControl.ItemActivateEventHandler(buttonListControlRunning_ItemActivate);
            buttonListControlShortcuts.ItemActivate += new ButtonListControl.ItemActivateEventHandler(buttonListControlShortcuts_ItemActivate);

            if ((Settings.Current.WindowSize.Width > 0) && (Settings.Current.WindowSize.Height > 0))
            {
                this.Size = Settings.Current.WindowSize;
            }

            if ((Settings.Current.WindowLocation.X != -1) && (Settings.Current.WindowLocation.Y != -1))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = Settings.Current.WindowLocation;
            }

            _previousWindowState = FormWindowState.Normal;
            if (Regex.IsMatch(Environment.CommandLine, @"(?<!\S)(?:/|--|-)minimi[sz]e\b", RegexOptions.IgnoreCase))
            {
                SendToTray();
                this.ShowInTaskbar = false;
            }
            if (Regex.IsMatch(Environment.CommandLine, @"(?<!\S)(?:/|--|-)help\b", RegexOptions.IgnoreCase))
            {
                var msg = @"
/minimize - cause the application to load in the notification tray
";
                MessageBox.Show(this, msg.TrimStart(), "Application Launcher command line options", MessageBoxButtons.OK);
            }

            if (Settings.Current.Minimize)
            {
                buttonExit.ButtonText = "Close";
                this.notifyIcon1.Visible = true;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 10000)
            {
                RestoreFromTray();
            }

            base.WndProc(ref m);
        }

        private void ApplicationLauncherForm_Load(object sender, EventArgs e)
        {
            if (_windows == null)
            {
                _windows = new DesktopWindows();
            }

            _imageList = new IconExtractor.ExecutableImageList(imageListRunningLarge, true);
            _imageList.NonExistantImage = -1;


            // Setup ObjectListViewRunning
            RebuildListRunning();

            buttonListControlRunning_SelectPreviousForegroundWindow();
            
            // Setup ObjectListViewShortcuts
            RebuildListShortcuts();

            uint processId = 0;
            uint idAttach = MS.Win32.GetWindowThreadProcessId(MS.Win32.GetForegroundWindow(), out processId);
            uint idAttachTo = MS.Win32.GetCurrentThreadId();
            MS.Win32.AttachThreadInput(idAttach, idAttachTo, true);
            MS.Win32.SetForegroundWindow(this.Handle);
            MS.Win32.SetFocus(this.Handle);
            MS.Win32.AttachThreadInput(idAttach, idAttachTo, false);
        }

        private void RebuildListRunning()
        {
            buttonListControlRunning.Clear();
            foreach (DesktopWindows.Window window in _windows.Windows)
            {
                if (!string.IsNullOrEmpty(window.Title) || (window.IconLarge != null))
                {
                    Image img = null;
                    if (window.IconLarge != null)
                        img = window.IconLarge.ToBitmap();
                    buttonListControlRunning.AddButton(window.Title, img, "", window);
                }
            }
        }

        private void RebuildListShortcuts()
        {
            buttonListControlShortcuts.Clear();
            int key = 1;
            foreach (Shortcut shortcut in Settings.Current.Shortcuts)
            {
                int index;
                if (shortcut.Icon.Length > 0)
                    index = _imageList.GetExecutableIndex(shortcut.Icon);
                else
                {
                    index = _imageList.GetExecutableIndex(shortcut.Executable);
                }

                Image image = null;
                if (index >= 0)
                    image = _imageList.ImageList.Images[index];

                if (key < 10)
                    buttonListControlShortcuts.AddButton(shortcut.Name, image, key.ToString(), shortcut);
                else
                    buttonListControlShortcuts.AddButton(shortcut.Name, image, "", shortcut);
                key++;
            }
        }

        void buttonListControlRunning_ItemActivate(object sender, ButtonListControl.ButtonListControlItem item)
        {
            DesktopWindows.Window window = item.Tag as DesktopWindows.Window;
            MS.Win32.SetForegroundWindow(window.Hwnd);

            MS.Win32.WINDOWPLACEMENT placement = new MS.Win32.WINDOWPLACEMENT();
            MS.Win32.GetWindowPlacement(window.Hwnd, ref placement);
            if ((placement.showCmd == MS.Win32.SW.MINIMIZE) ||
                (placement.showCmd == MS.Win32.SW.SHOWMINIMIZED))
            {
                MS.Win32.ShowWindow(window.Hwnd, MS.Win32.SW.RESTORE);
            }

            if (Settings.Current.Minimize)
                SendToTray();
            else
                Close();
        }
        void buttonListControlShortcuts_ItemActivate(object sender, ButtonListControl.ButtonListControlItem item)
        {
            Shortcut shortcut = item.Tag as Shortcut;
            ExecuteShortcut(shortcut);
        }

        private void ExecuteShortcut(int number)
        {
            if (number <= Settings.Current.Shortcuts.Count)
            {
                Shortcut shortcut = Settings.Current.Shortcuts[number - 1];
                ExecuteShortcut(shortcut);
            }
        }

        private void ExecuteShortcut(Shortcut shortcut)
        {
            if (shortcut.SwitchTasksIfAlreadyRunning)
            {
                foreach (DesktopWindows.Window window in _windows.Windows)
                {
                    string windowExe, windowArgs;
                    ParseCommandLine(window.CmdLine, out windowExe, out windowArgs);
                    if ((windowExe.Equals(shortcut.Executable, StringComparison.CurrentCultureIgnoreCase)) &&
                        (windowArgs.Equals(shortcut.Arguments, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        MS.Win32.SetForegroundWindow(window.Hwnd);

                        MS.Win32.WINDOWPLACEMENT placement = new MS.Win32.WINDOWPLACEMENT();
                        MS.Win32.GetWindowPlacement(window.Hwnd, ref placement);
                        if ((placement.showCmd == MS.Win32.SW.MINIMIZE) || 
                            (placement.showCmd == MS.Win32.SW.SHOWMINIMIZED))
                        {
                            MS.Win32.ShowWindow(window.Hwnd, MS.Win32.SW.RESTORE);
                        }

                        if (Settings.Current.Minimize)
                            SendToTray();
                        else
                            Close();
                        
                        return;
                    }
                }

            }

            string originalWorkingDirectory = null;
            if ((shortcut.WorkingFolder.Length > 0) && (System.IO.Directory.Exists(shortcut.WorkingFolder)))
            {
                originalWorkingDirectory = Environment.CurrentDirectory;
                Environment.CurrentDirectory = shortcut.WorkingFolder;
            }

            Process process = System.Diagnostics.Process.Start(shortcut.Executable, shortcut.Arguments);

            if (originalWorkingDirectory != null)
            {
                Environment.CurrentDirectory = originalWorkingDirectory;
            }

            if (Settings.Current.Minimize)
                SendToTray();
            else
                Close();
        }

        public static void ParseCommandLine(string commandLine, out string exe, out string args)
        {
            exe = (string.IsNullOrEmpty(commandLine) ? "" : commandLine.Trim());
            args = "";

            if (exe.Length > 0)
            {
                if (exe[0] == '"')
                {
                    int endOfExeIndex = exe.IndexOf("\"", 1);
                    if (endOfExeIndex != -1)
                    {
                        //endOfExeIndex++;
                        args = exe.Substring(endOfExeIndex + 1).TrimStart();
                        exe = exe.Substring(1, endOfExeIndex - 1);
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
            }
        }


        private void buttonEditShortcuts_Click(object sender, EventArgs e)
        {
            ShortcutsForm dialog = new ShortcutsForm();
            dialog.ShowDialog(this);

            RebuildListShortcuts();
        }

        private void ApplicationLauncherForm_KeyDown(object sender, KeyEventArgs e)
        {
            //Debug.WriteLine("key: " + e.KeyData.ToString());
            if (e.KeyData == Keys.Escape)
            {
                if (Settings.Current.Minimize)
                    SendToTray();
                else
                    this.Close();
                e.Handled = true;
            }
            if (e.KeyData == Keys.Right)
            {
                Control focused = MS.Win32.GetFocusControl();
                while (focused.Parent != null)
                {
                    if (buttonListControlRunning == focused)
                        buttonListControlShortcuts.Focus();
                    else if (buttonListControlShortcuts == focused)
                        buttonEditShortcuts.Focus();
                    else if (buttonEditShortcuts == focused)
                        buttonExit.Focus();
                    else if (buttonExit == focused)
                        buttonListControlRunning.Focus();
                    else
                    {
                        focused = focused.Parent;
                        continue;
                    }
                    e.Handled = true;
                    break;
                }
            }
            if (e.KeyData == Keys.Left)
            {
                Control focused = MS.Win32.GetFocusControl();
                while (focused.Parent != null)
                {
                    if (buttonExit == focused)
                        buttonEditShortcuts.Focus();
                    else if (buttonEditShortcuts == focused)
                        buttonListControlShortcuts.Focus();
                    else if (buttonListControlShortcuts == focused)
                        buttonListControlRunning.Focus();
                    else if (buttonListControlRunning == focused)
                        buttonExit.Focus();
                    else
                    {
                        focused = focused.Parent;
                        continue;
                    }
                    e.Handled = true;
                    break;
                }
            }
            if (e.KeyData == Keys.E)
            {
                buttonEditShortcuts.Focus();
                e.Handled = true;
            }
            if ((e.KeyData >= Keys.D1) && (e.KeyData <= Keys.D9))
            {
                ExecuteShortcut((int)e.KeyData - (int)Keys.D1 + 1);
                e.Handled = true;
            }
            if ((e.KeyData == Keys.NumPad1) || (e.KeyData == Keys.NumPad9))
            {
                ExecuteShortcut((int)e.KeyData - (int)Keys.NumPad1 + 1);
                e.Handled = true;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (Settings.Current.Minimize)
                SendToTray();
            else
                Close();
        }

        private void ApplicationLauncherForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MS.Win32.ReleaseCapture();
                MS.Win32.SendMessage(Handle, (uint)MS.Win32.WM.NCLBUTTONDOWN, new IntPtr(MS.Win32.HT_CAPTION), IntPtr.Zero);
            }
        }

        private void ApplicationLauncherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Current.WindowLocation = _normalBounds.Location;
            Settings.Current.WindowSize = _normalBounds.Size;
            Settings.Save();
        }

        private void ApplicationLauncherForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                _normalBounds = this.Bounds;
        }
        private void ApplicationLauncherForm_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                _normalBounds = this.Bounds;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Left)
                return false;
            if (keyData == Keys.Right)
                return false;
            return base.ProcessDialogKey(keyData);
        }

        private void ApplicationLauncherForm_Activated(object sender, EventArgs e)
        {
            buttonListControlRunning_Refresh();
        }

        private void buttonListControlRunning_Refresh()
        {
            if (_windows != null)
            {
                _windows.Refresh();

                RebuildListRunning();

                buttonListControlRunning_SelectPreviousForegroundWindow();
            }
        }

        private void buttonListControlRunning_SelectPreviousForegroundWindow()
        {
            if (_windows.Windows.Count > 1)
            {
                buttonListControlRunning.SelectObject(_windows.Windows[1]);
            }
            else if (_windows.Windows.Count > 0)
            {
                buttonListControlRunning.SelectObject(_windows.Windows[0]);
            }
        }

        private void SendToTray()
        {
            if (this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = _previousWindowState;

            buttonListControlRunning_Refresh();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestoreFromTray();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

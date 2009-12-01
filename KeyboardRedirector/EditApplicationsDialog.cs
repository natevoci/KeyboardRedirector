#region Copyright (C) 2009 Nate

/* 
 *	Copyright (C) 2009 Nate
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using MS;

namespace KeyboardRedirector
{
    public partial class EditApplicationsDialog : Form
    {
        IconExtractor.ExecutableImageList _imageList;
        SettingsApplication _selectedApplication = null;

        public SettingsApplication SelectedApplication
        {
            get { return _selectedApplication; }
            set { _selectedApplication = value; }
        }

        public EditApplicationsDialog()
        {
            string exeFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(exeFilename);

            InitializeComponent();

            listViewApplications.AddColumn("Application", 0, "Name");

            _imageList = new IconExtractor.ExecutableImageList(imageListApplicationIcons);
        }

        private void EditApplicationsDialog_Load(object sender, EventArgs e)
        {
            RefreshApplicationsList();
            listViewApplications.SelectedIndices.Clear();
            listViewApplications.SelectedIndices.Add(0);

            if (_selectedApplication != null)
                listViewApplications.SelectedItem = _selectedApplication;
        }

        private void RefreshApplicationsList()
        {
            int selectedIndex = -1;
            if (listViewApplications.SelectedIndices.Count > 0)
                selectedIndex = listViewApplications.SelectedIndices[0];

            listViewApplications.DataSource = null;
            listViewApplications.DataSource = Settings.Current.Applications;

            foreach (ListViewItem item in listViewApplications.Items)
            {
                SettingsApplication application = item.Tag as SettingsApplication;
                if (application.Name == "Default")
                {
                    item.ImageIndex = 0;
                }
                else
                {
                    if (application.ExecutableImage.Image != null)
                        _imageList.AddImage(application.Executable, application.ExecutableImage.Image);
                    item.ImageIndex = _imageList.GetExecutableIndex(application.Executable);
                }
            }

            if (selectedIndex >= listViewApplications.Items.Count)
                selectedIndex = listViewApplications.Items.Count - 1;
            if (selectedIndex >= 0)
            {
                listViewApplications.SelectedIndices.Clear();
                listViewApplications.SelectedIndices.Add(selectedIndex);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _selectedApplication = listViewApplications.SelectedItem as SettingsApplication;
            Settings.Save();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _selectedApplication = null;
            Settings.RevertToSaved();
        }

        private void listViewApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
            {
                buttonRemove.Enabled = false;
                textBoxApplicationName.Text = "";
                textBoxApplicationName.Enabled = false;
                textBoxWindowTitle.Text = "";
                textBoxWindowTitle.Enabled = false;
                checkBoxUseWindowTitle.Checked = false;
                checkBoxUseWindowTitle.Enabled = false;
                textBoxExecutable.Text = "";
                textBoxExecutable.Enabled = false;
                checkBoxUseExecutable.Checked = false;
                checkBoxUseExecutable.Enabled = false;
                buttonBrowse.Enabled = false;
                //labelFindFromWindow.Enabled = false;
            }
            else if (app.Name == "Default")
            {
                buttonRemove.Enabled = false;
                textBoxApplicationName.Text = "Default";
                textBoxApplicationName.Enabled = false;
                textBoxWindowTitle.Text = "";
                textBoxWindowTitle.Enabled = false;
                checkBoxUseWindowTitle.Checked = false;
                checkBoxUseWindowTitle.Enabled = false;
                textBoxExecutable.Text = "";
                textBoxExecutable.Enabled = false;
                checkBoxUseExecutable.Checked = false;
                checkBoxUseExecutable.Enabled = false;
                buttonBrowse.Enabled = false;
                //labelFindFromWindow.Enabled = false;
            }
            else
            {
                buttonRemove.Enabled = true;
                textBoxApplicationName.Text = app.Name;
                textBoxApplicationName.Enabled = true;
                textBoxWindowTitle.Text = app.WindowTitle;
                textBoxWindowTitle.Enabled = true;
                checkBoxUseWindowTitle.Checked = app.UseWindowTitle;
                checkBoxUseWindowTitle.Enabled = true;
                textBoxExecutable.Text = app.Executable;
                textBoxExecutable.Enabled = true;
                checkBoxUseExecutable.Checked = app.UseExecutable;
                checkBoxUseExecutable.Enabled = true;
                buttonBrowse.Enabled = true;
                //labelFindFromWindow.Enabled = true;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Settings.Current.Applications.Add(new SettingsApplication());
            RefreshApplicationsList();
            listViewApplications.SelectedIndices.Clear();
            listViewApplications.SelectedIndices.Add(listViewApplications.Items.Count - 1);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
                return;
            Settings.Current.Applications.Remove(app);
            RefreshApplicationsList();
        }


        private void textBoxApplicationName_TextChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
                return;

            app.Name = textBoxApplicationName.Text;
        }

        private void checkBoxUseWindowTitle_CheckedChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
                return;

            app.UseWindowTitle = checkBoxUseWindowTitle.Checked;
        }

        private void textBoxWindowTitle_TextChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
                return;

            app.WindowTitle = textBoxWindowTitle.Text;
        }

        private void checkBoxUseExecutable_CheckedChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
                return;

            app.UseExecutable = checkBoxUseExecutable.Checked;
        }

        private void textBoxExecutable_TextChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
                return;

            app.Executable = textBoxExecutable.Text;

            int imageIndex = _imageList.GetExecutableIndex(textBoxExecutable.Text);
            if (imageIndex <= 1)
            {
                pictureBoxExeIcon.Image = null;
            }
            else
            {
                pictureBoxExeIcon.Image = imageListApplicationIcons.Images[imageIndex];
                app.ExecutableImage.Image = imageListApplicationIcons.Images[imageIndex];
            }
        }
        private void textBoxExecutable_Leave(object sender, EventArgs e)
        {
            RefreshApplicationsList();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (textBoxExecutable.Text.Length > 0)
            {
                if (System.IO.Directory.Exists(textBoxExecutable.Text))
                {
                    ofd.InitialDirectory = textBoxExecutable.Text;
                }
                else if (System.IO.File.Exists(textBoxExecutable.Text))
                {
                    ofd.FileName = textBoxExecutable.Text;
                }
            }
            DialogResult result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                SetExecutable(ofd.FileName);
            }
        }

        private void SetExecutable(string executable)
        {
            int imageIndex = _imageList.GetExecutableIndex(executable);
            if (imageIndex > 1)
            {
                _imageList.AddImage(System.IO.Path.GetFileName(executable), imageListApplicationIcons.Images[imageIndex]);
            }

            textBoxExecutable.Text = System.IO.Path.GetFileName(executable);
            SettingsApplication newApplication = new SettingsApplication();
            if ((textBoxApplicationName.Text.Length == 0) || (textBoxApplicationName.Text == newApplication.Name))
                textBoxApplicationName.Text = System.IO.Path.GetFileNameWithoutExtension(executable);
            RefreshApplicationsList();
        }

        private bool _findFromWindowActive = false;
        private bool _findFromWindowValid = false;
        private string _findFromWindowOriginalName = "";
        private string _findFromWindowOriginalExecutable = "";
        private void labelFindFromWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _findFromWindowActive = true;

            SettingsApplication app = new SettingsApplication();
            app.Name = "Find Window";
            Settings.Current.Applications.Add(app);
            RefreshApplicationsList();
            listViewApplications.SelectedIndices.Clear();
            listViewApplications.SelectedIndices.Add(listViewApplications.Items.Count - 1);

            _findFromWindowOriginalName = textBoxApplicationName.Text;
            _findFromWindowOriginalExecutable = textBoxExecutable.Text;
        }
        private void labelFindFromWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _findFromWindowActive = false;

            if (_findFromWindowValid == false)
            {
                SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
                if (app != null)
                    Settings.Current.Applications.Remove(app);
            }

            RefreshApplicationsList();
        }
        private void buttonFindFromWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (_findFromWindowActive)
            {
                Control control = sender as Control;
                Point point = control.PointToScreen(e.Location);
                GrabApplicationUnderPoint(point);
            }
        }

        private IntPtr _lastHWNDFromGrabAppUnderPoint = IntPtr.Zero;
        private void GrabApplicationUnderPoint(Point point)
        {
            IntPtr hwnd = Win32.WindowFromPoint(point);
            if (hwnd == IntPtr.Zero)
                return;

            hwnd = Win32.GetAncestor(hwnd, Win32.GA.ROOT);
            if (hwnd == IntPtr.Zero)
                return;

            if (_lastHWNDFromGrabAppUnderPoint == hwnd)
                return;
            _lastHWNDFromGrabAppUnderPoint = hwnd;

            uint processId;
            Win32.GetWindowThreadProcessId(hwnd, out processId);
            Process process = Process.GetProcessById((int)processId);

            if (process.Id == Process.GetCurrentProcess().Id)
            {
                textBoxApplicationName.Text = _findFromWindowOriginalName;
                textBoxExecutable.Text = _findFromWindowOriginalExecutable;
                _findFromWindowValid = false;
            }
            else
            {
                try
                {
                    string name = process.ProcessName;
                    string executable = Win32.GetProcessExecutableName(process.Id);
                    
                    StringBuilder windowTitle = new StringBuilder(Win32.GETWINDOWTEXT_MAXLENGTH);
                    Win32.GetWindowText(hwnd, windowTitle, windowTitle.Capacity);
                    textBoxWindowTitle.Text = windowTitle.ToString();
                    checkBoxUseWindowTitle.Checked = false;

                    int imageIndex = _imageList.GetExecutableIndex(executable);
                    if (imageIndex > 1)
                    {
                        _imageList.AddImage(System.IO.Path.GetFileName(executable), imageListApplicationIcons.Images[imageIndex]);
                    }

                    textBoxApplicationName.Text = name;
                    textBoxExecutable.Text = System.IO.Path.GetFileName(executable);
                    checkBoxUseExecutable.Checked = true;

                    _findFromWindowValid = true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }


    }
}
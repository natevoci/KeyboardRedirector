using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;

namespace KeyboardRedirector
{
    public partial class EditApplicationsDialog : Form
    {
        ExecutableImageList _imageList;

        public EditApplicationsDialog()
        {
            InitializeComponent();

            listViewApplications.AddColumn("Application", 0, "Name");

            _imageList = new ExecutableImageList(imageListApplicationIcons);
        }

        private void EditApplicationsDialog_Load(object sender, EventArgs e)
        {
            RefreshApplicationsList();
            listViewApplications.SelectedIndices.Clear();
            listViewApplications.SelectedIndices.Add(0);
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
                    item.ImageIndex = 0;
                else
                    item.ImageIndex = _imageList.GetExecutableIndex(application.Executable);
            }

            if (selectedIndex >= 0)
            {
                listViewApplications.SelectedIndices.Clear();
                listViewApplications.SelectedIndices.Add(selectedIndex);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Settings.Save();
        }

        private void listViewApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
            {
                buttonRemove.Enabled = false;
                textBoxApplicationName.Text = "";
                textBoxApplicationName.Enabled = false;
                textBoxExecutable.Text = "";
                textBoxExecutable.Enabled = false;
                buttonBrowse.Enabled = false;
                labelFindFromWindow.Enabled = false;
            }
            else if (app.Name == "Default")
            {
                buttonRemove.Enabled = false;
                textBoxApplicationName.Text = "Default";
                textBoxApplicationName.Enabled = false;
                textBoxExecutable.Text = "";
                textBoxExecutable.Enabled = false;
                buttonBrowse.Enabled = false;
                labelFindFromWindow.Enabled = false;
            }
            else
            {
                buttonRemove.Enabled = true;
                textBoxApplicationName.Text = app.Name;
                textBoxApplicationName.Enabled = true;
                textBoxExecutable.Text = app.Executable;
                textBoxExecutable.Enabled = true;
                buttonBrowse.Enabled = true;
                labelFindFromWindow.Enabled = true;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Settings.Current.Applications.Add(new SettingsApplication());
            Settings.Save();
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
            Settings.Save();
            RefreshApplicationsList();
        }


        private void textBoxApplicationName_TextChanged(object sender, EventArgs e)
        {
            SettingsApplication app = listViewApplications.SelectedItem as SettingsApplication;
            if (app == null)
                return;

            app.Name = textBoxApplicationName.Text;
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
            textBoxExecutable.Text = executable;
            SettingsApplication newApplication = new SettingsApplication();
            if ((textBoxApplicationName.Text.Length == 0) || (textBoxApplicationName.Text == newApplication.Name))
                textBoxApplicationName.Text = System.IO.Path.GetFileNameWithoutExtension(executable);
            RefreshApplicationsList();
        }

        private bool _findFromWindowActive = false;
        private string _findFromWindowOriginalName = "";
        private string _findFromWindowOriginalExecutable = "";
        private void labelFindFromWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _findFromWindowActive = true;
            _findFromWindowOriginalName = textBoxApplicationName.Text;
            _findFromWindowOriginalExecutable = textBoxExecutable.Text;
        }
        private void labelFindFromWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _findFromWindowActive = false;
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
            }
            else
            {
                try
                {
                    string name = process.ProcessName;
                    string executable = Win32.GetProcessExecutableName(process.Id);
                    
                    //StringBuilder windowTitle = new StringBuilder("1024");
                    //Win32.GetWindowText(hwnd, windowTitle, windowTitle.Capacity);

                    textBoxApplicationName.Text = name;
                    textBoxExecutable.Text = executable;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }


    }
}
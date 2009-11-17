using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApplicationLauncher
{
    public partial class ShortcutsForm : Form
    {
        IconExtractor.ExecutableImageList _imageList;
        IconExtractor.ExecutableImageList _imageListIcons;

        public ShortcutsForm()
        {
            InitializeComponent();
        }

        private void ShortcutsForm_Load(object sender, EventArgs e)
        {
            _imageList = new IconExtractor.ExecutableImageList(imageListShortcuts, false);
            _imageList.NonExistantImage = -1;
            _imageListIcons = new IconExtractor.ExecutableImageList(imageListIcons, true);
            _imageListIcons.NonExistantImage = -1;

            olvColumn1.ImageGetter = delegate(object row)
            {
                Shortcut shortcut = row as Shortcut;
                if (shortcut.Icon.Length > 0)
                    return _imageList.GetExecutableIndex(shortcut.Icon);
                else
                    return _imageList.GetExecutableIndex(shortcut.Executable);
            };

            objectListViewShortcuts.SetObjects(Settings.Current.Shortcuts);
            //Rebind();
        }

        private void Rebind()
        {
            //objectListViewShortcuts.SetObjects(Settings.Current.Shortcuts);
            objectListViewShortcuts.BuildList(true);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Shortcut shortcut = new Shortcut();
            shortcut.Name = "New Shortcut";
            Settings.Current.Shortcuts.Add(shortcut);
            Rebind();
            objectListViewShortcuts.SelectedIndex = objectListViewShortcuts.Items.Count - 1;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (objectListViewShortcuts.SelectedIndex >= 0)
            {
                Shortcut shortcut = Settings.Current.Shortcuts[objectListViewShortcuts.SelectedIndex];
                Settings.Current.Shortcuts.Remove(shortcut);
                Settings.Save();
                Rebind();
            }
        }

        private void objectListViewShortcuts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectListViewShortcuts.SelectedIndex >= 0)
            {
                Shortcut shortcut = Settings.Current.Shortcuts[objectListViewShortcuts.SelectedIndex];
                splitContainer1.Panel2.Enabled = true;
                textBoxName.Text = shortcut.Name;
                textBoxExecutable.Text = shortcut.Executable;
                textBoxArguments.Text = shortcut.Arguments;
                checkBoxSwitchTasks.Checked = shortcut.SwitchTasksIfAlreadyRunning;
                textBoxStartIn.Text = shortcut.WorkingFolder;
                textBoxIcon.Text = shortcut.Icon;

                UpdateIcon();
            }
            else
            {
                splitContainer1.Panel2.Enabled = false;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Shortcut shortcut = Settings.Current.Shortcuts[objectListViewShortcuts.SelectedIndex];
            shortcut.Name = textBoxName.Text;
            shortcut.Executable = textBoxExecutable.Text;
            shortcut.Arguments = textBoxArguments.Text;
            shortcut.SwitchTasksIfAlreadyRunning = checkBoxSwitchTasks.Checked;
            shortcut.WorkingFolder = textBoxStartIn.Text;
            shortcut.Icon = textBoxIcon.Text;

            Settings.Save();
            Rebind();
        }

        private void buttonTargetBrowse_Click(object sender, EventArgs e)
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
                textBoxExecutable.Text = ofd.FileName;
                textBoxName.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);

                UpdateIcon();
            }
        }

        private void buttonStartInBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (textBoxStartIn.Text.Length > 0)
            {
                if (System.IO.Directory.Exists(textBoxStartIn.Text))
                {
                    dialog.SelectedPath = textBoxStartIn.Text;
                }
            }
            DialogResult result = dialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBoxStartIn.Text = dialog.SelectedPath;
            }
        }

        private void buttonIconBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (textBoxIcon.Text.Length > 0)
            {
                if (System.IO.Directory.Exists(textBoxIcon.Text))
                {
                    ofd.InitialDirectory = textBoxIcon.Text;
                }
                else if (System.IO.File.Exists(textBoxIcon.Text))
                {
                    ofd.FileName = textBoxIcon.Text;
                }
            }
            DialogResult result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBoxIcon.Text = ofd.FileName;

                UpdateIcon();
            }
        }

        private void textBoxIcon_Leave(object sender, EventArgs e)
        {
            UpdateIcon();
        }

        private void UpdateIcon()
        {
            int index;
            if (textBoxIcon.Text.Length > 0)
                index = _imageListIcons.GetExecutableIndex(textBoxIcon.Text);
            else
                index = _imageListIcons.GetExecutableIndex(textBoxExecutable.Text);

            if (index >= 0)
                pictureBox1.Image = imageListIcons.Images[index];
            else
                pictureBox1.Image = null;
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (objectListViewShortcuts.SelectedIndex >= 0)
            {
                Shortcut shortcut = Settings.Current.Shortcuts[objectListViewShortcuts.SelectedIndex];
                int index = Settings.Current.Shortcuts.IndexOf(shortcut);
                if (index > 0)
                {
                    Settings.Current.Shortcuts.RemoveAt(index);
                    Settings.Current.Shortcuts.Insert(index - 1, shortcut);
                    Settings.Save();
                    Rebind();
                }
            }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (objectListViewShortcuts.SelectedIndex >= 0)
            {
                Shortcut shortcut = Settings.Current.Shortcuts[objectListViewShortcuts.SelectedIndex];
                int index = Settings.Current.Shortcuts.IndexOf(shortcut);
                if (index < Settings.Current.Shortcuts.Count - 1)
                {
                    Settings.Current.Shortcuts.RemoveAt(index);
                    Settings.Current.Shortcuts.Insert(index + 1, shortcut);
                    Settings.Save();
                    Rebind();
                }
            }
        }

    }
}

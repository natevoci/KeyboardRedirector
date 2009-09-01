using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace KeyboardRedirector
{
    public partial class ActionPropertiesDialog : Form
    {
        public SettingsKeyboardKeyAction Action = null;

        private Dictionary<string, Keys> _virtualKeys = new Dictionary<string, Keys>();
        private KeyboardHookLowLevel _keyboardHook;
        //private KeyboardHook _keyboardHook;
        private uint _hookMessageId = 0x402;

        public ActionPropertiesDialog()
        {
            string exeFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(exeFilename);

            InitializeComponent();

            _keyboardHook = new KeyboardHookLowLevel();
            _keyboardHook.KeyDown += new KeyEventHandler(_keyboardHook_KeyDown);
            //_keyboardHook = new KeyboardHook(this, _hookMessageId, false);

            Keys key = (Keys)Enum.Parse(typeof(Keys), "Enter");
            key = (Keys)Enum.Parse(typeof(Keys), "Return");

            comboBoxKeyboardKey.DataSource = NiceKeyName.NameList;

            tabPageLaunchApplication.Tag = SettingsKeyboardKeyActionType.LaunchApplication;
            tabPageKeyboard.Tag = SettingsKeyboardKeyActionType.Keyboard;
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == (int)_hookMessageId)
            {
                message.Result = IntPtr.Zero;
                return;
            }

            base.WndProc(ref message);
        }

        private void ActionPropertiesDialog_Load(object sender, EventArgs e)
        {
            if (Action != null)
            {
                // Launch Application
                textBoxLaunchApplication.Text = Action.LaunchApplication.Command;
                if (Action.LaunchApplication.WaitForInputIdle)
                    radioButtonLaunchApplicationWaitForInputIdle.Checked = true;
                else if (Action.LaunchApplication.WaitForExit)
                    radioButtonLaunchApplicationWaitForExit.Checked = true;
                else
                    radioButtonLaunchApplicationDoNotWait.Checked = true;

                // Keyboard
                checkBoxKeyboardControl.Checked = Action.Keyboard.Control;
                checkBoxKeyboardShift.Checked = Action.Keyboard.Shift;
                checkBoxKeyboardAlt.Checked = Action.Keyboard.Alt;
                comboBoxKeyboardKey.Text = NiceKeyName.GetName(Action.Keyboard.VirtualKey);
                numericUpDown1.Value = Action.Keyboard.RepeatCount;

                // Active tab
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if ((SettingsKeyboardKeyActionType)page.Tag == Action.ActionType)
                        tabControl1.SelectedTab = page;
                }
            }
            else
            {
                tabControl1.Enabled = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Action != null)
            {
                // Launch Application
                Action.LaunchApplication.Command = textBoxLaunchApplication.Text;
                Action.LaunchApplication.WaitForInputIdle = radioButtonLaunchApplicationWaitForInputIdle.Checked;
                Action.LaunchApplication.WaitForExit = radioButtonLaunchApplicationWaitForExit.Checked;

                // Keyboard
                Action.Keyboard.Control = checkBoxKeyboardControl.Checked;
                Action.Keyboard.Shift = checkBoxKeyboardShift.Checked;
                Action.Keyboard.Alt = checkBoxKeyboardAlt.Checked;
                Keys key = NiceKeyName.GetKey(comboBoxKeyboardKey.Text);
                Action.Keyboard.VirtualKey = key;
                Action.Keyboard.RepeatCount = (int)numericUpDown1.Value;

                // Active ActionType
                Action.ActionType = (SettingsKeyboardKeyActionType)tabControl1.SelectedTab.Tag;
            }
        }

        private void buttonLaunchAppBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            if (textBoxLaunchApplication.Text.Length > 0)
            {
                if (System.IO.Directory.Exists(textBoxLaunchApplication.Text))
                {
                    ofd.InitialDirectory = textBoxLaunchApplication.Text;
                }
                else if (System.IO.File.Exists(textBoxLaunchApplication.Text))
                {
                    ofd.FileName = textBoxLaunchApplication.Text;
                }
            }
            DialogResult result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                textBoxLaunchApplication.Text = ofd.FileName;
            }
        }

        void _keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            checkBoxKeyboardControl.Checked = ((e.Modifiers & Keys.Control) != 0);
            checkBoxKeyboardShift.Checked = ((e.Modifiers & Keys.Shift) != 0);
            checkBoxKeyboardAlt.Checked = ((e.Modifiers & Keys.Alt) != 0);
            comboBoxKeyboardKey.Text = NiceKeyName.GetName(e.KeyCode);
            e.Handled = true;
        }

        private void richTextBoxKeyDetector_Enter(object sender, EventArgs e)
        {
            _keyboardHook.SetHook();
        }

        private void richTextBoxKeyDetector_Leave(object sender, EventArgs e)
        {
            _keyboardHook.ClearHook();
        }

        private void ActionPropertiesDialog_Deactivate(object sender, EventArgs e)
        {
            tabControl1.Focus();
        }


    }
}
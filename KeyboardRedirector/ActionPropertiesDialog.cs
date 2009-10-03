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

namespace KeyboardRedirector
{
    public partial class ActionPropertiesDialog : Form
    {
        public SettingsKeyboardKeyAction Action = null;

        public ActionPropertiesDialog()
        {
            string exeFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(exeFilename);

            InitializeComponent();

            comboBoxKeyboardKey.DataSource = NiceKeyName.NameList;

            tabPageLaunchApplication.Tag = SettingsKeyboardKeyActionType.LaunchApplication;
            tabPageKeyboard.Tag = SettingsKeyboardKeyActionType.Keyboard;
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
                checkBoxKeyboardExtended.Checked = Action.Keyboard.Extended;
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
                Action.Keyboard.Extended = checkBoxKeyboardExtended.Checked;
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

        private void richTextBoxKeyDetector_Enter(object sender, EventArgs e)
        {
            KeyboardHookExternal.Current.KeyEventLowLevel += new KeyHookEventHandler(Current_KeyEventLowLevel);
        }

        private void richTextBoxKeyDetector_Leave(object sender, EventArgs e)
        {
            KeyboardHookExternal.Current.KeyEventLowLevel -= new KeyHookEventHandler(Current_KeyEventLowLevel);
        }

        void Current_KeyEventLowLevel(object sender, KeyHookEventArgs e)
        {
            if (e.KeyCombination.KeyDown == false)
                return;

            checkBoxKeyboardControl.Checked = false;
            checkBoxKeyboardShift.Checked = false;
            checkBoxKeyboardAlt.Checked = false;
            foreach (KeysWithExtended key in e.KeyCombination.Modifiers)
            {
                if ((key.Keys == Keys.ControlKey) || (key.Keys == Keys.LControlKey) || (key.Keys == Keys.RControlKey))
                    checkBoxKeyboardControl.Checked = true;
                if ((key.Keys == Keys.ShiftKey) || (key.Keys == Keys.LShiftKey) || (key.Keys == Keys.RShiftKey))
                    checkBoxKeyboardShift.Checked = true;
                if ((key.Keys == Keys.Menu) || (key.Keys == Keys.LMenu) || (key.Keys == Keys.RMenu))
                    checkBoxKeyboardAlt.Checked = true;
            }

            comboBoxKeyboardKey.Text = NiceKeyName.GetName(e.KeyCombination.KeyWithExtended.Keys);
            checkBoxKeyboardExtended.Checked = e.KeyCombination.KeyWithExtended.Extended;

            e.Handled = true;
        }

        private void ActionPropertiesDialog_Deactivate(object sender, EventArgs e)
        {
            tabControl1.Focus();
        }


    }
}
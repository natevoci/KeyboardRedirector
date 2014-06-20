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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using MS;

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
            var notFoundOptions = (from n in Enum.GetNames(typeof(SettingsKeyboardKeyActionType))
                                   where n != SettingsKeyboardKeyActionType.WindowMessage.ToString()
                                   select n).ToList();
            notFoundOptions.Insert(0, "do nothing");
            comboBoxNotFound.DataSource = notFoundOptions;

            tabPageLaunchApplication.Tag = SettingsKeyboardKeyActionType.LaunchApplication;
            tabPageKeyboard.Tag = SettingsKeyboardKeyActionType.Keyboard;
            tabPageWindowMessage.Tag = SettingsKeyboardKeyActionType.WindowMessage;
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
                checkBoxKeyboardLWin.Checked = Action.Keyboard.LWin;
                checkBoxKeyboardRWin.Checked = Action.Keyboard.RWin;
                comboBoxKeyboardKey.Text = NiceKeyName.GetName(Action.Keyboard.VirtualKey);
                checkBoxKeyboardExtended.Checked = Action.Keyboard.Extended;
                numericUpDown1.Value = Action.Keyboard.RepeatCount;

                // Window Message
                textBoxProcessName.Text = Action.WindowMessage.ProcessName;
                textBoxWindowName.Text = Action.WindowMessage.WindowName;
                textBoxWindowClass.Text = Action.WindowMessage.WindowClass;
                if (Action.WindowMessage.NotFoundAction == SettingsKeyboardKeyActionType.WindowMessage)
                    comboBoxNotFound.Text = "do nothing";
                else
                    comboBoxNotFound.Text = Action.WindowMessage.NotFoundAction.ToString();
                textBoxMessageID.Text = Action.WindowMessage.Message.ToString();
                textBoxWParam.Text = Action.WindowMessage.WParam.ToString();
                textBoxLParam.Text = Action.WindowMessage.LParam.ToString();

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
                Action.Keyboard.LWin = checkBoxKeyboardLWin.Checked;
                Action.Keyboard.RWin = checkBoxKeyboardRWin.Checked;
                Keys key = NiceKeyName.GetKey(comboBoxKeyboardKey.Text);
                Action.Keyboard.VirtualKey = key;
                Action.Keyboard.Extended = checkBoxKeyboardExtended.Checked;
                Action.Keyboard.RepeatCount = (int)numericUpDown1.Value;

                // Window Message
                Action.WindowMessage.ProcessName = textBoxProcessName.Text;
                Action.WindowMessage.WindowName = textBoxWindowName.Text;
                Action.WindowMessage.WindowClass = textBoxWindowClass.Text;
                if (comboBoxNotFound.Text == "do nothing")
                    Action.WindowMessage.NotFoundAction = SettingsKeyboardKeyActionType.WindowMessage;
                else
                    Action.WindowMessage.NotFoundAction = (SettingsKeyboardKeyActionType)Enum.Parse(typeof(SettingsKeyboardKeyActionType), comboBoxNotFound.Text);
                Action.WindowMessage.Message = uint.Parse(textBoxMessageID.Text);
                Action.WindowMessage.WParam = uint.Parse(textBoxWParam.Text);
                Action.WindowMessage.LParam = uint.Parse(textBoxLParam.Text);

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

            if (this.InvokeRequired)
            {
                Current_KeyEventLowLevelDelegate function = new Current_KeyEventLowLevelDelegate(Current_KeyEventLowLevelThread);
                function.BeginInvoke(e.KeyCombination, asyncResult => { function.EndInvoke(asyncResult); }, null);
            }
            else
            {
                Current_KeyEventLowLevel(e.KeyCombination);
            }

            e.Handled = true;
        }

        delegate void Current_KeyEventLowLevelDelegate(KeyCombination keyCombination);
        void Current_KeyEventLowLevelThread(KeyCombination keyCombination)
        {
            this.Invoke(new Current_KeyEventLowLevelDelegate(Current_KeyEventLowLevel), keyCombination);
        }

        void Current_KeyEventLowLevel(KeyCombination keyCombination)
        {
            checkBoxKeyboardControl.Checked = false;
            checkBoxKeyboardShift.Checked = false;
            checkBoxKeyboardAlt.Checked = false;
            checkBoxKeyboardLWin.Checked = false;
            checkBoxKeyboardRWin.Checked = false;
            foreach (KeysWithExtended key in keyCombination.Modifiers)
            {

                if (key.IsControlKey)
                    checkBoxKeyboardControl.Checked = true;
                if (key.IsShiftKey)
                    checkBoxKeyboardShift.Checked = true;
                if (key.IsAltKey)
                    checkBoxKeyboardAlt.Checked = true;
                if (key.IsLWinKey)
                    checkBoxKeyboardLWin.Checked = true;
                if (key.IsRWinKey)
                    checkBoxKeyboardRWin.Checked = true;
            }

            comboBoxKeyboardKey.Text = NiceKeyName.GetName(keyCombination.KeyWithExtended.Keys);
            checkBoxKeyboardExtended.Checked = keyCombination.KeyWithExtended.Extended;

        }

        private void ActionPropertiesDialog_Deactivate(object sender, EventArgs e)
        {
            tabControl1.Focus();
        }

        private bool _findFromWindowActive = false;
        private bool _findFromWindowValid = false;
        private string _findFromWindowOriginalProcessName = "";
        private string _findFromWindowOriginalWindowClass = "";
        private string _findFromWindowOriginalWindowName = "";
        private void labelFindFromWindow_MouseDown(object sender, MouseEventArgs e)
        {
            _findFromWindowActive = true;

            _findFromWindowOriginalProcessName = textBoxProcessName.Text;
            _findFromWindowOriginalWindowClass = textBoxWindowClass.Text;
            _findFromWindowOriginalWindowName = textBoxWindowName.Text;
        }

        private void labelFindFromWindow_MouseUp(object sender, MouseEventArgs e)
        {
            _findFromWindowActive = false;
        }

        private void labelFindFromWindow_MouseMove(object sender, MouseEventArgs e)
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

            //hwnd = Win32.GetAncestor(hwnd, Win32.GA.ROOT);
            //if (hwnd == IntPtr.Zero)
            //    return;

            if (_lastHWNDFromGrabAppUnderPoint == hwnd)
                return;
            _lastHWNDFromGrabAppUnderPoint = hwnd;

            uint processId;
            Win32.GetWindowThreadProcessId(hwnd, out processId);
            Process process = Process.GetProcessById((int)processId);

            if (process.Id == Process.GetCurrentProcess().Id)
            {
                textBoxProcessName.Text = _findFromWindowOriginalProcessName;
                textBoxWindowClass.Text = _findFromWindowOriginalWindowClass;
                textBoxWindowName.Text = _findFromWindowOriginalWindowName;
                _findFromWindowValid = false;
            }
            else
            {
                try
                {
                    string name = process.ProcessName;
                    string executable = Win32.GetProcessExecutableName(process.Id);
                    textBoxProcessName.Text = name;

                    StringBuilder windowClass = new StringBuilder(Win32.GETWINDOWTEXT_MAXLENGTH);
                    Win32.GetClassName(hwnd, windowClass, windowClass.Capacity);
                    textBoxWindowClass.Text = windowClass.ToString();

                    StringBuilder windowTitle = new StringBuilder(Win32.GETWINDOWTEXT_MAXLENGTH);
                    Win32.GetWindowText(hwnd, windowTitle, windowTitle.Capacity);
                    textBoxWindowName.Text = windowTitle.ToString();

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
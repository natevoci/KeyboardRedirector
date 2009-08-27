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

using System.Windows.Forms;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace KeyboardRedirector
{
    public partial class Form1 : Form
    {
        InputDevice _inputDevice;
        int NumberOfKeyboards;

        public Form1()
        {
            InitializeComponent();

            // Create a new InputDevice object, get the number of
            // keyboards, and register the method which will handle the 
            // InputDevice KeyPressed event
            //_handle = Handle;
            _inputDevice = new InputDevice(Handle);
            NumberOfKeyboards = _inputDevice.EnumerateDevices();
            _inputDevice.KeyPressed += new InputDevice.KeyPressedEventHandler(m_KeyPressed);
            _inputDevice.DeviceEvent += new InputDevice.DeviceEventHandler(id_DeviceEvent);

            //Thread worker = new Thread(ThreadProc);
            //worker.Name = "worker";
            //worker.IsBackground = true;
            //worker.Start();
        }

        // The WndProc is overridden to allow InputDevice to intercept
        // messages to the window and thus catch WM_INPUT messages
        protected override void WndProc(ref Message message)
        {
            if (_inputDevice != null)
            {
                //Message msg;
                //NativeMethods.PeekMessage(out msg, _handle, NativeMethods.WM_INPUT, NativeMethods.WM_INPUT, NativeMethods.PeekMessageRemoveFlag.PM_NOREMOVE);
                //if (msg.Msg != 0)
                //{
                //    Debug.WriteLine("PeekMessage found message waiting.");
                //    id.ProcessMessage(msg);
                //}

                _inputDevice.ProcessMessage(message);
            }
            if (message.Msg == KeyboardHook.HookMessage)
            {
                uint wParam = (uint)message.WParam.ToInt32();
                bool peekMessage = ((wParam >> 31) != 0);
                wParam = wParam & 0x7FFFFFFF;

                KeyboardHook.KeyboardParams parameters = KeyboardHook.ConvertLParamToKeyboardParams(message.LParam.ToInt32());
                //Debug.WriteLine(message.ToString());
                Debug.WriteLine("HookMsg:" +
                    " " + (peekMessage ? "Peek" : "    ") +
                    " wparam=0x" + wParam.ToString("x") + " (" + ((Keys)wParam).ToString() + ")" + 
                    " repeatCount=0x" + parameters.repeatCount.ToString("x") +
                    " scanCode=0x" + parameters.scanCode.ToString("x") +
                    " transitionState=" + parameters.transitionState.ToString() +
                    " extendedKey=" + parameters.extendedKey.ToString() + 
                    " contextCode=" + parameters.contextCode.ToString() +
                    " previousKeyState=" + parameters.previousKeyState.ToString()
                    );

                if (message.WParam.ToInt32() == (int)Keys.NumPad4)
                    message.Result = new IntPtr(-1);
                
                //Debug.WriteLine(message.ToString());
                return;
            }

            base.WndProc(ref message);
        }

        private void m_KeyPressed(object sender, InputDevice.KeyControlEventArgs e)
        {
            //Replace() is just a cosmetic fix to stop ampersands turning into underlines
            lbHandle.Text = e.Keyboard.DeviceHandle.ToString();
            lbType.Text = e.Keyboard.DeviceType.ToString();
            lbName.Text = e.Keyboard.DeviceName.Replace("&", "&&");
            lbDescription.Text = e.Keyboard.Name;
            lbKey.Text = ((int)(e.Keyboard.keys)).ToString();
            lbNumKeyboards.Text = NumberOfKeyboards.ToString();
            lbVKey.Text = e.Keyboard.keys.ToString();
        }

        void id_DeviceEvent(object sender, InputDevice.DeviceInfo dInfo, InputDevice.RAWINPUT rawInput)
        {
            //throw new Exception("The method or operation is not implemented.");
            string text;
            if (rawInput.header.dwType == InputDevice.DeviceType.Keyboard)
            {
                Keys key = (Keys)rawInput.data.keyboard.VKey;
                text = string.Format("{0} 0x{1:x}({2}) 0x{3:x} {4}", rawInput.header.dwType, rawInput.data.keyboard.VKey, key, rawInput.data.keyboard.MakeCode, rawInput.data.keyboard.Message);
                System.Diagnostics.Debug.WriteLine("WM_INPUT: 0x" + dInfo.DeviceHandle.ToInt32().ToString("x8") + " " + text);
            }
            if (rawInput.header.dwType == InputDevice.DeviceType.HID)
            {
                text = string.Format("{0} {1} {2}", rawInput.header.dwType, rawInput.data.hid.dwSizHid, rawInput.data.hid.dwCount);
                System.Diagnostics.Debug.WriteLine("WM_INPUT: " + text);
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            richTextBoxList.Clear();
            richTextBoxList.AppendText("Number of Keyboards: " + NumberOfKeyboards.ToString() + Environment.NewLine);
            foreach (InputDevice.DeviceInfo info in _inputDevice.DeviceList.Values)
            {
                richTextBoxList.AppendText(Environment.NewLine);
                richTextBoxList.AppendText("Handle: 0x" + info.DeviceHandle.ToInt32().ToString("x8") + Environment.NewLine);
                richTextBoxList.AppendText(info.DeviceType + " - " + info.Name + Environment.NewLine);
                richTextBoxList.AppendText("DeviceName: " + info.DeviceName + Environment.NewLine);
                richTextBoxList.AppendText("DeviceDesc: " + info.DeviceDesc + Environment.NewLine);
            }
        }

        //private void ThreadProc()
        //{
        //    while (true)
        //    {
        //        //NativeMethods.MSG message;
        //        //Message message;
        //        //NativeMethods.PeekMessage(out message, _handle, NativeMethods.WM_INPUT, NativeMethods.WM_INPUT, NativeMethods.PeekMessageRemoveFlag.PM_NOREMOVE);
        //        //if (message.Msg != 0)
        //        //{
        //        //    id.ProcessMessage(message);
        //        //}
        //        //id.ProcessBuffer();
        //        System.Threading.Thread.Sleep(40);
        //    }
        //}

        private void checkBoxHookKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHookKeyboard.Checked)
            {
                bool result = KeyboardHook.SetHook(this);
                if (result == false)
                    richTextBoxList.AppendText("Failed to set hook" + Environment.NewLine);
            }
            else
            {
                bool result = KeyboardHook.ClearHook();
                if (result == false)
                    richTextBoxList.AppendText("Failed to set hook" + Environment.NewLine);
            }
        }

    }
}
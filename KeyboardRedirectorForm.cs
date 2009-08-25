using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RawInput
{
    public partial class KeyboardRedirectorForm : Form
    {
        InputDevice _inputDevice;
        List<DeviceInformation> _keyboards;
        List<KeyToHookInformation> _keysToHook;
        List<int> _capturingHandles;

        bool _detectKeyboard;

        class DeviceInformation
        {
            public InputDevice.DeviceInfo DeviceInfo = null;

            public DeviceInformation(InputDevice.DeviceInfo DeviceInfo)
            {
                this.DeviceInfo = DeviceInfo;
            }

            public override string ToString()
            {
                return DeviceInfo.Name;
            }
        }

        class KeyToHookInformation
        {
            public ushort VirtualKeyCode;
            public DateTime SeenAt;

            public KeyToHookInformation(ushort VirtualKeyCode)
            {
                this.VirtualKeyCode = VirtualKeyCode;
                this.SeenAt = DateTime.Now;
            }
        }


        public KeyboardRedirectorForm()
        {
            _keyboards = new List<DeviceInformation>();
            _keysToHook = new List<KeyToHookInformation>();
            _capturingHandles = new List<int>();

            InitializeComponent();

            _inputDevice = new InputDevice(Handle);
            _inputDevice.DeviceEvent += new InputDevice.DeviceEventHandler(InputDevice_DeviceEvent);

            foreach (InputDevice.DeviceInfo info in _inputDevice.DeviceList.Values)
            {
                if (info.DeviceType != InputDevice.DeviceType.Keyboard)
                    continue;
                if (info.DeviceHandle == IntPtr.Zero)
                    continue;

                _keyboards.Add(new DeviceInformation(info));
            }

            comboBoxKeyboards.DataSource = _keyboards;

            bool result = KeyboardHook.SetHook(this);
            if (result == false)
                richTextBoxEvents.AppendText("Failed to set hook" + Environment.NewLine);
        }

        private void KeyboardRedirectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyboardHook.ClearHook();
        }

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
                //System.Diagnostics.Debug.WriteLine(
                //    "HookMsg: " + (peekMessage ? "Peek" : "    ") +
                //    " wparam=0x" + wParam.ToString("x") + " (" + ((Keys)wParam).ToString() + ")" +
                //    " repeatCount=0x" + parameters.repeatCount.ToString("x") +
                //    " scanCode=0x" + parameters.scanCode.ToString("x") +
                //    " transitionState=" + parameters.transitionState.ToString() +
                //    " extendedKey=" + parameters.extendedKey.ToString() +
                //    " contextCode=" + parameters.contextCode.ToString() +
                //    " previousKeyState=" + parameters.previousKeyState.ToString()
                //    );

                bool block = false;
                lock (_keysToHook)
                {
                    for (int i = 0; i < _keysToHook.Count; i++)
                    {
                        if (_keysToHook[i].VirtualKeyCode == wParam)
                        {
                            block = true;
                            _keysToHook.RemoveAt(i);
                        }
                    }

                    // Remove any old stale entries
                    while ((_keysToHook.Count > 0) && (DateTime.Now.Subtract(_keysToHook[0].SeenAt).TotalMilliseconds > 500))
                    {
                        _keysToHook.RemoveAt(0);
                    }
                }

                if (block)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "Blocking: " + (peekMessage ? "Peek" : "    ") +
                        " wparam=0x" + wParam.ToString("x") + " (" + ((Keys)wParam).ToString() + ")" +
                        " repeatCount=0x" + parameters.repeatCount.ToString("x") +
                        " scanCode=0x" + parameters.scanCode.ToString("x") +
                        " transitionState=" + parameters.transitionState.ToString() +
                        " extendedKey=" + parameters.extendedKey.ToString() +
                        " contextCode=" + parameters.contextCode.ToString() +
                        " previousKeyState=" + parameters.previousKeyState.ToString()
                        );

                    message.Result = new IntPtr(-1);
                    return;
                }

                return;
            }
            
            base.WndProc(ref message);
        }

        void InputDevice_DeviceEvent(object sender, InputDevice.DeviceInfo dInfo, InputDevice.RAWINPUT rawInput)
        {
            if (rawInput.header.dwType == InputDevice.DeviceType.Keyboard)
            {
                Keys key = (Keys)rawInput.data.keyboard.VKey;
                string text = string.Format("{0} 0x{1:x}({2}) 0x{3:x} {4}",
                    rawInput.header.dwType,
                    rawInput.data.keyboard.VKey,
                    key,
                    rawInput.data.keyboard.MakeCode,
                    rawInput.data.keyboard.Message);

                System.Diagnostics.Debug.WriteLine("WM_INPUT: 0x" + dInfo.DeviceHandle.ToInt32().ToString("x8") + " " + text);

                if (_detectKeyboard)
                {
                    lock (_keysToHook)
                    {
                        _keysToHook.Add(new KeyToHookInformation(rawInput.data.keyboard.VKey));
                    }
                    _detectKeyboard = false;

                    foreach (DeviceInformation info in _keyboards)
                    {
                        if (info.DeviceInfo.DeviceHandle == dInfo.DeviceHandle)
                        {
                            comboBoxKeyboards.SelectedItem = null;
                            comboBoxKeyboards.SelectedItem = info;
                            break;
                        }
                    }
                    return;
                }

                lock (_capturingHandles)
                {
                    int handle = dInfo.DeviceHandle.ToInt32();
                    if (_capturingHandles.Contains(handle))
                    {
                        lock (_keysToHook)
                        {
                            _keysToHook.Add(new KeyToHookInformation(rawInput.data.keyboard.VKey));
                        }

                        if (richTextBoxEvents.Text.Length > 0)
                            richTextBoxEvents.AppendText(Environment.NewLine);
                        richTextBoxEvents.AppendText(rawInput.data.keyboard.Message.ToString().PadRight(7) + " 0x" + rawInput.data.keyboard.VKey.ToString("x2") + " (" + key.ToString() + ")");
                    }
                }

            }
            
        }


        private void RefreshKeyboardDetails()
        {
            DeviceInformation deviceInformation = comboBoxKeyboards.SelectedItem as DeviceInformation;
            if (deviceInformation == null)
            {
                labelDeviceDetails.Text = "";
                return;
            }
            InputDevice.DeviceInfo info = deviceInformation.DeviceInfo;
            if (info == null)
            {
                labelDeviceDetails.Text = "";
                return;
            }

            StringBuilder details = new StringBuilder();

            details.Append("Handle: 0x" + info.DeviceHandle.ToInt32().ToString("x8") + Environment.NewLine);
            details.Append("DeviceName: " + info.DeviceName + Environment.NewLine);
            details.Append("DeviceDesc: " + info.DeviceDesc + Environment.NewLine);

            labelDeviceDetails.Text = details.ToString();

            if (_capturingHandles.Contains(info.DeviceHandle.ToInt32()))
            {
                buttonStartCapturing.Enabled = false;
                buttonStopCapturing.Enabled = true;
            }
            else
            {
                buttonStartCapturing.Enabled = true;
                buttonStopCapturing.Enabled = false;
            }
        }


        private void comboBoxKeyboards_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshKeyboardDetails();
        }

        private void buttonDetect_Click(object sender, EventArgs e)
        {
            comboBoxKeyboards.Text = "";
            labelDeviceDetails.Text = "press any key on the keyboard you want to select...";
            _detectKeyboard = true;
        }

        private void buttonStartCapturing_Click(object sender, EventArgs e)
        {
            DeviceInformation deviceInformation = comboBoxKeyboards.SelectedItem as DeviceInformation;
            if (deviceInformation != null)
            {
                if (deviceInformation.DeviceInfo != null)
                {
                    lock (_capturingHandles)
                    {
                        int handle = deviceInformation.DeviceInfo.DeviceHandle.ToInt32();
                        if (!_capturingHandles.Contains(handle))
                            _capturingHandles.Add(handle);
                    }
                    RefreshKeyboardDetails();
                }
            }
        }

        private void buttonStopCapturing_Click(object sender, EventArgs e)
        {
            DeviceInformation deviceInformation = comboBoxKeyboards.SelectedItem as DeviceInformation;
            if (deviceInformation != null)
            {
                if (deviceInformation.DeviceInfo != null)
                {
                    lock (_capturingHandles)
                    {
                        int handle = deviceInformation.DeviceInfo.DeviceHandle.ToInt32();
                        if (_capturingHandles.Contains(handle))
                            _capturingHandles.Remove(handle);
                    }
                    RefreshKeyboardDetails();
                }
            }
        }

    }
}
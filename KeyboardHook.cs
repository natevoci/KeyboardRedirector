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
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KeyboardRedirector
{
    class KeyboardHook
    {
        [DllImport("KeyboardHook.dll", SetLastError = true)]
        private static extern bool SetHook(IntPtr hwnd, uint message);

        [DllImport("KeyboardHook.dll", SetLastError = true)]
        private static extern bool ClearHook(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        #region Constants
        const int INPUT_MOUSE = 0;
        const int INPUT_KEYBOARD = 1;
        const int INPUT_HARDWARE = 2;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_UNICODE = 0x0004;
        const uint KEYEVENTF_SCANCODE = 0x0008;
        const uint XBUTTON1 = 0x0001;
        const uint XBUTTON2 = 0x0002;
        const uint MOUSEEVENTF_MOVE = 0x0001;
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_XDOWN = 0x0080;
        const uint MOUSEEVENTF_XUP = 0x0100;
        const uint MOUSEEVENTF_WHEEL = 0x0800;
        const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        #endregion

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            public INPUTHEADER header;
            public INPUTDATA data;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUTHEADER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
        }
        [StructLayout(LayoutKind.Explicit)]
        internal struct INPUTDATA
        {
            [FieldOffset(0)]
            public MOUSEINPUT mouse;
            [FieldOffset(0)]
            public KEYBOARDINPUT keyboard;
            [FieldOffset(0)]
            public HARDWAREINPUT hardware;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBOARDINPUT
        {
            public ushort VKey;
            public ushort ScanCode;
            public uint dwFlags;
            public uint time;
            public IntPtr ExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }



        public enum KeyboardParamsContextCode
        {
            ALTUp = 0,
            ALTDown = 1
        }
        public enum KeyboardParamsPreviousKeyState
        {
            Up = 0,
            Down = 1
        }
        public enum KeyboardParamsTransitionState
        {
            Down = 0,
            Up = 1
        }

        public struct KeyboardParams
        {
            public Int16 repeatCount;
            public byte scanCode;
            public bool extendedKey;
            public int reserved;
            public KeyboardParamsContextCode contextCode;
            public KeyboardParamsPreviousKeyState previousKeyState;
            public KeyboardParamsTransitionState transitionState;
        }

        #endregion

        private const uint _hookMessage = 0x0401;
        private static Form _form;

        public static uint HookMessage
        {
            get
            {
                return _hookMessage;
            }
        }

        public static bool SetHook(Form form)
        {
            bool result = KeyboardHook.SetHook(form.Handle, _hookMessage);
            if (result)
                _form = form;
            return result;
        }

        public static bool ClearHook()
        {
            if (_form == null)
                return false;
            bool result = ClearHook(_form.Handle);
            if (result)
                _form = null;
            return result;
        }

        public static KeyboardParams ConvertLParamToKeyboardParams(int lParam)
        {
            KeyboardParams result;
            result.repeatCount = (short)GetValueFromBits(lParam, 0, 16);
            result.scanCode = (byte)GetValueFromBits(lParam, 16, 8);
            result.extendedKey = (GetValueFromBits(lParam, 24, 1) != 0);
            result.reserved = GetValueFromBits(lParam, 25, 4);
            result.contextCode = (KeyboardParamsContextCode)GetValueFromBits(lParam, 29, 1);
            result.previousKeyState = (KeyboardParamsPreviousKeyState)GetValueFromBits(lParam, 30, 1);
            result.transitionState = (KeyboardParamsTransitionState)GetValueFromBits(lParam, 31, 1);
            return result;
        }

        private static int GetValueFromBits(int lParam, int startAtBit, int bitCount)
        {
            lParam = lParam >> startAtBit;
            return lParam & ((1 << bitCount) - 1);
        }


    }
}

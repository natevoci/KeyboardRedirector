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
    public class KeyboardHook
    {
        private class KeyboardHook32
        {
            [DllImport("KeyboardHook32.dll", SetLastError = true)]
            public static extern bool SetHook(IntPtr hwnd, uint message);

            [DllImport("KeyboardHook32.dll", SetLastError = true)]
            public static extern bool ClearHook(IntPtr hwnd);

            [DllImport("KeyboardHook32.dll", SetLastError = true)]
            public static extern bool SetHook_LL(IntPtr hwnd, uint message);

            [DllImport("KeyboardHook32.dll", SetLastError = true)]
            public static extern bool ClearHook_LL(IntPtr hwnd);
        }

        private class KeyboardHook64
        {
            [DllImport("KeyboardHook64.dll", SetLastError = true)]
            public static extern bool SetHook(IntPtr hwnd, uint message);

            [DllImport("KeyboardHook64.dll", SetLastError = true)]
            public static extern bool ClearHook(IntPtr hwnd);

            [DllImport("KeyboardHook64.dll", SetLastError = true)]
            public static extern bool SetHook_LL(IntPtr hwnd, uint message);

            [DllImport("KeyboardHook64.dll", SetLastError = true)]
            public static extern bool ClearHook_LL(IntPtr hwnd);
        }

        private static bool SetHook(IntPtr hwnd, uint message)
        {
            if (Is64Bit())
                return KeyboardHook64.SetHook(hwnd, message);
            else
                return KeyboardHook32.SetHook(hwnd, message);
        }

        private static bool ClearHook(IntPtr hwnd)
        {
            if (Is64Bit())
                return KeyboardHook64.ClearHook(hwnd);
            else
                return KeyboardHook32.ClearHook(hwnd);
        }

        private static bool SetHook_LL(IntPtr hwnd, uint message)
        {
            if (Is64Bit())
                return KeyboardHook64.SetHook_LL(hwnd, message);
            else
                return KeyboardHook32.SetHook_LL(hwnd, message);
        }

        private static bool ClearHook_LL(IntPtr hwnd)
        {
            if (Is64Bit())
                return KeyboardHook64.ClearHook_LL(hwnd);
            else
                return KeyboardHook32.ClearHook_LL(hwnd);
        }

        public static bool Is64Bit()
        {
            if (Marshal.SizeOf(typeof(IntPtr)) == 8)
                return true;

            return false;
        }


        private uint _hookMessage;
        private Form _form;
        private bool _hooked;
        private bool _lowLevel = false;

        public KeyboardHook(Form form, uint hookMessage)
            : this(form, hookMessage, false)
        {
        }
        public KeyboardHook(Form form, uint hookMessage, bool lowLevel)
        {
            _form = form;
            _hookMessage = hookMessage;
            _hooked = false;
            _lowLevel = lowLevel;
        }

        public uint HookMessage
        {
            get { return _hookMessage; }
        }

        public bool SetHook()
        {
            bool result;
            if (_lowLevel)
                result = KeyboardHook.SetHook_LL(_form.Handle, _hookMessage);
            else
                result = KeyboardHook.SetHook(_form.Handle, _hookMessage);

            if (result)
                _hooked = true;
            return result;
        }

        public bool ClearHook()
        {
            if (_hooked == false)
                return false;

            bool result;
            if (_lowLevel)
                result = ClearHook_LL(_form.Handle);
            else
                result = ClearHook(_form.Handle);

            if (result)
                _hooked = false;
            return result;
        }

    }
}

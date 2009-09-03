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
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;

namespace KeyboardRedirector
{
    public class KeyboardHookLowLevel
    {
        private static KeyboardHookLowLevel _current = null;
        private static object _currentLock = new object();

        public static KeyboardHookLowLevel Current
        {
            get
            {
                lock (_currentLock)
                {
                    if (_current == null)
                        _current = new KeyboardHookLowLevel();
                    return _current;
                }
            }
        }

        private IntPtr _keyboardHookHandle;
        private Keys _modifiers;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        private delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// This field isn't really needed except to maintain a reference to the hook so
        /// that the GC doesn't clean it up while the unmanaged code is still using it.
        /// </summary>
        private HookProc _keyboardDelegate;


        #region Win32 API

        private const int WH_KEYBOARD_LL = 13;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern int UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        #endregion

        private KeyboardHookLowLevel()
        {
        }

        public void SetHook()
        {
            if (_keyboardHookHandle == IntPtr.Zero)
            {
                _modifiers = Keys.None;
                _keyboardDelegate = KeyboardHookProc;
                _keyboardHookHandle = KeyboardHookLowLevel.SetWindowsHookEx(WH_KEYBOARD_LL, _keyboardDelegate, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);

                if (_keyboardHookHandle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        public void ClearHook()
        {
            if (_keyboardHookHandle != IntPtr.Zero)
            {
                int result = KeyboardHookLowLevel.UnhookWindowsHookEx(_keyboardHookHandle);
                _keyboardHookHandle = IntPtr.Zero;
                _keyboardDelegate = null;
                if (result == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        private int KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool handled = false;

            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                Keys key = (Keys)hookStruct.vkCode;

                Keys modifier = Keys.None;
                if ((key == Keys.ShiftKey) || (key == Keys.LShiftKey) || (key == Keys.RShiftKey))
                    modifier = Keys.Shift;
                else if ((key == Keys.ControlKey) || (key == Keys.LControlKey) || (key == Keys.RControlKey))
                    modifier = Keys.Control;
                if ((key == Keys.Menu) || (key == Keys.LMenu) || (key == Keys.RMenu))
                    modifier = Keys.Alt;

                int keyCode = wParam.ToInt32();
                if ((keyCode == (int)Win32.WM.KEYDOWN) || (keyCode == (int)Win32.WM.SYSKEYDOWN))
                {
                    _modifiers |= modifier;

                    // Only trigger the event if this isn't a modifier
                    if ((modifier == Keys.None) && (KeyDown != null))
                    {
                        key |= _modifiers;
                        KeyEventArgs e = new KeyEventArgs(key);
                        KeyDown(this, e);
                        handled |= e.Handled;
                    }
                }

                if ((keyCode == (int)Win32.WM.KEYUP) || (keyCode == (int)Win32.WM.SYSKEYUP))
                {
                    _modifiers &= ~modifier;

                    // Only trigger the event if this isn't a modifier
                    if ((modifier == Keys.None) && (KeyUp != null))
                    {
                        key |= _modifiers;
                        KeyEventArgs e = new KeyEventArgs(key);
                        KeyUp(this, e);
                        handled |= e.Handled;
                    }
                }
            }

            if (handled)
                return -1;

            return CallNextHookEx(_keyboardHookHandle, nCode, wParam, lParam);
        }

    }
}

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
using System.Diagnostics;
using System.IO;
using MS;

namespace KeyboardRedirector
{
    public class KeyboardHookExternal
    {
        private static KeyboardHookExternal _current = null;
        private static object _currentLock = new object();

        public static KeyboardHookExternal Current
        {
            get
            {
                lock (_currentLock)
                {
                    if (_current == null)
                        _current = new KeyboardHookExternal();
                    return _current;
                }
            }
        }

        private IntPtr _hwnd = IntPtr.Zero;
        private uint _hookMessage = 0;
        private uint _hookMessageLowLevel = 0;
        private Process _hookProcess = null;

        private KeyCombination _keyState = new KeyCombination();
        private KeyCombination _keyStateLowLevel = new KeyCombination();

        public uint HookMessage
        {
            get { return _hookMessage; }
        }
        public uint HookMessageLowLevel
        {
            get { return _hookMessageLowLevel; }
        }
        public KeyCombination KeyState
        {
            get { return _keyState; }
        }
        public KeyCombination KeyStateLowLevel
        {
            get { return _keyStateLowLevel; }
        }

        public event KeyHookEventHandler KeyEvent;
        public event KeyHookEventHandler KeyEventLowLevel;

        private KeyboardHookExternal()
        {
        }

        public bool SetHook(IntPtr hwnd, uint message, uint messageLowLevel)
        {
            string exeFolder = Process.GetCurrentProcess().MainModule.FileName;
            exeFolder = Path.GetDirectoryName(exeFolder);
            if (Is64Bit())
            {
                exeFolder += @"\Hook64.exe";
            }
            else
            {
                exeFolder += @"\Hook32.exe";
            }

            string exeName = System.IO.Path.GetFileNameWithoutExtension(exeFolder);
            Process[] running = Process.GetProcessesByName(exeName);
            foreach (Process proc in running)
            {
                if (proc.MainModule.FileName.Equals(exeFolder, StringComparison.CurrentCultureIgnoreCase))
                    proc.Kill();
            }

            string args = hwnd.ToInt32().ToString() + @" " + message.ToString() + @" " + messageLowLevel.ToString();
            _hookProcess = Process.Start(exeFolder, args);
            if (_hookProcess == null)
                return false;

            _hookProcess.WaitForInputIdle();
            if (_hookProcess.HasExited)
                return false;

            _hwnd = hwnd;
            _hookMessage = message;
            _hookMessageLowLevel = messageLowLevel;

            return true;
        }

        public bool ClearHook()
        {
            if (_hookProcess != null)
            {
                if (_hookProcess.HasExited == false)
                    _hookProcess.Kill();
                _hookProcess.Close();
                _hookProcess = null;
            }

            return true;
        }

        public void RestartHooks()
        {
            IntPtr hwnd = _hwnd;
            uint hookMessage = _hookMessage;
            uint hookMessageLowLevel = _hookMessageLowLevel;

            ClearHook();
            SetHook(hwnd, hookMessage, hookMessageLowLevel);
        }

        public static bool Is64Bit()
        {
            if (Marshal.SizeOf(typeof(IntPtr)) == 8)
                return true;

            return false;
        }

        public bool IsHookMessage(Message message)
        {
            if (_hookProcess != null)
            {
                return (message.Msg == _hookMessage);
            }
            return false;
        }

        public int ProcessMessage(Message message)
        {
            bool handled = false;

            if (_hookProcess != null)
            {
                if (message.Msg == _hookMessage)
                {
                    Keys key = (Keys)message.WParam.ToInt32();

                    long lParam = message.LParam.ToInt64();
                    Win32.KeyboardParams parameters = new Win32.KeyboardParams(((int)(lParam & 0xFFFFFFFF)));
                    bool keyDown = (parameters.transitionState == Win32.KeyboardParams.TransitionState.Down);

                    _keyState.KeyPress(keyDown, key, parameters.extendedKey);

                    if (KeyEvent != null)
                    {
                        KeyHookEventArgs e = new KeyHookEventArgs(_keyState);
                        KeyEvent(this, e);
                        handled |= e.Handled;
                    }
                }
                if (message.Msg == _hookMessageLowLevel)
                {
                    Keys key = (Keys)message.WParam.ToInt32();

                    long lParam = message.LParam.ToInt64();
                    Win32.KeyboardParams parameters = new Win32.KeyboardParams(((int)(lParam & 0xFFFFFFFF)));
                    bool keyDown = (parameters.transitionState == Win32.KeyboardParams.TransitionState.Down);

                    _keyStateLowLevel.KeyPress(keyDown, key, parameters.extendedKey);

                    if (KeyEventLowLevel != null)
                    {
                        KeyHookEventArgs e = new KeyHookEventArgs(_keyStateLowLevel);
                        KeyEventLowLevel(this, e);
                        handled |= e.Handled;
                    }
                }

                if (handled)
                    return -1;
            }

            return 0;
        }

        public int KeysDownCount()
        {
            int keysLL = _keyStateLowLevel.KeysDownCount();
            int keys = _keyState.KeysDownCount();
            return (keysLL > keys) ? keysLL : keys;
        }

    }

    public delegate void KeyHookEventHandler(object sender, KeyHookEventArgs e);

    public class KeyHookEventArgs : EventArgs
    {
        private bool _handled;
        private readonly KeyCombination _keyCombination;

        public KeyHookEventArgs(KeyCombination keyCombination)
        {
            _keyCombination = new KeyCombination(keyCombination);
        }

        public bool Handled
        {
            get { return _handled; }
            set { _handled = value; }
        }

        public KeyCombination KeyCombination
        {
            get
            {
                return _keyCombination;
            }
        }

    }

}

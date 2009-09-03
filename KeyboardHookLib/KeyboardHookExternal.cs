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

        private uint _hookMessage = 0;
        private uint _hookMessageLowLevel = 0;
        private Process _hookProcess = null;

        public uint HookMessage
        {
            get { return _hookMessage; }
        }
        public uint HookMessageLowLevel
        {
            get { return _hookMessageLowLevel; }
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

            _hookMessage = message;
            _hookMessageLowLevel = messageLowLevel;

            return true;
        }

        public bool ClearHook()
        {
            if (_hookProcess != null)
            {
                //Win32.SendMessage(_hookProcess.MainWindowHandle, (uint)Win32.WM.CLOSE, IntPtr.Zero, IntPtr.Zero);
                //_hookProcess.WaitForExit();
                if (_hookProcess.HasExited == false)
                    _hookProcess.Kill();
                _hookProcess.Close();
                _hookProcess = null;
            }

            return true;
        }

        public bool Is64Bit()
        {
            if (Marshal.SizeOf(typeof(IntPtr)) == 8)
                return true;

            return false;
        }

        private KeyCombination _keyState = new KeyCombination();
        private KeyCombination _keyStateLowLevel = new KeyCombination();
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

                    _keyState.KeyPress(keyDown, key);

                    if (KeyEvent != null)
                    {
                        KeyHookEventArgs e = new KeyHookEventArgs(_keyState);
                        KeyEvent(this, e);
                        handled |= e.Handled;
                    }
                }
                if (message.Msg == _hookMessageLowLevel)
                {
                    Keys key = (Keys)message.LParam.ToInt32();
                    int msgId = message.WParam.ToInt32();
                    bool keyDown = ((msgId == (int)Win32.WM.KEYDOWN) ||
                                    (msgId == (int)Win32.WM.SYSKEYDOWN));

                    _keyStateLowLevel.KeyPress(keyDown, key);

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

    }

    public class KeyCombination
    {
        private Keys _key = Keys.None;
        private bool _keyDown = false;
        private List<Keys> _modifiers = new List<Keys>();
        private bool _transitionToUp = false;

        public KeyCombination()
        {
        }
        public KeyCombination(KeyCombination source)
        {
            this._key = source.Key;
            this._keyDown = source.KeyDown;
            this._modifiers.AddRange(source.Modifiers);
        }

        public Keys Key
        {
            get { return (_key); }
        }
        public List<Keys> Modifiers
        {
            get { return _modifiers; }
        }
        public bool KeyDown
        {
            get { return _keyDown; }
        }
        public bool TransitionToKeyUp
        {
            get { return _transitionToUp; }
        }

        public bool ModifierKeysOnly
        {
            get
            {
                if (IsModifierKey(_key) == false)
                    return false;
                foreach (Keys key in _modifiers)
                {
                    if (IsModifierKey(key) == false)
                        return false;
                }
                return true;
            }
        }

        private bool IsModifierKey(Keys key)
        {
            if ((key == Keys.ShiftKey) || (key == Keys.LShiftKey) || (key == Keys.RShiftKey))
                return true;
            if ((key == Keys.ControlKey) || (key == Keys.LControlKey) || (key == Keys.RControlKey))
                return true;
            if ((key == Keys.Menu) || (key == Keys.LMenu) || (key == Keys.RMenu))
                return true;
            if ((key == Keys.LWin) || (key == Keys.RWin))
                return true;
            return false;
        }

        public void KeyPress(bool keyDown, Keys key)
        {
            _transitionToUp = (!keyDown && _keyDown);

            // If we're transitioning from keydowns to keyups we'll stick the last key in the modifiers list
            // because the keys aren't always released in the opposite order they were pressed.
            if (_transitionToUp)
            {
                if (_modifiers.Contains(_key) == false)
                    _modifiers.Add(_key);
            }

            if (_modifiers.Contains(key))
                _modifiers.Remove(key);

            if (keyDown)
            {
                if ((_keyDown) && // If the last keystroke was a keydown then we add it to the modifiers
                    (_key != key))
                {
                    if (_modifiers.Contains(_key) == false)
                        _modifiers.Add(_key);
                }
                _key = key;
                _keyDown = true;
            }
            else
            {
                _key = key;
                _keyDown = false;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(_key.ToString());
            if (_modifiers.Count > 0)
            {
                List<string> mods = new List<string>();
                foreach (Keys key in _modifiers)
                {
                    mods.Add(key.ToString());
                }
                result.Append(" + (");
                result.Append(string.Join("+", mods.ToArray()));
                result.Append(")");
            }
            return result.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as KeyCombination);
        }

        public bool Equals(KeyCombination obj)
        {
            if (obj == null)
                return false;

            if (obj._key != _key)
                return false;

            if (obj._keyDown != _keyDown)
                return false;

            if (obj._modifiers.Count != _modifiers.Count)
                return false;

            foreach (Keys key in obj._modifiers)
            {
                if (_modifiers.Contains(key) == false)
                    return false;
            }

            return true;
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

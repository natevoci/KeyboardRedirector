#region Copyright (C) 2010 Nate

/* 
 *	Copyright (C) 2010 Nate
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

#if LOG
#define EXTENDED_LOGGING
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyboardRedirector
{
    class KeyToHookInformation
    {
        public KeysWithExtended Key;
        public bool KeyDown;
        public DateTime SeenAt;

        public KeyToHookInformation(KeysWithExtended key, bool keyDown)
        {
            this.Key = new KeysWithExtended(key);
            this.KeyDown = keyDown;
            this.SeenAt = DateTime.Now;
        }
    }

    class KeysToHookList
    {
        List<KeyToHookInformation> _keys = new List<KeyToHookInformation>();
        System.Threading.AutoResetEvent _changed = new System.Threading.AutoResetEvent(false);
        int _keysAdded = 0;
        int _keysRemovedDueToTimeout = 0;
        int _timeout = 1000;
        bool _testModifiers = true;

        public bool TestModifiers
        {
            get { return _testModifiers; }
            set { _testModifiers = value; }
        }

        public System.Threading.AutoResetEvent ChangedEvent
        {
            get { return _changed; }
        }
        public int KeysAdded
        {
            get { return _keysAdded; }
            set { _keysAdded = value; }
        }
        public int KeysRemovedDueToTimeout
        {
            get { return _keysRemovedDueToTimeout; }
            set { _keysRemovedDueToTimeout = value; }
        }
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }
        public int Count
        {
            get { return _keys.Count; }
        }
        public List<KeyToHookInformation> Keys
        {
            get { return _keys; }
        }

        public void Add(KeysWithExtended key, bool keyDown)
        {
            lock (this)
            {
                _keys.Add(new KeyToHookInformation(key, keyDown));
                _keysAdded++;
#if EXTENDED_LOGGING
                Log.MainLog.WriteDebug("     Added: " + key.ToString() + " " + (keyDown ? "down" : "up"));
#endif
            }
            _changed.Set();
        }
        public bool Remove(KeysWithExtended key, bool keyDown)
        {
            bool found = false;
            lock (this)
            {
                for (int i = 0; i < _keys.Count; i++)
                {
                    KeyToHookInformation info = _keys[i];

                    if (_testModifiers && (info.Key == key) && (info.KeyDown == keyDown))
                    {
#if EXTENDED_LOGGING
                        Log.MainLog.WriteDebug("     Removed: " + info.Key.ToString() + " " + (info.KeyDown ? "down" : "up"));
#endif
                        found = true;
                        _keys.RemoveAt(i--);
                    }
                    else if (!_testModifiers && (info.Key.Keys == key.Keys) && (info.KeyDown == keyDown))
                    {
#if EXTENDED_LOGGING
                        Log.MainLog.WriteDebug("     Removed: " + info.Key.ToString() + " " + (info.KeyDown ? "down" : "up"));
#endif
                        found = true;
                        _keys.RemoveAt(i--);
                    }
                    else if (DateTime.Now.Subtract(_keys[i].SeenAt).TotalMilliseconds > _timeout)
                    {
#if EXTENDED_LOGGING
                        Log.MainLog.WriteDebug("     Removed (timeout): " + info.Key.ToString() + " " + (info.KeyDown ? "down" : "up"));
#endif
                        _keys.RemoveAt(i--);
                        _keysRemovedDueToTimeout++;
                    }

                }
            }
            if (found)
                _changed.Set();
            return found;
        }
        public void Clear()
        {
            lock (this)
            {
                _keys.Clear();
                _keysAdded = 0;
                _keysRemovedDueToTimeout = 0;
            }
        }

        public string GetKeysString()
        {
            List<string> keys = new List<string>();
            foreach (KeyToHookInformation info in this.Keys)
            {
                keys.Add(info.Key.ToString() + " " + (info.KeyDown ? "down" : "up"));
            }
            return string.Join(", ", keys.ToArray());
        }

    }
}

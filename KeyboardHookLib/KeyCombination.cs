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

namespace KeyboardRedirector
{
    public class KeyCombination
    {
        private KeysWithExtended _key = KeysWithExtended.None;
        private bool _keyDown = false;
        private List<KeysWithExtended> _modifiers = new List<KeysWithExtended>();
        private bool _transitionToUp = false;

        private KeysWithExtended _stateKey;
        private List<KeysWithExtended> _stateModifiers = new List<KeysWithExtended>();

        private double _lastChange = 0;

        public KeyCombination()
        {
        }
        public KeyCombination(KeyCombination source)
        {
            this._key = source._key;
            this._keyDown = source._keyDown;
            this._modifiers.AddRange(source._modifiers);
            this._transitionToUp = source._transitionToUp;
            this._lastChange = source._lastChange;
        }

        public KeysWithExtended KeyWithExtended
        {
            get { return _key; }
        }
        public List<KeysWithExtended> Modifiers
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
                foreach (KeysWithExtended key in _modifiers)
                {
                    if (IsModifierKey(key) == false)
                        return false;
                }
                return true;
            }
        }

        private static bool IsModifierKey(KeysWithExtended keyWithExtended)
        {
            Keys key = keyWithExtended.Keys;
            if (keyWithExtended.IsShiftKey)
                return true;
            if (keyWithExtended.IsControlKey)
                return true;
            if (keyWithExtended.IsAltKey)
                return true;
            if ((key == Keys.LWin) || (key == Keys.RWin))
                return true;
            return false;
        }

        public void KeyPress(bool keyDown, Keys key, bool extended)
        {
            double now = Utils.Time.GetTime();
            double timeSinceLastChange = now - _lastChange;
            if (timeSinceLastChange > 1000)
            {
                _key = KeysWithExtended.None;
                _modifiers.Clear();
                _keyDown = false;
            }
            _lastChange = now;

            _transitionToUp = (!keyDown && _keyDown);

            KeysWithExtended keys = new KeysWithExtended(key, extended);

            // If we're transitioning from keydowns to keyups we'll stick the last key in the modifiers list
            // because the keys aren't always released in the opposite order they were pressed.
            if (_transitionToUp)
            {
                if ((ModifiersContains(_key) == false) && (_key != KeysWithExtended.None))
                {
                    _modifiers.Add(_key);
                }
            }

            if (ModifiersContains(keys))
            {
                ModifiersRemove(keys);
            }

            if (keyDown)
            {
                if ((_keyDown) && // If the last keystroke was a keydown then we add it to the modifiers
                    (_key != keys))
                {
                    if ((ModifiersContains(_key) == false) && (_key != KeysWithExtended.None))
                    {
                        _modifiers.Add(_key);
                    }
                }
                _key = keys;
                _keyDown = true;
            }
            else
            {
                _key = keys;
                _keyDown = false;
            }

        }

        public int KeysDownCount()
        {
            double now = Utils.Time.GetTime();
            double timeSinceLastChange = now - _lastChange;
            if (timeSinceLastChange > 1000)
                return 0;
            int count = _modifiers.Count;
            if (_keyDown)
                count++;
            return count;
        }

        private bool ModifiersContains(KeysWithExtended key)
        {
            foreach (KeysWithExtended k in _modifiers)
            {
                if (k == key)
                    return true;
            }
            return false;
        }

        private void ModifiersRemove(KeysWithExtended key)
        {
            for (int i = 0; i < _modifiers.Count; i++)
            {
                KeysWithExtended k = _modifiers[i];
                if (k == key)
                {
                    _modifiers.RemoveAt(i);
                    i--;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(_key.ToString());
            if (_modifiers.Count > 0)
            {
                List<string> mods = new List<string>();
                foreach (KeysWithExtended key in _modifiers)
                {
                    mods.Add(key.ToString());
                }
                result.Append(" + (");
                result.Append(string.Join("+", mods.ToArray()));
                result.Append(")");
            }
            return result.ToString();
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
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

            foreach (KeysWithExtended key in obj._modifiers)
            {
                if (_modifiers.Contains(key) == false)
                    return false;
            }

            return true;
        }
    }

}

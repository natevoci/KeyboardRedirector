using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace KeyboardRedirector
{
    public class KeyCombination
    {
        private Keys _key = Keys.None;
        private bool _keyDown = false;
        private List<Keys> _modifiers = new List<Keys>();
        private bool _transitionToUp = false;

        private Keys _stateKey = Keys.None;
        private List<Keys> _stateModifiers = new List<Keys>();

        public KeyCombination()
        {
        }
        public KeyCombination(KeyCombination source)
        {
            this._key = source.Key;
            this._keyDown = source.KeyDown;
            this._modifiers.AddRange(source.Modifiers);
            this._transitionToUp = source._transitionToUp;
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
                //return ((_key == Keys.None) && (_modifiers.Count > 0));
            }
        }

        private static bool IsModifierKey(Keys key)
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
                if ((_modifiers.Contains(_key) == false) && (_key != Keys.None))
                    _modifiers.Add(_key);
            }

            if (_modifiers.Contains(key))
                _modifiers.Remove(key);

            if (keyDown)
            {
                if ((_keyDown) && // If the last keystroke was a keydown then we add it to the modifiers
                    (_key != key))
                {
                    if ((_modifiers.Contains(_key) == false) && (_key != Keys.None))
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

            //_keyDown = keyDown;
            //if (keyDown == false)
            //{
            //    _key = _stateKey;
            //    _modifiers.Clear();
            //    _modifiers.AddRange(_stateModifiers);
            //}

            //if (IsModifierKey(key))
            //{
            //    if (_stateModifiers.Contains(key))
            //        _stateModifiers.Remove(key);
            //    if (keyDown)
            //        _stateModifiers.Add(key);
            //}
            //else
            //{
            //    if (keyDown)
            //        _stateKey = key;
            //    else
            //        _stateKey = Keys.None;
            //}

            //if (keyDown)
            //{
            //    _key = _stateKey;
            //    _modifiers.Clear();
            //    _modifiers.AddRange(_stateModifiers);
            //}

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

}

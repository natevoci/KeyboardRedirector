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
using System.Xml.Serialization;

namespace KeyboardRedirector
{
    public class Settings
    {
        #region Static
        private static XMLFileStore<Settings> _xmlStore;
        public static void EnsureXmlStoreExists()
        {
            if (_xmlStore == null)
            {
                string filename = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\settings.xml";
                _xmlStore = new XMLFileStore<Settings>(filename);
            }
        }
        public static void Save()
        {
            EnsureXmlStoreExists();
            _xmlStore.Save();
        }
        public static Settings Current
        {
            get
            {
                EnsureXmlStoreExists();
                return _xmlStore.Data;
            }
        }
        #endregion

        public bool MinimizeOnStart = false;
        public List<SettingsKeyboard> Keyboards = new List<SettingsKeyboard>();


        public SettingsKeyboard FindKeyboardByDeviceName(string deviceName)
        {
            foreach (SettingsKeyboard kb in Keyboards)
            {
                if (kb.DeviceName == deviceName)
                    return kb;
            }
            return null;
        }
    }

    public class SettingsKeyboard
    {
        public string Name = "";
        //public uint Handle = 0xFFFFFFFF;
        public string DeviceName = "";

        public bool CaptureAllKeys = false;

        public List<SettingsKeyboardKey> Keys = new List<SettingsKeyboardKey>();

        public SettingsKeyboardKey FindKey(Keys KeyCode)
        {
            foreach (SettingsKeyboardKey key in Keys)
            {
                if (key.Keys == KeyCode)
                    return key;
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            SettingsKeyboard objTyped = obj as SettingsKeyboard;
            return ((objTyped != null) && (DeviceName == objTyped.DeviceName));
        }
        public override int GetHashCode()
        {
            return DeviceName.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class SettingsKeyboardKey
    {
        private uint _keyCode = 0;

        public uint KeyCode
        {
            get { return _keyCode; }
            set { _keyCode = value; }
        }

        [XmlIgnore()]
        public Keys Keys
        {
            get { return (Keys)_keyCode; }
            set { _keyCode = (uint)value; }
        }

        public bool Capture = false;
        public string Name = "";

        public string LaunchApplication = "";

        public SettingsKeyboardKey()
        {
        }

        public SettingsKeyboardKey(Keys KeyCode)
        {
            this.Keys = KeyCode;
            Name = KeyCode.ToString();
        }

        public override bool Equals(object obj)
        {
            SettingsKeyboardKey objTyped = obj as SettingsKeyboardKey;
            return ((objTyped != null) && (Keys == objTyped.Keys));
        }
        public override int GetHashCode()
        {
            return Keys.GetHashCode();
        }
        public override string ToString()
        {
            if (Name.Length == 0)
                return "0x" + KeyCode.ToString("x6");
            else
                return "0x" + KeyCode.ToString("x6") + " - " + Name;
        }
    }

}

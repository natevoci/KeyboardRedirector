using System;
using System.Collections.Generic;
using System.Text;

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

        public SettingsKeyboardKey FindKey(ushort VirtualKeyCode)
        {
            foreach (SettingsKeyboardKey key in Keys)
            {
                if (key.VirtualKeyCode == VirtualKeyCode)
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
        public uint VirtualKeyCode = 0x00;
        public bool Capture = false;
        public string Name = "";

        public SettingsKeyboardKey()
        {
        }

        public SettingsKeyboardKey(uint VirtualKeyCode)
        {
            this.VirtualKeyCode = VirtualKeyCode;
            System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)VirtualKeyCode;
            Name = key.ToString();
        }

        public override bool Equals(object obj)
        {
            SettingsKeyboardKey objTyped = obj as SettingsKeyboardKey;
            return ((objTyped != null) && (VirtualKeyCode == objTyped.VirtualKeyCode));
        }
        public override int GetHashCode()
        {
            return VirtualKeyCode.GetHashCode();
        }
        public override string ToString()
        {
            if (Name.Length == 0)
                return "0x" + VirtualKeyCode.ToString("x2");
            else
                return "0x" + VirtualKeyCode.ToString("x2") + " - " + Name;
        }
    }

}

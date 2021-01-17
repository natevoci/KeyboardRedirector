#region Copyright (C) 2009,2010 Nate

/* 
 *	Copyright (C) 2009,2010 Nate
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
                string filename = SettingsPath + @"settings.xml";
                _xmlStore = new XMLFileStore<Settings>(filename);
            }
        }
        public static void Save()
        {
            EnsureXmlStoreExists();
            _xmlStore.Save();
        }
        public static void RevertToSaved()
        {
            EnsureXmlStoreExists();
            _xmlStore.Reload();
        }
        public static Settings Current
        {
            get
            {
                EnsureXmlStoreExists();
                return _xmlStore.Data;
            }
        }
        public static string SettingsPath
        {
            get
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                return path + @"\KeyboardRedirector\";
            }
        }
        #endregion

        public bool MinimizeOnStart = false;
        public bool LogOn = true;
        public SettingsKeyboardDeviceList KeyboardDevices = new SettingsKeyboardDeviceList();
        public SettingsKeyboardList Keyboards = new SettingsKeyboardList();
        public SettingsKeyboard LowLevelKeyboard = new SettingsKeyboard();
        public SettingsApplicationList Applications = new SettingsApplicationList();

        public Settings()
        {
            LowLevelKeyboard.Name = "Low Level";
            LowLevelKeyboard.KeyboardId = 0;
        }
    }


    public class SettingsApplicationList : List<SettingsApplication>
    {
        public SettingsApplicationList()
        {
        }

        public new void Add(SettingsApplication item)
        {
            if (FindByName(item.Name) != null)
                throw new ArgumentException("An element with the same application name already exists.");
            base.Add(item);
        }

        public SettingsApplication FindByName(string name)
        {
            foreach (SettingsApplication app in this)
            {
                if (string.Equals(app.Name, name, StringComparison.CurrentCultureIgnoreCase))
                    return app;
            }
            return null;
        }
    }
    public class SettingsApplication
    {
        public string Name = "New Application";

        public bool UseWindowTitle = false;
        public string WindowTitle = "";

        public bool UseExecutable = false;
        public string Executable = "";

        public ImageEx ExecutableImage = new ImageEx();

        public override string ToString()
        {
            return Name;
        }
    }

    public class SettingsKeyboardDeviceList : List<SettingsKeyboardDevice>
    {
        public SettingsKeyboardDevice FindByDeviceName(string deviceName)
        {
            foreach (SettingsKeyboardDevice kb in this)
            {
                if (kb.DeviceName == deviceName)
                    return kb;
            }
            return null;
        }
        public List<SettingsKeyboardDevice> FindByKeyboardId(int id)
        {
            List<SettingsKeyboardDevice> result = new List<SettingsKeyboardDevice>();
            foreach (SettingsKeyboardDevice kb in this)
            {
                if (kb.KeyboardId == id)
                    result.Add(kb);
            }
            return result;
        }
        public int MaxId()
        {
            int id = 0;
            foreach (SettingsKeyboardDevice kb in this)
            {
                if (kb.KeyboardId > id)
                    id = kb.KeyboardId;
            }
            return id;
        }
    }
    public class SettingsKeyboardDevice
    {
        public string DeviceName = "";
        public string Name = "";
        public int KeyboardId = 0;
    }



    public class SettingsKeyboardList : List<SettingsKeyboard>
    {
        public SettingsKeyboard FindByKeyboardId(int id)
        {
            foreach (SettingsKeyboard kb in this)
            {
                if (kb.KeyboardId == id)
                    return kb;
            }
            return null;
        }
    }
    public class SettingsKeyboard
    {
        public string Name = "";
        public int KeyboardId = -1;

        public bool CaptureAllKeys = false;

        public SettingsKeyboardKeyList Keys = new SettingsKeyboardKeyList();

        public override bool Equals(object obj)
        {
            SettingsKeyboard objTyped = obj as SettingsKeyboard;
            return ((objTyped != null) && (KeyboardId == objTyped.KeyboardId));
        }
        public override int GetHashCode()
        {
            return KeyboardId.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }

    public class SettingsKeyboardExport
    {
        public SettingsKeyboard Keyboard;
        public SettingsApplicationList Applications = new SettingsApplicationList();
    }

    public class SettingsKeyboardKeyList : List<SettingsKeyboardKey>
    {
        public SettingsKeyboardKey FindKey(KeyCombination keyCombo)
        {
            SettingsKeyboardKey keyComboKey = new SettingsKeyboardKey(keyCombo);
            foreach (SettingsKeyboardKey key in this)
            {
                if (key.Equals(keyComboKey))
                    return key;
            }
            return null;
        }
    }
    public class SettingsKeyboardKey
    {
        private List<uint> _keyCodes = new List<uint>();

        public List<uint> KeyCodes
        {
            get { return _keyCodes; }
        }

        public bool Enabled = true;
        public bool Capture = true;
        public int AntiRepeatTime = 0;
        public string Name = "";

        public SettingsKeyboardKeyFocusedApplicationList FocusedApplications = new SettingsKeyboardKeyFocusedApplicationList();

        public SettingsKeyboardKey()
        {
        }
        public SettingsKeyboardKey(KeyCombination keyCombination)
        {
            foreach (KeysWithExtended key in keyCombination.Modifiers)
            {
                _keyCodes.Add(key.keycode);
            }
            _keyCodes.Add(keyCombination.KeyWithExtended.keycode);


        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SettingsKeyboardKey);
        }
        public bool Equals(SettingsKeyboardKey obj)
        {
            if (obj == null)
                return false;

            if (_keyCodes.Count != obj._keyCodes.Count)
                return false;

            for (int i = 0; i < _keyCodes.Count; i++)
            {
                if (_keyCodes[i] != obj._keyCodes[i])
                    return false;
            }

            return true;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _keyCodes.Count; i++)
            {
                KeysWithExtended code = new KeysWithExtended(_keyCodes[i]);
                if (i > 0)
                    result.Append("+");
                result.Append(code.ToString());
            }
            if (Name.Length != 0)
                result.Append(" - " + Name);
            return result.ToString();
        }
    }



    public class SettingsKeyboardKeyFocusedApplicationList : List<SettingsKeyboardKeyFocusedApplication>
    {
        public SettingsKeyboardKeyFocusedApplication FindByName(string name)
        {
            foreach (SettingsKeyboardKeyFocusedApplication app in this)
            {
                if (string.Equals(app.ApplicationName, name, StringComparison.CurrentCultureIgnoreCase))
                    return app;
            }
            return null;
        }
        public SettingsKeyboardKeyFocusedApplication FindByExecutable(string windowTitle, string executable)
        {
            foreach (SettingsKeyboardKeyFocusedApplication app in this)
            {
                bool use = true;
                bool tested = false;
                if (app.Application.UseExecutable)
                {
                    string settingExe = app.Application.Executable;
                    string focusedExe = executable;
                    if (!settingExe.Contains(@"\"))
                        focusedExe = System.IO.Path.GetFileName(executable);

                    use &= (string.Equals(settingExe, focusedExe, StringComparison.CurrentCultureIgnoreCase));
                    tested = true;
                }
                if (app.Application.UseWindowTitle)
                {
                    use &= (string.Equals(app.Application.WindowTitle, windowTitle, StringComparison.CurrentCultureIgnoreCase));
                    tested = true;
                }

                if (use && tested)
                    return app;
            }
            return null;
        }
    }
    public class SettingsKeyboardKeyFocusedApplication
    {
        public string ApplicationName = "";

        public SettingsKeyboardKeyActionList Actions = new SettingsKeyboardKeyActionList();

        [XmlIgnore()]
        public SettingsApplication Application
        {
            get { return Settings.Current.Applications.FindByName(ApplicationName); }
            set { ApplicationName = value.Name; }
        }
    }



    public class SettingsKeyboardKeyActionList : List<SettingsKeyboardKeyAction>
    {
    }
    public class SettingsKeyboardKeyAction
    {
        //private Dictionary<SettingsKeyboardKeyActionType, SettingsKeyboardKeyTypedAction> _actionTypes = new Dictionary<SettingsKeyboardKeyActionType, SettingsKeyboardKeyTypedAction>();

        public SettingsKeyboardKeyActionType ActionType = SettingsKeyboardKeyActionType.Keyboard;
        public SettingsKeyboardKeyTypedActionLaunchApplication LaunchApplication = new SettingsKeyboardKeyTypedActionLaunchApplication();
        public SettingsKeyboardKeyTypedActionKeyboard Keyboard = new SettingsKeyboardKeyTypedActionKeyboard();
        public SettingsKeyboardKeyTypedActionWindowMessage WindowMessage = new SettingsKeyboardKeyTypedActionWindowMessage();

        public SettingsKeyboardKeyAction()
        {
            //_actionTypes.Add(SettingsKeyboardKeyActionType.LaunchApplication, LaunchApplication);
            //_actionTypes.Add(SettingsKeyboardKeyActionType.Keyboard, Keyboard);
        }

        public SettingsKeyboardKeyTypedAction CurrentActionType
        {
            get { return GetActionType(ActionType); }
        }

        public SettingsKeyboardKeyTypedAction GetActionType(SettingsKeyboardKeyActionType actionType)
        {
            if (actionType == SettingsKeyboardKeyActionType.LaunchApplication)
                return LaunchApplication;
            if (ActionType == SettingsKeyboardKeyActionType.Keyboard)
                return Keyboard;
            if (ActionType == SettingsKeyboardKeyActionType.WindowMessage)
                return WindowMessage;
            return null;
        }

    }
    public enum SettingsKeyboardKeyActionType
    {
        LaunchApplication,
        Keyboard,
        WindowMessage
    }
    public class SettingsKeyboardKeyTypedAction
    {
        public virtual string GetName()
        {
            return "";
        }
        public virtual string GetDetails()
        {
            return "";
        }
        public override string ToString()
        {
            return GetName() + " - " + GetDetails();
        }
    }
    public class SettingsKeyboardKeyTypedActionLaunchApplication : SettingsKeyboardKeyTypedAction
    {
        public string Command = "";
        public bool WaitForInputIdle = false;
        public bool WaitForExit = false;

        public override string GetName()
        {
            return "Launch Application";
        }
        public override string GetDetails()
        {
            return Command;
        }
    }
    public class SettingsKeyboardKeyTypedActionKeyboard : SettingsKeyboardKeyTypedAction
    {
        public bool Control = false;
        public bool Shift = false;
        public bool Alt = false;
        public bool LWin = false;
        public bool RWin = false;
        public ushort VirtualKeyCode = 0;
        public bool Extended = false;
        public int RepeatCount = 1;

        [XmlIgnore()]
        public Keys VirtualKey
        {
            get { return (Keys)VirtualKeyCode; }
            set { VirtualKeyCode = (ushort)value; }
        }

        public override string GetName()
        {
            return "Keyboard";
        }
        public override string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            if (Control)
                sb.Append("Control + ");
            if (Shift)
                sb.Append("Shift + ");
            if (Alt)
                sb.Append("Alt + ");
            if (LWin)
                sb.Append("LWin + ");
            if (RWin)
                sb.Append("RWin + ");
            KeysWithExtended keys = new KeysWithExtended(VirtualKey, Extended);
            sb.Append(keys.ToString());
            if (RepeatCount > 1)
                sb.Append("  x" + RepeatCount.ToString());
            return sb.ToString();
        }

    }
    public class SettingsKeyboardKeyTypedActionWindowMessage : SettingsKeyboardKeyTypedAction
    {
        public string ProcessName = "";
        public string WindowClass = "";
        public string WindowName = "";
        public SettingsKeyboardKeyActionType NotFoundAction = SettingsKeyboardKeyActionType.WindowMessage;
        public uint Message = 0;
        public uint WParam = 0;
        public uint LParam = 0;

        public override string GetName()
        {
            return "Window Message";
        }
        public override string GetDetails()
        {
            return WindowName + " msg=0x" + Message.ToString("x") + " wparam=0x" + WParam.ToString("x") + " lparam=0x" + LParam.ToString("x");
        }
    }

}

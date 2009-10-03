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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace KeyboardRedirector
{
    public class NiceKeyName
    {
        public static List<Keys> KeyList = new List<Keys>();
        public static List<string> NameList = new List<string>();

        private static Dictionary<string, Keys> _keyMappingKeys = new Dictionary<string, Keys>();
        private static Dictionary<Keys, string> _keyMappingNames = new Dictionary<Keys, string>();

        static NiceKeyName()
        {
            Regex numberRegex = new Regex(@"^D\d$", RegexOptions.IgnoreCase);

            string[] names = Enum.GetNames(typeof(Keys));
            Array values = Enum.GetValues(typeof(Keys));
            foreach (Keys key in values)
            {
                if (key >= Keys.KeyCode)
                    continue;

                if (_keyMappingNames.ContainsKey(key) == false)
                {
                    string keyName = key.ToString();
                    string niceName = keyName;

                    if (numberRegex.IsMatch(keyName))
                        niceName = keyName.Substring(1);

                    if (key == Keys.ShiftKey)
                        niceName = "Shift";
                    if (key == Keys.LShiftKey)
                        niceName = "LShift";
                    if (key == Keys.RShiftKey)
                        niceName = "RShift";
                    if (key == Keys.ControlKey)
                        niceName = "Control";
                    if (key == Keys.LControlKey)
                        niceName = "LControl";
                    if (key == Keys.RControlKey)
                        niceName = "RControl";
                    if (key == Keys.Menu)
                        niceName = "Alt";
                    if (key == Keys.LMenu)
                        niceName = "LAlt";
                    if (key == Keys.RMenu)
                        niceName = "RAlt";
                    if (key == Keys.CapsLock)
                        niceName = "CapsLock";

                    if (key == Keys.Back)
                        niceName = "Backspace";
                    if (key == Keys.Enter)
                        niceName = "Enter";
                    if (key == Keys.PageUp)
                        niceName = "PageUp";
                    if (key == Keys.PageDown)
                        niceName = "PageDown";
                    if (key == Keys.PrintScreen)
                        niceName = "PrintScreen";

                    if (key == Keys.OemSemicolon)
                        niceName = "OemSemicolon";
                    if (key == Keys.OemQuestion)
                        niceName = "OemQuestion";
                    if (key == Keys.Oemtilde)
                        niceName = "Oemtilde";
                    if (key == Keys.OemOpenBrackets)
                        niceName = "OemOpenBrackets";
                    if (key == Keys.OemPipe)
                        niceName = "OemPipe";
                    if (key == Keys.OemCloseBrackets)
                        niceName = "OemCloseBrackets";
                    if (key == Keys.OemQuotes)
                        niceName = "OemQuotes";
                    if (key == Keys.OemBackslash)
                        niceName = "OemBackslash";

                    KeyList.Add(key);
                    NameList.Add(niceName);

                    _keyMappingKeys.Add(niceName, key);
                    _keyMappingNames.Add(key, niceName);
                }
            }
        }

        public static Keys GetKey(string name)
        {
            return _keyMappingKeys[name];
        }

        public static string GetName(Keys key)
        {
            if (_keyMappingNames.ContainsKey(key) == false)
                return key.ToString();
            return _keyMappingNames[key];
        }

    }
}

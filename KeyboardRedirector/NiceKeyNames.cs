using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace KeyboardRedirector
{
    class NiceKeyName
    {
        public static List<Keys> KeyList = new List<Keys>();
        public static List<string> NameList = new List<string>();

        private static Dictionary<string, Keys> _keyMappingKeys = new Dictionary<string, Keys>();
        private static Dictionary<Keys, string> _keyMappingNames = new Dictionary<Keys, string>();

        static NiceKeyName()
        {
            Regex numberRegex = new Regex(@"D\d", RegexOptions.IgnoreCase);

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
                    {
                        niceName = keyName.Substring(1);
                    }

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
            return _keyMappingNames[key];
        }

    }
}

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
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;

namespace KeyboardRedirector
{
    public class XMLFileStore<T> where T : class, new()
    {
        private T _data;
        private string _xmlFilename;
        private DateTime _lastModified;
        private object _lock;

        public XMLFileStore(string xmlFilename)
        {
            _data = null;
            _xmlFilename = xmlFilename;
            _lastModified = DateTime.MinValue;
            _lock = new object();
        }

        public T Data
        {
            get
            {
                Load();
                return _data;
            }
        }

        private void Load()
        {
            lock (_lock)
            {
                DateTime lastModified = DateTime.MinValue;
                if (File.Exists(_xmlFilename))
                {
                    lastModified = File.GetLastWriteTimeUtc(_xmlFilename);
                }

                if ((_data == null) || (lastModified > _lastModified))
                {
                    if (File.Exists(_xmlFilename))
                    {
                        _lastModified = File.GetLastWriteTimeUtc(_xmlFilename);
                        using (Stream inStream = File.Open(_xmlFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
                            _data = x.Deserialize(inStream) as T;
                        }
                    }
                    else
                    {
                        _data = new T();
                    }
                }
            }
        }

        public void Save()
        {
            lock (_lock)
            {
                if (_data != null)
                {
                    FileInfo fi = new FileInfo(_xmlFilename);
                    if (fi.Directory.Exists == false)
                        fi.Directory.Create();

                    using (Stream outStream = File.Open(_xmlFilename, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
                        x.Serialize(outStream, _data);
                    }
                    _lastModified = File.GetLastWriteTimeUtc(_xmlFilename);
                }
            }
        }

    }
}

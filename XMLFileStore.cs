using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;

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

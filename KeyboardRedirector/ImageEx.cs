using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace KeyboardRedirector
{
    public class ImageEx
    {
        public byte[] ImageBits = null;

        [XmlIgnore()]
        public Image Image
        {
            get
            {
                if (ImageBits == null)
                    return null;
                MemoryStream ms = new MemoryStream(ImageBits);
                return Image.FromStream(ms);
            }
            set
            {
                if (value == null)
                {
                    ImageBits = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream();
                    value.Save(ms, ImageFormat.Png);
                    ImageBits = ms.ToArray();
                }
            }
        }
    }
}

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
using System.Drawing;

namespace IconExtractor
{
    public class ExecutableImageList
    {
        private ImageList _imageList = null;
        private int _nonExistantImage = 1;
        private IconExtractor _iconExtractor;
        private bool _preferLarge;

        private Dictionary<string, int> _executables = new Dictionary<string, int>();

        public int NonExistantImage
        {
            get { return _nonExistantImage; }
            set { _nonExistantImage = value; }
        }

        public ImageList ImageList
        {
            get { return _imageList; }
        }

        public ExecutableImageList(ImageList imageList)
        {
            _imageList = imageList;
            _iconExtractor = new IconExtractor();
            _preferLarge = false;
        }

        public ExecutableImageList(ImageList imageList, bool preferLarge)
        {
            _imageList = imageList;
            _iconExtractor = new IconExtractor();
            _preferLarge = preferLarge;
        }

        public int AddImage(string executable, Image image)
        {
            executable = StripQuotes(executable);
            string exe = GetExecutableKey(executable);

            int index = ImageList.Images.Count;
            if (_executables.ContainsKey(exe))
            {
                index = _executables[exe];
                ImageList.Images[index] = image;
            }
            else
            {
                ImageList.Images.Add(image);
                _executables.Add(exe, index);
            }
            return index;
        }

        public int GetExecutableIndex(string executable)
        {
            executable = StripQuotes(executable);
            string exe = GetExecutableKey(executable);

            if (_executables.ContainsKey(exe))
            {
                return _executables[exe];
            }
            else
            {
                Size size = new Size(16, 16);
                IconExtractor.LoadIconFlags flags = IconExtractor.LoadIconFlags.SmallIcon;
                if (_preferLarge)
                {
                    size = new Size(32, 32);
                    flags = IconExtractor.LoadIconFlags.LargeIcon;
                }

                Bitmap bm = _iconExtractor.LoadThumbnailFromImageFactory(executable, size, false);
                if (bm == null)
                {
                    Icon exeIcon = _iconExtractor.LoadIcon(executable, flags);
                    if (exeIcon == null)
                        return NonExistantImage;

                    bm = exeIcon.ToBitmap();
                }
                return AddImage(executable, bm);

            }
        }

        private string StripQuotes(string executable)
        {
            if ((executable.Length >= 2) && executable.StartsWith("\"") && executable.EndsWith("\""))
                executable = executable.Substring(1, executable.Length - 2);
            return executable;
        }

        private string GetExecutableKey(string executable)
        {
            return StripQuotes(executable).ToLower();
        }
    }
}

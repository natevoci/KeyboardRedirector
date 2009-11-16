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

namespace ApplicationLauncher
{
    class ExecutableImageList
    {
        private ImageList _imageList = null;
        private int _nonExistantImage = 1;
        private IconExtractor.IconExtractor _iconExtractor;

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
            _iconExtractor = new IconExtractor.IconExtractor();
        }

        public int GetExecutableIndex(string executable)
        {
            executable = executable.ToLower();
            if (_executables.ContainsKey(executable))
            {
                return _executables[executable];
            }
            else
            {
                int index = ImageList.Images.Count;

                Bitmap bm = _iconExtractor.LoadThumbnailFromImageFactory(executable, new Size(32, 32), false);
                if (bm == null)
                {
                    //Icon exeIcon = Icon.ExtractAssociatedIcon(executable);
                    Icon exeIcon = _iconExtractor.LoadIcon(executable, IconExtractor.IconExtractor.LoadIconFlags.LargeIcon);
                    if (exeIcon == null)
                        return NonExistantImage;

                    ImageList.Images.Add(exeIcon);
                }
                else
                {
                    ImageList.Images.Add(bm);
                }
                _executables.Add(executable, index);
                return index;

            }

        }
    }
}

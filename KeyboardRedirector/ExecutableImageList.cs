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

namespace KeyboardRedirector
{
    class ExecutableImageList
    {
        private ImageList _imageList = null;
        private Dictionary<string, int> _executables = new Dictionary<string, int>();

        public ImageList ImageList
        {
            get { return _imageList; }
        }

        public ExecutableImageList(ImageList imageList)
        {
            _imageList = imageList;
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
                if (System.IO.File.Exists(executable))
                {
                    Icon exeIcon = Icon.ExtractAssociatedIcon(executable);
                    int index = ImageList.Images.Count;
                    ImageList.Images.Add(exeIcon);
                    _executables.Add(executable, index);
                    return index;
                }
                else
                {
                    return 1;
                }

            }

        }
    }
}

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

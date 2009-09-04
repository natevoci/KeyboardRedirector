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
using System.Runtime.InteropServices;

namespace KeyboardRedirector
{
    class RichTextBoxEx : RichTextBox
    {
        const int SB_VERT = 1;
        const int EM_SETSCROLLPOS = 0x0400 + 222;

        [DllImport("user32", CharSet = CharSet.Auto)]
        static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

        [DllImport("user32", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, POINT lParam);

        [StructLayout(LayoutKind.Sequential)]
        class POINT
        {
            public int x;
            public int y;

            public POINT() { }

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (SelectionStart == Text.Length)
            {
                ScrollToEnd();
            }

            base.OnTextChanged(e);
        }

        public void ScrollToEnd()
        {
            int min, max;
            GetScrollRange(Handle, SB_VERT, out min, out max);
            SendMessage(Handle, EM_SETSCROLLPOS, 0, new POINT(0, max - Height));
        }
    }
}

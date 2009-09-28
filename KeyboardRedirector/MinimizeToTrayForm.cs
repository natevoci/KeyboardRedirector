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

namespace KeyboardRedirector
{
    public class MinimizeToTrayForm : Form
    {
        private FormWindowState _previousWindowState = FormWindowState.Normal;
        private NotifyIcon _notifyIcon;
        private bool _loaded = false;

        public NotifyIcon NotifyIcon
        {
            get { return _notifyIcon; }
            set
            {
                if (_notifyIcon != null)
                    _notifyIcon.MouseDoubleClick -= new MouseEventHandler(NotifyIcon_MouseDoubleClick);
                _notifyIcon = value;
                if (_notifyIcon != null)
                    _notifyIcon.MouseDoubleClick += new MouseEventHandler(NotifyIcon_MouseDoubleClick);
            }
        }

        public MinimizeToTrayForm()
        {
            string exeFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(exeFilename);

            _previousWindowState = FormWindowState.Normal;
            NotifyIcon = new NotifyIcon();
            NotifyIcon.Icon = this.Icon;
        }

        protected override void OnLoad(EventArgs e)
        {
            _loaded = true;
            NotifyIcon.BalloonTipTitle = this.Text;
            NotifyIcon.BalloonTipText = this.Text;
            base.OnLoad(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            NotifyIcon.Dispose();
            NotifyIcon = null;
            base.OnClosed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (_loaded == true)
            {
                if (this.WindowState == FormWindowState.Minimized)
                    SendToTray();
                else
                {
                    _previousWindowState = this.WindowState;
                    RestoreFromTray();
                }
            }

            base.OnResize(e);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestoreFromTray();
        }




        protected void SendToTray()
        {
            if (_notifyIcon == null)
                return;

            if (this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
            this._notifyIcon.Visible = true;
            this.Hide();
            // Do not use ShowInTaskbar = false. It kills the WM_INPUT messages.
            //this.ShowInTaskbar = false;
        }

        protected void RestoreFromTray()
        {
            if (_notifyIcon == null)
                return;

            //this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = _previousWindowState;
            this._notifyIcon.Visible = false;
        }

    }
}

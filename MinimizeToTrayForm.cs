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
            if (this.WindowState == FormWindowState.Minimized)
                SendToTray();
            else
                _previousWindowState = this.WindowState;

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
        }

        protected void RestoreFromTray()
        {
            if (_notifyIcon == null)
                return;

            this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = _previousWindowState;
            this._notifyIcon.Visible = false;
        }

    }
}

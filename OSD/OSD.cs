using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OSD
{
    class OSD : MrSmarty.CodeProject.FloatingOSDWindow
    {
        private Size _padding = new Size(20, 10);

        public static OSD ShowOSD(string text)
        {
            var osd = new OSD();
            var pt = new Point(Screen.PrimaryScreen.Bounds.Width/2, 0);
            var font = new Font(FontFamily.GenericSansSerif, 20.0f);
            osd.Show(pt, (byte)255, Color.White, font, 3000, MrSmarty.CodeProject.FloatingWindow.AnimateMode.RollTopToBottom, 100, text);
            return osd;
        }

        public override void PerformSizing()
        {
            base.Size = new Size(base.Size.Width + (2*_padding.Width), base.Size.Height + (2*_padding.Height));
            base.Location = new Point(base.Location.X - (base.Size.Width / 2), base.Location.Y);
        }

        protected override void PerformPaint(PaintEventArgs e)
        {
            if (base.Handle == IntPtr.Zero)
                return;
            Graphics g = e.Graphics;

            using (LinearGradientBrush b = new LinearGradientBrush(this.Bound, Color.FromArgb(160, 30, 30, 30), Color.FromArgb(30, 0, 0, 0), LinearGradientMode.Vertical))
                e.Graphics.FillRectangle(b, this.Bound);

            if (this._gp != null)
                this._gp.Dispose();
            this._gp = new GraphicsPath();
            var layout = new Rectangle(base.Bound.X + _padding.Width, base.Bound.Y + _padding.Height, base.Bound.Width, base.Bound.Height);
            this._gp.AddString(this._text, this._textFont.FontFamily, (int)this._textFont.Style, g.DpiY * this._textFont.SizeInPoints / 72, layout, this._stringFormat);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillPath(this._brush, this._gp);
        }

    }
}

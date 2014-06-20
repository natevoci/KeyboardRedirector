using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace VolumeChanger
{
    //public partial class VolumeOSDForm : ModelessDialog
    public class VolumeOSDForm : FloatingWindow
    {
        private Timer _timer;

        private double _percentage = 45.0;
        private bool _mute = false;

        public double Percentage
        {
            get { return _percentage; }
            set
            {
                if (value < 0.0)
                    value = 0.0;
                if (value > 100.0)
                    value = 100.0;
                if (_percentage != value)
                {
                    _percentage = value;
                    this.Invalidate();
                }
            }
        }

        public bool Mute
        {
            get { return _mute; }
            set
            {
                if (_mute != value)
                {
                    _mute = value;
                    this.Invalidate();
                }
            }
        }

        public VolumeOSDForm()
        {
        }

        public override void Show()
        {
            Show(0);
        }

        public void Show(int timeout)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            if (Visible == false)
            {
                Point startPosition = new Point(Screen.PrimaryScreen.WorkingArea.Right, Screen.PrimaryScreen.WorkingArea.Bottom);
                startPosition.Offset(-320, -60);
                this.Location = startPosition;
                this.Size = new System.Drawing.Size(292, 24);

                base.ShowAnimate(AnimateMode.Blend, 10);
            }

            if (timeout > 0)
            {
                _timer = new Timer();
                _timer.Tick += new EventHandler(_timer_Tick);
                _timer.Interval = timeout;
                _timer.Start();
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
            HideAnimate(AnimateMode.Blend, 40);
        }

        protected override void PerformPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush b = new LinearGradientBrush(this.Bound, Color.FromArgb(80, 80, 80), Color.Black, 90f))
            {
                e.Graphics.FillRectangle(b, this.Bound);
            }

            Rectangle barBound = new Rectangle(Bound.Left + 70, Bound.Top + 2, Bound.Width - 70 - 50, Bound.Height - 2 - 2);
            using (LinearGradientBrush b = new LinearGradientBrush(barBound, Color.Black, Color.DarkGray, 90f))
            {
                e.Graphics.FillRectangle(b, barBound);
            }

            barBound.Inflate(-2, -2);
            int position = (int)(barBound.Width * (_percentage / 100.0));
            if (position > 0)
            {
                Rectangle volBound = new Rectangle(barBound.Left, barBound.Top, position, barBound.Height);
                if (_mute)
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(volBound, Color.FromArgb(110, 0, 0), Color.FromArgb(255, 0, 90), 90f))
                    {
                        e.Graphics.FillRectangle(b, volBound);
                    }
                }
                else
                {
                    using (LinearGradientBrush b = new LinearGradientBrush(volBound, Color.FromArgb(0, 110, 0), Color.FromArgb(90, 255, 0), 90f))
                    {
                        e.Graphics.FillRectangle(b, volBound);
                    }
                }
            }

            //Font font = new Font(FontFamily.GenericSansSerif, 12f, FontStyle.Regular);
            Font font = new Font("Verdana", 12f, FontStyle.Bold, GraphicsUnit.Pixel);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.None;

            string text = "Volume";
            e.Graphics.DrawString(text, font, Brushes.White, new PointF(2f, Bound.Height/2f), format);

            format.Alignment = StringAlignment.Far;
            text = _percentage.ToString("0") + "%";
            e.Graphics.DrawString(text, font, Brushes.White, new PointF(this.Bound.Right - 2f, Bound.Height / 2f), format);
        }

    }
}
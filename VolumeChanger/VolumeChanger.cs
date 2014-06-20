using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VolumeChanger
{
    public partial class VolumeChanger : Form
    {
        public const long VOLUME_MAX = 65535;

        private FormWindowState _previousWindowState = FormWindowState.Normal;

        private PercentageConverter _perc;
        private AudioMixer _mixer;

        private double _currentPercent = 0.0;
        private long _currentVolume = 0;
        private bool _currentMute = false;

        private double _shapeValueMinimum;
        private double _shapeValueMaximum;

        private VolumeOSDForm _osd;

        public VolumeChanger()
        {
            _perc = new PercentageConverter(VOLUME_MAX);

            if (Environment.OSVersion.Version.Major >= 6)
                _mixer = new AudioMixerVista();
            else
                _mixer = new AudioMixerXP((int)this.Handle);

            InitializeComponent();
            SetShapeValues(AppSettings.Default.Shape, AppSettings.Default.ShapeValue);

            _previousWindowState = FormWindowState.Normal;
            if (AppSettings.Default.Minimize)
            {
                //Show();
                SendToTray();
                //this.Visible = false;
                this.ShowInTaskbar = false;
            }

            _osd = new VolumeOSDForm();
            //_osd.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RestartMixerMonitor();

            //ShowVolume();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 10000)
            {
                VolumeAdjust(m.WParam.ToInt32() * -1.0);
            }
            if (m.Msg == 10001)
            {
                VolumeAdjust(m.WParam.ToInt32());
            }

            base.WndProc(ref m);

            AudioMixerXP mixerXP = _mixer as AudioMixerXP;
            if (mixerXP != null)
            {
                mixerXP.WndProc(m);
            }
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            VolumeDown();
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            VolumeUp();
        }

        private delegate void ShowVolumeDelegate();
        private void ShowVolume()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ShowVolumeDelegate(ShowVolume));
                return;
            }

            _currentVolume = _mixer.GetVolume();
            _currentMute = _mixer.GetMute();
            _currentPercent = _perc.VolumeToPercent(_currentVolume);

            //richTextBox1.AppendText(_currentVolume.ToString() + "  " + _currentPercent.ToString("0") + "%" + Environment.NewLine);
            //richTextBox1.ScrollToCaret();
            labelStatus.Text = _currentVolume.ToString() + "  " + _currentPercent.ToString("0") + "%";
            trackBar1.Value = (int)_currentPercent;

            _osd.Percentage = _currentPercent;
            _osd.Mute = _currentMute;
            _osd.Show(2000);
        }

        private void VolumeUp()
        {
            VolumeAdjust(2.0);
        }

        private void VolumeDown()
        {
            VolumeAdjust(-2.0);
        }

        private void VolumeAdjust(double offsetPercent)
        {
            try
            {
                long vol = _mixer.GetVolume();
                double percent = _perc.VolumeToPercent(vol);

                double width = Math.Ceiling(Math.Abs(offsetPercent));
                double value = Math.Round(percent / width);
                if (offsetPercent > 0)
                    value++;
                else
                    value--;
                percent = value * width;

                VolumeSetPercent(percent);
            }
            catch
            {
            }
        }

        private void VolumeSetPercent(double percent)
        {
            try
            {
                long vol = _perc.PercentToVolume(percent);

                if (vol < 0)
                    vol = 0;
                if (vol > VOLUME_MAX)
                    vol = VOLUME_MAX;
                _mixer.SetVolume((int)vol);
                ShowVolume();
            }
            catch
            {
            }
        }



        private void VolumeChanger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None)
            {
                if (e.KeyCode == Keys.U)
                {
                    VolumeUp();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.D)
                {
                    VolumeDown();
                    e.Handled = true;
                }
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestoreFromTray();
        }

        private void VolumeChanger_Move(object sender, EventArgs e)
        {
            //UpdateTrayIcon();
        }

        private void VolumeChanger_Resize(object sender, EventArgs e)
        {
            UpdateTrayIcon();
        }

        private void UpdateTrayIcon()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                SendToTray();
            }
            else
            {
                _previousWindowState = this.WindowState;
            }
        }

        private void SendToTray()
        {
            if (this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
            this.notifyIcon1.Visible = true;
            this.Hide();

            RestartMixerMonitor();
        }

        private void RestoreFromTray()
        {
            this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = _previousWindowState;
            this.notifyIcon1.Visible = false;

            RestartMixerMonitor();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreFromTray();
        }

        private void volumeUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolumeUp();
        }

        private void volumeDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolumeDown();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_currentPercent != trackBar1.Value)
                VolumeSetPercent((double)trackBar1.Value);
        }

        private void SetShapeValues(string shape, double value)
        {
            if (comboBoxShape.Items.Contains(shape))
                comboBoxShape.SelectedItem = AppSettings.Default.Shape;
            else
                comboBoxShape.SelectedIndex = 0;

            if (value < _shapeValueMinimum)
                value = _shapeValueMinimum;
            if (value > _shapeValueMaximum)
                value = _shapeValueMaximum;

            comboBoxShapeValue.SelectedItem = value;
        }

        private void comboBoxShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            string shape = comboBoxShape.SelectedItem as string;

            if (shape == "Power")
            {
                comboBoxShapeValue.Items.Clear();
                comboBoxShapeValue.Items.Add(1.5);
                comboBoxShapeValue.Items.Add(2.0);
                comboBoxShapeValue.Items.Add(3.0);
                comboBoxShapeValue.SelectedItem = 1.5;
                _shapeValueMinimum = 1.5;
                _shapeValueMaximum = 3.0;
            }
            else if (shape == "Exponential")
            {
                comboBoxShapeValue.Items.Clear();
                comboBoxShapeValue.Items.Add(2.0);
                comboBoxShapeValue.Items.Add(4.0);
                comboBoxShapeValue.Items.Add(10.0);
                comboBoxShapeValue.Items.Add(25.0);
                comboBoxShapeValue.Items.Add(100.0);
                comboBoxShapeValue.Items.Add(1000.0);
                comboBoxShapeValue.SelectedItem = 4.0;
                _shapeValueMinimum = 2;
                _shapeValueMaximum = 1000;
            }
            else
            {
                comboBoxShapeValue.Items.Clear();
                comboBoxShapeValue.Items.Add(1.0);
                comboBoxShapeValue.SelectedItem = 1.0;
                _shapeValueMinimum = 1;
                _shapeValueMaximum = 1;
            }
        }

        private void comboBoxShapeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string shape = comboBoxShape.SelectedItem as string;
            double value = (double)comboBoxShapeValue.SelectedItem;

            if (shape == "Power")
            {
                _perc.SetPower(value);
            }
            else if (shape == "Exponential")
            {
                _perc.SetExponential(value);
            }
            else
            {
                _perc.SetLinear();
            }
        }

        private void RestartMixerMonitor()
        {
            _mixer.VolumeChanged += new AudioMixer.VolumeChangedHandler(_mixer_VolumeChanged);
        }

        private void VolumeChanger_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mixer.VolumeChanged -= new AudioMixer.VolumeChangedHandler(_mixer_VolumeChanged);
        }

        void _mixer_VolumeChanged()
        {
            ShowVolume();
        }


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeyTester
{
    public partial class KeyTesterForm : Form
    {
        public KeyTesterForm()
        {
            InitializeComponent();
        }

        private void KeyTesterForm_KeyDown(object sender, KeyEventArgs e)
        {
            richTextBoxEvents.AppendText("KeyDown\t" + e.KeyCode.ToString() + Environment.NewLine);
            e.Handled = true;
        }

        private void KeyTesterForm_KeyUp(object sender, KeyEventArgs e)
        {
            richTextBoxEvents.AppendText("KeyUp\t" + e.KeyCode.ToString() + Environment.NewLine);
            e.Handled = true;
        }

    }
}

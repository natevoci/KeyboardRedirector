using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApplicationLauncher
{
    public partial class ButtonListControl : UserControl
    {
        public class ButtonListControlItem
        {
            public string Text;
            public Image Icon;
            public object Tag;
        }

        private const int _buttonHeight = 34;
        private const int _buttonGap = 6;


        public delegate void ItemActivateEventHandler(object sender, ButtonListControlItem item);
        public event ItemActivateEventHandler ItemActivate;

        public ButtonListControl()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.Controls.Clear();
        }

        public void AddButton(string text, Image image, string keyText, object tag)
        {
            CSharpControls.VistaButton button = new CSharpControls.VistaButton();
            button.AllowDefaultButtonBorder = true;
            button.BackColor = System.Drawing.Color.DarkGray;
            button.ButtonText = text;
            button.DialogResult = System.Windows.Forms.DialogResult.None;
            button.GlowColor = Color.FromArgb(80, 80, 255);
            button.KeyText = keyText;
            button.Location = new System.Drawing.Point(0, (_buttonHeight + _buttonGap) * (this.Controls.Count));
            button.Name = "button" + (this.Controls.Count + 1).ToString();
            button.Size = new System.Drawing.Size(this.ClientSize.Width, _buttonHeight);
            button.TabIndex = this.Controls.Count;

            ButtonListControlItem item = new ButtonListControlItem();
            item.Text = text;
            item.Icon = image;
            item.Tag = tag;
            button.Tag = item;

            button.Click += new EventHandler(button_Click);

            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Image = image;

            this.Controls.Add(button);
        }

        public void SelectObject(object obj)
        {
            foreach (Control c in this.Controls)
            {
                CSharpControls.VistaButton button = c as CSharpControls.VistaButton;
                if (button.Tag == obj)
                    c.Focus();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                c.Size = new System.Drawing.Size(this.ClientSize.Width, _buttonHeight);
            }
            base.OnResize(e);
        }

        void button_Click(object sender, EventArgs e)
        {
            if (ItemActivate != null)
            {
                CSharpControls.VistaButton button = sender as CSharpControls.VistaButton;
                ItemActivate(this, button.Tag as ButtonListControlItem);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Left)
                return false;
            if (keyData == Keys.Right)
                return false;
            return base.ProcessDialogKey(keyData);
        }

    }
}

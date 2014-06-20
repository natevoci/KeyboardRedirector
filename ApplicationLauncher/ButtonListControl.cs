using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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

        private int _buttonCount = 0;

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
            _buttonCount = 0;
        }

        public void AddButton(string text, Image image, string keyText, object tag)
        {
            CSharpControls.VistaButton button = new CSharpControls.VistaButton();
            button.AllowDefaultButtonBorder = true;
            //button.BackColor = Color.White;
            button.ButtonColor = Color.FromArgb(31, 35, 40);
            button.ButtonText = text;
            button.CornerRadius = 6;
            button.DialogResult = System.Windows.Forms.DialogResult.None;
            button.FocusColor = Color.FromArgb(1, 57, 140);
            button.FocusGlowColor = Color.FromArgb(100, Color.Wheat);
            button.GlowColor = Color.Wheat;
            button.HighlightColor = Color.FromArgb(200, 255, 255, 255);
            button.ImageSize = new Size(28, 28);
            button.KeyText = keyText;
            button.Location = new System.Drawing.Point(0, (_buttonHeight + _buttonGap) * (_buttonCount));
            button.Name = "button" + (_buttonCount + 1).ToString();
            button.Size = new System.Drawing.Size(this.ClientSize.Width, _buttonHeight);
            button.TabIndex = _buttonCount;

            ButtonListControlItem item = new ButtonListControlItem();
            item.Text = text;
            item.Icon = image;
            item.Tag = tag;
            button.Tag = item;

            button.Click += new EventHandler(button_Click);

            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Image = image;

            this.Controls.Add(button);

            _buttonCount++;
        }

        public void SelectObject(object obj)
        {
            foreach (Control c in this.Controls)
            {
                CSharpControls.VistaButton button = c as CSharpControls.VistaButton;
                if (button == null)
                    continue;

                var item = button.Tag as ButtonListControlItem;
                if (item == null)
                    continue;

                if (item.Tag == obj)
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

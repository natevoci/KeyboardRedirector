namespace KeyboardRedirector
{
    partial class ActionPropertiesDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLaunchApplication = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxLaunchApplication = new System.Windows.Forms.TextBox();
            this.buttonLaunchAppBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPageKeyboard = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxKeyDetector = new System.Windows.Forms.RichTextBox();
            this.comboBoxKeyboardKey = new System.Windows.Forms.ComboBox();
            this.checkBoxKeyboardControl = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardAlt = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardShift = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonLaunchApplicationDoNotWait = new System.Windows.Forms.RadioButton();
            this.radioButtonLaunchApplicationWaitForInputIdle = new System.Windows.Forms.RadioButton();
            this.radioButtonLaunchApplicationWaitForExit = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageLaunchApplication.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageKeyboard.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPageLaunchApplication);
            this.tabControl1.Controls.Add(this.tabPageKeyboard);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(478, 280);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageLaunchApplication
            // 
            this.tabPageLaunchApplication.Controls.Add(this.groupBox2);
            this.tabPageLaunchApplication.Location = new System.Drawing.Point(4, 25);
            this.tabPageLaunchApplication.Name = "tabPageLaunchApplication";
            this.tabPageLaunchApplication.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLaunchApplication.Size = new System.Drawing.Size(470, 251);
            this.tabPageLaunchApplication.TabIndex = 0;
            this.tabPageLaunchApplication.Text = "Launch Application";
            this.tabPageLaunchApplication.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioButtonLaunchApplicationWaitForExit);
            this.groupBox2.Controls.Add(this.radioButtonLaunchApplicationWaitForInputIdle);
            this.groupBox2.Controls.Add(this.radioButtonLaunchApplicationDoNotWait);
            this.groupBox2.Controls.Add(this.textBoxLaunchApplication);
            this.groupBox2.Controls.Add(this.buttonLaunchAppBrowse);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 239);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Launch Application";
            // 
            // textBoxLaunchApplication
            // 
            this.textBoxLaunchApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLaunchApplication.Location = new System.Drawing.Point(6, 19);
            this.textBoxLaunchApplication.Name = "textBoxLaunchApplication";
            this.textBoxLaunchApplication.Size = new System.Drawing.Size(412, 20);
            this.textBoxLaunchApplication.TabIndex = 0;
            // 
            // buttonLaunchAppBrowse
            // 
            this.buttonLaunchAppBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLaunchAppBrowse.Location = new System.Drawing.Point(424, 17);
            this.buttonLaunchAppBrowse.Name = "buttonLaunchAppBrowse";
            this.buttonLaunchAppBrowse.Size = new System.Drawing.Size(28, 23);
            this.buttonLaunchAppBrowse.TabIndex = 1;
            this.buttonLaunchAppBrowse.Text = "...";
            this.buttonLaunchAppBrowse.UseVisualStyleBackColor = true;
            this.buttonLaunchAppBrowse.Click += new System.EventHandler(this.buttonLaunchAppBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(39, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "This can be anything that will work in the Start->Run box.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(39, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "eg. C:\\Windows\\system32\\calc.exe";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(58, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "http://nate.dynalias.net";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(58, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "calc";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(58, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(225, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "notepad C:\\Users\\Nate\\Documents\\notes.txt";
            // 
            // tabPageKeyboard
            // 
            this.tabPageKeyboard.Controls.Add(this.groupBox1);
            this.tabPageKeyboard.Location = new System.Drawing.Point(4, 25);
            this.tabPageKeyboard.Name = "tabPageKeyboard";
            this.tabPageKeyboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKeyboard.Size = new System.Drawing.Size(470, 251);
            this.tabPageKeyboard.TabIndex = 1;
            this.tabPageKeyboard.Text = "Keyboard";
            this.tabPageKeyboard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.richTextBoxKeyDetector);
            this.groupBox1.Controls.Add(this.comboBoxKeyboardKey);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardControl);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardAlt);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardShift);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 239);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keystroke details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "times";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Repeat";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(54, 92);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown1.TabIndex = 6;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // richTextBoxKeyDetector
            // 
            this.richTextBoxKeyDetector.AcceptsTab = true;
            this.richTextBoxKeyDetector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxKeyDetector.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxKeyDetector.Location = new System.Drawing.Point(6, 19);
            this.richTextBoxKeyDetector.Name = "richTextBoxKeyDetector";
            this.richTextBoxKeyDetector.Size = new System.Drawing.Size(446, 29);
            this.richTextBoxKeyDetector.TabIndex = 0;
            this.richTextBoxKeyDetector.Text = "Type here to detect key";
            this.richTextBoxKeyDetector.Enter += new System.EventHandler(this.richTextBoxKeyDetector_Enter);
            this.richTextBoxKeyDetector.Leave += new System.EventHandler(this.richTextBoxKeyDetector_Leave);
            // 
            // comboBoxKeyboardKey
            // 
            this.comboBoxKeyboardKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxKeyboardKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKeyboardKey.FormattingEnabled = true;
            this.comboBoxKeyboardKey.Location = new System.Drawing.Point(168, 61);
            this.comboBoxKeyboardKey.Name = "comboBoxKeyboardKey";
            this.comboBoxKeyboardKey.Size = new System.Drawing.Size(284, 21);
            this.comboBoxKeyboardKey.TabIndex = 4;
            // 
            // checkBoxKeyboardControl
            // 
            this.checkBoxKeyboardControl.AutoSize = true;
            this.checkBoxKeyboardControl.Location = new System.Drawing.Point(6, 63);
            this.checkBoxKeyboardControl.Name = "checkBoxKeyboardControl";
            this.checkBoxKeyboardControl.Size = new System.Drawing.Size(59, 17);
            this.checkBoxKeyboardControl.TabIndex = 1;
            this.checkBoxKeyboardControl.Text = "Control";
            this.checkBoxKeyboardControl.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardAlt
            // 
            this.checkBoxKeyboardAlt.AutoSize = true;
            this.checkBoxKeyboardAlt.Location = new System.Drawing.Point(124, 63);
            this.checkBoxKeyboardAlt.Name = "checkBoxKeyboardAlt";
            this.checkBoxKeyboardAlt.Size = new System.Drawing.Size(38, 17);
            this.checkBoxKeyboardAlt.TabIndex = 3;
            this.checkBoxKeyboardAlt.Text = "Alt";
            this.checkBoxKeyboardAlt.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardShift
            // 
            this.checkBoxKeyboardShift.AutoSize = true;
            this.checkBoxKeyboardShift.Location = new System.Drawing.Point(71, 63);
            this.checkBoxKeyboardShift.Name = "checkBoxKeyboardShift";
            this.checkBoxKeyboardShift.Size = new System.Drawing.Size(47, 17);
            this.checkBoxKeyboardShift.TabIndex = 2;
            this.checkBoxKeyboardShift.Text = "Shift";
            this.checkBoxKeyboardShift.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(334, 298);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(415, 298);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // radioButtonLaunchApplicationDoNotWait
            // 
            this.radioButtonLaunchApplicationDoNotWait.AutoSize = true;
            this.radioButtonLaunchApplicationDoNotWait.Location = new System.Drawing.Point(6, 123);
            this.radioButtonLaunchApplicationDoNotWait.Name = "radioButtonLaunchApplicationDoNotWait";
            this.radioButtonLaunchApplicationDoNotWait.Size = new System.Drawing.Size(79, 17);
            this.radioButtonLaunchApplicationDoNotWait.TabIndex = 8;
            this.radioButtonLaunchApplicationDoNotWait.TabStop = true;
            this.radioButtonLaunchApplicationDoNotWait.Text = "Do not wait";
            this.radioButtonLaunchApplicationDoNotWait.UseVisualStyleBackColor = true;
            // 
            // radioButtonLaunchApplicationWaitForInputIdle
            // 
            this.radioButtonLaunchApplicationWaitForInputIdle.AutoSize = true;
            this.radioButtonLaunchApplicationWaitForInputIdle.Location = new System.Drawing.Point(6, 146);
            this.radioButtonLaunchApplicationWaitForInputIdle.Name = "radioButtonLaunchApplicationWaitForInputIdle";
            this.radioButtonLaunchApplicationWaitForInputIdle.Size = new System.Drawing.Size(241, 17);
            this.radioButtonLaunchApplicationWaitForInputIdle.TabIndex = 8;
            this.radioButtonLaunchApplicationWaitForInputIdle.TabStop = true;
            this.radioButtonLaunchApplicationWaitForInputIdle.Text = "Wait for application to load (max time 10 secs)";
            this.radioButtonLaunchApplicationWaitForInputIdle.UseVisualStyleBackColor = true;
            // 
            // radioButtonLaunchApplicationWaitForExit
            // 
            this.radioButtonLaunchApplicationWaitForExit.AutoSize = true;
            this.radioButtonLaunchApplicationWaitForExit.Location = new System.Drawing.Point(6, 169);
            this.radioButtonLaunchApplicationWaitForExit.Name = "radioButtonLaunchApplicationWaitForExit";
            this.radioButtonLaunchApplicationWaitForExit.Size = new System.Drawing.Size(237, 17);
            this.radioButtonLaunchApplicationWaitForExit.TabIndex = 8;
            this.radioButtonLaunchApplicationWaitForExit.TabStop = true;
            this.radioButtonLaunchApplicationWaitForExit.Text = "Wait for application to exit (max time 10 secs)";
            this.radioButtonLaunchApplicationWaitForExit.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(413, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "Only use this for commands that will finish quickly, such as command line command" +
                "s.";
            // 
            // ActionPropertiesDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(502, 333);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Name = "ActionPropertiesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Action Properties";
            this.Deactivate += new System.EventHandler(this.ActionPropertiesDialog_Deactivate);
            this.Load += new System.EventHandler(this.ActionPropertiesDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageLaunchApplication.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageKeyboard.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLaunchApplication;
        private System.Windows.Forms.TabPage tabPageKeyboard;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonLaunchAppBrowse;
        private System.Windows.Forms.TextBox textBoxLaunchApplication;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxKeyboardKey;
        private System.Windows.Forms.CheckBox checkBoxKeyboardAlt;
        private System.Windows.Forms.CheckBox checkBoxKeyboardShift;
        private System.Windows.Forms.CheckBox checkBoxKeyboardControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBoxKeyDetector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton radioButtonLaunchApplicationWaitForExit;
        private System.Windows.Forms.RadioButton radioButtonLaunchApplicationWaitForInputIdle;
        private System.Windows.Forms.RadioButton radioButtonLaunchApplicationDoNotWait;
        private System.Windows.Forms.Label label3;
    }
}
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
            this.tabPageKeyboard = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.richTextBoxKeyDetector = new System.Windows.Forms.RichTextBox();
            this.comboBoxKeyboardKey = new System.Windows.Forms.ComboBox();
            this.checkBoxKeyboardRWin = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardLWin = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardControl = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardExtended = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardAlt = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyboardShift = new System.Windows.Forms.CheckBox();
            this.tabPageLaunchApplication = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonLaunchApplicationWaitForExit = new System.Windows.Forms.RadioButton();
            this.radioButtonLaunchApplicationWaitForInputIdle = new System.Windows.Forms.RadioButton();
            this.radioButtonLaunchApplicationDoNotWait = new System.Windows.Forms.RadioButton();
            this.textBoxLaunchApplication = new System.Windows.Forms.TextBox();
            this.buttonLaunchAppBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPageWindowMessage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxMessageID = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxWParam = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxLParam = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxNotFound = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelFindFromWindow = new System.Windows.Forms.Label();
            this.textBoxWindowHandle = new System.Windows.Forms.TextBox();
            this.textBoxWindowName = new System.Windows.Forms.TextBox();
            this.textBoxWindowClass = new System.Windows.Forms.TextBox();
            this.textBoxProcessName = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageKeyboard.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPageLaunchApplication.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageWindowMessage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPageKeyboard);
            this.tabControl1.Controls.Add(this.tabPageLaunchApplication);
            this.tabControl1.Controls.Add(this.tabPageWindowMessage);
            this.tabControl1.Location = new System.Drawing.Point(12, 11);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(478, 258);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageKeyboard
            // 
            this.tabPageKeyboard.Controls.Add(this.groupBox1);
            this.tabPageKeyboard.Location = new System.Drawing.Point(4, 25);
            this.tabPageKeyboard.Name = "tabPageKeyboard";
            this.tabPageKeyboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKeyboard.Size = new System.Drawing.Size(470, 229);
            this.tabPageKeyboard.TabIndex = 1;
            this.tabPageKeyboard.Text = "Keyboard";
            this.tabPageKeyboard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxKeyboardAlt);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardShift);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.richTextBoxKeyDetector);
            this.groupBox1.Controls.Add(this.comboBoxKeyboardKey);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardRWin);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardLWin);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardControl);
            this.groupBox1.Controls.Add(this.checkBoxKeyboardExtended);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 221);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keystroke details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "times";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Repeat";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(54, 108);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(44, 21);
            this.numericUpDown1.TabIndex = 9;
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
            this.richTextBoxKeyDetector.Location = new System.Drawing.Point(6, 18);
            this.richTextBoxKeyDetector.Name = "richTextBoxKeyDetector";
            this.richTextBoxKeyDetector.Size = new System.Drawing.Size(446, 27);
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
            this.comboBoxKeyboardKey.Location = new System.Drawing.Point(168, 56);
            this.comboBoxKeyboardKey.Name = "comboBoxKeyboardKey";
            this.comboBoxKeyboardKey.Size = new System.Drawing.Size(207, 20);
            this.comboBoxKeyboardKey.TabIndex = 4;
            // 
            // checkBoxKeyboardRWin
            // 
            this.checkBoxKeyboardRWin.AutoSize = true;
            this.checkBoxKeyboardRWin.Location = new System.Drawing.Point(71, 79);
            this.checkBoxKeyboardRWin.Name = "checkBoxKeyboardRWin";
            this.checkBoxKeyboardRWin.Size = new System.Drawing.Size(48, 16);
            this.checkBoxKeyboardRWin.TabIndex = 7;
            this.checkBoxKeyboardRWin.Text = "RWin";
            this.checkBoxKeyboardRWin.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardLWin
            // 
            this.checkBoxKeyboardLWin.AutoSize = true;
            this.checkBoxKeyboardLWin.Location = new System.Drawing.Point(6, 79);
            this.checkBoxKeyboardLWin.Name = "checkBoxKeyboardLWin";
            this.checkBoxKeyboardLWin.Size = new System.Drawing.Size(48, 16);
            this.checkBoxKeyboardLWin.TabIndex = 6;
            this.checkBoxKeyboardLWin.Text = "LWin";
            this.checkBoxKeyboardLWin.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardControl
            // 
            this.checkBoxKeyboardControl.AutoSize = true;
            this.checkBoxKeyboardControl.Location = new System.Drawing.Point(6, 58);
            this.checkBoxKeyboardControl.Name = "checkBoxKeyboardControl";
            this.checkBoxKeyboardControl.Size = new System.Drawing.Size(66, 16);
            this.checkBoxKeyboardControl.TabIndex = 1;
            this.checkBoxKeyboardControl.Text = "Control";
            this.checkBoxKeyboardControl.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardExtended
            // 
            this.checkBoxKeyboardExtended.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxKeyboardExtended.AutoSize = true;
            this.checkBoxKeyboardExtended.Location = new System.Drawing.Point(380, 58);
            this.checkBoxKeyboardExtended.Name = "checkBoxKeyboardExtended";
            this.checkBoxKeyboardExtended.Size = new System.Drawing.Size(72, 16);
            this.checkBoxKeyboardExtended.TabIndex = 5;
            this.checkBoxKeyboardExtended.Text = "Extended";
            this.checkBoxKeyboardExtended.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardAlt
            // 
            this.checkBoxKeyboardAlt.AutoSize = true;
            this.checkBoxKeyboardAlt.Location = new System.Drawing.Point(124, 58);
            this.checkBoxKeyboardAlt.Name = "checkBoxKeyboardAlt";
            this.checkBoxKeyboardAlt.Size = new System.Drawing.Size(42, 16);
            this.checkBoxKeyboardAlt.TabIndex = 3;
            this.checkBoxKeyboardAlt.Text = "Alt";
            this.checkBoxKeyboardAlt.UseVisualStyleBackColor = true;
            // 
            // checkBoxKeyboardShift
            // 
            this.checkBoxKeyboardShift.AutoSize = true;
            this.checkBoxKeyboardShift.Location = new System.Drawing.Point(71, 58);
            this.checkBoxKeyboardShift.Name = "checkBoxKeyboardShift";
            this.checkBoxKeyboardShift.Size = new System.Drawing.Size(54, 16);
            this.checkBoxKeyboardShift.TabIndex = 2;
            this.checkBoxKeyboardShift.Text = "Shift";
            this.checkBoxKeyboardShift.UseVisualStyleBackColor = true;
            // 
            // tabPageLaunchApplication
            // 
            this.tabPageLaunchApplication.Controls.Add(this.groupBox2);
            this.tabPageLaunchApplication.Location = new System.Drawing.Point(4, 25);
            this.tabPageLaunchApplication.Name = "tabPageLaunchApplication";
            this.tabPageLaunchApplication.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLaunchApplication.Size = new System.Drawing.Size(470, 229);
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
            this.groupBox2.Size = new System.Drawing.Size(458, 221);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Launch Application";
            // 
            // radioButtonLaunchApplicationWaitForExit
            // 
            this.radioButtonLaunchApplicationWaitForExit.AutoSize = true;
            this.radioButtonLaunchApplicationWaitForExit.Location = new System.Drawing.Point(6, 156);
            this.radioButtonLaunchApplicationWaitForExit.Name = "radioButtonLaunchApplicationWaitForExit";
            this.radioButtonLaunchApplicationWaitForExit.Size = new System.Drawing.Size(305, 16);
            this.radioButtonLaunchApplicationWaitForExit.TabIndex = 8;
            this.radioButtonLaunchApplicationWaitForExit.TabStop = true;
            this.radioButtonLaunchApplicationWaitForExit.Text = "Wait for application to exit (max time 10 secs)";
            this.radioButtonLaunchApplicationWaitForExit.UseVisualStyleBackColor = true;
            // 
            // radioButtonLaunchApplicationWaitForInputIdle
            // 
            this.radioButtonLaunchApplicationWaitForInputIdle.AutoSize = true;
            this.radioButtonLaunchApplicationWaitForInputIdle.Location = new System.Drawing.Point(6, 135);
            this.radioButtonLaunchApplicationWaitForInputIdle.Name = "radioButtonLaunchApplicationWaitForInputIdle";
            this.radioButtonLaunchApplicationWaitForInputIdle.Size = new System.Drawing.Size(305, 16);
            this.radioButtonLaunchApplicationWaitForInputIdle.TabIndex = 8;
            this.radioButtonLaunchApplicationWaitForInputIdle.TabStop = true;
            this.radioButtonLaunchApplicationWaitForInputIdle.Text = "Wait for application to load (max time 10 secs)";
            this.radioButtonLaunchApplicationWaitForInputIdle.UseVisualStyleBackColor = true;
            // 
            // radioButtonLaunchApplicationDoNotWait
            // 
            this.radioButtonLaunchApplicationDoNotWait.AutoSize = true;
            this.radioButtonLaunchApplicationDoNotWait.Location = new System.Drawing.Point(6, 114);
            this.radioButtonLaunchApplicationDoNotWait.Name = "radioButtonLaunchApplicationDoNotWait";
            this.radioButtonLaunchApplicationDoNotWait.Size = new System.Drawing.Size(89, 16);
            this.radioButtonLaunchApplicationDoNotWait.TabIndex = 8;
            this.radioButtonLaunchApplicationDoNotWait.TabStop = true;
            this.radioButtonLaunchApplicationDoNotWait.Text = "Do not wait";
            this.radioButtonLaunchApplicationDoNotWait.UseVisualStyleBackColor = true;
            // 
            // textBoxLaunchApplication
            // 
            this.textBoxLaunchApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLaunchApplication.Location = new System.Drawing.Point(6, 18);
            this.textBoxLaunchApplication.Name = "textBoxLaunchApplication";
            this.textBoxLaunchApplication.Size = new System.Drawing.Size(412, 21);
            this.textBoxLaunchApplication.TabIndex = 0;
            // 
            // buttonLaunchAppBrowse
            // 
            this.buttonLaunchAppBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLaunchAppBrowse.Location = new System.Drawing.Point(424, 16);
            this.buttonLaunchAppBrowse.Name = "buttonLaunchAppBrowse";
            this.buttonLaunchAppBrowse.Size = new System.Drawing.Size(28, 21);
            this.buttonLaunchAppBrowse.TabIndex = 1;
            this.buttonLaunchAppBrowse.Text = "...";
            this.buttonLaunchAppBrowse.UseVisualStyleBackColor = true;
            this.buttonLaunchAppBrowse.Click += new System.EventHandler(this.buttonLaunchAppBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(39, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "This can be anything that will work in the Start->Run box.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(39, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "eg. C:\\Windows\\system32\\calc.exe";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(58, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "http://nate.dynalias.net";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(58, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "calc";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(413, 28);
            this.label3.TabIndex = 5;
            this.label3.Text = "Only use this for commands that will finish quickly, such as command line command" +
    "s.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(58, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(225, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "notepad C:\\Users\\Nate\\Documents\\notes.txt";
            // 
            // tabPageWindowMessage
            // 
            this.tabPageWindowMessage.Controls.Add(this.groupBox4);
            this.tabPageWindowMessage.Controls.Add(this.groupBox3);
            this.tabPageWindowMessage.Location = new System.Drawing.Point(4, 25);
            this.tabPageWindowMessage.Name = "tabPageWindowMessage";
            this.tabPageWindowMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWindowMessage.Size = new System.Drawing.Size(470, 229);
            this.tabPageWindowMessage.TabIndex = 2;
            this.tabPageWindowMessage.Text = "Window Message";
            this.tabPageWindowMessage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.textBoxMessageID);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.textBoxWParam);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.textBoxLParam);
            this.groupBox4.Location = new System.Drawing.Point(6, 130);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(458, 96);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Windows Message";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 68);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "LParam";
            // 
            // textBoxMessageID
            // 
            this.textBoxMessageID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessageID.Location = new System.Drawing.Point(88, 18);
            this.textBoxMessageID.Name = "textBoxMessageID";
            this.textBoxMessageID.Size = new System.Drawing.Size(364, 21);
            this.textBoxMessageID.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "WParam";
            // 
            // textBoxWParam
            // 
            this.textBoxWParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWParam.Location = new System.Drawing.Point(88, 42);
            this.textBoxWParam.Name = "textBoxWParam";
            this.textBoxWParam.Size = new System.Drawing.Size(364, 21);
            this.textBoxWParam.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "Message ID";
            // 
            // textBoxLParam
            // 
            this.textBoxLParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLParam.Location = new System.Drawing.Point(88, 66);
            this.textBoxLParam.Name = "textBoxLParam";
            this.textBoxLParam.Size = new System.Drawing.Size(364, 21);
            this.textBoxLParam.TabIndex = 5;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.comboBoxNotFound);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.labelFindFromWindow);
            this.groupBox3.Controls.Add(this.textBoxWindowHandle);
            this.groupBox3.Controls.Add(this.textBoxWindowName);
            this.groupBox3.Controls.Add(this.textBoxWindowClass);
            this.groupBox3.Controls.Add(this.textBoxProcessName);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(458, 119);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Find Window";
            // 
            // comboBoxNotFound
            // 
            this.comboBoxNotFound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxNotFound.FormattingEnabled = true;
            this.comboBoxNotFound.Location = new System.Drawing.Point(131, 92);
            this.comboBoxNotFound.Name = "comboBoxNotFound";
            this.comboBoxNotFound.Size = new System.Drawing.Size(122, 20);
            this.comboBoxNotFound.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 95);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(119, 12);
            this.label15.TabIndex = 6;
            this.label15.Text = "If window not found";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "Window Name";
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(276, 96);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 9;
            this.label16.Text = "hwnd";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Window Class";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Process Name";
            // 
            // labelFindFromWindow
            // 
            this.labelFindFromWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFindFromWindow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFindFromWindow.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.labelFindFromWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelFindFromWindow.Location = new System.Drawing.Point(265, 18);
            this.labelFindFromWindow.Name = "labelFindFromWindow";
            this.labelFindFromWindow.Size = new System.Drawing.Size(187, 67);
            this.labelFindFromWindow.TabIndex = 8;
            this.labelFindFromWindow.Text = "Click here and drag to find an application from it\'s window";
            this.labelFindFromWindow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelFindFromWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelFindFromWindow_MouseDown);
            this.labelFindFromWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelFindFromWindow_MouseMove);
            this.labelFindFromWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelFindFromWindow_MouseUp);
            // 
            // textBoxWindowHandle
            // 
            this.textBoxWindowHandle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWindowHandle.Location = new System.Drawing.Point(315, 93);
            this.textBoxWindowHandle.Name = "textBoxWindowHandle";
            this.textBoxWindowHandle.ReadOnly = true;
            this.textBoxWindowHandle.Size = new System.Drawing.Size(137, 21);
            this.textBoxWindowHandle.TabIndex = 10;
            // 
            // textBoxWindowName
            // 
            this.textBoxWindowName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWindowName.Location = new System.Drawing.Point(88, 66);
            this.textBoxWindowName.Name = "textBoxWindowName";
            this.textBoxWindowName.Size = new System.Drawing.Size(165, 21);
            this.textBoxWindowName.TabIndex = 5;
            // 
            // textBoxWindowClass
            // 
            this.textBoxWindowClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWindowClass.Location = new System.Drawing.Point(88, 42);
            this.textBoxWindowClass.Name = "textBoxWindowClass";
            this.textBoxWindowClass.Size = new System.Drawing.Size(165, 21);
            this.textBoxWindowClass.TabIndex = 3;
            // 
            // textBoxProcessName
            // 
            this.textBoxProcessName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessName.Location = new System.Drawing.Point(88, 18);
            this.textBoxProcessName.Name = "textBoxProcessName";
            this.textBoxProcessName.Size = new System.Drawing.Size(165, 21);
            this.textBoxProcessName.TabIndex = 1;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(334, 275);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 21);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(415, 275);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ActionPropertiesDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(502, 307);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Name = "ActionPropertiesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Action Properties";
            this.Deactivate += new System.EventHandler(this.ActionPropertiesDialog_Deactivate);
            this.Load += new System.EventHandler(this.ActionPropertiesDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageKeyboard.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPageLaunchApplication.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageWindowMessage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBoxKeyboardExtended;
        private System.Windows.Forms.CheckBox checkBoxKeyboardRWin;
        private System.Windows.Forms.CheckBox checkBoxKeyboardLWin;
        private System.Windows.Forms.TabPage tabPageWindowMessage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxProcessName;
        private System.Windows.Forms.Label labelFindFromWindow;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLParam;
        private System.Windows.Forms.TextBox textBoxWParam;
        private System.Windows.Forms.TextBox textBoxMessageID;
        private System.Windows.Forms.TextBox textBoxWindowName;
        private System.Windows.Forms.TextBox textBoxWindowClass;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBoxNotFound;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxWindowHandle;
    }
}
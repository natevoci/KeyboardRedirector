namespace RawInput
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbCaption = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbHandle = new System.Windows.Forms.Label();
            this.lbNumKeyboards = new System.Windows.Forms.Label();
            this.lbType = new System.Windows.Forms.Label();
            this.lbKey = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.gbDetails = new System.Windows.Forms.GroupBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbVKey = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBoxList = new System.Windows.Forms.RichTextBox();
            this.checkBoxHookKeyboard = new System.Windows.Forms.CheckBox();
            this.gbDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Handle:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Device Desc:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Code:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 39);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "# Keyboards:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Virtual Key:";
            // 
            // lbCaption
            // 
            this.lbCaption.Location = new System.Drawing.Point(17, 9);
            this.lbCaption.Name = "lbCaption";
            this.lbCaption.Size = new System.Drawing.Size(596, 30);
            this.lbCaption.TabIndex = 14;
            this.lbCaption.Text = "Press any key on an attached keyboard, or enter text in the textbox below, to see" +
                " details of the input device and the key(s) you pressed.";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(281, 457);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbHandle
            // 
            this.lbHandle.AutoSize = true;
            this.lbHandle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHandle.Location = new System.Drawing.Point(83, 18);
            this.lbHandle.Name = "lbHandle";
            this.lbHandle.Size = new System.Drawing.Size(0, 13);
            this.lbHandle.TabIndex = 16;
            // 
            // lbNumKeyboards
            // 
            this.lbNumKeyboards.AutoSize = true;
            this.lbNumKeyboards.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumKeyboards.Location = new System.Drawing.Point(83, 39);
            this.lbNumKeyboards.Name = "lbNumKeyboards";
            this.lbNumKeyboards.Size = new System.Drawing.Size(0, 13);
            this.lbNumKeyboards.TabIndex = 17;
            // 
            // lbType
            // 
            this.lbType.AutoSize = true;
            this.lbType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbType.Location = new System.Drawing.Point(379, 18);
            this.lbType.Name = "lbType";
            this.lbType.Size = new System.Drawing.Size(0, 13);
            this.lbType.TabIndex = 18;
            // 
            // lbKey
            // 
            this.lbKey.AutoSize = true;
            this.lbKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbKey.Location = new System.Drawing.Point(51, 19);
            this.lbKey.Name = "lbKey";
            this.lbKey.Size = new System.Drawing.Size(0, 13);
            this.lbKey.TabIndex = 19;
            // 
            // tbText
            // 
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Location = new System.Drawing.Point(201, 42);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(240, 20);
            this.tbText.TabIndex = 0;
            // 
            // gbDetails
            // 
            this.gbDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDetails.Controls.Add(this.lbDescription);
            this.gbDetails.Controls.Add(this.lbName);
            this.gbDetails.Controls.Add(this.label6);
            this.gbDetails.Controls.Add(this.label1);
            this.gbDetails.Controls.Add(this.lbType);
            this.gbDetails.Controls.Add(this.label2);
            this.gbDetails.Controls.Add(this.lbNumKeyboards);
            this.gbDetails.Controls.Add(this.label4);
            this.gbDetails.Controls.Add(this.label3);
            this.gbDetails.Controls.Add(this.lbHandle);
            this.gbDetails.Location = new System.Drawing.Point(16, 107);
            this.gbDetails.Name = "gbDetails";
            this.gbDetails.Size = new System.Drawing.Size(597, 111);
            this.gbDetails.TabIndex = 20;
            this.gbDetails.TabStop = false;
            this.gbDetails.Text = "Device details";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescription.Location = new System.Drawing.Point(85, 84);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(0, 13);
            this.lbDescription.TabIndex = 22;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(85, 60);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(0, 13);
            this.lbName.TabIndex = 20;
            // 
            // lbVKey
            // 
            this.lbVKey.AutoSize = true;
            this.lbVKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVKey.Location = new System.Drawing.Point(202, 19);
            this.lbVKey.Name = "lbVKey";
            this.lbVKey.Size = new System.Drawing.Size(0, 13);
            this.lbVKey.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbVKey);
            this.groupBox1.Controls.Add(this.lbKey);
            this.groupBox1.Location = new System.Drawing.Point(19, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(594, 40);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Latest keystroke";
            // 
            // richTextBoxList
            // 
            this.richTextBoxList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxList.Location = new System.Drawing.Point(20, 224);
            this.richTextBoxList.Name = "richTextBoxList";
            this.richTextBoxList.Size = new System.Drawing.Size(593, 227);
            this.richTextBoxList.TabIndex = 23;
            this.richTextBoxList.Text = "";
            // 
            // checkBoxHookKeyboard
            // 
            this.checkBoxHookKeyboard.AutoSize = true;
            this.checkBoxHookKeyboard.Location = new System.Drawing.Point(473, 40);
            this.checkBoxHookKeyboard.Name = "checkBoxHookKeyboard";
            this.checkBoxHookKeyboard.Size = new System.Drawing.Size(100, 17);
            this.checkBoxHookKeyboard.TabIndex = 24;
            this.checkBoxHookKeyboard.Text = "Hook Keyboard";
            this.checkBoxHookKeyboard.UseVisualStyleBackColor = true;
            this.checkBoxHookKeyboard.CheckedChanged += new System.EventHandler(this.checkBoxHookKeyboard_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 492);
            this.Controls.Add(this.checkBoxHookKeyboard);
            this.Controls.Add(this.richTextBoxList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lbCaption);
            this.Controls.Add(this.gbDetails);
            this.Name = "Form1";
            this.Text = "Raw Keyboard Input";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbDetails.ResumeLayout(false);
            this.gbDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbCaption;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbHandle;
        private System.Windows.Forms.Label lbNumKeyboards;
        private System.Windows.Forms.Label lbType;
        private System.Windows.Forms.Label lbKey;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.GroupBox gbDetails;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbVKey;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxList;
        private System.Windows.Forms.CheckBox checkBoxHookKeyboard;
    }
}


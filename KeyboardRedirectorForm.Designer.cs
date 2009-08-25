namespace RawInput
{
    partial class KeyboardRedirectorForm
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
            this.comboBoxKeyboards = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDetect = new System.Windows.Forms.Button();
            this.labelDeviceDetails = new System.Windows.Forms.Label();
            this.buttonStopCapturing = new System.Windows.Forms.Button();
            this.buttonStartCapturing = new System.Windows.Forms.Button();
            this.richTextBoxEvents = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxKeyboards
            // 
            this.comboBoxKeyboards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxKeyboards.FormattingEnabled = true;
            this.comboBoxKeyboards.Location = new System.Drawing.Point(6, 19);
            this.comboBoxKeyboards.Name = "comboBoxKeyboards";
            this.comboBoxKeyboards.Size = new System.Drawing.Size(426, 21);
            this.comboBoxKeyboards.TabIndex = 0;
            this.comboBoxKeyboards.SelectedIndexChanged += new System.EventHandler(this.comboBoxKeyboards_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonDetect);
            this.groupBox1.Controls.Add(this.labelDeviceDetails);
            this.groupBox1.Controls.Add(this.buttonStopCapturing);
            this.groupBox1.Controls.Add(this.buttonStartCapturing);
            this.groupBox1.Controls.Add(this.comboBoxKeyboards);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 145);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyboard List";
            // 
            // buttonDetect
            // 
            this.buttonDetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDetect.Location = new System.Drawing.Point(438, 17);
            this.buttonDetect.Name = "buttonDetect";
            this.buttonDetect.Size = new System.Drawing.Size(130, 23);
            this.buttonDetect.TabIndex = 3;
            this.buttonDetect.Text = "Detect from keystroke";
            this.buttonDetect.UseVisualStyleBackColor = true;
            this.buttonDetect.Click += new System.EventHandler(this.buttonDetect_Click);
            // 
            // labelDeviceDetails
            // 
            this.labelDeviceDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDeviceDetails.Location = new System.Drawing.Point(6, 52);
            this.labelDeviceDetails.Name = "labelDeviceDetails";
            this.labelDeviceDetails.Size = new System.Drawing.Size(562, 58);
            this.labelDeviceDetails.TabIndex = 2;
            this.labelDeviceDetails.Text = "label1";
            // 
            // buttonStopCapturing
            // 
            this.buttonStopCapturing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStopCapturing.Location = new System.Drawing.Point(112, 116);
            this.buttonStopCapturing.Name = "buttonStopCapturing";
            this.buttonStopCapturing.Size = new System.Drawing.Size(100, 23);
            this.buttonStopCapturing.TabIndex = 1;
            this.buttonStopCapturing.Text = "Stop Capturing";
            this.buttonStopCapturing.UseVisualStyleBackColor = true;
            this.buttonStopCapturing.Click += new System.EventHandler(this.buttonStopCapturing_Click);
            // 
            // buttonStartCapturing
            // 
            this.buttonStartCapturing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartCapturing.Location = new System.Drawing.Point(6, 116);
            this.buttonStartCapturing.Name = "buttonStartCapturing";
            this.buttonStartCapturing.Size = new System.Drawing.Size(100, 23);
            this.buttonStartCapturing.TabIndex = 1;
            this.buttonStartCapturing.Text = "Start Capturing";
            this.buttonStartCapturing.UseVisualStyleBackColor = true;
            this.buttonStartCapturing.Click += new System.EventHandler(this.buttonStartCapturing_Click);
            // 
            // richTextBoxEvents
            // 
            this.richTextBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxEvents.DetectUrls = false;
            this.richTextBoxEvents.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEvents.Location = new System.Drawing.Point(12, 163);
            this.richTextBoxEvents.Name = "richTextBoxEvents";
            this.richTextBoxEvents.Size = new System.Drawing.Size(574, 166);
            this.richTextBoxEvents.TabIndex = 2;
            this.richTextBoxEvents.Text = "";
            // 
            // KeyboardRedirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 341);
            this.Controls.Add(this.richTextBoxEvents);
            this.Controls.Add(this.groupBox1);
            this.Name = "KeyboardRedirectorForm";
            this.Text = "Keyboard Redirector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardRedirectorForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxKeyboards;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStopCapturing;
        private System.Windows.Forms.Button buttonStartCapturing;
        private System.Windows.Forms.Label labelDeviceDetails;
        private System.Windows.Forms.Button buttonDetect;
        private System.Windows.Forms.RichTextBox richTextBoxEvents;
    }
}
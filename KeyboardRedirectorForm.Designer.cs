namespace KeyboardRedirector
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node2", 2, 3);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardRedirectorForm));
            this.richTextBoxEvents = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxKeyDetector = new System.Windows.Forms.RichTextBox();
            this.treeViewKeys = new System.Windows.Forms.TreeView();
            this.imageListTreeViewKey = new System.Windows.Forms.ImageList(this.components);
            this.panelKeyProperties = new System.Windows.Forms.Panel();
            this.buttonLaunchAppBrowse = new System.Windows.Forms.Button();
            this.textBoxLaunchApplication = new System.Windows.Forms.TextBox();
            this.textBoxKeyName = new System.Windows.Forms.TextBox();
            this.labelKeyDetails = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxCaptureKey = new System.Windows.Forms.CheckBox();
            this.panelKeyboardProperties = new System.Windows.Forms.Panel();
            this.textBoxKeyboardName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeyboardDetails = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCaptureAllKeys = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.contextMenuStripNotifyIcon.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelKeyProperties.SuspendLayout();
            this.panelKeyboardProperties.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxEvents
            // 
            this.richTextBoxEvents.DetectUrls = false;
            this.richTextBoxEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEvents.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEvents.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEvents.Name = "richTextBoxEvents";
            this.richTextBoxEvents.Size = new System.Drawing.Size(702, 101);
            this.richTextBoxEvents.TabIndex = 2;
            this.richTextBoxEvents.Text = "";
            // 
            // contextMenuStripNotifyIcon
            // 
            this.contextMenuStripNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStripNotifyIcon.Name = "contextMenuStripNotifyIcon";
            this.contextMenuStripNotifyIcon.Size = new System.Drawing.Size(119, 54);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxKeyDetector);
            this.splitContainer1.Panel1.Controls.Add(this.treeViewKeys);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelKeyProperties);
            this.splitContainer1.Panel2.Controls.Add(this.panelKeyboardProperties);
            this.splitContainer1.Size = new System.Drawing.Size(702, 473);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 3;
            // 
            // richTextBoxKeyDetector
            // 
            this.richTextBoxKeyDetector.AcceptsTab = true;
            this.richTextBoxKeyDetector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxKeyDetector.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxKeyDetector.Location = new System.Drawing.Point(3, 442);
            this.richTextBoxKeyDetector.Name = "richTextBoxKeyDetector";
            this.richTextBoxKeyDetector.Size = new System.Drawing.Size(285, 29);
            this.richTextBoxKeyDetector.TabIndex = 1;
            this.richTextBoxKeyDetector.Text = "Type here to detect keys";
            this.richTextBoxKeyDetector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxKeyDetector_KeyDown);
            // 
            // treeViewKeys
            // 
            this.treeViewKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewKeys.HideSelection = false;
            this.treeViewKeys.ImageIndex = 0;
            this.treeViewKeys.ImageList = this.imageListTreeViewKey;
            this.treeViewKeys.Location = new System.Drawing.Point(3, 3);
            this.treeViewKeys.Name = "treeViewKeys";
            treeNode4.Name = "Node1";
            treeNode4.SelectedImageIndex = 3;
            treeNode4.Text = "Node1";
            treeNode5.Name = "Node0";
            treeNode5.Text = "Node0";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "Node2";
            treeNode6.SelectedImageIndex = 3;
            treeNode6.Text = "Node2";
            this.treeViewKeys.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            this.treeViewKeys.SelectedImageIndex = 0;
            this.treeViewKeys.Size = new System.Drawing.Size(285, 433);
            this.treeViewKeys.TabIndex = 0;
            this.treeViewKeys.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewKeys_AfterSelect);
            this.treeViewKeys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeViewKeys_KeyUp);
            // 
            // imageListTreeViewKey
            // 
            this.imageListTreeViewKey.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeViewKey.ImageStream")));
            this.imageListTreeViewKey.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeViewKey.Images.SetKeyName(0, "TreeViewIcon-Green.png");
            this.imageListTreeViewKey.Images.SetKeyName(1, "TreeViewIcon-Red.png");
            this.imageListTreeViewKey.Images.SetKeyName(2, "TreeViewIcon-Yellow.png");
            this.imageListTreeViewKey.Images.SetKeyName(3, "TreeViewIcon-Blue.png");
            this.imageListTreeViewKey.Images.SetKeyName(4, "TreeViewIcon-Blue-Yellow.png");
            // 
            // panelKeyProperties
            // 
            this.panelKeyProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeyProperties.Controls.Add(this.buttonLaunchAppBrowse);
            this.panelKeyProperties.Controls.Add(this.textBoxLaunchApplication);
            this.panelKeyProperties.Controls.Add(this.textBoxKeyName);
            this.panelKeyProperties.Controls.Add(this.labelKeyDetails);
            this.panelKeyProperties.Controls.Add(this.label5);
            this.panelKeyProperties.Controls.Add(this.label4);
            this.panelKeyProperties.Controls.Add(this.label10);
            this.panelKeyProperties.Controls.Add(this.label9);
            this.panelKeyProperties.Controls.Add(this.label8);
            this.panelKeyProperties.Controls.Add(this.label7);
            this.panelKeyProperties.Controls.Add(this.label6);
            this.panelKeyProperties.Controls.Add(this.label3);
            this.panelKeyProperties.Controls.Add(this.checkBoxCaptureKey);
            this.panelKeyProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelKeyProperties.Location = new System.Drawing.Point(3, 197);
            this.panelKeyProperties.Name = "panelKeyProperties";
            this.panelKeyProperties.Size = new System.Drawing.Size(401, 273);
            this.panelKeyProperties.TabIndex = 0;
            // 
            // buttonLaunchAppBrowse
            // 
            this.buttonLaunchAppBrowse.Location = new System.Drawing.Point(366, 148);
            this.buttonLaunchAppBrowse.Name = "buttonLaunchAppBrowse";
            this.buttonLaunchAppBrowse.Size = new System.Drawing.Size(28, 23);
            this.buttonLaunchAppBrowse.TabIndex = 7;
            this.buttonLaunchAppBrowse.Text = "...";
            this.buttonLaunchAppBrowse.UseVisualStyleBackColor = true;
            this.buttonLaunchAppBrowse.Click += new System.EventHandler(this.buttonLaunchAppBrowse_Click);
            // 
            // textBoxLaunchApplication
            // 
            this.textBoxLaunchApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLaunchApplication.Location = new System.Drawing.Point(6, 150);
            this.textBoxLaunchApplication.Name = "textBoxLaunchApplication";
            this.textBoxLaunchApplication.Size = new System.Drawing.Size(354, 21);
            this.textBoxLaunchApplication.TabIndex = 6;
            this.textBoxLaunchApplication.TextChanged += new System.EventHandler(this.textBoxLaunchApplication_TextChanged);
            // 
            // textBoxKeyName
            // 
            this.textBoxKeyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyName.Location = new System.Drawing.Point(43, 28);
            this.textBoxKeyName.Name = "textBoxKeyName";
            this.textBoxKeyName.Size = new System.Drawing.Size(349, 21);
            this.textBoxKeyName.TabIndex = 6;
            this.textBoxKeyName.TextChanged += new System.EventHandler(this.textBoxKeyName_TextChanged);
            // 
            // labelKeyDetails
            // 
            this.labelKeyDetails.AutoSize = true;
            this.labelKeyDetails.Location = new System.Drawing.Point(3, 7);
            this.labelKeyDetails.Name = "labelKeyDetails";
            this.labelKeyDetails.Size = new System.Drawing.Size(60, 13);
            this.labelKeyDetails.TabIndex = 5;
            this.labelKeyDetails.Text = "Key Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Launch Application";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "This prevents the application in focus recieving the raw keystroke";
            // 
            // checkBoxCaptureKey
            // 
            this.checkBoxCaptureKey.AutoSize = true;
            this.checkBoxCaptureKey.Location = new System.Drawing.Point(6, 81);
            this.checkBoxCaptureKey.Name = "checkBoxCaptureKey";
            this.checkBoxCaptureKey.Size = new System.Drawing.Size(86, 17);
            this.checkBoxCaptureKey.TabIndex = 1;
            this.checkBoxCaptureKey.Text = "Capture Key";
            this.checkBoxCaptureKey.UseVisualStyleBackColor = true;
            this.checkBoxCaptureKey.CheckedChanged += new System.EventHandler(this.checkBoxCaptureKey_CheckedChanged);
            // 
            // panelKeyboardProperties
            // 
            this.panelKeyboardProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeyboardProperties.Controls.Add(this.textBoxKeyboardName);
            this.panelKeyboardProperties.Controls.Add(this.label2);
            this.panelKeyboardProperties.Controls.Add(this.textBoxKeyboardDetails);
            this.panelKeyboardProperties.Controls.Add(this.label1);
            this.panelKeyboardProperties.Controls.Add(this.checkBoxCaptureAllKeys);
            this.panelKeyboardProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelKeyboardProperties.Location = new System.Drawing.Point(3, 3);
            this.panelKeyboardProperties.Name = "panelKeyboardProperties";
            this.panelKeyboardProperties.Size = new System.Drawing.Size(401, 188);
            this.panelKeyboardProperties.TabIndex = 0;
            // 
            // textBoxKeyboardName
            // 
            this.textBoxKeyboardName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyboardName.Location = new System.Drawing.Point(43, 57);
            this.textBoxKeyboardName.Name = "textBoxKeyboardName";
            this.textBoxKeyboardName.Size = new System.Drawing.Size(349, 21);
            this.textBoxKeyboardName.TabIndex = 6;
            this.textBoxKeyboardName.TextChanged += new System.EventHandler(this.textBoxKeyboardName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name";
            // 
            // textBoxKeyboardDetails
            // 
            this.textBoxKeyboardDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyboardDetails.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxKeyboardDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxKeyboardDetails.Location = new System.Drawing.Point(3, 3);
            this.textBoxKeyboardDetails.Multiline = true;
            this.textBoxKeyboardDetails.Name = "textBoxKeyboardDetails";
            this.textBoxKeyboardDetails.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxKeyboardDetails.Size = new System.Drawing.Size(395, 48);
            this.textBoxKeyboardDetails.TabIndex = 4;
            this.textBoxKeyboardDetails.Text = "Keyboard Details";
            this.textBoxKeyboardDetails.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "This prevents the application in focus recieving the raw keystroke";
            // 
            // checkBoxCaptureAllKeys
            // 
            this.checkBoxCaptureAllKeys.AutoSize = true;
            this.checkBoxCaptureAllKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCaptureAllKeys.Location = new System.Drawing.Point(6, 87);
            this.checkBoxCaptureAllKeys.Name = "checkBoxCaptureAllKeys";
            this.checkBoxCaptureAllKeys.Size = new System.Drawing.Size(198, 17);
            this.checkBoxCaptureAllKeys.TabIndex = 0;
            this.checkBoxCaptureAllKeys.Text = "Capture All Keys from this keyboard";
            this.checkBoxCaptureAllKeys.UseVisualStyleBackColor = true;
            this.checkBoxCaptureAllKeys.CheckedChanged += new System.EventHandler(this.checkBoxCaptureAllKeys_CheckedChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBoxEvents);
            this.splitContainer2.Size = new System.Drawing.Size(702, 578);
            this.splitContainer2.SplitterDistance = 473;
            this.splitContainer2.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(39, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(285, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "This can be anything that will work in the Start->Run box.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(39, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "eg. C:\\Windows\\system32\\calc.exe";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(58, 200);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "calc";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(58, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(225, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "notepad C:\\Users\\Nate\\Documents\\notes.txt";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(58, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "http://nate.dynalias.net";
            // 
            // KeyboardRedirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 578);
            this.Controls.Add(this.splitContainer2);
            this.Name = "KeyboardRedirectorForm";
            this.Text = "Keyboard Redirector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardRedirectorForm_FormClosing);
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panelKeyProperties.ResumeLayout(false);
            this.panelKeyProperties.PerformLayout();
            this.panelKeyboardProperties.ResumeLayout(false);
            this.panelKeyboardProperties.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxEvents;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewKeys;
        private System.Windows.Forms.Panel panelKeyProperties;
        private System.Windows.Forms.Panel panelKeyboardProperties;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxCaptureAllKeys;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxCaptureKey;
        private System.Windows.Forms.ImageList imageListTreeViewKey;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBoxKeyboardDetails;
        private System.Windows.Forms.TextBox textBoxKeyboardName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxKeyName;
        private System.Windows.Forms.Label labelKeyDetails;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBoxKeyDetector;
        private System.Windows.Forms.Button buttonLaunchAppBrowse;
        private System.Windows.Forms.TextBox textBoxLaunchApplication;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}
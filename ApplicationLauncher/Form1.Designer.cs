namespace ApplicationLauncher
{
    partial class ApplicationLauncherForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationLauncherForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imageListRunningLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListRunningSmall = new System.Windows.Forms.ImageList(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonExit = new CSharpControls.VistaButton();
            this.buttonEditShortcuts = new CSharpControls.VistaButton();
            this.buttonListControlRunning = new ApplicationLauncher.ButtonListControl();
            this.buttonListControlShortcuts = new ApplicationLauncher.ButtonListControl();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonListControlRunning);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ApplicationLauncherForm_MouseDown);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.buttonListControlShortcuts);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ApplicationLauncherForm_MouseDown);
            this.splitContainer1.Size = new System.Drawing.Size(837, 569);
            this.splitContainer1.SplitterDistance = 417;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Running Applications";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ApplicationLauncherForm_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Shortcuts";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ApplicationLauncherForm_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Shortcuts";
            // 
            // imageListRunningLarge
            // 
            this.imageListRunningLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListRunningLarge.ImageStream")));
            this.imageListRunningLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListRunningLarge.Images.SetKeyName(0, "Blank.ico");
            // 
            // imageListRunningSmall
            // 
            this.imageListRunningSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListRunningSmall.ImageStream")));
            this.imageListRunningSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListRunningSmall.Images.SetKeyName(0, "Blank.ico");
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Application Launcher";
            this.notifyIcon1.BalloonTipTitle = "Application Launcher";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(105, 54);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(101, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.AllowDefaultButtonBorder = true;
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonExit.ButtonText = "Exit";
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonExit.FocusColor = System.Drawing.Color.Empty;
            this.buttonExit.FocusGlowColor = System.Drawing.Color.Red;
            this.buttonExit.KeyText = "";
            this.buttonExit.Location = new System.Drawing.Point(757, 12);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(68, 23);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonEditShortcuts
            // 
            this.buttonEditShortcuts.AllowDefaultButtonBorder = true;
            this.buttonEditShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditShortcuts.BackColor = System.Drawing.Color.Transparent;
            this.buttonEditShortcuts.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(128)))));
            this.buttonEditShortcuts.ButtonText = "Edit Shortcuts";
            this.buttonEditShortcuts.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonEditShortcuts.FocusColor = System.Drawing.Color.Empty;
            this.buttonEditShortcuts.FocusGlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(255)))));
            this.buttonEditShortcuts.KeyText = "";
            this.buttonEditShortcuts.Location = new System.Drawing.Point(651, 12);
            this.buttonEditShortcuts.Name = "buttonEditShortcuts";
            this.buttonEditShortcuts.Size = new System.Drawing.Size(100, 23);
            this.buttonEditShortcuts.TabIndex = 2;
            this.buttonEditShortcuts.Click += new System.EventHandler(this.buttonEditShortcuts_Click);
            // 
            // buttonListControlRunning
            // 
            this.buttonListControlRunning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonListControlRunning.AutoScroll = true;
            this.buttonListControlRunning.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.buttonListControlRunning.Location = new System.Drawing.Point(3, 42);
            this.buttonListControlRunning.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonListControlRunning.Name = "buttonListControlRunning";
            this.buttonListControlRunning.Size = new System.Drawing.Size(411, 524);
            this.buttonListControlRunning.TabIndex = 1;
            // 
            // buttonListControlShortcuts
            // 
            this.buttonListControlShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonListControlShortcuts.AutoScroll = true;
            this.buttonListControlShortcuts.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.buttonListControlShortcuts.Location = new System.Drawing.Point(2, 42);
            this.buttonListControlShortcuts.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonListControlShortcuts.Name = "buttonListControlShortcuts";
            this.buttonListControlShortcuts.Size = new System.Drawing.Size(411, 524);
            this.buttonListControlShortcuts.TabIndex = 1;
            // 
            // ApplicationLauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(9)))), ((int)(((byte)(15)))));
            this.ClientSize = new System.Drawing.Size(837, 569);
            this.ControlBox = false;
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonEditShortcuts);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApplicationLauncherForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.ApplicationLauncherForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ApplicationLauncherForm_FormClosing);
            this.Load += new System.EventHandler(this.ApplicationLauncherForm_Load);
            this.SizeChanged += new System.EventHandler(this.ApplicationLauncherForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ApplicationLauncherForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ApplicationLauncherForm_MouseDown);
            this.Move += new System.EventHandler(this.ApplicationLauncherForm_Move);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageListRunningLarge;
        private System.Windows.Forms.ImageList imageListRunningSmall;
        private System.Windows.Forms.Label label3;
        private CSharpControls.VistaButton buttonEditShortcuts;
        private ButtonListControl buttonListControlRunning;
        private ButtonListControl buttonListControlShortcuts;
        private CSharpControls.VistaButton buttonExit;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}


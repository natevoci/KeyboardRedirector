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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node2", 2, 3);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyboardRedirectorForm));
            this.richTextBoxEvents = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewKeys = new System.Windows.Forms.TreeView();
            this.contextMenuStripTreeViewEvents = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTreeViewKey = new System.Windows.Forms.ImageList(this.components);
            this.panelKeyProperties = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.listViewApplicationsInFocus = new KeyboardRedirector.BindingListView();
            this.imageListApplications = new System.Windows.Forms.ImageList(this.components);
            this.buttonEditApplications = new System.Windows.Forms.Button();
            this.buttonAddAction = new System.Windows.Forms.Button();
            this.listViewActions = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.buttonRemoveAction = new System.Windows.Forms.Button();
            this.buttonEditAction = new System.Windows.Forms.Button();
            this.textBoxKeyName = new System.Windows.Forms.TextBox();
            this.labelKeyDetails = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxCaptureKey = new System.Windows.Forms.CheckBox();
            this.panelKeyboardProperties = new System.Windows.Forms.Panel();
            this.textBoxKeyboardName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeyboardDetails = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCaptureAllKeys = new System.Windows.Forms.CheckBox();
            this.richTextBoxKeyDetector = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.checkBoxMinimiseOnStart = new System.Windows.Forms.CheckBox();
            this.timerMinimiseOnStart = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripNotifyIcon.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripTreeViewEvents.SuspendLayout();
            this.panelKeyProperties.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panelKeyboardProperties.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
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
            this.richTextBoxEvents.TabIndex = 0;
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
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewKeys);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelKeyProperties);
            this.splitContainer1.Panel2.Controls.Add(this.panelKeyboardProperties);
            this.splitContainer1.Size = new System.Drawing.Size(702, 446);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewKeys
            // 
            this.treeViewKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewKeys.ContextMenuStrip = this.contextMenuStripTreeViewEvents;
            this.treeViewKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewKeys.HideSelection = false;
            this.treeViewKeys.ImageIndex = 0;
            this.treeViewKeys.ImageList = this.imageListTreeViewKey;
            this.treeViewKeys.Location = new System.Drawing.Point(3, 3);
            this.treeViewKeys.Name = "treeViewKeys";
            treeNode1.Name = "Node1";
            treeNode1.SelectedImageIndex = 3;
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Node0";
            treeNode3.ImageIndex = 2;
            treeNode3.Name = "Node2";
            treeNode3.SelectedImageIndex = 3;
            treeNode3.Text = "Node2";
            this.treeViewKeys.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            this.treeViewKeys.SelectedImageIndex = 0;
            this.treeViewKeys.Size = new System.Drawing.Size(281, 436);
            this.treeViewKeys.TabIndex = 1;
            this.treeViewKeys.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewKeys_AfterSelect);
            this.treeViewKeys.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewKeys_MouseDown);
            this.treeViewKeys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeViewKeys_KeyUp);
            // 
            // contextMenuStripTreeViewEvents
            // 
            this.contextMenuStripTreeViewEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStripTreeViewEvents.Name = "contextMenuStripTreeViewEvents";
            this.contextMenuStripTreeViewEvents.Size = new System.Drawing.Size(132, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
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
            this.panelKeyProperties.Controls.Add(this.groupBox1);
            this.panelKeyProperties.Controls.Add(this.textBoxKeyName);
            this.panelKeyProperties.Controls.Add(this.labelKeyDetails);
            this.panelKeyProperties.Controls.Add(this.label4);
            this.panelKeyProperties.Controls.Add(this.label3);
            this.panelKeyProperties.Controls.Add(this.checkBoxCaptureKey);
            this.panelKeyProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelKeyProperties.Location = new System.Drawing.Point(3, 113);
            this.panelKeyProperties.Name = "panelKeyProperties";
            this.panelKeyProperties.Size = new System.Drawing.Size(397, 326);
            this.panelKeyProperties.TabIndex = 1;
            this.panelKeyProperties.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer4);
            this.groupBox1.Location = new System.Drawing.Point(3, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 208);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actions";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 17);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.listViewApplicationsInFocus);
            this.splitContainer4.Panel1.Controls.Add(this.buttonEditApplications);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.buttonAddAction);
            this.splitContainer4.Panel2.Controls.Add(this.listViewActions);
            this.splitContainer4.Panel2.Controls.Add(this.buttonRemoveAction);
            this.splitContainer4.Panel2.Controls.Add(this.buttonEditAction);
            this.splitContainer4.Size = new System.Drawing.Size(385, 188);
            this.splitContainer4.SplitterDistance = 87;
            this.splitContainer4.TabIndex = 19;
            // 
            // listViewApplicationsInFocus
            // 
            this.listViewApplicationsInFocus.AllowColumnSort = false;
            this.listViewApplicationsInFocus.AllowDrop = true;
            this.listViewApplicationsInFocus.AllowReorder = false;
            this.listViewApplicationsInFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewApplicationsInFocus.DataSource = null;
            this.listViewApplicationsInFocus.Filter = "";
            this.listViewApplicationsInFocus.FullRowSelect = true;
            this.listViewApplicationsInFocus.HideSelection = false;
            this.listViewApplicationsInFocus.Location = new System.Drawing.Point(3, 3);
            this.listViewApplicationsInFocus.MultiSelect = false;
            this.listViewApplicationsInFocus.Name = "listViewApplicationsInFocus";
            this.listViewApplicationsInFocus.OwnerDraw = true;
            this.listViewApplicationsInFocus.SelectedIndex = -1;
            this.listViewApplicationsInFocus.SelectedItem = null;
            this.listViewApplicationsInFocus.Size = new System.Drawing.Size(144, 81);
            this.listViewApplicationsInFocus.SmallImageList = this.imageListApplications;
            this.listViewApplicationsInFocus.TabIndex = 18;
            this.listViewApplicationsInFocus.UseCompatibleStateImageBehavior = false;
            this.listViewApplicationsInFocus.View = System.Windows.Forms.View.Details;
            this.listViewApplicationsInFocus.SelectedIndexChanged += new System.EventHandler(this.listViewApplicationsInFocus_SelectedIndexChanged);
            // 
            // imageListApplications
            // 
            this.imageListApplications.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListApplications.ImageStream")));
            this.imageListApplications.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListApplications.Images.SetKeyName(0, "DefaultApplication.png");
            this.imageListApplications.Images.SetKeyName(1, "DefaultApplicationNotFound.png");
            // 
            // buttonEditApplications
            // 
            this.buttonEditApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditApplications.Location = new System.Drawing.Point(153, 3);
            this.buttonEditApplications.Name = "buttonEditApplications";
            this.buttonEditApplications.Size = new System.Drawing.Size(99, 23);
            this.buttonEditApplications.TabIndex = 17;
            this.buttonEditApplications.Text = "Edit Applications";
            this.buttonEditApplications.UseVisualStyleBackColor = true;
            this.buttonEditApplications.Click += new System.EventHandler(this.buttonEditApplications_Click);
            // 
            // buttonAddAction
            // 
            this.buttonAddAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddAction.Location = new System.Drawing.Point(172, 71);
            this.buttonAddAction.Name = "buttonAddAction";
            this.buttonAddAction.Size = new System.Drawing.Size(66, 23);
            this.buttonAddAction.TabIndex = 16;
            this.buttonAddAction.Text = "Add";
            this.buttonAddAction.UseVisualStyleBackColor = true;
            this.buttonAddAction.Click += new System.EventHandler(this.buttonAddAction_Click);
            // 
            // listViewActions
            // 
            this.listViewActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewActions.FullRowSelect = true;
            this.listViewActions.GridLines = true;
            this.listViewActions.Location = new System.Drawing.Point(3, 3);
            this.listViewActions.Name = "listViewActions";
            this.listViewActions.Size = new System.Drawing.Size(379, 62);
            this.listViewActions.TabIndex = 0;
            this.listViewActions.UseCompatibleStateImageBehavior = false;
            this.listViewActions.View = System.Windows.Forms.View.Details;
            this.listViewActions.SelectedIndexChanged += new System.EventHandler(this.listViewActions_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Action";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 220;
            // 
            // buttonRemoveAction
            // 
            this.buttonRemoveAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveAction.Location = new System.Drawing.Point(316, 71);
            this.buttonRemoveAction.Name = "buttonRemoveAction";
            this.buttonRemoveAction.Size = new System.Drawing.Size(66, 23);
            this.buttonRemoveAction.TabIndex = 16;
            this.buttonRemoveAction.Text = "Remove";
            this.buttonRemoveAction.UseVisualStyleBackColor = true;
            this.buttonRemoveAction.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonEditAction
            // 
            this.buttonEditAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditAction.Location = new System.Drawing.Point(244, 71);
            this.buttonEditAction.Name = "buttonEditAction";
            this.buttonEditAction.Size = new System.Drawing.Size(66, 23);
            this.buttonEditAction.TabIndex = 16;
            this.buttonEditAction.Text = "Edit";
            this.buttonEditAction.UseVisualStyleBackColor = true;
            this.buttonEditAction.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // textBoxKeyName
            // 
            this.textBoxKeyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyName.Location = new System.Drawing.Point(43, 28);
            this.textBoxKeyName.Name = "textBoxKeyName";
            this.textBoxKeyName.Size = new System.Drawing.Size(351, 21);
            this.textBoxKeyName.TabIndex = 2;
            this.textBoxKeyName.TextChanged += new System.EventHandler(this.textBoxKeyName_TextChanged);
            // 
            // labelKeyDetails
            // 
            this.labelKeyDetails.AutoSize = true;
            this.labelKeyDetails.Location = new System.Drawing.Point(3, 7);
            this.labelKeyDetails.Name = "labelKeyDetails";
            this.labelKeyDetails.Size = new System.Drawing.Size(60, 13);
            this.labelKeyDetails.TabIndex = 0;
            this.labelKeyDetails.Text = "Key Details";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(39, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "This prevents the application in focus recieving the raw keystroke";
            // 
            // checkBoxCaptureKey
            // 
            this.checkBoxCaptureKey.AutoSize = true;
            this.checkBoxCaptureKey.Location = new System.Drawing.Point(6, 65);
            this.checkBoxCaptureKey.Name = "checkBoxCaptureKey";
            this.checkBoxCaptureKey.Size = new System.Drawing.Size(86, 17);
            this.checkBoxCaptureKey.TabIndex = 3;
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
            this.panelKeyboardProperties.Size = new System.Drawing.Size(397, 140);
            this.panelKeyboardProperties.TabIndex = 0;
            this.panelKeyboardProperties.Visible = false;
            // 
            // textBoxKeyboardName
            // 
            this.textBoxKeyboardName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyboardName.Location = new System.Drawing.Point(43, 57);
            this.textBoxKeyboardName.Name = "textBoxKeyboardName";
            this.textBoxKeyboardName.Size = new System.Drawing.Size(351, 21);
            this.textBoxKeyboardName.TabIndex = 2;
            this.textBoxKeyboardName.TextChanged += new System.EventHandler(this.textBoxKeyboardName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
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
            this.textBoxKeyboardDetails.Size = new System.Drawing.Size(391, 48);
            this.textBoxKeyboardDetails.TabIndex = 0;
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
            this.label1.TabIndex = 4;
            this.label1.Text = "This prevents the application in focus recieving the raw keystroke";
            // 
            // checkBoxCaptureAllKeys
            // 
            this.checkBoxCaptureAllKeys.AutoSize = true;
            this.checkBoxCaptureAllKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCaptureAllKeys.Location = new System.Drawing.Point(6, 87);
            this.checkBoxCaptureAllKeys.Name = "checkBoxCaptureAllKeys";
            this.checkBoxCaptureAllKeys.Size = new System.Drawing.Size(198, 17);
            this.checkBoxCaptureAllKeys.TabIndex = 3;
            this.checkBoxCaptureAllKeys.Text = "Capture All Keys from this keyboard";
            this.checkBoxCaptureAllKeys.UseVisualStyleBackColor = true;
            this.checkBoxCaptureAllKeys.CheckedChanged += new System.EventHandler(this.checkBoxCaptureAllKeys_CheckedChanged);
            // 
            // richTextBoxKeyDetector
            // 
            this.richTextBoxKeyDetector.AcceptsTab = true;
            this.richTextBoxKeyDetector.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxKeyDetector.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxKeyDetector.Name = "richTextBoxKeyDetector";
            this.richTextBoxKeyDetector.Size = new System.Drawing.Size(229, 29);
            this.richTextBoxKeyDetector.TabIndex = 0;
            this.richTextBoxKeyDetector.Text = "Type here to detect keys";
            this.richTextBoxKeyDetector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxKeyDetector_KeyDown);
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
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBoxEvents);
            this.splitContainer2.Size = new System.Drawing.Size(702, 584);
            this.splitContainer2.SplitterDistance = 479;
            this.splitContainer2.TabIndex = 4;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.checkBoxMinimiseOnStart);
            this.splitContainer3.Panel1.Controls.Add(this.richTextBoxKeyDetector);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new System.Drawing.Size(702, 479);
            this.splitContainer3.SplitterDistance = 29;
            this.splitContainer3.TabIndex = 1;
            // 
            // checkBoxMinimiseOnStart
            // 
            this.checkBoxMinimiseOnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMinimiseOnStart.AutoSize = true;
            this.checkBoxMinimiseOnStart.Location = new System.Drawing.Point(589, 9);
            this.checkBoxMinimiseOnStart.Name = "checkBoxMinimiseOnStart";
            this.checkBoxMinimiseOnStart.Size = new System.Drawing.Size(108, 17);
            this.checkBoxMinimiseOnStart.TabIndex = 1;
            this.checkBoxMinimiseOnStart.Text = "Minimise On Start";
            this.checkBoxMinimiseOnStart.UseVisualStyleBackColor = true;
            this.checkBoxMinimiseOnStart.CheckedChanged += new System.EventHandler(this.checkBoxMinimiseOnStart_CheckedChanged);
            // 
            // timerMinimiseOnStart
            // 
            this.timerMinimiseOnStart.Tick += new System.EventHandler(this.timerMinimiseOnStart_Tick);
            // 
            // KeyboardRedirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 584);
            this.Controls.Add(this.splitContainer2);
            this.Name = "KeyboardRedirectorForm";
            this.Text = "Keyboard Redirector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardRedirectorForm_FormClosing);
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripTreeViewEvents.ResumeLayout(false);
            this.panelKeyProperties.ResumeLayout(false);
            this.panelKeyProperties.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.panelKeyboardProperties.ResumeLayout(false);
            this.panelKeyboardProperties.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeViewEvents;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.CheckBox checkBoxMinimiseOnStart;
        private System.Windows.Forms.Timer timerMinimiseOnStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView listViewActions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonAddAction;
        private System.Windows.Forms.Button buttonRemoveAction;
        private System.Windows.Forms.Button buttonEditAction;
        private System.Windows.Forms.Button buttonEditApplications;
        private BindingListView listViewApplicationsInFocus;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ImageList imageListApplications;
    }
}
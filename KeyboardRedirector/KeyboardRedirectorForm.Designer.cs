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
            this.richTextBoxEvents = new KeyboardRedirector.RichTextBoxEx();
            this.contextMenuStripNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewKeys = new System.Windows.Forms.TreeView();
            this.contextMenuStripTreeViewEvents = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTreeViewKey = new System.Windows.Forms.ImageList(this.components);
            this.panelKeyProperties = new System.Windows.Forms.Panel();
            this.numericUpDownAntiRepeat = new System.Windows.Forms.NumericUpDown();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.labelFocussedAppInfo = new System.Windows.Forms.Label();
            this.listViewApplicationsInFocus = new KeyboardRedirector.BindingListView();
            this.imageListApplications = new System.Windows.Forms.ImageList(this.components);
            this.buttonRemoveApplication = new System.Windows.Forms.Button();
            this.buttonAddApplications = new System.Windows.Forms.Button();
            this.buttonAddAction = new System.Windows.Forms.Button();
            this.listViewActions = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.buttonRemoveAction = new System.Windows.Forms.Button();
            this.buttonEditAction = new System.Windows.Forms.Button();
            this.textBoxKeyName = new System.Windows.Forms.TextBox();
            this.labelKeyDetails = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelKeyCapture = new System.Windows.Forms.Label();
            this.checkBoxKeyCapture = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyEnabled = new System.Windows.Forms.CheckBox();
            this.panelDevices = new System.Windows.Forms.Panel();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.listViewDevices = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.listViewDevicesKeyboard = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelKeyboardProperties = new System.Windows.Forms.Panel();
            this.textBoxKeyboardName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeyboardDetails = new System.Windows.Forms.TextBox();
            this.labelCaptureAllKeys = new System.Windows.Forms.Label();
            this.checkBoxCaptureAllKeys = new System.Windows.Forms.CheckBox();
            this.richTextBoxKeyDetector = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.checkBoxCaptureLowLevel = new System.Windows.Forms.CheckBox();
            this.checkBoxMinimiseOnStart = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.tabPageKeyEvents = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBoxKeyEventsLowLevel = new KeyboardRedirector.RichTextBoxEx();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBoxKeyEventsHook = new KeyboardRedirector.RichTextBoxEx();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBoxKeyEventsWMInput = new KeyboardRedirector.RichTextBoxEx();
            this.timerMinimiseOnStart = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripNotifyIcon.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripTreeViewEvents.SuspendLayout();
            this.panelKeyProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAntiRepeat)).BeginInit();
            this.groupBoxActions.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panelDevices.SuspendLayout();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.panelKeyboardProperties.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageMessages.SuspendLayout();
            this.tabPageKeyEvents.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxEvents
            // 
            this.richTextBoxEvents.DetectUrls = false;
            this.richTextBoxEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEvents.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEvents.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEvents.Name = "richTextBoxEvents";
            this.richTextBoxEvents.Size = new System.Drawing.Size(716, 75);
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
            this.splitContainer1.Panel2.Controls.Add(this.panelDevices);
            this.splitContainer1.Panel2.Controls.Add(this.panelKeyboardProperties);
            this.splitContainer1.Size = new System.Drawing.Size(724, 464);
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
            this.treeViewKeys.Size = new System.Drawing.Size(281, 454);
            this.treeViewKeys.TabIndex = 0;
            this.treeViewKeys.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewKeys_AfterSelect);
            this.treeViewKeys.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewKeys_MouseDown);
            this.treeViewKeys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeViewKeys_KeyUp);
            // 
            // contextMenuStripTreeViewEvents
            // 
            this.contextMenuStripTreeViewEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.removeAllKeysToolStripMenuItem,
            this.executeActionsToolStripMenuItem});
            this.contextMenuStripTreeViewEvents.Name = "contextMenuStripTreeViewEvents";
            this.contextMenuStripTreeViewEvents.Size = new System.Drawing.Size(162, 70);
            this.contextMenuStripTreeViewEvents.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTreeViewEvents_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // removeAllKeysToolStripMenuItem
            // 
            this.removeAllKeysToolStripMenuItem.Name = "removeAllKeysToolStripMenuItem";
            this.removeAllKeysToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.removeAllKeysToolStripMenuItem.Text = "Remove All Keys";
            this.removeAllKeysToolStripMenuItem.Click += new System.EventHandler(this.removeAllKeysToolStripMenuItem_Click);
            // 
            // executeActionsToolStripMenuItem
            // 
            this.executeActionsToolStripMenuItem.Name = "executeActionsToolStripMenuItem";
            this.executeActionsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.executeActionsToolStripMenuItem.Text = "Execute Actions";
            this.executeActionsToolStripMenuItem.Click += new System.EventHandler(this.executeActionsToolStripMenuItem_Click);
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
            this.imageListTreeViewKey.Images.SetKeyName(5, "Config.png");
            this.imageListTreeViewKey.Images.SetKeyName(6, "DefaultApplication.png");
            // 
            // panelKeyProperties
            // 
            this.panelKeyProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeyProperties.Controls.Add(this.numericUpDownAntiRepeat);
            this.panelKeyProperties.Controls.Add(this.groupBoxActions);
            this.panelKeyProperties.Controls.Add(this.textBoxKeyName);
            this.panelKeyProperties.Controls.Add(this.labelKeyDetails);
            this.panelKeyProperties.Controls.Add(this.label1);
            this.panelKeyProperties.Controls.Add(this.label4);
            this.panelKeyProperties.Controls.Add(this.labelKeyCapture);
            this.panelKeyProperties.Controls.Add(this.checkBoxKeyCapture);
            this.panelKeyProperties.Controls.Add(this.checkBoxKeyEnabled);
            this.panelKeyProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelKeyProperties.Location = new System.Drawing.Point(49, 46);
            this.panelKeyProperties.Name = "panelKeyProperties";
            this.panelKeyProperties.Size = new System.Drawing.Size(419, 436);
            this.panelKeyProperties.TabIndex = 1;
            this.panelKeyProperties.Visible = false;
            // 
            // numericUpDownAntiRepeat
            // 
            this.numericUpDownAntiRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownAntiRepeat.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownAntiRepeat.Location = new System.Drawing.Point(357, 55);
            this.numericUpDownAntiRepeat.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownAntiRepeat.Name = "numericUpDownAntiRepeat";
            this.numericUpDownAntiRepeat.Size = new System.Drawing.Size(59, 21);
            this.numericUpDownAntiRepeat.TabIndex = 5;
            this.numericUpDownAntiRepeat.ValueChanged += new System.EventHandler(this.numericUpDownAntiRepeat_ValueChanged);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxActions.Controls.Add(this.splitContainer4);
            this.groupBoxActions.Location = new System.Drawing.Point(3, 115);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(413, 318);
            this.groupBoxActions.TabIndex = 8;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
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
            this.splitContainer4.Panel1.Controls.Add(this.labelFocussedAppInfo);
            this.splitContainer4.Panel1.Controls.Add(this.listViewApplicationsInFocus);
            this.splitContainer4.Panel1.Controls.Add(this.buttonRemoveApplication);
            this.splitContainer4.Panel1.Controls.Add(this.buttonAddApplications);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.buttonAddAction);
            this.splitContainer4.Panel2.Controls.Add(this.listViewActions);
            this.splitContainer4.Panel2.Controls.Add(this.buttonRemoveAction);
            this.splitContainer4.Panel2.Controls.Add(this.buttonEditAction);
            this.splitContainer4.Size = new System.Drawing.Size(407, 298);
            this.splitContainer4.SplitterDistance = 133;
            this.splitContainer4.TabIndex = 0;
            // 
            // labelFocussedAppInfo
            // 
            this.labelFocussedAppInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFocussedAppInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFocussedAppInfo.Location = new System.Drawing.Point(232, 29);
            this.labelFocussedAppInfo.Name = "labelFocussedAppInfo";
            this.labelFocussedAppInfo.Size = new System.Drawing.Size(172, 101);
            this.labelFocussedAppInfo.TabIndex = 3;
            this.labelFocussedAppInfo.Text = resources.GetString("labelFocussedAppInfo.Text");
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
            this.listViewApplicationsInFocus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewApplicationsInFocus.HideSelection = false;
            this.listViewApplicationsInFocus.Location = new System.Drawing.Point(3, 3);
            this.listViewApplicationsInFocus.MultiSelect = false;
            this.listViewApplicationsInFocus.Name = "listViewApplicationsInFocus";
            this.listViewApplicationsInFocus.OwnerDraw = true;
            this.listViewApplicationsInFocus.SelectedIndex = -1;
            this.listViewApplicationsInFocus.SelectedItem = null;
            this.listViewApplicationsInFocus.Size = new System.Drawing.Size(223, 127);
            this.listViewApplicationsInFocus.SmallImageList = this.imageListApplications;
            this.listViewApplicationsInFocus.TabIndex = 0;
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
            // buttonRemoveApplication
            // 
            this.buttonRemoveApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveApplication.Location = new System.Drawing.Point(302, 3);
            this.buttonRemoveApplication.Name = "buttonRemoveApplication";
            this.buttonRemoveApplication.Size = new System.Drawing.Size(64, 23);
            this.buttonRemoveApplication.TabIndex = 2;
            this.buttonRemoveApplication.Text = "Remove";
            this.buttonRemoveApplication.UseVisualStyleBackColor = true;
            this.buttonRemoveApplication.Click += new System.EventHandler(this.buttonRemoveApplication_Click);
            // 
            // buttonAddApplications
            // 
            this.buttonAddApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddApplications.Location = new System.Drawing.Point(232, 3);
            this.buttonAddApplications.Name = "buttonAddApplications";
            this.buttonAddApplications.Size = new System.Drawing.Size(64, 23);
            this.buttonAddApplications.TabIndex = 1;
            this.buttonAddApplications.Text = "Add";
            this.buttonAddApplications.UseVisualStyleBackColor = true;
            this.buttonAddApplications.Click += new System.EventHandler(this.buttonAddApplications_Click);
            // 
            // buttonAddAction
            // 
            this.buttonAddAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddAction.Location = new System.Drawing.Point(194, 135);
            this.buttonAddAction.Name = "buttonAddAction";
            this.buttonAddAction.Size = new System.Drawing.Size(66, 23);
            this.buttonAddAction.TabIndex = 1;
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
            this.listViewActions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewActions.HideSelection = false;
            this.listViewActions.Location = new System.Drawing.Point(3, 3);
            this.listViewActions.MultiSelect = false;
            this.listViewActions.Name = "listViewActions";
            this.listViewActions.Size = new System.Drawing.Size(401, 126);
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
            this.buttonRemoveAction.Location = new System.Drawing.Point(338, 135);
            this.buttonRemoveAction.Name = "buttonRemoveAction";
            this.buttonRemoveAction.Size = new System.Drawing.Size(66, 23);
            this.buttonRemoveAction.TabIndex = 3;
            this.buttonRemoveAction.Text = "Remove";
            this.buttonRemoveAction.UseVisualStyleBackColor = true;
            this.buttonRemoveAction.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonEditAction
            // 
            this.buttonEditAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditAction.Location = new System.Drawing.Point(266, 135);
            this.buttonEditAction.Name = "buttonEditAction";
            this.buttonEditAction.Size = new System.Drawing.Size(66, 23);
            this.buttonEditAction.TabIndex = 2;
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
            this.textBoxKeyName.Size = new System.Drawing.Size(373, 21);
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
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Antirepeat time [ms]";
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
            // labelKeyCapture
            // 
            this.labelKeyCapture.AutoSize = true;
            this.labelKeyCapture.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKeyCapture.Location = new System.Drawing.Point(39, 94);
            this.labelKeyCapture.Name = "labelKeyCapture";
            this.labelKeyCapture.Size = new System.Drawing.Size(321, 13);
            this.labelKeyCapture.TabIndex = 7;
            this.labelKeyCapture.Text = "This prevents the application in focus recieving the raw keystroke";
            // 
            // checkBoxKeyCapture
            // 
            this.checkBoxKeyCapture.AutoSize = true;
            this.checkBoxKeyCapture.Location = new System.Drawing.Point(9, 78);
            this.checkBoxKeyCapture.Name = "checkBoxKeyCapture";
            this.checkBoxKeyCapture.Size = new System.Drawing.Size(86, 17);
            this.checkBoxKeyCapture.TabIndex = 6;
            this.checkBoxKeyCapture.Text = "Capture Key";
            this.checkBoxKeyCapture.UseVisualStyleBackColor = true;
            this.checkBoxKeyCapture.CheckedChanged += new System.EventHandler(this.checkBoxKeyCapture_CheckedChanged);
            // 
            // checkBoxKeyEnabled
            // 
            this.checkBoxKeyEnabled.AutoSize = true;
            this.checkBoxKeyEnabled.Location = new System.Drawing.Point(9, 55);
            this.checkBoxKeyEnabled.Name = "checkBoxKeyEnabled";
            this.checkBoxKeyEnabled.Size = new System.Drawing.Size(64, 17);
            this.checkBoxKeyEnabled.TabIndex = 3;
            this.checkBoxKeyEnabled.Text = "Enabled";
            this.checkBoxKeyEnabled.UseVisualStyleBackColor = true;
            this.checkBoxKeyEnabled.CheckedChanged += new System.EventHandler(this.checkBoxKeyEnabled_CheckedChanged);
            // 
            // panelDevices
            // 
            this.panelDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDevices.Controls.Add(this.splitContainer7);
            this.panelDevices.Controls.Add(this.label3);
            this.panelDevices.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.panelDevices.Location = new System.Drawing.Point(3, 70);
            this.panelDevices.Name = "panelDevices";
            this.panelDevices.Size = new System.Drawing.Size(351, 454);
            this.panelDevices.TabIndex = 1;
            this.panelDevices.Visible = false;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer7.Location = new System.Drawing.Point(3, 26);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.listViewDevices);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.listViewDevicesKeyboard);
            this.splitContainer7.Panel2.Controls.Add(this.label8);
            this.splitContainer7.Size = new System.Drawing.Size(345, 425);
            this.splitContainer7.SplitterDistance = 173;
            this.splitContainer7.TabIndex = 6;
            // 
            // listViewDevices
            // 
            this.listViewDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listViewDevices.FullRowSelect = true;
            this.listViewDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDevices.HideSelection = false;
            this.listViewDevices.Location = new System.Drawing.Point(3, 3);
            this.listViewDevices.MultiSelect = false;
            this.listViewDevices.Name = "listViewDevices";
            this.listViewDevices.Size = new System.Drawing.Size(339, 166);
            this.listViewDevices.SmallImageList = this.imageListTreeViewKey;
            this.listViewDevices.TabIndex = 5;
            this.listViewDevices.UseCompatibleStateImageBehavior = false;
            this.listViewDevices.View = System.Windows.Forms.View.Details;
            this.listViewDevices.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewDevices_ItemSelectionChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 200;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Device";
            this.columnHeader4.Width = 300;
            // 
            // listViewDevicesKeyboard
            // 
            this.listViewDevicesKeyboard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDevicesKeyboard.CheckBoxes = true;
            this.listViewDevicesKeyboard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.listViewDevicesKeyboard.FullRowSelect = true;
            this.listViewDevicesKeyboard.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDevicesKeyboard.HideSelection = false;
            this.listViewDevicesKeyboard.Location = new System.Drawing.Point(3, 23);
            this.listViewDevicesKeyboard.MultiSelect = false;
            this.listViewDevicesKeyboard.Name = "listViewDevicesKeyboard";
            this.listViewDevicesKeyboard.Size = new System.Drawing.Size(339, 222);
            this.listViewDevicesKeyboard.TabIndex = 5;
            this.listViewDevicesKeyboard.UseCompatibleStateImageBehavior = false;
            this.listViewDevicesKeyboard.View = System.Windows.Forms.View.Details;
            this.listViewDevicesKeyboard.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewDevicesKeyboard_ItemCheck);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Name";
            this.columnHeader5.Width = 400;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Keystrokes from select device are sent to:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Devices";
            // 
            // panelKeyboardProperties
            // 
            this.panelKeyboardProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKeyboardProperties.Controls.Add(this.textBoxKeyboardName);
            this.panelKeyboardProperties.Controls.Add(this.label2);
            this.panelKeyboardProperties.Controls.Add(this.textBoxKeyboardDetails);
            this.panelKeyboardProperties.Controls.Add(this.labelCaptureAllKeys);
            this.panelKeyboardProperties.Controls.Add(this.checkBoxCaptureAllKeys);
            this.panelKeyboardProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelKeyboardProperties.Location = new System.Drawing.Point(91, 3);
            this.panelKeyboardProperties.Name = "panelKeyboardProperties";
            this.panelKeyboardProperties.Size = new System.Drawing.Size(419, 343);
            this.panelKeyboardProperties.TabIndex = 0;
            this.panelKeyboardProperties.Visible = false;
            // 
            // textBoxKeyboardName
            // 
            this.textBoxKeyboardName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyboardName.Location = new System.Drawing.Point(43, 57);
            this.textBoxKeyboardName.Name = "textBoxKeyboardName";
            this.textBoxKeyboardName.Size = new System.Drawing.Size(373, 21);
            this.textBoxKeyboardName.TabIndex = 1;
            this.textBoxKeyboardName.TextChanged += new System.EventHandler(this.textBoxKeyboardName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
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
            this.textBoxKeyboardDetails.Size = new System.Drawing.Size(413, 48);
            this.textBoxKeyboardDetails.TabIndex = 0;
            this.textBoxKeyboardDetails.Text = "Keyboard Details";
            this.textBoxKeyboardDetails.WordWrap = false;
            // 
            // labelCaptureAllKeys
            // 
            this.labelCaptureAllKeys.AutoSize = true;
            this.labelCaptureAllKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaptureAllKeys.Location = new System.Drawing.Point(39, 107);
            this.labelCaptureAllKeys.Name = "labelCaptureAllKeys";
            this.labelCaptureAllKeys.Size = new System.Drawing.Size(321, 13);
            this.labelCaptureAllKeys.TabIndex = 4;
            this.labelCaptureAllKeys.Text = "This prevents the application in focus recieving the raw keystroke";
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
            this.richTextBoxKeyDetector.Size = new System.Drawing.Size(232, 29);
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
            this.splitContainer2.Panel2.Controls.Add(this.buttonClear);
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(724, 602);
            this.splitContainer2.SplitterDistance = 497;
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
            this.splitContainer3.Panel1.Controls.Add(this.checkBoxCaptureLowLevel);
            this.splitContainer3.Panel1.Controls.Add(this.checkBoxMinimiseOnStart);
            this.splitContainer3.Panel1.Controls.Add(this.richTextBoxKeyDetector);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new System.Drawing.Size(724, 497);
            this.splitContainer3.SplitterDistance = 29;
            this.splitContainer3.TabIndex = 0;
            // 
            // checkBoxCaptureLowLevel
            // 
            this.checkBoxCaptureLowLevel.AutoSize = true;
            this.checkBoxCaptureLowLevel.Location = new System.Drawing.Point(241, 9);
            this.checkBoxCaptureLowLevel.Name = "checkBoxCaptureLowLevel";
            this.checkBoxCaptureLowLevel.Size = new System.Drawing.Size(71, 17);
            this.checkBoxCaptureLowLevel.TabIndex = 1;
            this.checkBoxCaptureLowLevel.Text = "Low level";
            this.checkBoxCaptureLowLevel.UseVisualStyleBackColor = true;
            this.checkBoxCaptureLowLevel.CheckedChanged += new System.EventHandler(this.checkBoxMinimiseOnStart_CheckedChanged);
            // 
            // checkBoxMinimiseOnStart
            // 
            this.checkBoxMinimiseOnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMinimiseOnStart.AutoSize = true;
            this.checkBoxMinimiseOnStart.Location = new System.Drawing.Point(611, 9);
            this.checkBoxMinimiseOnStart.Name = "checkBoxMinimiseOnStart";
            this.checkBoxMinimiseOnStart.Size = new System.Drawing.Size(108, 17);
            this.checkBoxMinimiseOnStart.TabIndex = 2;
            this.checkBoxMinimiseOnStart.Text = "Minimise On Start";
            this.checkBoxMinimiseOnStart.UseVisualStyleBackColor = true;
            this.checkBoxMinimiseOnStart.CheckedChanged += new System.EventHandler(this.checkBoxMinimiseOnStart_CheckedChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(644, 0);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 19);
            this.buttonClear.TabIndex = 1;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageMessages);
            this.tabControl1.Controls.Add(this.tabPageKeyEvents);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(724, 101);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageMessages
            // 
            this.tabPageMessages.Controls.Add(this.richTextBoxEvents);
            this.tabPageMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessages.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMessages.Name = "tabPageMessages";
            this.tabPageMessages.Size = new System.Drawing.Size(716, 75);
            this.tabPageMessages.TabIndex = 0;
            this.tabPageMessages.Text = "Messages";
            this.tabPageMessages.UseVisualStyleBackColor = true;
            // 
            // tabPageKeyEvents
            // 
            this.tabPageKeyEvents.Controls.Add(this.splitContainer5);
            this.tabPageKeyEvents.Location = new System.Drawing.Point(4, 22);
            this.tabPageKeyEvents.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageKeyEvents.Name = "tabPageKeyEvents";
            this.tabPageKeyEvents.Size = new System.Drawing.Size(798, 144);
            this.tabPageKeyEvents.TabIndex = 1;
            this.tabPageKeyEvents.Text = "Key Events";
            this.tabPageKeyEvents.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.label6);
            this.splitContainer5.Panel1.Controls.Add(this.richTextBoxKeyEventsLowLevel);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer5.Size = new System.Drawing.Size(798, 144);
            this.splitContainer5.SplitterDistance = 264;
            this.splitContainer5.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Low Level Events";
            // 
            // richTextBoxKeyEventsLowLevel
            // 
            this.richTextBoxKeyEventsLowLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxKeyEventsLowLevel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxKeyEventsLowLevel.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.richTextBoxKeyEventsLowLevel.Location = new System.Drawing.Point(0, 16);
            this.richTextBoxKeyEventsLowLevel.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBoxKeyEventsLowLevel.Name = "richTextBoxKeyEventsLowLevel";
            this.richTextBoxKeyEventsLowLevel.Size = new System.Drawing.Size(260, 124);
            this.richTextBoxKeyEventsLowLevel.TabIndex = 0;
            this.richTextBoxKeyEventsLowLevel.Text = "";
            this.richTextBoxKeyEventsLowLevel.WordWrap = false;
            // 
            // splitContainer6
            // 
            this.splitContainer6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.label5);
            this.splitContainer6.Panel1.Controls.Add(this.richTextBoxKeyEventsHook);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.label7);
            this.splitContainer6.Panel2.Controls.Add(this.richTextBoxKeyEventsWMInput);
            this.splitContainer6.Size = new System.Drawing.Size(530, 144);
            this.splitContainer6.SplitterDistance = 267;
            this.splitContainer6.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Application Level Events";
            // 
            // richTextBoxKeyEventsHook
            // 
            this.richTextBoxKeyEventsHook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxKeyEventsHook.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxKeyEventsHook.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.richTextBoxKeyEventsHook.Location = new System.Drawing.Point(0, 16);
            this.richTextBoxKeyEventsHook.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBoxKeyEventsHook.Name = "richTextBoxKeyEventsHook";
            this.richTextBoxKeyEventsHook.Size = new System.Drawing.Size(263, 124);
            this.richTextBoxKeyEventsHook.TabIndex = 0;
            this.richTextBoxKeyEventsHook.Text = "";
            this.richTextBoxKeyEventsHook.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Raw Input Events";
            // 
            // richTextBoxKeyEventsWMInput
            // 
            this.richTextBoxKeyEventsWMInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxKeyEventsWMInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxKeyEventsWMInput.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.richTextBoxKeyEventsWMInput.Location = new System.Drawing.Point(0, 16);
            this.richTextBoxKeyEventsWMInput.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBoxKeyEventsWMInput.Name = "richTextBoxKeyEventsWMInput";
            this.richTextBoxKeyEventsWMInput.Size = new System.Drawing.Size(255, 124);
            this.richTextBoxKeyEventsWMInput.TabIndex = 0;
            this.richTextBoxKeyEventsWMInput.Text = "";
            this.richTextBoxKeyEventsWMInput.WordWrap = false;
            // 
            // timerMinimiseOnStart
            // 
            this.timerMinimiseOnStart.Tick += new System.EventHandler(this.timerMinimiseOnStart_Tick);
            // 
            // KeyboardRedirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 602);
            this.Controls.Add(this.splitContainer2);
            this.Name = "KeyboardRedirectorForm";
            this.Text = "Keyboard Redirector";
            this.Load += new System.EventHandler(this.KeyboardRedirectorForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardRedirectorForm_FormClosing);
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripTreeViewEvents.ResumeLayout(false);
            this.panelKeyProperties.ResumeLayout(false);
            this.panelKeyProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAntiRepeat)).EndInit();
            this.groupBoxActions.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.panelDevices.ResumeLayout(false);
            this.panelDevices.PerformLayout();
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.Panel2.PerformLayout();
            this.splitContainer7.ResumeLayout(false);
            this.panelKeyboardProperties.ResumeLayout(false);
            this.panelKeyboardProperties.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageMessages.ResumeLayout(false);
            this.tabPageKeyEvents.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            this.splitContainer6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBoxEx richTextBoxEvents;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewKeys;
        private System.Windows.Forms.Panel panelKeyProperties;
        private System.Windows.Forms.Panel panelKeyboardProperties;
        private System.Windows.Forms.Label labelCaptureAllKeys;
        private System.Windows.Forms.CheckBox checkBoxCaptureAllKeys;
        private System.Windows.Forms.Label labelKeyCapture;
        private System.Windows.Forms.CheckBox checkBoxKeyEnabled;
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
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.ListView listViewActions;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonAddAction;
        private System.Windows.Forms.Button buttonRemoveAction;
        private System.Windows.Forms.Button buttonEditAction;
        private System.Windows.Forms.Button buttonAddApplications;
        private BindingListView listViewApplicationsInFocus;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ImageList imageListApplications;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageMessages;
        private System.Windows.Forms.TabPage tabPageKeyEvents;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private RichTextBoxEx richTextBoxKeyEventsLowLevel;
        private RichTextBoxEx richTextBoxKeyEventsHook;
        private RichTextBoxEx richTextBoxKeyEventsWMInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonRemoveApplication;
        private System.Windows.Forms.Label labelFocussedAppInfo;
        private System.Windows.Forms.CheckBox checkBoxCaptureLowLevel;
        private System.Windows.Forms.CheckBox checkBoxKeyCapture;
        private System.Windows.Forms.ToolStripMenuItem removeAllKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeActionsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDownAntiRepeat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewDevices;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelDevices;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.ListView listViewDevicesKeyboard;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label8;
    }
}
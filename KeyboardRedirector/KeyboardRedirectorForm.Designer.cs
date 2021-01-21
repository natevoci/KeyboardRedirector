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
            this.openSettingsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewKeys = new System.Windows.Forms.TreeView();
            this.contextMenuStripTreeViewEvents = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemImportExportSplitter = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTreeViewKey = new System.Windows.Forms.ImageList(this.components);
            this.panelKeyProperties = new System.Windows.Forms.Panel();
            this.numericUpDownAntiRepeat = new System.Windows.Forms.NumericUpDown();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.labelFocussedAppInfo = new System.Windows.Forms.Label();
            this.listViewApplicationsInFocus = new KeyboardRedirector.BindingListView();
            this.imageListApplications = new System.Windows.Forms.ImageList(this.components);
            this.buttonRemoveApplication = new System.Windows.Forms.Button();
            this.buttonEditApplication = new System.Windows.Forms.Button();
            this.buttonAddApplications = new System.Windows.Forms.Button();
            this.buttonAddAction = new System.Windows.Forms.Button();
            this.listViewActions = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonEditApplications = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.listViewDevices = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewDevicesKeyboard = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label8 = new System.Windows.Forms.Label();
            this.panelKeyboardProperties = new System.Windows.Forms.Panel();
            this.textBoxKeyboardName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKeyboardDetails = new System.Windows.Forms.TextBox();
            this.labelCaptureAllKeys = new System.Windows.Forms.Label();
            this.checkBoxCaptureAllKeys = new System.Windows.Forms.CheckBox();
            this.richTextBoxKeyDetector = new System.Windows.Forms.RichTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.buttonSetting = new System.Windows.Forms.Button();
            this.checkBoxLogging = new System.Windows.Forms.CheckBox();
            this.checkBoxCaptureLowLevel = new System.Windows.Forms.CheckBox();
            this.checkBoxMinimiseOnStart = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayLogMessages = new System.Windows.Forms.CheckBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageMessages = new System.Windows.Forms.TabPage();
            this.contextMenuStripNotifyIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripTreeViewEvents.SuspendLayout();
            this.panelKeyProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAntiRepeat)).BeginInit();
            this.groupBoxActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panelDevices.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.panelKeyboardProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageMessages.SuspendLayout();
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
            this.openSettingsFolderToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStripNotifyIcon.Name = "contextMenuStripNotifyIcon";
            this.contextMenuStripNotifyIcon.Size = new System.Drawing.Size(200, 76);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // openSettingsFolderToolStripMenuItem
            // 
            this.openSettingsFolderToolStripMenuItem.Name = "openSettingsFolderToolStripMenuItem";
            this.openSettingsFolderToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.openSettingsFolderToolStripMenuItem.Text = "Open Settings Folder";
            this.openSettingsFolderToolStripMenuItem.Click += new System.EventHandler(this.openSettingsFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
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
            this.splitContainer1.Size = new System.Drawing.Size(724, 418);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewKeys
            // 
            this.treeViewKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewKeys.BackColor = System.Drawing.Color.White;
            this.treeViewKeys.ContextMenuStrip = this.contextMenuStripTreeViewEvents;
            this.treeViewKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewKeys.FullRowSelect = true;
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
            this.treeViewKeys.ShowLines = false;
            this.treeViewKeys.Size = new System.Drawing.Size(281, 408);
            this.treeViewKeys.TabIndex = 0;
            this.treeViewKeys.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewKeys_BeforeSelect);
            this.treeViewKeys.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewKeys_AfterSelect);
            this.treeViewKeys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeViewKeys_KeyUp);
            this.treeViewKeys.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewKeys_MouseDown);
            // 
            // contextMenuStripTreeViewEvents
            // 
            this.contextMenuStripTreeViewEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.removeAllKeysToolStripMenuItem,
            this.executeActionsToolStripMenuItem,
            this.toolStripMenuItemImportExportSplitter,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.contextMenuStripTreeViewEvents.Name = "contextMenuStripTreeViewEvents";
            this.contextMenuStripTreeViewEvents.Size = new System.Drawing.Size(173, 120);
            this.contextMenuStripTreeViewEvents.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTreeViewEvents_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "Del";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // removeAllKeysToolStripMenuItem
            // 
            this.removeAllKeysToolStripMenuItem.Name = "removeAllKeysToolStripMenuItem";
            this.removeAllKeysToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.removeAllKeysToolStripMenuItem.Text = "Remove All Keys";
            this.removeAllKeysToolStripMenuItem.Click += new System.EventHandler(this.removeAllKeysToolStripMenuItem_Click);
            // 
            // executeActionsToolStripMenuItem
            // 
            this.executeActionsToolStripMenuItem.Name = "executeActionsToolStripMenuItem";
            this.executeActionsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.executeActionsToolStripMenuItem.Text = "Execute Actions";
            this.executeActionsToolStripMenuItem.Click += new System.EventHandler(this.executeActionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItemImportExportSplitter
            // 
            this.toolStripMenuItemImportExportSplitter.Name = "toolStripMenuItemImportExportSplitter";
            this.toolStripMenuItemImportExportSplitter.Size = new System.Drawing.Size(169, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
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
            this.panelKeyProperties.Controls.Add(this.numericUpDownAntiRepeat);
            this.panelKeyProperties.Controls.Add(this.groupBoxActions);
            this.panelKeyProperties.Controls.Add(this.textBoxKeyName);
            this.panelKeyProperties.Controls.Add(this.labelKeyDetails);
            this.panelKeyProperties.Controls.Add(this.label1);
            this.panelKeyProperties.Controls.Add(this.label4);
            this.panelKeyProperties.Controls.Add(this.labelKeyCapture);
            this.panelKeyProperties.Controls.Add(this.checkBoxKeyCapture);
            this.panelKeyProperties.Controls.Add(this.checkBoxKeyEnabled);
            this.panelKeyProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKeyProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelKeyProperties.Location = new System.Drawing.Point(0, 0);
            this.panelKeyProperties.Name = "panelKeyProperties";
            this.panelKeyProperties.Size = new System.Drawing.Size(425, 414);
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
            this.numericUpDownAntiRepeat.Location = new System.Drawing.Point(363, 51);
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
            this.groupBoxActions.Location = new System.Drawing.Point(3, 106);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(419, 306);
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
            this.splitContainer4.Panel1.Controls.Add(this.buttonEditApplication);
            this.splitContainer4.Panel1.Controls.Add(this.buttonAddApplications);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.buttonAddAction);
            this.splitContainer4.Panel2.Controls.Add(this.listViewActions);
            this.splitContainer4.Panel2.Controls.Add(this.buttonRemoveAction);
            this.splitContainer4.Panel2.Controls.Add(this.buttonEditAction);
            this.splitContainer4.Size = new System.Drawing.Size(413, 286);
            this.splitContainer4.SplitterDistance = 127;
            this.splitContainer4.TabIndex = 0;
            // 
            // labelFocussedAppInfo
            // 
            this.labelFocussedAppInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFocussedAppInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFocussedAppInfo.Location = new System.Drawing.Point(238, 27);
            this.labelFocussedAppInfo.Name = "labelFocussedAppInfo";
            this.labelFocussedAppInfo.Size = new System.Drawing.Size(172, 97);
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
            this.listViewApplicationsInFocus.Size = new System.Drawing.Size(229, 122);
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
            this.buttonRemoveApplication.Location = new System.Drawing.Point(346, 3);
            this.buttonRemoveApplication.Name = "buttonRemoveApplication";
            this.buttonRemoveApplication.Size = new System.Drawing.Size(64, 21);
            this.buttonRemoveApplication.TabIndex = 2;
            this.buttonRemoveApplication.Text = "Remove";
            this.buttonRemoveApplication.UseVisualStyleBackColor = true;
            this.buttonRemoveApplication.Click += new System.EventHandler(this.buttonRemoveApplication_Click);
            // 
            // buttonEditApplication
            // 
            this.buttonEditApplication.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditApplication.Location = new System.Drawing.Point(288, 3);
            this.buttonEditApplication.Name = "buttonEditApplication";
            this.buttonEditApplication.Size = new System.Drawing.Size(52, 21);
            this.buttonEditApplication.TabIndex = 1;
            this.buttonEditApplication.Text = "Edit";
            this.buttonEditApplication.UseVisualStyleBackColor = true;
            this.buttonEditApplication.Click += new System.EventHandler(this.buttonEditApplication_Click);
            // 
            // buttonAddApplications
            // 
            this.buttonAddApplications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddApplications.Location = new System.Drawing.Point(238, 3);
            this.buttonAddApplications.Name = "buttonAddApplications";
            this.buttonAddApplications.Size = new System.Drawing.Size(44, 21);
            this.buttonAddApplications.TabIndex = 1;
            this.buttonAddApplications.Text = "Add";
            this.buttonAddApplications.UseVisualStyleBackColor = true;
            this.buttonAddApplications.Click += new System.EventHandler(this.buttonAddApplications_Click);
            // 
            // buttonAddAction
            // 
            this.buttonAddAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddAction.Location = new System.Drawing.Point(200, 131);
            this.buttonAddAction.Name = "buttonAddAction";
            this.buttonAddAction.Size = new System.Drawing.Size(66, 21);
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
            this.listViewActions.Size = new System.Drawing.Size(407, 123);
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
            this.buttonRemoveAction.Location = new System.Drawing.Point(344, 131);
            this.buttonRemoveAction.Name = "buttonRemoveAction";
            this.buttonRemoveAction.Size = new System.Drawing.Size(66, 21);
            this.buttonRemoveAction.TabIndex = 3;
            this.buttonRemoveAction.Text = "Remove";
            this.buttonRemoveAction.UseVisualStyleBackColor = true;
            this.buttonRemoveAction.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonEditAction
            // 
            this.buttonEditAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditAction.Location = new System.Drawing.Point(272, 131);
            this.buttonEditAction.Name = "buttonEditAction";
            this.buttonEditAction.Size = new System.Drawing.Size(66, 21);
            this.buttonEditAction.TabIndex = 2;
            this.buttonEditAction.Text = "Edit";
            this.buttonEditAction.UseVisualStyleBackColor = true;
            this.buttonEditAction.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // textBoxKeyName
            // 
            this.textBoxKeyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyName.Location = new System.Drawing.Point(43, 26);
            this.textBoxKeyName.Name = "textBoxKeyName";
            this.textBoxKeyName.Size = new System.Drawing.Size(379, 21);
            this.textBoxKeyName.TabIndex = 2;
            this.textBoxKeyName.TextChanged += new System.EventHandler(this.textBoxKeyName_TextChanged);
            // 
            // labelKeyDetails
            // 
            this.labelKeyDetails.AutoSize = true;
            this.labelKeyDetails.Location = new System.Drawing.Point(3, 6);
            this.labelKeyDetails.Name = "labelKeyDetails";
            this.labelKeyDetails.Size = new System.Drawing.Size(60, 13);
            this.labelKeyDetails.TabIndex = 0;
            this.labelKeyDetails.Text = "Key Details";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Antirepeat time [ms]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Name";
            // 
            // labelKeyCapture
            // 
            this.labelKeyCapture.AutoSize = true;
            this.labelKeyCapture.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKeyCapture.Location = new System.Drawing.Point(39, 87);
            this.labelKeyCapture.Name = "labelKeyCapture";
            this.labelKeyCapture.Size = new System.Drawing.Size(321, 13);
            this.labelKeyCapture.TabIndex = 7;
            this.labelKeyCapture.Text = "This prevents the application in focus recieving the raw keystroke";
            // 
            // checkBoxKeyCapture
            // 
            this.checkBoxKeyCapture.AutoSize = true;
            this.checkBoxKeyCapture.Location = new System.Drawing.Point(9, 72);
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
            this.checkBoxKeyEnabled.Location = new System.Drawing.Point(9, 51);
            this.checkBoxKeyEnabled.Name = "checkBoxKeyEnabled";
            this.checkBoxKeyEnabled.Size = new System.Drawing.Size(64, 17);
            this.checkBoxKeyEnabled.TabIndex = 3;
            this.checkBoxKeyEnabled.Text = "Enabled";
            this.checkBoxKeyEnabled.UseVisualStyleBackColor = true;
            this.checkBoxKeyEnabled.CheckedChanged += new System.EventHandler(this.checkBoxKeyEnabled_CheckedChanged);
            // 
            // panelDevices
            // 
            this.panelDevices.Controls.Add(this.groupBox2);
            this.panelDevices.Controls.Add(this.groupBox1);
            this.panelDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDevices.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.panelDevices.Location = new System.Drawing.Point(0, 0);
            this.panelDevices.Name = "panelDevices";
            this.panelDevices.Size = new System.Drawing.Size(425, 414);
            this.panelDevices.TabIndex = 1;
            this.panelDevices.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonEditApplications);
            this.groupBox2.Location = new System.Drawing.Point(3, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 50);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Applications";
            // 
            // buttonEditApplications
            // 
            this.buttonEditApplications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditApplications.Location = new System.Drawing.Point(10, 18);
            this.buttonEditApplications.Name = "buttonEditApplications";
            this.buttonEditApplications.Size = new System.Drawing.Size(403, 21);
            this.buttonEditApplications.TabIndex = 0;
            this.buttonEditApplications.Text = "Edit Applications";
            this.buttonEditApplications.UseVisualStyleBackColor = true;
            this.buttonEditApplications.Click += new System.EventHandler(this.buttonEditApplications_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer7);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 353);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Devices";
            // 
            // splitContainer7
            // 
            this.splitContainer7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer7.Location = new System.Drawing.Point(10, 23);
            this.splitContainer7.Margin = new System.Windows.Forms.Padding(0);
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
            this.splitContainer7.Size = new System.Drawing.Size(406, 327);
            this.splitContainer7.SplitterDistance = 131;
            this.splitContainer7.TabIndex = 6;
            // 
            // listViewDevices
            // 
            this.listViewDevices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.listViewDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDevices.FullRowSelect = true;
            this.listViewDevices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDevices.HideSelection = false;
            this.listViewDevices.Location = new System.Drawing.Point(0, 0);
            this.listViewDevices.MultiSelect = false;
            this.listViewDevices.Name = "listViewDevices";
            this.listViewDevices.Size = new System.Drawing.Size(406, 131);
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
            this.listViewDevicesKeyboard.CheckBoxes = true;
            this.listViewDevicesKeyboard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.listViewDevicesKeyboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDevicesKeyboard.FullRowSelect = true;
            this.listViewDevicesKeyboard.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDevicesKeyboard.HideSelection = false;
            this.listViewDevicesKeyboard.Location = new System.Drawing.Point(0, 13);
            this.listViewDevicesKeyboard.MultiSelect = false;
            this.listViewDevicesKeyboard.Name = "listViewDevicesKeyboard";
            this.listViewDevicesKeyboard.Size = new System.Drawing.Size(406, 179);
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
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Keystrokes from select device are sent to:";
            // 
            // panelKeyboardProperties
            // 
            this.panelKeyboardProperties.Controls.Add(this.textBoxKeyboardName);
            this.panelKeyboardProperties.Controls.Add(this.label2);
            this.panelKeyboardProperties.Controls.Add(this.textBoxKeyboardDetails);
            this.panelKeyboardProperties.Controls.Add(this.labelCaptureAllKeys);
            this.panelKeyboardProperties.Controls.Add(this.checkBoxCaptureAllKeys);
            this.panelKeyboardProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKeyboardProperties.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelKeyboardProperties.Location = new System.Drawing.Point(0, 0);
            this.panelKeyboardProperties.Name = "panelKeyboardProperties";
            this.panelKeyboardProperties.Size = new System.Drawing.Size(425, 414);
            this.panelKeyboardProperties.TabIndex = 0;
            this.panelKeyboardProperties.Visible = false;
            // 
            // textBoxKeyboardName
            // 
            this.textBoxKeyboardName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxKeyboardName.Location = new System.Drawing.Point(43, 53);
            this.textBoxKeyboardName.Name = "textBoxKeyboardName";
            this.textBoxKeyboardName.Size = new System.Drawing.Size(379, 21);
            this.textBoxKeyboardName.TabIndex = 1;
            this.textBoxKeyboardName.TextChanged += new System.EventHandler(this.textBoxKeyboardName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
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
            this.textBoxKeyboardDetails.Size = new System.Drawing.Size(419, 44);
            this.textBoxKeyboardDetails.TabIndex = 0;
            this.textBoxKeyboardDetails.Text = "Keyboard Details";
            this.textBoxKeyboardDetails.WordWrap = false;
            // 
            // labelCaptureAllKeys
            // 
            this.labelCaptureAllKeys.AutoSize = true;
            this.labelCaptureAllKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaptureAllKeys.Location = new System.Drawing.Point(39, 99);
            this.labelCaptureAllKeys.Name = "labelCaptureAllKeys";
            this.labelCaptureAllKeys.Size = new System.Drawing.Size(321, 13);
            this.labelCaptureAllKeys.TabIndex = 4;
            this.labelCaptureAllKeys.Text = "This prevents the application in focus recieving the raw keystroke";
            // 
            // checkBoxCaptureAllKeys
            // 
            this.checkBoxCaptureAllKeys.AutoSize = true;
            this.checkBoxCaptureAllKeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCaptureAllKeys.Location = new System.Drawing.Point(6, 80);
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
            this.richTextBoxKeyDetector.Size = new System.Drawing.Size(232, 27);
            this.richTextBoxKeyDetector.TabIndex = 0;
            this.richTextBoxKeyDetector.Text = "Type here to detect keys";
            this.richTextBoxKeyDetector.GotFocus += new System.EventHandler(this.richTextBoxKeyDetector_GotFocus);
            this.richTextBoxKeyDetector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBoxKeyDetector_KeyDown);
            this.richTextBoxKeyDetector.LostFocus += new System.EventHandler(this.richTextBoxKeyDetector_LostFocus);
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
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxDisplayLogMessages);
            this.splitContainer2.Panel2.Controls.Add(this.buttonClear);
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(724, 556);
            this.splitContainer2.SplitterDistance = 451;
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
            this.splitContainer3.Panel1.Controls.Add(this.buttonSetting);
            this.splitContainer3.Panel1.Controls.Add(this.checkBoxLogging);
            this.splitContainer3.Panel1.Controls.Add(this.checkBoxCaptureLowLevel);
            this.splitContainer3.Panel1.Controls.Add(this.checkBoxMinimiseOnStart);
            this.splitContainer3.Panel1.Controls.Add(this.richTextBoxKeyDetector);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer3.Size = new System.Drawing.Size(724, 451);
            this.splitContainer3.SplitterDistance = 29;
            this.splitContainer3.TabIndex = 0;
            // 
            // buttonSetting
            // 
            this.buttonSetting.Location = new System.Drawing.Point(395, 4);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(61, 23);
            this.buttonSetting.TabIndex = 4;
            this.buttonSetting.Text = "Setting";
            this.buttonSetting.UseVisualStyleBackColor = true;
            this.buttonSetting.Click += new System.EventHandler(this.openSettingsFolderToolStripMenuItem_Click);
            // 
            // checkBoxLogging
            // 
            this.checkBoxLogging.AutoSize = true;
            this.checkBoxLogging.Location = new System.Drawing.Point(324, 8);
            this.checkBoxLogging.Name = "checkBoxLogging";
            this.checkBoxLogging.Size = new System.Drawing.Size(66, 16);
            this.checkBoxLogging.TabIndex = 3;
            this.checkBoxLogging.Text = "Logging";
            this.checkBoxLogging.UseVisualStyleBackColor = true;
            this.checkBoxLogging.CheckedChanged += new System.EventHandler(this.CheckBoxLogging_CheckedChanged);
            // 
            // checkBoxCaptureLowLevel
            // 
            this.checkBoxCaptureLowLevel.AutoSize = true;
            this.checkBoxCaptureLowLevel.Location = new System.Drawing.Point(241, 8);
            this.checkBoxCaptureLowLevel.Name = "checkBoxCaptureLowLevel";
            this.checkBoxCaptureLowLevel.Size = new System.Drawing.Size(78, 16);
            this.checkBoxCaptureLowLevel.TabIndex = 1;
            this.checkBoxCaptureLowLevel.Text = "Low level";
            this.checkBoxCaptureLowLevel.UseVisualStyleBackColor = true;
            this.checkBoxCaptureLowLevel.CheckedChanged += new System.EventHandler(this.checkBoxCaptureLowLevel_CheckedChanged);
            // 
            // checkBoxMinimiseOnStart
            // 
            this.checkBoxMinimiseOnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxMinimiseOnStart.AutoSize = true;
            this.checkBoxMinimiseOnStart.Location = new System.Drawing.Point(593, 8);
            this.checkBoxMinimiseOnStart.Name = "checkBoxMinimiseOnStart";
            this.checkBoxMinimiseOnStart.Size = new System.Drawing.Size(126, 16);
            this.checkBoxMinimiseOnStart.TabIndex = 2;
            this.checkBoxMinimiseOnStart.Text = "Minimise On Start";
            this.checkBoxMinimiseOnStart.UseVisualStyleBackColor = true;
            this.checkBoxMinimiseOnStart.CheckedChanged += new System.EventHandler(this.checkBoxMinimiseOnStart_CheckedChanged);
            // 
            // checkBoxDisplayLogMessages
            // 
            this.checkBoxDisplayLogMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDisplayLogMessages.AutoSize = true;
            this.checkBoxDisplayLogMessages.Location = new System.Drawing.Point(498, 1);
            this.checkBoxDisplayLogMessages.Name = "checkBoxDisplayLogMessages";
            this.checkBoxDisplayLogMessages.Size = new System.Drawing.Size(144, 16);
            this.checkBoxDisplayLogMessages.TabIndex = 2;
            this.checkBoxDisplayLogMessages.Text = "Display Log Messages";
            this.checkBoxDisplayLogMessages.UseVisualStyleBackColor = true;
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(644, 0);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 18);
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
            // KeyboardRedirectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 556);
            this.Controls.Add(this.splitContainer2);
            this.Name = "KeyboardRedirectorForm";
            this.Text = "Keyboard Redirector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardRedirectorForm_FormClosing);
            this.Load += new System.EventHandler(this.KeyboardRedirectorForm_Load);
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripTreeViewEvents.ResumeLayout(false);
            this.panelKeyProperties.ResumeLayout(false);
            this.panelKeyProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAntiRepeat)).EndInit();
            this.groupBoxActions.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.panelDevices.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.panelKeyboardProperties.ResumeLayout(false);
            this.panelKeyboardProperties.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageMessages.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelDevices;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.ListView listViewDevicesKeyboard;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemImportExportSplitter;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Button buttonEditApplication;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonEditApplications;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxDisplayLogMessages;
        private System.Windows.Forms.ToolStripMenuItem openSettingsFolderToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxLogging;
        private System.Windows.Forms.Button buttonSetting;
    }
}
namespace ApplicationLauncher
{
    partial class ShortcutsForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.objectListViewShortcuts = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = new BrightIdeasSoftware.OLVColumn();
            this.olvColumn2 = new BrightIdeasSoftware.OLVColumn();
            this.imageListShortcuts = new System.Windows.Forms.ImageList(this.components);
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.checkBoxSwitchTasks = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonIconBrowse = new System.Windows.Forms.Button();
            this.buttonStartInBrowse = new System.Windows.Forms.Button();
            this.buttonTargetBrowse = new System.Windows.Forms.Button();
            this.textBoxIcon = new System.Windows.Forms.TextBox();
            this.textBoxStartIn = new System.Windows.Forms.TextBox();
            this.textBoxExecutable = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewShortcuts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonMoveDown);
            this.splitContainer1.Panel1.Controls.Add(this.buttonMoveUp);
            this.splitContainer1.Panel1.Controls.Add(this.objectListViewShortcuts);
            this.splitContainer1.Panel1.Controls.Add(this.buttonRemove);
            this.splitContainer1.Panel1.Controls.Add(this.buttonAdd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxSwitchTasks);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSave);
            this.splitContainer1.Panel2.Controls.Add(this.buttonIconBrowse);
            this.splitContainer1.Panel2.Controls.Add(this.buttonStartInBrowse);
            this.splitContainer1.Panel2.Controls.Add(this.buttonTargetBrowse);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxIcon);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxStartIn);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxExecutable);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxName);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Size = new System.Drawing.Size(446, 411);
            this.splitContainer1.SplitterDistance = 231;
            this.splitContainer1.TabIndex = 0;
            // 
            // objectListViewShortcuts
            // 
            this.objectListViewShortcuts.AllColumns.Add(this.olvColumn1);
            this.objectListViewShortcuts.AllColumns.Add(this.olvColumn2);
            this.objectListViewShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListViewShortcuts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.objectListViewShortcuts.FullRowSelect = true;
            this.objectListViewShortcuts.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.objectListViewShortcuts.HideSelection = false;
            this.objectListViewShortcuts.ItemRenderer = null;
            this.objectListViewShortcuts.Location = new System.Drawing.Point(3, 3);
            this.objectListViewShortcuts.Name = "objectListViewShortcuts";
            this.objectListViewShortcuts.ShowGroups = false;
            this.objectListViewShortcuts.Size = new System.Drawing.Size(436, 192);
            this.objectListViewShortcuts.SmallImageList = this.imageListShortcuts;
            this.objectListViewShortcuts.TabIndex = 0;
            this.objectListViewShortcuts.UseCompatibleStateImageBehavior = false;
            this.objectListViewShortcuts.View = System.Windows.Forms.View.Details;
            this.objectListViewShortcuts.SelectedIndexChanged += new System.EventHandler(this.objectListViewShortcuts_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 144;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Executable";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Target";
            this.olvColumn2.Width = 153;
            // 
            // imageListShortcuts
            // 
            this.imageListShortcuts.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListShortcuts.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListShortcuts.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(364, 201);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 4;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(283, 201);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 3;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // checkBoxSwitchTasks
            // 
            this.checkBoxSwitchTasks.AutoSize = true;
            this.checkBoxSwitchTasks.Location = new System.Drawing.Point(87, 64);
            this.checkBoxSwitchTasks.Name = "checkBoxSwitchTasks";
            this.checkBoxSwitchTasks.Size = new System.Drawing.Size(206, 17);
            this.checkBoxSwitchTasks.TabIndex = 5;
            this.checkBoxSwitchTasks.Text = "Switch tasks instead if already running";
            this.checkBoxSwitchTasks.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(49, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(364, 139);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonIconBrowse
            // 
            this.buttonIconBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonIconBrowse.Location = new System.Drawing.Point(409, 113);
            this.buttonIconBrowse.Name = "buttonIconBrowse";
            this.buttonIconBrowse.Size = new System.Drawing.Size(30, 23);
            this.buttonIconBrowse.TabIndex = 11;
            this.buttonIconBrowse.Text = "...";
            this.buttonIconBrowse.UseVisualStyleBackColor = true;
            this.buttonIconBrowse.Click += new System.EventHandler(this.buttonIconBrowse_Click);
            // 
            // buttonStartInBrowse
            // 
            this.buttonStartInBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartInBrowse.Location = new System.Drawing.Point(409, 87);
            this.buttonStartInBrowse.Name = "buttonStartInBrowse";
            this.buttonStartInBrowse.Size = new System.Drawing.Size(30, 23);
            this.buttonStartInBrowse.TabIndex = 8;
            this.buttonStartInBrowse.Text = "...";
            this.buttonStartInBrowse.UseVisualStyleBackColor = true;
            this.buttonStartInBrowse.Click += new System.EventHandler(this.buttonStartInBrowse_Click);
            // 
            // buttonTargetBrowse
            // 
            this.buttonTargetBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTargetBrowse.Location = new System.Drawing.Point(409, 36);
            this.buttonTargetBrowse.Name = "buttonTargetBrowse";
            this.buttonTargetBrowse.Size = new System.Drawing.Size(30, 23);
            this.buttonTargetBrowse.TabIndex = 4;
            this.buttonTargetBrowse.Text = "...";
            this.buttonTargetBrowse.UseVisualStyleBackColor = true;
            this.buttonTargetBrowse.Click += new System.EventHandler(this.buttonTargetBrowse_Click);
            // 
            // textBoxIcon
            // 
            this.textBoxIcon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIcon.Location = new System.Drawing.Point(87, 115);
            this.textBoxIcon.Name = "textBoxIcon";
            this.textBoxIcon.Size = new System.Drawing.Size(316, 20);
            this.textBoxIcon.TabIndex = 10;
            // 
            // textBoxStartIn
            // 
            this.textBoxStartIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStartIn.Location = new System.Drawing.Point(87, 89);
            this.textBoxStartIn.Name = "textBoxStartIn";
            this.textBoxStartIn.Size = new System.Drawing.Size(316, 20);
            this.textBoxStartIn.TabIndex = 7;
            // 
            // textBoxExecutable
            // 
            this.textBoxExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExecutable.Location = new System.Drawing.Point(87, 38);
            this.textBoxExecutable.Name = "textBoxExecutable";
            this.textBoxExecutable.Size = new System.Drawing.Size(316, 20);
            this.textBoxExecutable.TabIndex = 3;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(87, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(352, 20);
            this.textBoxName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Icon:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Start in:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Location = new System.Drawing.Point(3, 201);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveUp.TabIndex = 1;
            this.buttonMoveUp.Text = "Move Up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Location = new System.Drawing.Point(84, 201);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveDown.TabIndex = 2;
            this.buttonMoveDown.Text = "Move Down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListIcons.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ShortcutsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 411);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ShortcutsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shortcuts";
            this.Load += new System.EventHandler(this.ShortcutsForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewShortcuts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonIconBrowse;
        private System.Windows.Forms.Button buttonStartInBrowse;
        private System.Windows.Forms.Button buttonTargetBrowse;
        private System.Windows.Forms.TextBox textBoxIcon;
        private System.Windows.Forms.TextBox textBoxStartIn;
        private System.Windows.Forms.TextBox textBoxExecutable;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonSave;
        private BrightIdeasSoftware.ObjectListView objectListViewShortcuts;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.ImageList imageListShortcuts;
        private System.Windows.Forms.CheckBox checkBoxSwitchTasks;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.ImageList imageListIcons;
    }
}
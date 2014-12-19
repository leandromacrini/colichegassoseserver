namespace PushNotificationServer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openServerConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.gridConfigurations = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.ConfigName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pushes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Errors = new System.Windows.Forms.DataGridViewImageColumn();
            this.Start = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Stop = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.trayMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConfigurations)).BeginInit();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "trayIcon";
            this.trayIcon.Visible = true;
            this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openServerConsoleToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(185, 54);
            // 
            // openServerConsoleToolStripMenuItem
            // 
            this.openServerConsoleToolStripMenuItem.Name = "openServerConsoleToolStripMenuItem";
            this.openServerConsoleToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openServerConsoleToolStripMenuItem.Text = "Open Server Console";
            this.openServerConsoleToolStripMenuItem.Click += new System.EventHandler(this.openServerConsoleToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.gridConfigurations);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 538);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Push Configurations";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(197, 489);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(121, 43);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "New Configuration";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // gridConfigurations
            // 
            this.gridConfigurations.AllowUserToAddRows = false;
            this.gridConfigurations.AllowUserToDeleteRows = false;
            this.gridConfigurations.AllowUserToResizeColumns = false;
            this.gridConfigurations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridConfigurations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridConfigurations.ColumnHeadersHeight = 30;
            this.gridConfigurations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridConfigurations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.ConfigName,
            this.Pushes,
            this.Errors,
            this.Start,
            this.Stop,
            this.Edit,
            this.Delete});
            this.gridConfigurations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridConfigurations.Location = new System.Drawing.Point(0, 19);
            this.gridConfigurations.MultiSelect = false;
            this.gridConfigurations.Name = "gridConfigurations";
            this.gridConfigurations.ReadOnly = true;
            this.gridConfigurations.RowHeadersVisible = false;
            this.gridConfigurations.Size = new System.Drawing.Size(514, 464);
            this.gridConfigurations.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(544, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 538);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server Statistics";
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 40;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ConfigName
            // 
            this.ConfigName.HeaderText = "Configuration Name";
            this.ConfigName.MinimumWidth = 50;
            this.ConfigName.Name = "ConfigName";
            this.ConfigName.ReadOnly = true;
            this.ConfigName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Pushes
            // 
            this.Pushes.HeaderText = "Pushes";
            this.Pushes.MinimumWidth = 80;
            this.Pushes.Name = "Pushes";
            this.Pushes.ReadOnly = true;
            this.Pushes.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Errors
            // 
            this.Errors.HeaderText = "Errors";
            this.Errors.MinimumWidth = 40;
            this.Errors.Name = "Errors";
            this.Errors.ReadOnly = true;
            this.Errors.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Errors.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Start
            // 
            this.Start.HeaderText = "Start";
            this.Start.MinimumWidth = 40;
            this.Start.Name = "Start";
            this.Start.ReadOnly = true;
            this.Start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Stop
            // 
            this.Stop.HeaderText = "Stop";
            this.Stop.MinimumWidth = 40;
            this.Stop.Name = "Stop";
            this.Stop.ReadOnly = true;
            this.Stop.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 40;
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.MinimumWidth = 40;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Push Notification Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.trayMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridConfigurations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem openServerConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gridConfigurations;
        private System.Windows.Forms.DataGridViewImageColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConfigName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pushes;
        private System.Windows.Forms.DataGridViewImageColumn Errors;
        private System.Windows.Forms.DataGridViewButtonColumn Start;
        private System.Windows.Forms.DataGridViewButtonColumn Stop;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}


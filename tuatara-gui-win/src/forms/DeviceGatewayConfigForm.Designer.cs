namespace tuatara_gui
{
    partial class DeviceGatewayConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceGatewayConfigForm));
            this.grpDevice = new System.Windows.Forms.GroupBox();
            this.grpIGDInfo = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.checkBoxFilter = new System.Windows.Forms.CheckBox();
            this.labelForwards = new System.Windows.Forms.Label();
            this.btnPortCollections = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRemoveForward = new System.Windows.Forms.Button();
            this.textDeviceExternalIP = new System.Windows.Forms.TextBox();
            this.btnAddForward = new System.Windows.Forms.Button();
            this.listViewDeviceMappings = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textDeviceModel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textDeviceName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpDevice.SuspendLayout();
            this.grpIGDInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDevice
            // 
            this.grpDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDevice.Controls.Add(this.grpIGDInfo);
            this.grpDevice.Controls.Add(this.textDeviceModel);
            this.grpDevice.Controls.Add(this.label5);
            this.grpDevice.Controls.Add(this.textDeviceName);
            this.grpDevice.Controls.Add(this.label4);
            this.grpDevice.Enabled = false;
            this.grpDevice.Location = new System.Drawing.Point(12, 12);
            this.grpDevice.Name = "grpDevice";
            this.grpDevice.Size = new System.Drawing.Size(538, 467);
            this.grpDevice.TabIndex = 4;
            this.grpDevice.TabStop = false;
            this.grpDevice.Text = "Device";
            // 
            // grpIGDInfo
            // 
            this.grpIGDInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIGDInfo.Controls.Add(this.btnRefresh);
            this.grpIGDInfo.Controls.Add(this.checkBoxFilter);
            this.grpIGDInfo.Controls.Add(this.labelForwards);
            this.grpIGDInfo.Controls.Add(this.btnPortCollections);
            this.grpIGDInfo.Controls.Add(this.label6);
            this.grpIGDInfo.Controls.Add(this.btnRemoveForward);
            this.grpIGDInfo.Controls.Add(this.textDeviceExternalIP);
            this.grpIGDInfo.Controls.Add(this.btnAddForward);
            this.grpIGDInfo.Controls.Add(this.listViewDeviceMappings);
            this.grpIGDInfo.Location = new System.Drawing.Point(10, 69);
            this.grpIGDInfo.Name = "grpIGDInfo";
            this.grpIGDInfo.Size = new System.Drawing.Size(518, 386);
            this.grpIGDInfo.TabIndex = 12;
            this.grpIGDInfo.TabStop = false;
            this.grpIGDInfo.Text = "Internet Gateway Info";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(10, 357);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 23);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // checkBoxFilter
            // 
            this.checkBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxFilter.AutoSize = true;
            this.checkBoxFilter.Checked = true;
            this.checkBoxFilter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFilter.Location = new System.Drawing.Point(407, 44);
            this.checkBoxFilter.Name = "checkBoxFilter";
            this.checkBoxFilter.Size = new System.Drawing.Size(105, 17);
            this.checkBoxFilter.TabIndex = 14;
            this.checkBoxFilter.Text = "Filter By Local IP";
            this.checkBoxFilter.UseVisualStyleBackColor = true;
            this.checkBoxFilter.CheckedChanged += new System.EventHandler(this.checkBoxFilter_CheckedChanged);
            // 
            // labelForwards
            // 
            this.labelForwards.AutoSize = true;
            this.labelForwards.Location = new System.Drawing.Point(7, 44);
            this.labelForwards.Name = "labelForwards";
            this.labelForwards.Size = new System.Drawing.Size(112, 13);
            this.labelForwards.TabIndex = 13;
            this.labelForwards.Text = "Current Port Forwards:";
            // 
            // btnPortCollections
            // 
            this.btnPortCollections.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPortCollections.Enabled = false;
            this.btnPortCollections.Location = new System.Drawing.Point(111, 357);
            this.btnPortCollections.Name = "btnPortCollections";
            this.btnPortCollections.Size = new System.Drawing.Size(127, 23);
            this.btnPortCollections.TabIndex = 12;
            this.btnPortCollections.Text = "Port Collections";
            this.btnPortCollections.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "External Gateway IPv4";
            // 
            // btnRemoveForward
            // 
            this.btnRemoveForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveForward.Enabled = false;
            this.btnRemoveForward.Location = new System.Drawing.Point(244, 357);
            this.btnRemoveForward.Name = "btnRemoveForward";
            this.btnRemoveForward.Size = new System.Drawing.Size(135, 23);
            this.btnRemoveForward.TabIndex = 11;
            this.btnRemoveForward.Text = "Remove Port Forward";
            this.btnRemoveForward.UseVisualStyleBackColor = true;
            this.btnRemoveForward.Click += new System.EventHandler(this.btnRemoveForward_Click);
            // 
            // textDeviceExternalIP
            // 
            this.textDeviceExternalIP.Location = new System.Drawing.Point(127, 17);
            this.textDeviceExternalIP.Name = "textDeviceExternalIP";
            this.textDeviceExternalIP.ReadOnly = true;
            this.textDeviceExternalIP.Size = new System.Drawing.Size(130, 20);
            this.textDeviceExternalIP.TabIndex = 8;
            // 
            // btnAddForward
            // 
            this.btnAddForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddForward.Location = new System.Drawing.Point(385, 357);
            this.btnAddForward.Name = "btnAddForward";
            this.btnAddForward.Size = new System.Drawing.Size(127, 23);
            this.btnAddForward.TabIndex = 10;
            this.btnAddForward.Text = "Add Port Forward";
            this.btnAddForward.UseVisualStyleBackColor = true;
            this.btnAddForward.Click += new System.EventHandler(this.btnAddForward_Click);
            // 
            // listViewDeviceMappings
            // 
            this.listViewDeviceMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDeviceMappings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader8});
            this.listViewDeviceMappings.FullRowSelect = true;
            this.listViewDeviceMappings.GridLines = true;
            this.listViewDeviceMappings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDeviceMappings.Location = new System.Drawing.Point(9, 63);
            this.listViewDeviceMappings.Name = "listViewDeviceMappings";
            this.listViewDeviceMappings.Size = new System.Drawing.Size(503, 288);
            this.listViewDeviceMappings.TabIndex = 7;
            this.listViewDeviceMappings.UseCompatibleStateImageBehavior = false;
            this.listViewDeviceMappings.View = System.Windows.Forms.View.Details;
            this.listViewDeviceMappings.SelectedIndexChanged += new System.EventHandler(this.listViewDeviceMappings_SelectedIndexChanged);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Name";
            this.columnHeader7.Width = 210;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Internal IPv4";
            this.columnHeader4.Width = 87;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Protocol";
            this.columnHeader5.Width = 55;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Internal Port";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "External Port";
            this.columnHeader8.Width = 74;
            // 
            // textDeviceModel
            // 
            this.textDeviceModel.Location = new System.Drawing.Point(47, 43);
            this.textDeviceModel.Name = "textDeviceModel";
            this.textDeviceModel.ReadOnly = true;
            this.textDeviceModel.Size = new System.Drawing.Size(278, 20);
            this.textDeviceModel.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Model";
            // 
            // textDeviceName
            // 
            this.textDeviceName.Location = new System.Drawing.Point(47, 17);
            this.textDeviceName.Name = "textDeviceName";
            this.textDeviceName.ReadOnly = true;
            this.textDeviceName.Size = new System.Drawing.Size(278, 20);
            this.textDeviceName.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Name";
            // 
            // DeviceGatewayConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 491);
            this.Controls.Add(this.grpDevice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(578, 529);
            this.Name = "DeviceGatewayConfigForm";
            this.Text = "Configure Gateway uPnP Device";
            this.Load += new System.EventHandler(this.DeviceGatewayConfigForm_Load);
            this.grpDevice.ResumeLayout(false);
            this.grpDevice.PerformLayout();
            this.grpIGDInfo.ResumeLayout(false);
            this.grpIGDInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDevice;
        private System.Windows.Forms.GroupBox grpIGDInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRemoveForward;
        private System.Windows.Forms.TextBox textDeviceExternalIP;
        private System.Windows.Forms.Button btnAddForward;
        private System.Windows.Forms.ListView listViewDeviceMappings;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TextBox textDeviceModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textDeviceName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPortCollections;
        private System.Windows.Forms.Label labelForwards;
        private System.Windows.Forms.CheckBox checkBoxFilter;
        private System.Windows.Forms.Button btnRefresh;
    }
}
namespace netgametools_csharp
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
            this.grpDevice = new System.Windows.Forms.GroupBox();
            this.groupIGDInfo = new System.Windows.Forms.GroupBox();
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
            this.groupIGDInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDevice
            // 
            this.grpDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpDevice.Controls.Add(this.groupIGDInfo);
            this.grpDevice.Controls.Add(this.textDeviceModel);
            this.grpDevice.Controls.Add(this.label5);
            this.grpDevice.Controls.Add(this.textDeviceName);
            this.grpDevice.Controls.Add(this.label4);
            this.grpDevice.Enabled = false;
            this.grpDevice.Location = new System.Drawing.Point(12, 12);
            this.grpDevice.Name = "grpDevice";
            this.grpDevice.Size = new System.Drawing.Size(535, 467);
            this.grpDevice.TabIndex = 4;
            this.grpDevice.TabStop = false;
            this.grpDevice.Text = "Device";
            // 
            // groupIGDInfo
            // 
            this.groupIGDInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupIGDInfo.Controls.Add(this.label6);
            this.groupIGDInfo.Controls.Add(this.btnRemoveForward);
            this.groupIGDInfo.Controls.Add(this.textDeviceExternalIP);
            this.groupIGDInfo.Controls.Add(this.btnAddForward);
            this.groupIGDInfo.Controls.Add(this.listViewDeviceMappings);
            this.groupIGDInfo.Location = new System.Drawing.Point(10, 69);
            this.groupIGDInfo.Name = "groupIGDInfo";
            this.groupIGDInfo.Size = new System.Drawing.Size(515, 386);
            this.groupIGDInfo.TabIndex = 12;
            this.groupIGDInfo.TabStop = false;
            this.groupIGDInfo.Text = "Internet Gateway Info";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "External IPv4";
            // 
            // btnRemoveForward
            // 
            this.btnRemoveForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveForward.Location = new System.Drawing.Point(82, 357);
            this.btnRemoveForward.Name = "btnRemoveForward";
            this.btnRemoveForward.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveForward.TabIndex = 11;
            this.btnRemoveForward.Text = "Remove";
            this.btnRemoveForward.UseVisualStyleBackColor = true;
            // 
            // textDeviceExternalIP
            // 
            this.textDeviceExternalIP.Location = new System.Drawing.Point(82, 17);
            this.textDeviceExternalIP.Name = "textDeviceExternalIP";
            this.textDeviceExternalIP.ReadOnly = true;
            this.textDeviceExternalIP.Size = new System.Drawing.Size(130, 20);
            this.textDeviceExternalIP.TabIndex = 8;
            // 
            // btnAddForward
            // 
            this.btnAddForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddForward.Location = new System.Drawing.Point(6, 357);
            this.btnAddForward.Name = "btnAddForward";
            this.btnAddForward.Size = new System.Drawing.Size(75, 23);
            this.btnAddForward.TabIndex = 10;
            this.btnAddForward.Text = "Add";
            this.btnAddForward.UseVisualStyleBackColor = true;
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
            this.listViewDeviceMappings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDeviceMappings.Location = new System.Drawing.Point(9, 43);
            this.listViewDeviceMappings.MultiSelect = false;
            this.listViewDeviceMappings.Name = "listViewDeviceMappings";
            this.listViewDeviceMappings.Size = new System.Drawing.Size(500, 308);
            this.listViewDeviceMappings.TabIndex = 7;
            this.listViewDeviceMappings.UseCompatibleStateImageBehavior = false;
            this.listViewDeviceMappings.View = System.Windows.Forms.View.Details;
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
            this.ClientSize = new System.Drawing.Size(559, 491);
            this.Controls.Add(this.grpDevice);
            this.Name = "DeviceGatewayConfigForm";
            this.Text = "DeviceGatewayConfigForm";
            this.grpDevice.ResumeLayout(false);
            this.grpDevice.PerformLayout();
            this.groupIGDInfo.ResumeLayout(false);
            this.groupIGDInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDevice;
        private System.Windows.Forms.GroupBox groupIGDInfo;
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
    }
}
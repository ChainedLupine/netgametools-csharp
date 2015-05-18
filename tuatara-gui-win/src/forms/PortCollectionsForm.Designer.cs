namespace tuatara_gui
{
    partial class PortCollectionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortCollectionsForm));
            this.grpPorts = new System.Windows.Forms.GroupBox();
            this.comboPortCollections = new System.Windows.Forms.ComboBox();
            this.btnRemoveFromDevice = new System.Windows.Forms.Button();
            this.buttonAddToDevice = new System.Windows.Forms.Button();
            this.grpPortDetails = new System.Windows.Forms.GroupBox();
            this.dataGridViewCollection = new System.Windows.Forms.DataGridView();
            this.ColumnProtocol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textCollectionTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPortCollectionRemove = new System.Windows.Forms.Button();
            this.btnPortCollectionAdd = new System.Windows.Forms.Button();
            this.grpPorts.SuspendLayout();
            this.grpPortDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPorts
            // 
            this.grpPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPorts.Controls.Add(this.comboPortCollections);
            this.grpPorts.Controls.Add(this.btnRemoveFromDevice);
            this.grpPorts.Controls.Add(this.buttonAddToDevice);
            this.grpPorts.Controls.Add(this.grpPortDetails);
            this.grpPorts.Controls.Add(this.btnPortCollectionRemove);
            this.grpPorts.Controls.Add(this.btnPortCollectionAdd);
            this.grpPorts.Location = new System.Drawing.Point(12, 12);
            this.grpPorts.Name = "grpPorts";
            this.grpPorts.Size = new System.Drawing.Size(478, 420);
            this.grpPorts.TabIndex = 5;
            this.grpPorts.TabStop = false;
            this.grpPorts.Text = "Port Collections";
            // 
            // comboPortCollections
            // 
            this.comboPortCollections.FormattingEnabled = true;
            this.comboPortCollections.Location = new System.Drawing.Point(7, 20);
            this.comboPortCollections.Name = "comboPortCollections";
            this.comboPortCollections.Size = new System.Drawing.Size(197, 21);
            this.comboPortCollections.TabIndex = 8;
            // 
            // btnRemoveFromDevice
            // 
            this.btnRemoveFromDevice.Enabled = false;
            this.btnRemoveFromDevice.Location = new System.Drawing.Point(142, 49);
            this.btnRemoveFromDevice.Name = "btnRemoveFromDevice";
            this.btnRemoveFromDevice.Size = new System.Drawing.Size(130, 23);
            this.btnRemoveFromDevice.TabIndex = 7;
            this.btnRemoveFromDevice.Text = "x Remove from Device";
            this.btnRemoveFromDevice.UseVisualStyleBackColor = true;
            // 
            // buttonAddToDevice
            // 
            this.buttonAddToDevice.Enabled = false;
            this.buttonAddToDevice.Location = new System.Drawing.Point(6, 47);
            this.buttonAddToDevice.Name = "buttonAddToDevice";
            this.buttonAddToDevice.Size = new System.Drawing.Size(130, 23);
            this.buttonAddToDevice.TabIndex = 6;
            this.buttonAddToDevice.Text = "<- Add to Device";
            this.buttonAddToDevice.UseVisualStyleBackColor = true;
            // 
            // grpPortDetails
            // 
            this.grpPortDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPortDetails.Controls.Add(this.dataGridViewCollection);
            this.grpPortDetails.Controls.Add(this.textCollectionTitle);
            this.grpPortDetails.Controls.Add(this.label1);
            this.grpPortDetails.Enabled = false;
            this.grpPortDetails.Location = new System.Drawing.Point(7, 78);
            this.grpPortDetails.Name = "grpPortDetails";
            this.grpPortDetails.Size = new System.Drawing.Size(465, 336);
            this.grpPortDetails.TabIndex = 5;
            this.grpPortDetails.TabStop = false;
            this.grpPortDetails.Text = "Collection";
            // 
            // dataGridViewCollection
            // 
            this.dataGridViewCollection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCollection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCollection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnProtocol,
            this.ColumnPort});
            this.dataGridViewCollection.Location = new System.Drawing.Point(6, 43);
            this.dataGridViewCollection.Name = "dataGridViewCollection";
            this.dataGridViewCollection.Size = new System.Drawing.Size(453, 287);
            this.dataGridViewCollection.TabIndex = 5;
            this.dataGridViewCollection.Visible = false;
            // 
            // ColumnProtocol
            // 
            this.ColumnProtocol.DataPropertyName = "Protocol";
            this.ColumnProtocol.HeaderText = "Protocol";
            this.ColumnProtocol.Items.AddRange(new object[] {
            "UDP",
            "TCP"});
            this.ColumnProtocol.Name = "ColumnProtocol";
            this.ColumnProtocol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnProtocol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ColumnPort
            // 
            this.ColumnPort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPort.DataPropertyName = "Port";
            this.ColumnPort.HeaderText = "Port";
            this.ColumnPort.MaxInputLength = 6;
            this.ColumnPort.Name = "ColumnPort";
            this.ColumnPort.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // textCollectionTitle
            // 
            this.textCollectionTitle.Location = new System.Drawing.Point(39, 17);
            this.textCollectionTitle.MaxLength = 50;
            this.textCollectionTitle.Name = "textCollectionTitle";
            this.textCollectionTitle.Size = new System.Drawing.Size(163, 20);
            this.textCollectionTitle.TabIndex = 4;
            this.textCollectionTitle.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Title";
            // 
            // btnPortCollectionRemove
            // 
            this.btnPortCollectionRemove.Enabled = false;
            this.btnPortCollectionRemove.Location = new System.Drawing.Point(268, 20);
            this.btnPortCollectionRemove.Name = "btnPortCollectionRemove";
            this.btnPortCollectionRemove.Size = new System.Drawing.Size(59, 23);
            this.btnPortCollectionRemove.TabIndex = 2;
            this.btnPortCollectionRemove.Text = "Remove";
            this.btnPortCollectionRemove.UseVisualStyleBackColor = true;
            // 
            // btnPortCollectionAdd
            // 
            this.btnPortCollectionAdd.Location = new System.Drawing.Point(210, 20);
            this.btnPortCollectionAdd.Name = "btnPortCollectionAdd";
            this.btnPortCollectionAdd.Size = new System.Drawing.Size(52, 23);
            this.btnPortCollectionAdd.TabIndex = 1;
            this.btnPortCollectionAdd.Text = "Add";
            this.btnPortCollectionAdd.UseVisualStyleBackColor = true;
            // 
            // PortCollectionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 476);
            this.Controls.Add(this.grpPorts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PortCollectionsForm";
            this.Text = "PortCollectionsForm";
            this.grpPorts.ResumeLayout(false);
            this.grpPortDetails.ResumeLayout(false);
            this.grpPortDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPorts;
        private System.Windows.Forms.ComboBox comboPortCollections;
        private System.Windows.Forms.Button btnRemoveFromDevice;
        private System.Windows.Forms.Button buttonAddToDevice;
        private System.Windows.Forms.GroupBox grpPortDetails;
        private System.Windows.Forms.DataGridView dataGridViewCollection;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnProtocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPort;
        private System.Windows.Forms.TextBox textCollectionTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPortCollectionRemove;
        private System.Windows.Forms.Button btnPortCollectionAdd;
    }
}
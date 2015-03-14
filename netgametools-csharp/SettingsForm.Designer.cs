namespace netgametools_csharp
{
    partial class SettingsForm
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
            this.checkBoxIGDOnly = new System.Windows.Forms.CheckBox();
            this.comboAdapters = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textCurrentIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxSkipSafety = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxIGDOnly
            // 
            this.checkBoxIGDOnly.AutoSize = true;
            this.checkBoxIGDOnly.Checked = true;
            this.checkBoxIGDOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIGDOnly.Location = new System.Drawing.Point(6, 29);
            this.checkBoxIGDOnly.Name = "checkBoxIGDOnly";
            this.checkBoxIGDOnly.Size = new System.Drawing.Size(192, 17);
            this.checkBoxIGDOnly.TabIndex = 12;
            this.checkBoxIGDOnly.Text = "List Internet Gateway Devices Only";
            this.checkBoxIGDOnly.UseVisualStyleBackColor = true;
            this.checkBoxIGDOnly.CheckedChanged += new System.EventHandler(this.checkBoxIGDOnly_CheckedChanged);
            // 
            // comboAdapters
            // 
            this.comboAdapters.FormattingEnabled = true;
            this.comboAdapters.Location = new System.Drawing.Point(136, 21);
            this.comboAdapters.Name = "comboAdapters";
            this.comboAdapters.Size = new System.Drawing.Size(298, 21);
            this.comboAdapters.TabIndex = 11;
            this.comboAdapters.SelectedIndexChanged += new System.EventHandler(this.comboAdapters_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Current Network Adapter";
            // 
            // textCurrentIP
            // 
            this.textCurrentIP.Location = new System.Drawing.Point(79, 48);
            this.textCurrentIP.Name = "textCurrentIP";
            this.textCurrentIP.ReadOnly = true;
            this.textCurrentIP.Size = new System.Drawing.Size(130, 20);
            this.textCurrentIP.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Current IPv4";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboAdapters);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textCurrentIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 84);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network Adapter";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxSkipSafety);
            this.groupBox2.Controls.Add(this.checkBoxIGDOnly);
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 110);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // checkBoxSkipSafety
            // 
            this.checkBoxSkipSafety.AutoSize = true;
            this.checkBoxSkipSafety.Checked = true;
            this.checkBoxSkipSafety.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSkipSafety.Location = new System.Drawing.Point(6, 52);
            this.checkBoxSkipSafety.Name = "checkBoxSkipSafety";
            this.checkBoxSkipSafety.Size = new System.Drawing.Size(119, 17);
            this.checkBoxSkipSafety.TabIndex = 13;
            this.checkBoxSkipSafety.Text = "Skip Safety Checks";
            this.checkBoxSkipSafety.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(385, 218);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 261);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SettingsForm";
            this.Text = "uPnP Config Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxIGDOnly;
        private System.Windows.Forms.ComboBox comboAdapters;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textCurrentIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox checkBoxSkipSafety;

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using chainedlupine.UPnP;

namespace netgametools_csharp
{
    public partial class AddPortForwardForm : Form
    {
        Device device;

        public AddPortForwardForm()
        {
            InitializeComponent();

        }
        
        public void Init (Device selectedDevice)
        {
            device = selectedDevice ;
            UpdateInternalExternal();
            numericUpDownInternal.Value = 80;
            comboBoxProtocol.Items.Clear();
            comboBoxProtocol.Items.Add("TCP");
            comboBoxProtocol.Items.Add("UDP");
            comboBoxProtocol.SelectedIndex = 0;
        }

        private void UpdateInternalExternal()
        {
            textInternal.Text = string.Format ("{0}:{1}", ProgramSettings.GetCurrentLocalIP().ToString(), numericUpDownInternal.Value.ToString());
            textExternal.Text = string.Format("{0}:{1}", DeviceGateway.GetExternalIP(device), numericUpDownExternal.Value.ToString());
        }

        private void numericUpDownInternal_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownExternal.Value = numericUpDownInternal.Value;
            UpdateInternalExternal();
        }

        private void numericUpDownExternal_ValueChanged(object sender, EventArgs e)
        {
            UpdateInternalExternal();
        }

        private void comboBoxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInternalExternal();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textName.Text.Length == 0)
            {
                MessageBox.Show("Name cannot be empty!");
                return;
            }

            DeviceGateway.AddPortMapping(device,
                remoteHost: DeviceGateway.GetExternalIP(device).ToString(), 
                externalPort: Convert.ToUInt16(numericUpDownExternal.Value), 
                protocol: comboBoxProtocol.SelectedItem.ToString(), 
                internalPort: Convert.ToUInt16(numericUpDownInternal.Value), 
                internalClient: ProgramSettings.GetCurrentLocalIP().ToString(), 
                desc: textName.Text
            );

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using chainedlupine.tuatara;
using System.Net.Sockets;
using System.Net;

namespace tuatara_gui
{
    public partial class DeviceGatewayConfigForm : Form
    {
        Device device;
        List<DeviceGatewayPortRecord> mappings = new List<DeviceGatewayPortRecord>();
       
        public DeviceGatewayConfigForm()
        {
            InitializeComponent();
        }

        public void Init (Device selectedDevice)
        {
            device = selectedDevice ;
        }

        private void LoadForwardsFromDevice()
        {
            mappings = DeviceGateway.GetPortMappingEntries(device);
        }

        private void LoadForwardsToList()
        {
            listViewDeviceMappings.Items.Clear();

            if (DeviceGateway.isGateway(device))
            {
                grpIGDInfo.Visible = true;

                foreach (DeviceGatewayPortRecord portRec in mappings)
                {
                    IPAddress currIP = IPAddress.Parse(portRec.InternalClient);
                    IPAddress localIP = ProgramSettings.GetCurrentLocalIP();
                    if (ProgramSettings.settings.FilterMappingsByLocalIP && currIP.ToString() != localIP.ToString())
                        continue;

                    ListViewItem item = new ListViewItem(portRec.Desc);
                    item.SubItems.Add(portRec.InternalClient);
                    item.SubItems.Add(portRec.Protocol);
                    item.SubItems.Add(portRec.InternalPort.ToString());
                    item.SubItems.Add(portRec.ExternalPort.ToString());
                    item.Tag = portRec.GetHashCode();
                    listViewDeviceMappings.Items.Add(item);
                }
            }
            else
                grpIGDInfo.Visible = false;

            btnRemoveForward.Enabled = false;
        }

        private void DeviceGatewayConfigForm_Load(object sender, EventArgs e)
        {
            grpDevice.Enabled = true;

            checkBoxFilter.Checked = ProgramSettings.settings.FilterMappingsByLocalIP;

            if (device != null)
            {

                LoadForwardsFromDevice();
                LoadForwardsToList();

                textDeviceModel.Text = device.descModelName;
                textDeviceName.Text = device.descFriendlyName;

                if (DeviceGateway.isGateway(device))
                {
                    textDeviceExternalIP.Text = DeviceGateway.GetExternalIP(device);
                }
                else
                    textDeviceExternalIP.Text = "None";

            }

            textDeviceName.Select(0, 0);
        }

        private void btnAddForward_Click(object sender, EventArgs e)
        {
            AddPortForwardForm form = new AddPortForwardForm();
            form.Init(device);

            form.Location = new Point(Location.X + 50, Location.Y + 50);

            form.ShowDialog();

            LoadForwardsFromDevice();
            LoadForwardsToList();
        }

        private void btnRemoveForward_Click(object sender, EventArgs e)
        {
            List<DeviceGatewayPortRecord> toDelete = new List<DeviceGatewayPortRecord>();
            string portStr = "";

            if (listViewDeviceMappings.SelectedIndices.Count > 0)
            {
                foreach (int index in listViewDeviceMappings.SelectedIndices)
                {
                    ListViewItem item = listViewDeviceMappings.Items[index];

                    foreach (DeviceGatewayPortRecord portRec in mappings)
                    {
                        if ((int)item.Tag == portRec.GetHashCode())
                        {
                            toDelete.Add(portRec);
                            portStr += string.Format("Internal {0}:{1} to External {2}:{3}, {4}\n", portRec.InternalClient,
                                portRec.InternalPort, DeviceGateway.GetExternalIP(device), portRec.ExternalPort, portRec.Protocol);
                        }
                    }

                }
            }

            if (MessageBox.Show ("Are you sure you want to remove these mappings?\n\n" + portStr, "Delete Port Mappings", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            foreach (DeviceGatewayPortRecord portRec in toDelete)
            {
                DeviceGateway.DeletePortMapping(device, portRec.RemoteHost, portRec.ExternalPort, portRec.Protocol);
            }

            LoadForwardsFromDevice();
            LoadForwardsToList();
        }

        private void listViewDeviceMappings_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveForward.Enabled = listViewDeviceMappings.SelectedIndices.Count > 0;

            if (listViewDeviceMappings.SelectedIndices.Count > 1)
                btnRemoveForward.Text = "Remove Port Forwards";
            else
                btnRemoveForward.Text = "Remove Port Forward";
        }

        private void checkBoxFilter_CheckedChanged(object sender, EventArgs e)
        {
            ProgramSettings.settings.FilterMappingsByLocalIP = checkBoxFilter.Checked;

            LoadForwardsToList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadForwardsFromDevice();
            LoadForwardsToList();
        }
    }
}

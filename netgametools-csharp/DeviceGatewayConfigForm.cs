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
    public partial class DeviceGatewayConfigForm : Form
    {
        Device device;
       
        public DeviceGatewayConfigForm()
        {
            InitializeComponent();
        }

        public void Init (Device selectedDevice)
        {
            device = selectedDevice ;
        }

        private void DeviceGatewayConfigForm_Load(object sender, EventArgs e)
        {
            grpDevice.Enabled = true;

            if (device != null)
            {
                listViewDeviceMappings.Items.Clear();

                if (DeviceGateway.isGateway(device))
                {
                    grpIGDInfo.Visible = true;
                    List<DeviceGatewayPortRecord> mappings = DeviceGateway.GetPortMappingEntries(device);

                    foreach (DeviceGatewayPortRecord portRec in mappings)
                    {
                        ListViewItem item = new ListViewItem(portRec.Desc);
                        item.SubItems.Add(portRec.InternalClient);
                        item.SubItems.Add(portRec.Protocol);
                        item.SubItems.Add(portRec.InternalPort.ToString());
                        item.SubItems.Add(portRec.ExternalPort.ToString());
                        listViewDeviceMappings.Items.Add(item);
                    }
                }
                else
                    grpIGDInfo.Visible = false;
            }
        }
    }
}

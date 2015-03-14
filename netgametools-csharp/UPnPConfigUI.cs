using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using chainedlupine.UPnP;
using System.Diagnostics;

namespace netgametools_csharp
{
    enum ePortCollectionProtocol { TCP, UDP } ;

    public partial class UPnPConfigUI : Form
    {

        public UPnPConfigUI()
        {
            ProgramSettings.Init();

            InitializeComponent();

        }

        private void On_Load(object sender, EventArgs e)
        {
        }

        private void BuildSelectedDeviceList()
        {
            listViewDevices.Items.Clear();

            if (ProgramSettings.cp.knownDeviceList == null)
                return;

            foreach (Device device in ProgramSettings.cp.knownDeviceList)
            {
                if (ProgramSettings.settings.ShowOnlyNetworkDevices && !DeviceGateway.isGateway(device))
                    continue;

                ListViewItem item = new ListViewItem(device.descFriendlyName);
                item.SubItems.Add(device.descModelName);
                item.SubItems.Add(device.descManufacturer);

                if (DeviceGateway.isGateway(device))
                {
                    item.SubItems.Add(DeviceGateway.GetExternalIP(device));
                    //Debug.WriteLine(string.Format("ports mapped={0}", DeviceGateway.GetPortMappingNumberOfEntries(device)));
                    //List<DeviceGatewayPortRecord> mappings = DeviceGateway.GetPortMappingEntries(device);
                } 
                else
                    item.SubItems.Add("None");

                item.SubItems.Add(device.uuid);

                listViewDevices.Items.Add(item);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            WriteStatus("Searching!", Color.Blue);
            using (SearchBusyForm busyForm = new SearchBusyForm())
            {
                Form darker;

                darker = new Form();
                darker.ControlBox = darker.MinimizeBox = false;
                darker.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                darker.Text = "";
                darker.BackColor = Color.Black;
                darker.Opacity = 0.7f;
                darker.Show();
                darker.Size = ClientSize;
                darker.Location = PointToScreen(Point.Empty);

                busyForm.StartPosition = FormStartPosition.Manual;
                busyForm.Location = new Point(this.Location.X + (this.Width - busyForm.Width) / 2, this.Location.Y + (this.Height - busyForm.Height) / 2);
                busyForm.Show();
                busyForm.Update();


                if (ProgramSettings.cp.FindAllDevices())
                {
                    WriteStatus(string.Format("Found {0} devices on network!", ProgramSettings.cp.knownDeviceList.Count));
                    BuildSelectedDeviceList();

                }
                else
                {
                    WriteStatus("Unable to find any uPnP-enabled devices!", Color.Red);
                }

                busyForm.Close();
                darker.Close();
            }
        }

        private void WriteStatus (string text, Color? c = null)
        {
            textStatus.Text = text;
            textStatus.ForeColor = c ?? Color.Black;
            textStatus.Invalidate();
            textStatus.Update();
            textStatus.Refresh();
            Application.DoEvents();
        }

        private void checkBoxIGDOnly_CheckedChanged(object sender, EventArgs e)
        {
            BuildSelectedDeviceList();
        }

        private void listViewDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConfig.Enabled = listViewDevices.SelectedItems.Count > 0; 
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();

            ProgramSettings.CenterFormToParentClientArea(this, form);
            form.ShowDialog();

            BuildSelectedDeviceList();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            string uuid = listViewDevices.SelectedItems[0].SubItems[4].Text;

            Device selectedDevice = ProgramSettings.SelectDeviceByUUID(uuid);

            DeviceGatewayConfigForm form = new DeviceGatewayConfigForm();
            form.Init(selectedDevice);

            //ProgramSettings.CenterFormToParentClientArea(this, form);
            form.ShowDialog(); 
        }


    }

    class PortCollectionDetail
    {
        public ePortCollectionProtocol Protocol { get; set; }
        public ushort Port { get; set; }

        public PortCollectionDetail()
        {
            this.Protocol = ePortCollectionProtocol.TCP;
            this.Port = 0;
        }

        public PortCollectionDetail(ePortCollectionProtocol protocol, ushort port)
        {
            this.Protocol = protocol;
            this.Port = port;
        }
    }


}

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

namespace netgametools_csharp
{
    enum ePortCollectionProtocol { TCP, UDP } ;

    public partial class UPnPConfigUI : Form
    {
        private BindingSource pcBindingSource = new BindingSource();

        ControlPoint cp = new ControlPoint();        

        public UPnPConfigUI()
        {
            InitializeComponent();

            pcBindingSource.DataSource = typeof(PortCollectionDetail);
            pcBindingSource.AllowNew = true;

            DataGridViewComboBoxColumn col = dataGridViewCollection.Columns[0] as DataGridViewComboBoxColumn;
            col.ValueType = typeof(ePortCollectionProtocol);
            col.DataSource = Enum.GetValues(typeof(ePortCollectionProtocol));

            dataGridViewCollection.AutoGenerateColumns = false;
            dataGridViewCollection.DataSource = pcBindingSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*try
            {
                Console.WriteLine(UPnP.NAT.Discover());
                System.Windows.Forms.MessageBox.Show("You have an UPnP-enabled router and your IP is: " + UPnP.NAT.GetExternalIP());
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("You do not have an UPnP-enabled router.");
            }
            */
        }


        private void On_Load(object sender, EventArgs e)
        {

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType != NetworkInterfaceType.Loopback && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            comboAdapters.Items.Add(item.Name);
                        }
                    }
                }
            }

            if (comboAdapters.Items.Count > 0)
                comboAdapters.SelectedIndex = 0;
        }

        private void comboAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            textCurrentIP.Text = "NO IPV4 ADDRESS";

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.Name.Contains (comboAdapters.Text))
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            textCurrentIP.Text = ip.Address.ToString() ;
                        }
                    }
                }
            }

        }

        private void comboPortCollections_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox control = (ComboBox)sender;

            if (control.SelectedIndex >= 0)
            {
                btnPortCollectionRemove.Enabled = true;
                grpPortDetails.Enabled = true;
                textCollectionTitle.Text = comboPortCollections.Text;

                // Load datagrid
                // Protocol (TCP/UDP), Port
                pcBindingSource.Clear();
                pcBindingSource.Add(new PortCollectionDetail(ePortCollectionProtocol.TCP, 1000));
                pcBindingSource.Add(new PortCollectionDetail(ePortCollectionProtocol.UDP, 5000));

                dataGridViewCollection.Visible = true;
            }
            else
            {
                grpPortDetails.Enabled = false;
                btnPortCollectionRemove.Enabled = false;
                dataGridViewCollection.Visible = false;
            }
                
        }

        private void btnPortCollectionAdd_Click(object sender, EventArgs e)
        {
            comboPortCollections.Items.Add("New Collection");
            comboPortCollections.SelectedIndex = comboPortCollections.Items.Count - 1;
        }

        private void btnPortCollectionRemove_Click(object sender, EventArgs e)
        {
            comboPortCollections.Items.RemoveAt(comboPortCollections.SelectedIndex);

            if (comboPortCollections.Items.Count > 0)
                comboPortCollections.SelectedIndex = 0;
            else
            {
                grpPortDetails.Enabled = false;
                dataGridViewCollection.Visible = false;
                comboPortCollections.Text = "";
            }
        }

        private void textCollectionTitle_TextChanged(object sender, EventArgs e)
        {
            if (textCollectionTitle.Text.Length > 0)

                comboPortCollections.Items[comboPortCollections.SelectedIndex] = textCollectionTitle.Text;
            else
                textCollectionTitle.Text = comboPortCollections.Text;
        }

        private void BuildSelectedDeviceList()
        {
            grpDevice.Enabled = false;
            listViewDevices.Items.Clear();

            foreach (Device device in cp.knownDeviceList)
            {
                if (checkBoxIGDOnly.Checked && !DeviceGateway.isGateway(device))
                    continue;

                ListViewItem item = new ListViewItem(device.descFriendlyName);
                item.SubItems.Add(device.descModelName);
                item.SubItems.Add(device.descManufacturer);

                if (DeviceGateway.isGateway(device))
                    item.SubItems.Add(DeviceGateway.GetExternalIP(device));
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


                if (cp.FindAllDevices())
                {
                    WriteStatus(string.Format("Found {0} devices on network!", cp.knownDeviceList.Count));
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

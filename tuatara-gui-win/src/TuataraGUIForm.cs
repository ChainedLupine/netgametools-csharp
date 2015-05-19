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
using chainedlupine.tuatara;
using System.Diagnostics;
using System.Threading;

namespace tuatara_gui
{
    enum ePortCollectionProtocol { TCP, UDP } ;

    public partial class TuataraGUIForm : Form
    {
        private BackgroundWorker _bgWorker;

        public TuataraGUIForm()
        {
            ProgramSettings.Init();

            InitializeComponent();

            // Set form name to be our version info
            this.Text += string.Format(" v{0}", Application.ProductVersion);

        }

        private void On_Load(object sender, EventArgs e)
        {
        }

        private void BuildSelectedDeviceList()
        {
            if (_bgWorker != null)
                return;
            
            listViewDevices.Items.Clear();
            //ProgramSettings.debugConsoleForm.ClearDevices();

            if (ProgramSettings.controlPoint.knownDeviceList == null)
                return;

            foreach (Device device in ProgramSettings.controlPoint.knownDeviceList)
            {
                //ProgramSettings.debugConsoleForm.LoadDeviceXml(device);


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

        private void bgWorkerLoaduPnPDevices(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker ;

            ProgramSettings.controlPoint.FindAllDevicesThreadStart(ProgramSettings.settings.StrictProtocolMode);

            while (ProgramSettings.controlPoint.FindAllDevicesThreadRun() && !bgWorker.CancellationPending)
            {
                //Thread.Sleep(10);
            }

            ProgramSettings.controlPoint.FindAllDevicesThreadGetResult();
        }

        private void bgWorkerLoadCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            _bgWorker = null;
            btnSearch.Enabled = true;
            BuildSelectedDeviceList();
            progressBar.Style = ProgressBarStyle.Continuous;
            WriteStatus(string.Format("Found {0} uPnP devices on network!", ProgramSettings.controlPoint.knownDeviceList.Count));
            btnSearch.Text = "Search";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (_bgWorker != null)
            {
                _bgWorker.CancelAsync();
                _bgWorker = null;
                btnSearch.Text = "Search";
                WriteStatus("Search cancelled!", Color.Red);
                progressBar.Style = ProgressBarStyle.Continuous;
                return;
            }

            btnSearch.Text = "Cancel";

            WriteStatus("Searching!", Color.Blue);

            //btnSearch.Enabled = false;
            //listViewDevices.Items.Clear();

            progressBar.Style = ProgressBarStyle.Marquee;

            // Create loading thread
            _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerSupportsCancellation = true;
            _bgWorker.DoWork += new DoWorkEventHandler(bgWorkerLoaduPnPDevices);
            _bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorkerLoadCompleted);
            _bgWorker.RunWorkerAsync();

        }

        private void WriteStatus (string text, Color? c = null)
        {
            labelStatus.Text = text;
            labelStatus.ForeColor = c ?? Color.Black;
            //textStatus.Invalidate();
            //textStatus.Update();
            //textStatus.Refresh();
            //Application.DoEvents();
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
            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(Location.X + 50, Location.Y + 50);
            form.Init(selectedDevice);

            //ProgramSettings.CenterFormToParentClientArea(this, form);
            form.ShowDialog(); 
        }

        private void consoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramSettings.debugConsoleForm.Show();
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

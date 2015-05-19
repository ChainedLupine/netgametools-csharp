using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net ;
using chainedlupine.tuatara;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace tuatara_gui
{

    static class ControlExtensions
    {
        static public void UIThread(this Control control, Action code)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(code);
                return;
            }
            code.Invoke();
        }

        static public void UIThreadInvoke(this Control control, Action code)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(code);
                return;
            }
            code.Invoke();
        }
    }

    class NetworkAdapterDetail
    {
        public string name ;
        public IPAddress address ;
    }

    [XmlType("Settings")]
    public class Settings
    {
        [XmlAttribute("Version", DataType = "string")]
        public string Version { get; set; }

        [XmlElement("ShowOnlyNetworkDevices")]
        public bool ShowOnlyNetworkDevices { get; set; }

        [XmlElement("StrictProtocolMode")]
        public bool StrictProtocolMode { get; set; }

        [XmlElement("FilterMappingsByLocalIP")]
        public bool FilterMappingsByLocalIP { get; set; }

        [XmlElement("AlwaysShowDebugConsole")]
        public bool AlwaysShowDebugConsole { get; set; }

        [XmlElement("SelectedAdapterName")]
        public string SelectedAdapterName;


        public Settings()
        {
            Version = "1";
            ShowOnlyNetworkDevices = true;
            StrictProtocolMode = false;
            FilterMappingsByLocalIP = true;
            AlwaysShowDebugConsole = false;
            SelectedAdapterName = "None";
        }
    }

    class ProgramSettings
    {
        const string SettingsDirectory = "TuataraGUI" ;
        const string SettingsFile = "settings.xml" ;

        static public Settings settings = new Settings();

        static public List<NetworkAdapterDetail> adapters = new List<NetworkAdapterDetail>() ;

        static public ControlPoint controlPoint;

        static public DebugConsoleForm debugConsoleForm;

        static public void Init()
        {
            controlPoint = new ControlPoint();
            LoadNetworkInterfaces();

            debugConsoleForm = new DebugConsoleForm();
            IntPtr dummyHandle = debugConsoleForm.Handle; // This should trigger the form to create, and therefore invokes will fire

            // Link loggers to debug form
            debugConsoleForm.LinkLogger();

            if (settings.AlwaysShowDebugConsole)
                debugConsoleForm.Show();
        }

        static public void CenterFormToParentClientArea(Form parent, Form child)
        {
            child.StartPosition = FormStartPosition.Manual;
            child.Location = new Point(parent.Location.X + (parent.Width - child.Width) / 2, parent.Location.Y + (parent.Height - child.Height) / 2);
        }

        static public Device SelectDeviceByUUID(string uuid)
        {
            return controlPoint.FindDeviceByUUID(uuid);
        }

        static public IPAddress GetCurrentLocalIP()
        {
            return GetSelectedAdapterDetails(settings.SelectedAdapterName).address;
        }

        static public NetworkAdapterDetail GetSelectedAdapterDetails(string name)
        {
            foreach (NetworkAdapterDetail adapter in adapters)
            {
                if (adapter.name == name)
                    return adapter;
            }

            return null;
        }

        static public void LoadNetworkInterfaces()
        {
            adapters.Clear() ;

            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType != NetworkInterfaceType.Loopback && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            NetworkAdapterDetail detail = new NetworkAdapterDetail() ;
                            detail.address = IPAddress.Parse (ip.Address.ToString()) ;
                            detail.name = item.Name ;
                            adapters.Add(detail);
                        }
                    }
                }
            }

            if (GetSelectedAdapterDetails(settings.SelectedAdapterName) == null)
            {
                settings.SelectedAdapterName = adapters[0].name;
            }

         //   if (comboAdapters.Items.Count > 0)
          //      comboAdapters.SelectedIndex = 0;

        }

        public static void SaveSettings()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Path.Combine(SettingsDirectory, SettingsFile));

            (new FileInfo(fileName)).Directory.Create();

            Type[] extraSerializeTypes = { };
            XmlSerializer serializer = new XmlSerializer(typeof(Settings), extraSerializeTypes);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, settings);
            fs.Close(); 

        }

        public static void LoadSettings()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Path.Combine(SettingsDirectory, SettingsFile));

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                Type[] extraSerializeTypes = { };
                XmlSerializer serializer = new XmlSerializer(typeof(Settings), extraSerializeTypes); 
                settings = (Settings)serializer.Deserialize(fs);
            } 
            catch
            {
                settings = new Settings();
            }

        }
    }
}

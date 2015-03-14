using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net ;
using chainedlupine.UPnP;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace netgametools_csharp
{
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

        [XmlElement("SkipSafetyChecks")]
        public bool SkipSafetyChecks { get; set; }

        [XmlElement("FilterMappingsByLocalIP")]
        public bool FilterMappingsByLocalIP { get; set; }

        public Settings()
        {
            Version = "1";
            ShowOnlyNetworkDevices = true;
            SkipSafetyChecks = false;
            FilterMappingsByLocalIP = true;
        }
    }

    class ProgramSettings
    {
        static public Settings settings = new Settings();

        static public List<NetworkAdapterDetail> adapters = new List<NetworkAdapterDetail>() ;

        static public ControlPoint cp;

        static public int selectedAdapter;

        static public void Init()
        {
            cp = new ControlPoint();
            LoadNetworkInterfaces();

        }

        static public void CenterFormToParentClientArea(Form parent, Form child)
        {
            child.StartPosition = FormStartPosition.Manual;
            child.Location = new Point(parent.Location.X + (parent.Width - child.Width) / 2, parent.Location.Y + (parent.Height - child.Height) / 2);
        }

        static public Device SelectDeviceByUUID(string uuid)
        {
            return cp.FindDeviceByUUID(uuid);
        }

        static public IPAddress GetCurrentLocalIP()
        {
            return adapters[selectedAdapter].address;
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

            selectedAdapter = 0;

         //   if (comboAdapters.Items.Count > 0)
          //      comboAdapters.SelectedIndex = 0;

        }

        public static void SaveSettings()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Path.Combine("UPnPConfig", "settings.xml"));

            Type[] extraSerializeTypes = { };
            XmlSerializer serializer = new XmlSerializer(typeof(Settings), extraSerializeTypes);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, settings);
            fs.Close(); 

            /*
            XDocument doc = new XDocument(
                new XElement("settings",
                    new XAttribute ("version", 1),
                    new XElement("optionShowOnlyNetworkDevices", optionShowOnlyNetworkDevices.ToString()),
                    new XElement("optionSkipSafetyChecks", optionSkipSafetyChecks.ToString()),
                    new XElement("optionFilterMappingsByLocalIP", optionFilterMappingsByLocalIP.ToString())
                    )
                );

            (new FileInfo(fileName)).Directory.Create();

            doc.Save(fileName) ; */


        }

        public static void LoadSettings()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Path.Combine ("UPnPConfig", "settings.xml"));

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

            /*try
            {
                XDocument doc = XDocument.Load(fileName);

                XElement Xsettings = doc.Element("settings");

                optionShowOnlyNetworkDevices = Convert.ToBoolean(Xsettings.Element("optionShowOnlyNetworkDevices").Value);
                optionSkipSafetyChecks = Convert.ToBoolean(Xsettings.Element("optionSkipSafetyChecks").Value);
                optionFilterMappingsByLocalIP = Convert.ToBoolean(Xsettings.Element("optionFilterMappingsByLocalIP").Value);
            }
            catch
            {
                return;
            }*/

        }
    }
}

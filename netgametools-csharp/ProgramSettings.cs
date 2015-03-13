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

namespace netgametools_csharp
{
    class NetworkAdapterDetail
    {
        public string name ;
        public IPAddress address ;
    }

    class ProgramSettings
    {
        static public List<NetworkAdapterDetail> adapters = new List<NetworkAdapterDetail>() ;

        static public bool optionShowOnlyNetworkDevices = true;

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
    }
}

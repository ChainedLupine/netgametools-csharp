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

namespace netgametools_csharp
{
    class NetworkAdapterDetail
    {
        public string name ;
        public IPAddress address ;
    }

    class NetworkControl
    {
        List<NetworkAdapterDetail> adapters = new List<NetworkAdapterDetail>() ;

        public bool optionShowOnlyNetworkDevices = true;

        public void LoadNetworkInterfaces()
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
                        }
                    }
                }
            }

         //   if (comboAdapters.Items.Count > 0)
          //      comboAdapters.SelectedIndex = 0;

        }
    }
}

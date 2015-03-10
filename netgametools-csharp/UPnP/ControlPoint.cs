using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chainedlupine.UPnP
{
    public class ControlPoint
    {
        public List<Device> knownDeviceList { get { return _uPnPDevices; } }

        private List<Device> _uPnPDevices;

        private SSDP _ssdpDiscoverer ;

        public ControlPoint()
        {
            _ssdpDiscoverer = new SSDP();

            Service.RegisterInterfaces();
        }

        public bool FindAllDevices()
        {
            _uPnPDevices = _ssdpDiscoverer.Discover(SSDP.DEVICETYPE_ROOTDEVICE);

            return _uPnPDevices.Count > 0;
        }

        public Device FindDeviceByUUID(string uuid)
        {
            foreach (Device device in _uPnPDevices)
            {
                if (device.uuid == uuid)
                    return device;
            }
            return null;
        }


    }


}

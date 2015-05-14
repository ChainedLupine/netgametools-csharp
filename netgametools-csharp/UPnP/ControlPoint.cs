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

        public bool FindAllDevices(bool safetyChecks = true)
        {
            Logger.WriteLine("FindAllDevices is looking, safetyChecks=" + (safetyChecks ? "T" : "F"));

            _uPnPDevices = _ssdpDiscoverer.Discover(SSDP.DEVICETYPE_ROOTDEVICE, safetyChecks: safetyChecks);

            Logger.WriteLine(string.Format("FindAllDevices found {0} devices.", _uPnPDevices.Count));

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

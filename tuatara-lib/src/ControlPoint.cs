using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace chainedlupine.tuatara
{
    public class ControlPoint
    {
        public List<Device> knownDeviceList { get { if (_uPnPDevices != null) return _uPnPDevices.ToList(); else return null; } }

        private DeviceList _uPnPDevices;

        private SSDP _ssdpDiscoverer ;

        int DevicesCount { get { return _uPnPDevices.Count ; } }

        public ControlPoint()
        {
            Service.RegisterInterfaces();
        }

        public bool FindAllDevices(bool strictMode = true)
        {
            SSDP ssdpDiscoverer = new SSDP() ;

            Logger.WriteLine("FindAllDevices is looking, strictMode=" + (strictMode ? "T" : "F"));

            ssdpDiscoverer.Discover(SSDP.DEVICETYPE_ROOTDEVICE, strictMode: strictMode);

            while (ssdpDiscoverer.Run())
                Thread.Sleep(250);

            _uPnPDevices = ssdpDiscoverer.GetResultDeviceList();

            ssdpDiscoverer.Dispose();

            Logger.WriteLine(string.Format("FindAllDevices found {0} devices.", _uPnPDevices.Count));

            return _uPnPDevices.Count > 0;
        }

        public void FindAllDevicesThreadStart(bool strictMode = true)
        {
            if (_ssdpDiscoverer != null)
                _ssdpDiscoverer.Dispose();

            _ssdpDiscoverer = new SSDP();

            Logger.WriteLine("FindAllDevices(Threaded) is looking, strictMode=" + (strictMode ? "T" : "F"));

            _ssdpDiscoverer.Discover(SSDP.DEVICETYPE_ROOTDEVICE, strictMode: strictMode);
        }

        public bool FindAllDevicesThreadRun ()
        {
            return _ssdpDiscoverer.Run();
        }

        public void FindAllDevicesThreadGetResult()
        {
            _uPnPDevices = _ssdpDiscoverer.GetResultDeviceList();
            _ssdpDiscoverer.Dispose();
            _ssdpDiscoverer = null;
        }

        public Device FindDeviceByUUID(string uuid)
        {
            return _uPnPDevices.GetByUUID(uuid);
        }


    }


}

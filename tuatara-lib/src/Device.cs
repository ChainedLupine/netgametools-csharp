using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace chainedlupine.tuatara
{

    public static class Extensions
    {
        public static string AsString(this XmlDocument xmlDoc)
        {

            StringBuilder sb = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (var xmlWriter = XmlWriter.Create(sb, settings))
            {
                xmlDoc.Save(xmlWriter);
            }

            return sb.ToString() ;
        }

        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public static XDocument ToXDocument(this XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }
    }
    
    public class Device
    {

        public enum DeviceTypeEnum { InternetGatewayDevice, Unknown } ;


        public Uri deviceUri;

        public string uuid ;

        public Dictionary<string, string> discoveredKeys = new Dictionary<string, string>();

        public string descFriendlyName;
        public string descManufacturer;
        public string descModelName;
        public string descModelNumber;
        public string descModelDesc;
        public string descSerialNumber;

        public IPAddress cachedExternalIP;

        public DeviceTypeEnum deviceType;

        public List<Service> services = new List<Service>();

        public XDocument debugRawXml;

        private void setDeviceType (string desc)
        {
            deviceType = DeviceTypeEnum.Unknown;

            if (desc.Contains ("urn:schemas-upnp-org:device:InternetGatewayDevice"))
            {
                deviceType = DeviceTypeEnum.InternetGatewayDevice;
            }
        }

        public void retrieveDeviceProfile(bool strictMode = true)
        {
            XDocument xDeviceProfile ;

            Logger.WriteLine("Retriving device profile from " + deviceUri.ToString());
            try
            {
                WebRequest req = WebRequest.Create(deviceUri.ToString()) ; 
                req.Timeout = 1000 * 2 ;
                xDeviceProfile = XDocument.Load(req.GetResponse().GetResponseStream());
            }
            catch
            {
                throw new TuataraException("Unable to load device description XML from " + deviceUri.ToString());
            }

            debugRawXml = xDeviceProfile;

            XNamespace deviceNs = "urn:schemas-upnp-org:device-1-0";

            XElement xDevice = xDeviceProfile.Element (deviceNs + "root").Element(deviceNs + "device");

            XElement xNode = xDevice.Element(deviceNs + "deviceType");
            setDeviceType(xNode.Value);

            descFriendlyName = xDevice.Element(deviceNs + "friendlyName").Value;
            descManufacturer = xDevice.Element(deviceNs + "manufacturer").Value;
            descModelName = xDevice.Element(deviceNs + "manufacturer").Value;
            descModelNumber = xDevice.Element(deviceNs + "manufacturer").Value;
            descModelDesc = xDevice.Element(deviceNs + "manufacturer").Value;
            descSerialNumber = xDevice.Element(deviceNs + "manufacturer").Value;

            MatchCollection matches = Regex.Matches(xDevice.Element(deviceNs + "UDN").Value, @"^uuid:([\w\d-]+)");

            if (matches.Count == 1 && matches[0].Groups.Count == 2)
            {
                uuid = matches[0].Groups[1].Value;
            }
            else
                uuid = null;

            IEnumerable<XElement> xServiceDescs = xDevice.Descendants(deviceNs + "service");

            services = Service.LoadServices(deviceUri, deviceNs, xServiceDescs, strictMode);
        }
    }

    public class DeviceGatewayPortRecord
    {
        public string RemoteHost;
        public ushort ExternalPort;
        public string Protocol;
        public ushort InternalPort;
        public string InternalClient;
        public bool Enabled;
        public string Desc;
        public int LeaseDuration;
    }


    public class DeviceGateway
    {
        static public bool isGateway(Device device)
        {
            return device.deviceType == Device.DeviceTypeEnum.InternetGatewayDevice;
        }

        static public string GetExternalIP(Device device)
        {
            if (!isGateway(device))
                throw new TuataraException("This device is not an internet gateway!");

            if (device.cachedExternalIP != null)
                return device.cachedExternalIP.ToString();

            Service service = Service.GetServiceOfType(device.services, "urn:schemas-upnp-org:service:WANIPConnection:1");
            if (service == null)
                throw new TuataraException("No WANIPConnection service on this device!");

            IServiceWANIPConnection wanService = service.serviceInterface as IServiceWANIPConnection;

            //Debug.WriteLine (string.Format ("External IP = {0}", wanService.getExternalIP()));

            device.cachedExternalIP = IPAddress.Parse (wanService.GetExternalIP()) ;

            return device.cachedExternalIP.ToString();
        }

        static public int GetPortMappingNumberOfEntries(Device device)
        {
            if (!isGateway(device))
                throw new TuataraException("This device is not an internet gateway!");

            Service service = Service.GetServiceOfType(device.services, "urn:schemas-upnp-org:service:WANIPConnection:1");
            if (service == null)
                throw new TuataraException("No WANIPConnection service on this device!");

            IServiceWANIPConnection wanService = service.serviceInterface as IServiceWANIPConnection;

            //Logger.WriteLine (string.Format ("External IP = {0}", wanService.GetExternalIP()));

            return wanService.GetPortMappingNumberOfEntries();
        }

        static public List<DeviceGatewayPortRecord> GetPortMappingEntries(Device device)
        {
            if (!isGateway(device))
                throw new TuataraException("This device is not an internet gateway!");

            List<DeviceGatewayPortRecord> mappings = new List<DeviceGatewayPortRecord>();

            Service service = Service.GetServiceOfType(device.services, "urn:schemas-upnp-org:service:WANIPConnection:1");
            if (service == null)
                throw new TuataraException("No WANIPConnection service on this device!");

            IServiceWANIPConnection wanService = service.serviceInterface as IServiceWANIPConnection;

            try
            {
                mappings = wanService.GetPortMappingEntries();
            }
            catch (Exception e)
            {
                Logger.WriteLine(string.Format("Failure to map ports: {0}", e.Message));
            }

            return mappings;
        }

        static public void DeletePortMapping(Device device, string remoteHost, ushort externalPort, string protocol)
        {
            if (!isGateway(device))
                throw new TuataraException("This device is not an internet gateway!");

            Service service = Service.GetServiceOfType(device.services, "urn:schemas-upnp-org:service:WANIPConnection:1");
            if (service == null)
                throw new TuataraException("No WANIPConnection service on this device!");

            IServiceWANIPConnection wanService = service.serviceInterface as IServiceWANIPConnection;

            //Debug.WriteLine (string.Format ("External IP = {0}", wanService.getExternalIP()));

            wanService.DeletePortMapping(remoteHost, externalPort, protocol);
        }

        static public void AddPortMapping(Device device, string remoteHost, ushort externalPort, string protocol, ushort internalPort, string internalClient, string desc)
        {
            if (!isGateway(device))
                throw new TuataraException("This device is not an internet gateway!");

            Service service = Service.GetServiceOfType(device.services, "urn:schemas-upnp-org:service:WANIPConnection:1");
            if (service == null)
                throw new TuataraException("No WANIPConnection service on this device!");

            IServiceWANIPConnection wanService = service.serviceInterface as IServiceWANIPConnection;

            //Debug.WriteLine (string.Format ("External IP = {0}", wanService.getExternalIP()));

            wanService.AddPortMapping(remoteHost, externalPort, protocol, internalPort, internalClient, desc);

        }

    }
}

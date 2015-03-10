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

namespace chainedlupine.UPnP
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

        public DeviceTypeEnum deviceType;

        public List<Service> services = new List<Service>();

        private void setDeviceType (string desc)
        {
            deviceType = DeviceTypeEnum.Unknown;

            if (desc.Contains ("urn:schemas-upnp-org:device:InternetGatewayDevice"))
            {
                deviceType = DeviceTypeEnum.InternetGatewayDevice;
            }
        }

        public void retrieveDeviceProfile()
        {
            XDocument xDeviceProfile ;

            try
            {
                 xDeviceProfile = XDocument.Load(WebRequest.Create(deviceUri.ToString()).GetResponse().GetResponseStream());
            }
            catch
            {
                throw new Exception("Unable to load device description XML from " + deviceUri.ToString());
            }

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

            services = Service.LoadServices(deviceUri, deviceNs, xServiceDescs);
        }
    }

    public class DeviceGateway
    {
        static public bool isGateway(Device device)
        {
            return device.deviceType == Device.DeviceTypeEnum.InternetGatewayDevice;
        }

        static public string GetExternalIP(Device device)
        {
            Service test = Service.GetServiceOfType(device.services, "urn:schemas-upnp-org:service:WANIPConnection:1");
            IServiceWANIPConnection wanService = test.serviceInterface as IServiceWANIPConnection;

            //Debug.WriteLine (string.Format ("External IP = {0}", wanService.getExternalIP()));

            return wanService.getExternalIP() ;
        }

    }
}

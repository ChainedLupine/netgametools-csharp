using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
    }
    class Device
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

        public string descDeviceType;
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
            XmlDocument profileXML = new XmlDocument();
            try
            {
                profileXML.Load(WebRequest.Create(deviceUri.ToString()).GetResponse().GetResponseStream());
            }
            catch
            {
                throw new Exception("Unable to load device description XML from " + deviceUri.ToString());
            }

            //Debug.WriteLine (profileXML.AsString()) ;
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(profileXML.NameTable);
            nsMgr.AddNamespace("tns", "urn:schemas-upnp-org:device-1-0");
                
            // get core description
            XmlNode node = profileXML.SelectSingleNode("//tns:device/tns:deviceType/text()", nsMgr);
            descDeviceType = node.InnerText;
            setDeviceType(node.InnerText);

            node = profileXML.SelectSingleNode("//tns:device/tns:friendlyName/text()", nsMgr);
            descFriendlyName = node.InnerText;

            node = profileXML.SelectSingleNode("//tns:device/tns:manufacturer/text()", nsMgr);
            descManufacturer = node.InnerText;

            node = profileXML.SelectSingleNode("//tns:device/tns:modelName/text()", nsMgr);
            descModelName = node.InnerText;

            node = profileXML.SelectSingleNode("//tns:device/tns:modelNumber/text()", nsMgr);
            descModelNumber = node.InnerText;

            node = profileXML.SelectSingleNode("//tns:device/tns:modelDescription/text()", nsMgr);
            descModelDesc = node.InnerText;

            node = profileXML.SelectSingleNode("//tns:device/tns:serialNumber/text()", nsMgr);
            descSerialNumber = node.InnerText;

            node = profileXML.SelectSingleNode("//tns:device/tns:UDN/text()", nsMgr);

            MatchCollection matches = Regex.Matches (node.InnerText, @"^uuid:([\w\d-]+)") ;

            if (matches.Count == 1 && matches[0].Groups.Count == 2)
            {
                uuid = matches[0].Groups[1].Value;
            }
            else
                uuid = null;

            XmlNodeList xnlServiceDescList = profileXML.SelectNodes("//tns:device/tns:serviceList/descendant::tns:service", nsMgr);
            foreach (XmlNode xnServiceNode in xnlServiceDescList)
            {
                XmlNode xnServiceType = xnServiceNode.SelectSingleNode("tns:serviceType/text()", nsMgr);
                XmlNode xnServiceUrl = xnServiceNode.SelectSingleNode("tns:SCPDURL/text()", nsMgr);
                Uri descriptionUrl = new Uri(deviceUri, xnServiceUrl.InnerText);

                Debug.WriteLine(string.Format("service {0} desc url={1}", xnServiceType.InnerText, descriptionUrl.ToString()));

                if (xnServiceType.InnerText.Contains ("urn:schemas-upnp-org:service:WANIPConnection"))
                {
                    ServiceWANIPConnection service = new ServiceWANIPConnection(deviceUri, profileXML, nsMgr, xnServiceNode);
                    services.Add(service);

                    XmlDocument woo = new XmlDocument();
                    XmlElement action = woo.CreateElement("u", "GetExternalIPAddress", "urn:schemas-upnp-org:service:WANIPConnection:1");
                    woo.AppendChild(action);

                    XmlDocument resp = service.ExecSOAPRequest(woo);
                    Debug.WriteLine(resp.AsString());
                }

                /*XmlNode xnServiceUrl = xnServiceNode.SelectSingleNode("tns:SCPDURL/text()", nsMgr);
                Uri descriptionUrl = new Uri(deviceUri, xnServiceUrl.InnerText);

                XmlNode xnServiceType = xnServiceNode.SelectSingleNode("tns:serviceType/text()", nsMgr);

                Debug.WriteLine(string.Format("service {0} desc url={1}", xnServiceType.InnerText, descriptionUrl.ToString()));

                XmlDocument serviceDescXML = new XmlDocument();
                serviceDescXML.Load(WebRequest.Create(descriptionUrl.ToString()).GetResponse().GetResponseStream());
                //Debug.WriteLine(serviceDescXML.AsString());
                 * */
            }

        }
    }
}

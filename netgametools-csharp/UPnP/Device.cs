using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Net;
using System.Diagnostics;
using System.IO;

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

        public Uri deviceUri;

        public string uuid ;

        public Dictionary<string, string> discoveredKeys = new Dictionary<string, string>();

        public bool retrieveProfile()
        {
            try
            {
                XmlDocument profileXML = new XmlDocument();
                profileXML.Load(WebRequest.Create(deviceUri.ToString()).GetResponse().GetResponseStream());
                Debug.WriteLine (profileXML.AsString()) ;
                XmlNamespaceManager nsMgr = new XmlNamespaceManager(profileXML.NameTable);
                nsMgr.AddNamespace("tns", "urn:schemas-upnp-org:device-1-0");
                XmlNode typen = profileXML.SelectSingleNode("//tns:device/tns:deviceType/text()", nsMgr);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

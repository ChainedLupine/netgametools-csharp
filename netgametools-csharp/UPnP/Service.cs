using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;

namespace chainedlupine.UPnP
{
    public class Service
    {
        protected List<String> supportedActions = new List<String>();

        public Service(Uri deviceUri, XmlDocument profileXML, XmlNamespaceManager nsMgr, XmlNode descNode)
        {
            XmlNode xnServiceType = descNode.SelectSingleNode("tns:serviceType/text()", nsMgr);
            XmlNode xnServiceUrl = descNode.SelectSingleNode("tns:SCPDURL/text()", nsMgr);
            Uri descriptionUrl = new Uri(deviceUri, xnServiceUrl.InnerText);

            XmlDocument serviceDescXML = new XmlDocument();
            serviceDescXML.Load(WebRequest.Create(descriptionUrl.ToString()).GetResponse().GetResponseStream());

            XmlNamespaceManager nsServiceMgr = new XmlNamespaceManager(serviceDescXML.NameTable);
            nsServiceMgr.AddNamespace("tns", "urn:schemas-upnp-org:service-1-0");

            XmlNodeList actionListNode = serviceDescXML.SelectNodes("//tns:actionList/descendant::tns:action", nsServiceMgr);
            foreach (XmlNode xnAction in actionListNode)
            {
                XmlNode xnName = xnAction.SelectSingleNode("tns:name/text()", nsServiceMgr);
                supportedActions.Add(xnName.InnerText);
                //Debug.WriteLine(xnName.InnerText);
            }


        }
    }

    public class ServiceWANIPConnection: Service
    {
        public ServiceWANIPConnection(Uri deviceUri, XmlDocument profileXML, XmlNamespaceManager nsMgr, XmlNode descNode)
            : base(deviceUri, profileXML, nsMgr, descNode)
        {

        }

        public string getExternalIP()
        {
            if (supportedActions.IndexOf("GetExternalIPAddress") == -1)
                return null;

            string ip = "";

            return ip;
        }
    }
}

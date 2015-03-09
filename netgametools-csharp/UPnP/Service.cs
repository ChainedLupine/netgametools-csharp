using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace chainedlupine.UPnP
{
    public class Service
    {
        protected List<String> supportedActions = new List<String>();

        protected Uri serviceControlUri ;

        protected const string SOAP_NAMESPACE_URI = "http://schemas.xmlsoap.org/soap/envelope/" ;

        public Service(Uri deviceUri, XmlDocument profileXML, XmlNamespaceManager nsMgr, XmlNode descNode)
        {
            XmlNode xnServiceType = descNode.SelectSingleNode("tns:serviceType/text()", nsMgr);
            XmlNode xnServiceDescUrl = descNode.SelectSingleNode("tns:SCPDURL/text()", nsMgr);
            Uri descriptionUrl = new Uri(deviceUri, xnServiceDescUrl.InnerText);

            XmlNode xnServiceControlUrl = descNode.SelectSingleNode("tns:controlURL/text()", nsMgr);
            serviceControlUri = new Uri(deviceUri, xnServiceControlUrl.InnerText);

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

        // Returns the response
        public XDocument ExecSOAPRequest (XElement request)
        {
            XNamespace soapNs = SOAP_NAMESPACE_URI;

            XDocument xReq = new XDocument(
                    new XDeclaration ("1.0", null, null),
                    new XElement (soapNs + "Envelope", 
                        new XAttribute(XNamespace.Xmlns + "s", SOAP_NAMESPACE_URI),
                        new XAttribute(soapNs + "encodingStyle", "http://schemas.xmlsoap.org/soap/encoding/"),
                        new XElement(soapNs + "Body",
                            request
                        )
                    )
                );

            Debug.WriteLine(xReq.ToXmlDocument().AsString());

            string serviceFuncName = request.Name.LocalName;

            WebRequest wr = HttpWebRequest.Create(serviceControlUri);
            wr.Method = "POST";

            byte[] b = Encoding.UTF8.GetBytes(xReq.ToString());
            wr.Headers.Add("SOAPACTION", "\"urn:schemas-upnp-org:service:WANIPConnection:1#" + serviceFuncName + "\"");
            wr.ContentType = "text/xml; charset=\"utf-8\"";
            wr.ContentLength = b.Length;
            wr.GetRequestStream().Write(b, 0, b.Length);

            WebResponse wres = wr.GetResponse();
            Stream streamResp = wres.GetResponseStream();
            XDocument xResp = XDocument.Load(streamResp);

            return xResp ;
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

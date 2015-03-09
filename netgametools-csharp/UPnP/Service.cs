using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        public XmlDocument ExecSOAPRequest (XmlDocument request)
        {
            XmlDocument req = new XmlDocument();

            XmlDeclaration xmlDecl = req.CreateXmlDeclaration("1.0", null, null);
            req.InsertBefore(xmlDecl, req.DocumentElement);

            XmlElement envelope = req.CreateElement("s", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
            XmlAttribute encodingAttribute = req.CreateAttribute("s", "encodingStyle", "http://schemas.xmlsoap.org/soap/envelope/");
            encodingAttribute.InnerText = "http://schemas.xmlsoap.org/soap/encoding/";
            envelope.SetAttributeNode(encodingAttribute);

            req.AppendChild(envelope);

            XmlElement body = req.CreateElement("s", "Body", "http://schemas.xmlsoap.org/soap/envelope/");
            envelope.AppendChild(body);

            XmlNode bodyRequest = body.OwnerDocument.ImportNode(request.DocumentElement, true);
            body.AppendChild(bodyRequest);

            Debug.WriteLine(req.AsString());

            XmlDocument resp = new XmlDocument() ;

            string func = request.DocumentElement.LocalName;

            WebRequest r = HttpWebRequest.Create(serviceControlUri);
            r.Method = "POST";
            byte[] b = Encoding.UTF8.GetBytes(req.OuterXml);
            r.Headers.Add("SOAPACTION", "\"urn:schemas-upnp-org:service:WANIPConnection:1#" + func + "\"");
            r.ContentType = "text/xml; charset=\"utf-8\"";
            r.ContentLength = b.Length;
            r.GetRequestStream().Write(b, 0, b.Length);

            WebResponse wres = r.GetResponse();
            Stream ress = wres.GetResponseStream();
            resp.Load(ress);

            return resp ;
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

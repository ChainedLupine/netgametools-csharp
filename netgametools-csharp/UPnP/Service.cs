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
        public bool safetyChecks = true;

        public string serviceType;

        public Uri serviceControlUri;
        public Uri serviceDescUri;
        public List<String> supportedActions = new List<String>();

        public IService serviceInterface ;

        protected const string SOAP_NAMESPACE_URI = "http://schemas.xmlsoap.org/soap/envelope/" ;

        private static List<IService> _registeredServiceInterfaces ;

        public Service(Uri deviceUri, XNamespace ns, XElement xServiceDesc, bool safetyChecks)
        {
            this.safetyChecks = safetyChecks;

            serviceType = xServiceDesc.Element(ns + "serviceType").Value;
            serviceControlUri = new Uri(deviceUri, xServiceDesc.Element(ns + "controlURL").Value);
            serviceDescUri = new Uri(deviceUri, xServiceDesc.Element(ns + "SCPDURL").Value);

            if (safetyChecks)
            {
                // Load service description
                XNamespace nsService = "urn:schemas-upnp-org:service-1-0";

                XDocument xDesc = XDocument.Load(WebRequest.Create(serviceDescUri.ToString()).GetResponse().GetResponseStream());

                foreach (XElement xAction in xDesc.Descendants(nsService + "action"))
                {
                    string actionName = xAction.Element(nsService + "name").Value;
                    supportedActions.Add(actionName);
                }

                Debug.WriteLine(string.Format("Found {0} actions for service {1}", supportedActions.Count, serviceType));
            } else
                Debug.WriteLine(string.Format("Adding unsafe service {0}.", serviceType));

        }

        public static void RegisterInterfaces()
        {
            _registeredServiceInterfaces = new List<IService>() ;
            _registeredServiceInterfaces.Add(new IServiceWANIPConnection());
        }

        public static Service GetServiceOfType (List<Service> services, string serviceUrn)
        {
            foreach (Service service in services)
            {
                if (service.serviceType == serviceUrn)
                    return service;
            }

            return null;
        }

        public static List<Service> LoadServices(Uri deviceUri, XNamespace ns, IEnumerable<XElement> xServiceList, bool safetyChecks = true)
        {
            List<Service> services = new List<Service>();

            foreach (XElement xService in xServiceList)
            {
                Service service = new Service(deviceUri, ns, xService, safetyChecks);
                services.Add(service);

                foreach (IService iService in _registeredServiceInterfaces)
                {
                    //Debug.WriteLine(iService.getServiceUrn());
                    if (service.serviceType == iService.getServiceUrn())
                    {
                        IService newServiceInterface = iService.newInstance();
                        newServiceInterface.setService(service);
                    }
                }
            }

            return services;
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

            //Debug.WriteLine(xReq.ToXmlDocument().AsString());

            string serviceFuncName = request.Name.LocalName;

            WebRequest wr = HttpWebRequest.Create(serviceControlUri);
            wr.Method = "POST";

            byte[] b = Encoding.UTF8.GetBytes(xReq.ToString());
            wr.Headers.Add("SOAPACTION", "\"urn:schemas-upnp-org:service:WANIPConnection:1#" + serviceFuncName + "\"");
            wr.ContentType = "text/xml; charset=\"utf-8\"";
            wr.ContentLength = b.Length;
            wr.GetRequestStream().Write(b, 0, b.Length);

            WebResponse wres ;
            try
            {
                wres = wr.GetResponse();
            } catch (WebException we)
            {
                throw new Exception(string.Format("Unable to complete SOAP request for action {0}, error {1}!", serviceFuncName, ((HttpWebResponse)we.Response).StatusCode));
            }

            Stream streamResp = wres.GetResponseStream();
            XDocument xResp = XDocument.Load(streamResp);

            return xResp ;
        }
    }

    public interface IService
    {
        string getServiceUrn();
        void setService(Service service);
        IService newInstance();
    }

    public class IServiceBase: IService
    {
        protected Service _service;
        virtual public string getServiceUrn() { return ""; } 

        public void setService(Service service)
        {
            _service = service;
            _service.serviceInterface = this;
        }

        virtual public IService newInstance()
        {
            return new IServiceBase();
        }

    }

    public class IServiceWANIPConnection : IServiceBase
    {
        override public string getServiceUrn() { return "urn:schemas-upnp-org:service:WANIPConnection:1"; }

        private string _serviceNamespaceURN = "urn:schemas-upnp-org:service:WANIPConnection:1";
        XNamespace _uNs;

        public IServiceWANIPConnection()
        {
            _uNs = _serviceNamespaceURN;
        }

        override public IService newInstance()
        {
            return new IServiceWANIPConnection();
        }

        public string GetExternalIP()
        {
            if (_service.safetyChecks && _service.supportedActions.IndexOf("GetExternalIPAddress") == -1)
                throw new Exception("GetExternalIPAddress not supported!");

            string ip = "";

            XElement woo = new XElement(_uNs + "GetExternalIPAddress",
                    new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN)
                );

            XDocument xResp = _service.ExecSOAPRequest(woo);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());

            ip = (from el in xResp.Descendants()
                 where el.Name == "NewExternalIPAddress"
                 select el).Single().Value;


            return ip;
        }

        public int GetPortMappingNumberOfEntries()
        {
            if (_service.safetyChecks && _service.supportedActions.IndexOf("GetPortMappingNumberOfEntries") == -1)
                throw new Exception("GetPortMappingNumberOfEntries not supported!");

            XElement woo = new XElement(_uNs + "GetPortMappingNumberOfEntries",
                    new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN)
                );

            XDocument xResp = _service.ExecSOAPRequest(woo);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());

            string result = (from el in xResp.Descendants()
                  where el.Name == "PortMappingNumberOfEntries"
                  select el).Single().Value;


            return Convert.ToInt32(result);
        }

        public List<DeviceGatewayPortRecord> GetPortMappingEntries()
        {
            if (_service.safetyChecks && _service.supportedActions.IndexOf("GetGenericPortMappingEntry") == -1)
                throw new Exception("GetGenericPortMappingEntry not supported!");

            List<DeviceGatewayPortRecord> mappings = new List<DeviceGatewayPortRecord>();

            int portIndex = 0;

            do
            {
                XElement xAction = new XElement(_uNs + "GetGenericPortMappingEntry",
                        new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN),
                        new XElement("NewPortMappingIndex", portIndex.ToString())
                    );

                try
                {
                    XDocument xResp = _service.ExecSOAPRequest(xAction);

                    DeviceGatewayPortRecord portRec = new DeviceGatewayPortRecord();
                    portRec.RemoteHost = xResp.Descendants("NewRemoteHost").Single().Value;
                    portRec.InternalClient = xResp.Descendants("NewInternalClient").Single().Value;
                    portRec.ExternalPort = Convert.ToUInt16(xResp.Descendants("NewExternalPort").Single().Value);
                    portRec.InternalPort = Convert.ToUInt16(xResp.Descendants("NewInternalPort").Single().Value);
                    portRec.Protocol = xResp.Descendants("NewProtocol").Single().Value;
                    portRec.Desc = xResp.Descendants("NewPortMappingDescription").Single().Value;
                    portRec.LeaseDuration = Convert.ToInt32(xResp.Descendants("NewLeaseDuration").Single().Value);
                    
                    mappings.Add(portRec);

                    //Debug.WriteLine(xResp.ToXmlDocument().AsString());
                    portIndex++;
                } catch
                {
                    portIndex = -1;
                }

            } while (portIndex > 0);

            return mappings;
        }

        public void DeletePortMapping(string remoteHost, ushort externalPort, string protocol)
        {
            if (_service.safetyChecks && _service.supportedActions.IndexOf("DeletePortMapping") == -1)
                throw new Exception("DeletePortMapping not supported!");

            string ip = "";

            XElement woo = new XElement(_uNs + "DeletePortMapping",
                    new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN),
                    new XElement("NewRemoteHost", remoteHost),
                    new XElement("NewExternalPort", externalPort.ToString()),
                    new XElement("NewProtocol", protocol)
                );

            XDocument xResp = _service.ExecSOAPRequest(woo);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());
        }

        public void AddPortMapping(string remoteHost, ushort externalPort, string protocol, ushort internalPort, string internalClient, string desc)
        {
            if (_service.safetyChecks && _service.supportedActions.IndexOf("AddPortMapping") == -1)
                throw new Exception("AddPortMapping not supported!");

            string ip = "";

            XElement woo = new XElement(_uNs + "AddPortMapping",
                    new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN),
                    new XElement("NewRemoteHost", remoteHost),
                    new XElement("NewExternalPort", externalPort.ToString()),
                    new XElement("NewProtocol", protocol),
                    new XElement("NewInternalPort", internalPort.ToString()),
                    new XElement("NewInternalClient", internalClient),
                    new XElement("NewEnabled", "True"),
                    new XElement("NewPortMappingDescription", desc),
                    new XElement("NewLeaseDuration", 0)
                );

            XDocument xResp = _service.ExecSOAPRequest(woo);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());
        }

    }
}

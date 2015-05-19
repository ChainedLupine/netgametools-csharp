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

namespace chainedlupine.tuatara
{
    public class Service
    {
        public bool strictProtocol = true;

        public string serviceType;

        public Uri serviceControlUri;
        public Uri serviceDescUri;
        public List<String> supportedActions = new List<String>();

        public IService serviceInterface ;

        public XDocument debugRawXml;

        protected const string SOAP_NAMESPACE_URI = "http://schemas.xmlsoap.org/soap/envelope/" ;

        private static List<IService> _registeredServiceInterfaces ;

        public Service(Uri deviceUri, XNamespace ns, XElement xServiceDesc, bool strictProtocol)
        {
            this.strictProtocol = strictProtocol;

            serviceType = xServiceDesc.Element(ns + "serviceType").Value;
            serviceControlUri = new Uri(deviceUri, xServiceDesc.Element(ns + "controlURL").Value);
            serviceDescUri = new Uri(deviceUri, xServiceDesc.Element(ns + "SCPDURL").Value);

            Logger.WriteLine(string.Format("Creating service of type {0}, controlURL={1}", serviceType, serviceControlUri));

            // Load service description
            XNamespace nsService = "urn:schemas-upnp-org:service-1-0";

            Logger.WriteLine("Loading service description XML from SCPDURL=" + serviceDescUri);

            XDocument xDesc = XDocument.Load(WebRequest.Create(serviceDescUri.ToString()).GetResponse().GetResponseStream());
            debugRawXml = xDesc;

            foreach (XElement xAction in xDesc.Descendants(nsService + "action"))
            {
                string actionName = xAction.Element(nsService + "name").Value;
                supportedActions.Add(actionName);
                Logger.WriteLine("Supported action: " + actionName);
            }

            Logger.WriteLine(string.Format("Discovered {0} actions for this service.", supportedActions.Count));
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
                throw new TuataraException(string.Format("Unable to complete SOAP request for action {0}, error {1}!", serviceFuncName, ((HttpWebResponse)we.Response).StatusCode));
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
            if (_service.strictProtocol && _service.supportedActions.IndexOf("GetExternalIPAddress") == -1)
                throw new TuataraException("GetExternalIPAddress not supported!");

            string ip = "";

            XElement soapReq = new XElement(_uNs + "GetExternalIPAddress",
                    new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN)
                );

            Logger.WriteLine(string.Format("Sending SOAP request for GetExternalIPAddress"));

            XDocument xResp = _service.ExecSOAPRequest(soapReq);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());

            ip = (from el in xResp.Descendants()
                 where el.Name == "NewExternalIPAddress"
                 select el).Single().Value;


            return ip;
        }

        public int GetPortMappingNumberOfEntries()
        {
            if (_service.strictProtocol && _service.supportedActions.IndexOf("GetPortMappingNumberOfEntries") == -1)
                throw new TuataraException("GetPortMappingNumberOfEntries not supported!");

            XElement soapReq = new XElement(_uNs + "GetPortMappingNumberOfEntries",
                    new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN)
                );

            Logger.WriteLine(string.Format("Sending SOAP request for GetPortMappingNumberOfEntries"));

            XDocument xResp = _service.ExecSOAPRequest(soapReq);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());

            string result = (from el in xResp.Descendants()
                  where el.Name == "PortMappingNumberOfEntries"
                  select el).Single().Value;


            return Convert.ToInt32(result);
        }

        public List<DeviceGatewayPortRecord> GetPortMappingEntries()
        {
            if (_service.strictProtocol && _service.supportedActions.IndexOf("GetGenericPortMappingEntry") == -1)
                throw new TuataraException("GetGenericPortMappingEntry not supported!");

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
                    Logger.WriteLine(string.Format("Sending SOAP request for GetGenericPortMappingEntry"));
                    XDocument xResp = _service.ExecSOAPRequest(xAction);

                    Logger.WriteLine(string.Format("GetGenericPortMappingEntry returned info for index #{0}", portIndex));

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
                    Logger.WriteLine(string.Format("GetGenericPortMappingEntry exception, {0} entries found", (portIndex)));
                    portIndex = -1;
                }

            } while (portIndex > 0);

            return mappings;
        }

        public void DeletePortMapping(string remoteHost, ushort externalPort, string protocol)
        {
            if (_service.strictProtocol && _service.supportedActions.IndexOf("DeletePortMapping") == -1)
                throw new TuataraException("DeletePortMapping not supported!");

            XElement soapReq = new XElement(_uNs + "DeletePortMapping",
                    new XAttribute(XNamespace.Xmlns + "u", _serviceNamespaceURN),
                    new XElement("NewRemoteHost", remoteHost),
                    new XElement("NewExternalPort", externalPort.ToString()),
                    new XElement("NewProtocol", protocol)
                );

            Logger.WriteLine(string.Format("Sending SOAP request for DeletePortMapping"));

            XDocument xResp = _service.ExecSOAPRequest(soapReq);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());
        }

        public void AddPortMapping(string remoteHost, ushort externalPort, string protocol, ushort internalPort, string internalClient, string desc)
        {
            if (_service.strictProtocol && _service.supportedActions.IndexOf("AddPortMapping") == -1)
                throw new TuataraException("AddPortMapping not supported!");

            XElement soapReq = new XElement(_uNs + "AddPortMapping",
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

            Logger.WriteLine(string.Format("Sending SOAP request for AddPortMapping"));

            XDocument xResp = _service.ExecSOAPRequest(soapReq);
            //Debug.WriteLine(xResp.ToXmlDocument().AsString());
        }

    }
}

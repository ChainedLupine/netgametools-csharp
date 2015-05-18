using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace chainedlupine.tuatara
{
    public class DeviceList
    {
        List<Device> _list = new List<Device>();

        public void Add (Device device)
        {
            _list.Add(device);
        }

        public int Count { get { return _list.Count; } }

        public bool isPresent (Device device)
        {
            foreach (Device d in _list)
            {
                if (d.uuid == device.uuid)
                    return true;
            }
            return false;
        }

        public Device GetByUUID (string uuid)
        {
            foreach (Device d in _list)
            {
                if (d.uuid == uuid)
                    return d;
            }

            return null;
        }

        public List<Device> ToList()
        {
            return new List<Device>(_list);
        }
    }

    public class httpresponse
    {
        public int status = -1;
        public string statusText = "";

        public Dictionary<string, string> values;

        public bool decode (string raw)
        {
            values = new Dictionary<string, string>();

            List<string> lines = raw.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            if (lines.Count > 0)
            {

                // Check first line
                string codeRaw = lines[0];
                lines.RemoveAt(0);
                MatchCollection matches = Regex.Matches(codeRaw, @"^HTTP\/1.1\s+(\d+)\s+([\w,\d]+)", RegexOptions.IgnoreCase);

                if (matches.Count == 1 && matches[0].Groups.Count == 3)
                {
                    if (!int.TryParse (matches[0].Groups[1].Value, out status))
                        return false ;

                    statusText = matches[0].Groups[2].Value;

                    foreach (string line in lines)
                    {
                        foreach (Match match in Regex.Matches (line, @"^([\w-_]+):(.*$)"))
                        {
                            if (match.Groups.Count == 3)
                                values.Add(match.Groups[1].Value.ToLower(), match.Groups[2].Value.TrimStart(' '));
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }

    public class SSDP
    {
        public const string DEVICETYPE_ROOTDEVICE = "upnp:rootdevice" ;
        public const string DEVICETYPE_ALL = "ssdp:all";

        const ushort SSDP_PORT = 1900;

        // Only used for threaded manager
        Socket _detectSocket;
        DeviceList _devices;
        List<string> _foundUrls;
        List<Device> _devicesToProfile;
        DateTime _startTime;
        bool _strictMode;
        byte[] _reqData;
        IPEndPoint _endpoint;
        byte[] _fixedBuffer = new byte[0x1000];

        public SSDP()
        {
            _detectSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _detectSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            _detectSocket.Blocking = false;
        }

        public void Dispose()
        {
            _detectSocket.Dispose();
            _detectSocket = null;
        }

        public string getUUID(string raw)
        {
            MatchCollection matches = Regex.Matches (raw, @"^uuid:([\w\d-]+)::") ;

            if (matches.Count == 1 && matches[0].Groups.Count == 2)
            {
                return matches[0].Groups[1].Value;
            } else
                return  "" ;
        }

        public DeviceList GetResultDeviceList()
        {
            return _devices;
        }

        public bool Run ()
        {
            bool running = true;

            int length = 0;
            if (_detectSocket.Available > 0)
            {
                length = _detectSocket.Receive(_fixedBuffer);

                string resp = Encoding.ASCII.GetString(_fixedBuffer, 0, length);

                Logger.WriteLine(string.Format("Received data, length of {0}.", length));

                httpresponse response = new httpresponse();
                if (response.decode(resp) && response.status == 200)
                {
                    Logger.WriteLine("Data decodes to valid HTTP respond!");

                    string url = response.values["location"];
                    if (_foundUrls.IndexOf(url) >= 0)
                    {
                        Logger.WriteLineWarn("Already handled URL of " + url + ", skipping duplicate response.");
                    }
                    else
                    {
                        _foundUrls.Add(url);

                        // We've got a valid status
                        Device device = new Device();
                        device.deviceUri = new Uri(url);

                        Logger.WriteLine("Location URL from http response is " + device.deviceUri.ToString());

                        // Save device, for profile loading, later
                        device.discoveredKeys = new Dictionary<string, string>(response.values);

                        _devicesToProfile.Add(device);
                    }
                }
            }

            // After our detect wait time, then start loading/decoding device profiles from replies to our discovery query
            if (DateTime.Now.Subtract(_startTime).TotalSeconds > 3)
            {
                if (_devicesToProfile.Count > 0)
                {
                    Device device = _devicesToProfile[0];
                    _devicesToProfile.RemoveAt(0);

                    try
                    {
                        device.retrieveDeviceProfile(_strictMode);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLineError(string.Format("Unable to retrieve profile: {0}", e.Message));
                    }

                    if (device.uuid != null && !_devices.isPresent(device))
                    {
                        _devices.Add(device);
                    }
                    else
                        Logger.WriteLineWarn("Not adding device, already present or null UUID.");

                }

                running = _devicesToProfile.Count != 0;
            }

            
            return running ;
        }

        public void Discover(string deviceType, ushort port = SSDP_PORT, bool strictMode = true)
        {
            
            string req = 
                "M-SEARCH * HTTP/1.1\r\n" +
                "HOST: 239.255.255.250:" + port.ToString() + "\r\n" +
                "ST:" + deviceType + "\r\n" +
                "MAN:\"ssdp:discover\"\r\n" +
                "MX:1\r\n\r\n";

            _reqData = Encoding.ASCII.GetBytes(req);
            _endpoint = new IPEndPoint(IPAddress.Broadcast, port);

            _startTime = DateTime.Now;

            _devices = new DeviceList();
            _devicesToProfile = new List<Device>();

            _foundUrls = new List<string>();

            _strictMode = strictMode;

            Logger.WriteLine("Sending M-SEARCH UDP packet...");
            _detectSocket.SendTo(_reqData, _endpoint);
        }
    }
}

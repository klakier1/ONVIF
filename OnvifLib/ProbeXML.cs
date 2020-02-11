using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class ProbeXML
    {
        private string _templateDevice =
            @"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"" xmlns:a=""http://schemas.xmlsoap.org/ws/2004/08/addressing""><s:Header><a:Action s:mustUnderstand=""1"">http://schemas.xmlsoap.org/ws/2005/04/discovery/Probe</a:Action><a:MessageID>uuid:{0}</a:MessageID><a:ReplyTo><a:Address>http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous</a:Address></a:ReplyTo><a:To s:mustUnderstand=""1"">urn:schemas-xmlsoap-org:ws:2005:04:discovery</a:To></s:Header><s:Body><Probe xmlns=""http://schemas.xmlsoap.org/ws/2005/04/discovery""><d:Types xmlns:d=""http://schemas.xmlsoap.org/ws/2005/04/discovery"" xmlns:dp0=""http://www.onvif.org/ver10/device/wsdl"">dp0:Device</d:Types></Probe></s:Body></s:Envelope>";
        private string _templateNetworkVideoTransmitter =
            @"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"" xmlns:a=""http://schemas.xmlsoap.org/ws/2004/08/addressing""><s:Header><a:Action s:mustUnderstand=""1"">http://schemas.xmlsoap.org/ws/2005/04/discovery/Probe</a:Action><a:MessageID>uuid:{0}</a:MessageID><a:ReplyTo><a:Address>http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous</a:Address></a:ReplyTo><a:To s:mustUnderstand=""1"">urn:schemas-xmlsoap-org:ws:2005:04:discovery</a:To></s:Header><s:Body><Probe xmlns=""http://schemas.xmlsoap.org/ws/2005/04/discovery""><d:Types xmlns:d=""http://schemas.xmlsoap.org/ws/2005/04/discovery"" xmlns:dp0=""http://www.onvif.org/ver10/network/wsdl"">dp0:NetworkVideoTransmitter</d:Types></Probe></s:Body></s:Envelope>";

        public Guid MessageGuid { get; set; }

        public ProbeXML()
        {
            MessageGuid = Guid.NewGuid();
        }

        public ProbeXML(Guid guid)
        {
            MessageGuid = guid;
        }

        public string GetProbeDeviceXML()
        {
            return String.Format(_templateDevice, MessageGuid.ToString());
        }

        public string GetProbeNetworkVideoTransmitterXML()
        {
            return String.Format(_templateNetworkVideoTransmitter, MessageGuid.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;
using OnvifLib.Models.NameSpaces;

namespace OnvifLib
{
    public class XMLProbeMatchParser
    {
        public const string TestString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:SOAP-ENC=\"http://www.w3.org/2003/05/soap-encoding\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" xmlns:wsdd=\"http://schemas.xmlsoap.org/ws/2005/04/discovery\" xmlns:chan=\"http://schemas.microsoft.com/ws/2005/02/duplex\" xmlns:wsa5=\"http://www.w3.org/2005/08/addressing\" xmlns:xmime=\"http://tempuri.org/xmime.xsd\" xmlns:xop=\"http://www.w3.org/2004/08/xop/include\" xmlns:wsrfbf=\"http://docs.oasis-open.org/wsrf/bf-2\" xmlns:wstop=\"http://docs.oasis-open.org/wsn/t-1\" xmlns:tt=\"http://www.onvif.org/ver10/schema\" xmlns:ns3=\"http://www.onvif.org/ver10/pacs\" xmlns:wsrfr=\"http://docs.oasis-open.org/wsrf/r-2\" xmlns:ns1=\"http://www.onvif.org/ver10/actionengine/wsdl\" xmlns:ns2=\"http://www.onvif.org/ver10/accesscontrol/wsdl\" xmlns:ns4=\"http://www.onvif.org/ver10/doorcontrol/wsdl\" xmlns:tad=\"http://www.onvif.org/ver10/analyticsdevice/wsdl\" xmlns:daae=\"http://www.onvif.org/ver20/analytics/wsdl/AnalyticsEngineBinding\" xmlns:dare=\"http://www.onvif.org/ver20/analytics/wsdl/RuleEngineBinding\" xmlns:tan=\"http://www.onvif.org/ver20/analytics/wsdl\" xmlns:dn=\"http://www.onvif.org/ver10/network/wsdl\" xmlns:ter=\"http://www.onvif.org/ver10/error\" xmlns:tds=\"http://www.onvif.org/ver10/device/wsdl\" xmlns:tev=\"http://www.onvif.org/ver10/events/wsdl\" xmlns:wsnt=\"http://docs.oasis-open.org/wsn/b-2\" xmlns:timg=\"http://www.onvif.org/ver20/imaging/wsdl\" xmlns:tls=\"http://www.onvif.org/ver10/display/wsdl\" xmlns:tmd=\"http://www.onvif.org/ver10/deviceIO/wsdl\" xmlns:tptz=\"http://www.onvif.org/ver20/ptz/wsdl\" xmlns:trc=\"http://www.onvif.org/ver10/recording/wsdl\" xmlns:trp=\"http://www.onvif.org/ver10/replay/wsdl\" xmlns:trt=\"http://www.onvif.org/ver10/media/wsdl\" xmlns:trv=\"http://www.onvif.org/ver10/receiver/wsdl\" xmlns:tse=\"http://www.onvif.org/ver10/search/wsdl\" xmlns:tns1=\"http://www.onvif.org/ver10/topics\" xmlns:tnshik=\"http://www.hikvision.com/2011/event/topics\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"><SOAP-ENV:Header><wsa:MessageID>uuid:39ade894-2df3-50b2-a205-00001b0bd832</wsa:MessageID><wsa:RelatesTo>uuid:c1621058-1793-432a-82d2-6cc84aff1439</wsa:RelatesTo><wsa:To SOAP-ENV:mustUnderstand=\"true\">http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous</wsa:To><wsa:Action SOAP-ENV:mustUnderstand=\"true\">http://schemas.xmlsoap.org/ws/2005/04/discovery/ProbeMatches</wsa:Action></SOAP-ENV:Header><SOAP-ENV:Body><wsdd:ProbeMatches><wsdd:ProbeMatch><wsa:EndpointReference><wsa:Address>urn:uuid:39ade894-2df3-50b2-a205-00001b0bd832</wsa:Address><wsa:PortType>ttl</wsa:PortType></wsa:EndpointReference><wsdd:Types>dn:NetworkVideoTransmitter</wsdd:Types><wsdd:Scopes>onvif://www.onvif.org/type/network_video_transmitter onvif://www.onvif.org/type/video_encoder onvif://www.onvif.org/type/audio_encoder onvif://www.onvif.org/manufacturer/HeroSpeed onvif://www.onvif.org/type/ptz onvif://www.onvif.org/Profile/Streaming onvif://www.onvif.org/Profile/G onvif://www.onvif.org/Profile/G onvif://www.onvif.org/hardware/IPCamera onvif://www.onvif.org/name/HeroSpeed onvif://www.onvif.org/location/Guangzhou </wsdd:Scopes><wsdd:XAddrs>http://192.168.1.4/onvif/device_service</wsdd:XAddrs><wsdd:MetadataVersion>1</wsdd:MetadataVersion></wsdd:ProbeMatch><wsdd:ProbeMatch><wsa:EndpointReference><wsa:Address>urn:uuid:39ade894-2df3-50b2-a205-00001b0bd832</wsa:Address><wsa:PortType>ttl</wsa:PortType></wsa:EndpointReference><wsdd:Types>dn:NetworkVideoTransmitter</wsdd:Types><wsdd:Scopes>onvif://www.onvif.org/type/network_video_transmitter onvif://www.onvif.org/type/video_encoder onvif://www.onvif.org/type/audio_encoder onvif://www.onvif.org/manufacturer/HeroSpeed onvif://www.onvif.org/type/ptz onvif://www.onvif.org/Profile/Streaming onvif://www.onvif.org/Profile/G onvif://www.onvif.org/Profile/G onvif://www.onvif.org/hardware/IPCamera onvif://www.onvif.org/name/HeroSpeed onvif://www.onvif.org/location/Guangzhou </wsdd:Scopes><wsdd:XAddrs>http://192.168.1.4/onvif/device_service</wsdd:XAddrs><wsdd:MetadataVersion>1</wsdd:MetadataVersion></wsdd:ProbeMatch></wsdd:ProbeMatches></SOAP-ENV:Body></SOAP-ENV:Envelope>";
        public XMLProbeMatchParser() { }

        public List<ProbeMatch> Parse(string xml)
        {
            var doc = XDocument.Parse(xml);
            var probeMatchList = doc.Element(NameSpaces.SOAP_ENV + "Envelope")
                .Element(NameSpaces.SOAP_ENV + "Body")
                .Element(NameSpaces.wsdd + "ProbeMatches")
                .Elements(NameSpaces.wsdd + "ProbeMatch")
                .Select(p =>
                {
                    return new ProbeMatch(
                        new Uri(p.Element(NameSpaces.wsdd + "XAddrs").Value),
                        //new Guid(p.Element(NameSpaces.wsa + "EndpointReference").Element(NameSpaces.wsa + "Address").Value),
                        p.Element(NameSpaces.wsa + "EndpointReference").Element(NameSpaces.wsa + "Address").Value,
                        p.Element(NameSpaces.wsdd + "Types").Value
                        );
                }).ToList();


            return probeMatchList;
        }
    }

    public class ProbeMatch
    {
        public Uri XAddrs;
        public String EndPointReference;
        public List<String> Types = new List<String>();

        public ProbeMatch()
        {

        }
        public ProbeMatch(Uri xAddrs, String endPointReference, String types)
        {
            XAddrs = xAddrs;
            EndPointReference = endPointReference;
            Types.Add(types);
        }

        public override string ToString()
        {
            String types = "";
            foreach (string type in Types)
                types += ($"{type} ");
            return $"{ XAddrs.ToString() } => { EndPointReference } => { types }";
        }
    }
}

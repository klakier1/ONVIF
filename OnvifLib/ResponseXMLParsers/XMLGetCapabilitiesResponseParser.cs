using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;
using OnvifLib.Models.NameSpaces;

namespace OnvifLib
{
    public class XMLGetCapabilitiesResponseParser
    {
        public readonly string TestString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:SOAP-ENC=\"http://www.w3.org/2003/05/soap-encoding\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" xmlns:wsdd=\"http://schemas.xmlsoap.org/ws/2005/04/discovery\" xmlns:chan=\"http://schemas.microsoft.com/ws/2005/02/duplex\" xmlns:wsa5=\"http://www.w3.org/2005/08/addressing\" xmlns:xmime=\"http://tempuri.org/xmime.xsd\" xmlns:xop=\"http://www.w3.org/2004/08/xop/include\" xmlns:wsrfbf=\"http://docs.oasis-open.org/wsrf/bf-2\" xmlns:wstop=\"http://docs.oasis-open.org/wsn/t-1\" xmlns:tt=\"http://www.onvif.org/ver10/schema\" xmlns:ns3=\"http://www.onvif.org/ver10/pacs\" xmlns:wsrfr=\"http://docs.oasis-open.org/wsrf/r-2\" xmlns:ns1=\"http://www.onvif.org/ver10/actionengine/wsdl\" xmlns:ns2=\"http://www.onvif.org/ver10/accesscontrol/wsdl\" xmlns:ns4=\"http://www.onvif.org/ver10/doorcontrol/wsdl\" xmlns:tad=\"http://www.onvif.org/ver10/analyticsdevice/wsdl\" xmlns:daae=\"http://www.onvif.org/ver20/analytics/wsdl/AnalyticsEngineBinding\" xmlns:dare=\"http://www.onvif.org/ver20/analytics/wsdl/RuleEngineBinding\" xmlns:tan=\"http://www.onvif.org/ver20/analytics/wsdl\" xmlns:dn=\"http://www.onvif.org/ver10/network/wsdl\" xmlns:ter=\"http://www.onvif.org/ver10/error\" xmlns:tds=\"http://www.onvif.org/ver10/device/wsdl\" xmlns:tev=\"http://www.onvif.org/ver10/events/wsdl\" xmlns:wsnt=\"http://docs.oasis-open.org/wsn/b-2\" xmlns:timg=\"http://www.onvif.org/ver20/imaging/wsdl\" xmlns:tls=\"http://www.onvif.org/ver10/display/wsdl\" xmlns:tmd=\"http://www.onvif.org/ver10/deviceIO/wsdl\" xmlns:tptz=\"http://www.onvif.org/ver20/ptz/wsdl\" xmlns:trc=\"http://www.onvif.org/ver10/recording/wsdl\" xmlns:trp=\"http://www.onvif.org/ver10/replay/wsdl\" xmlns:trt=\"http://www.onvif.org/ver10/media/wsdl\" xmlns:trv=\"http://www.onvif.org/ver10/receiver/wsdl\" xmlns:tse=\"http://www.onvif.org/ver10/search/wsdl\" xmlns:tns1=\"http://www.onvif.org/ver10/topics\" xmlns:tnshik=\"http://www.hikvision.com/2011/event/topics\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"><SOAP-ENV:Header/><SOAP-ENV:Body><tds:GetCapabilitiesResponse><tds:Capabilities><tt:Analytics><tt:XAddr>http://192.168.2.124/onvif/Analytics</tt:XAddr><tt:RuleSupport>true</tt:RuleSupport><tt:AnalyticsModuleSupport>true</tt:AnalyticsModuleSupport></tt:Analytics><tt:Device><tt:XAddr>http://192.168.2.124/onvif/Device</tt:XAddr><tt:Network><tt:IPFilter>false</tt:IPFilter><tt:ZeroConfiguration>false</tt:ZeroConfiguration><tt:IPVersion6>false</tt:IPVersion6><tt:DynDNS>false</tt:DynDNS><tt:Extension><tt:Dot11Configuration>false</tt:Dot11Configuration></tt:Extension></tt:Network><tt:System><tt:DiscoveryResolve>false</tt:DiscoveryResolve><tt:DiscoveryBye>true</tt:DiscoveryBye><tt:RemoteDiscovery>false</tt:RemoteDiscovery><tt:SystemBackup>false</tt:SystemBackup><tt:SystemLogging>false</tt:SystemLogging><tt:FirmwareUpgrade>false</tt:FirmwareUpgrade><tt:SupportedVersions><tt:Major>2</tt:Major><tt:Minor>0</tt:Minor></tt:SupportedVersions><tt:SupportedVersions><tt:Major>2</tt:Major><tt:Minor>10</tt:Minor></tt:SupportedVersions><tt:SupportedVersions><tt:Major>2</tt:Major><tt:Minor>20</tt:Minor></tt:SupportedVersions><tt:SupportedVersions><tt:Major>2</tt:Major><tt:Minor>30</tt:Minor></tt:SupportedVersions><tt:SupportedVersions><tt:Major>2</tt:Major><tt:Minor>40</tt:Minor></tt:SupportedVersions><tt:Extension><tt:HttpFirmwareUpgrade>true</tt:HttpFirmwareUpgrade><tt:HttpSystemBackup>true</tt:HttpSystemBackup><tt:HttpSystemLogging>true</tt:HttpSystemLogging><tt:HttpSupportInformation>false</tt:HttpSupportInformation></tt:Extension></tt:System><tt:IO><tt:InputConnectors>1</tt:InputConnectors><tt:RelayOutputs>1</tt:RelayOutputs></tt:IO><tt:Security><tt:TLS1.1>false</tt:TLS1.1><tt:TLS1.2>false</tt:TLS1.2><tt:OnboardKeyGeneration>false</tt:OnboardKeyGeneration><tt:AccessPolicyConfig>false</tt:AccessPolicyConfig><tt:X.509Token>false</tt:X.509Token><tt:SAMLToken>false</tt:SAMLToken><tt:KerberosToken>false</tt:KerberosToken><tt:RELToken>false</tt:RELToken><tt:Extension><tt:TLS1.0>false</tt:TLS1.0></tt:Extension></tt:Security></tt:Device><tt:Events><tt:XAddr>http://192.168.2.124/onvif/Events</tt:XAddr><tt:WSSubscriptionPolicySupport>true</tt:WSSubscriptionPolicySupport><tt:WSPullPointSupport>true</tt:WSPullPointSupport><tt:WSPausableSubscriptionManagerInterfaceSupport>true</tt:WSPausableSubscriptionManagerInterfaceSupport></tt:Events><tt:Imaging><tt:XAddr>http://192.168.2.124/onvif/Imaging</tt:XAddr></tt:Imaging><tt:Media><tt:XAddr>http://192.168.2.124/onvif/Media</tt:XAddr><tt:StreamingCapabilities><tt:RTPMulticast>false</tt:RTPMulticast><tt:RTP_TCP>true</tt:RTP_TCP><tt:RTP_RTSP_TCP>true</tt:RTP_RTSP_TCP></tt:StreamingCapabilities></tt:Media><tt:PTZ><tt:XAddr>http://192.168.2.124/onvif/PTZ</tt:XAddr></tt:PTZ><tt:Extension><tt:DeviceIO><tt:XAddr>http://192.168.2.124:/onvif/DeviceIO</tt:XAddr><tt:VideoSources>1</tt:VideoSources><tt:VideoOutputs>1</tt:VideoOutputs><tt:AudioSources>1</tt:AudioSources><tt:AudioOutputs>1</tt:AudioOutputs><tt:RelayOutputs>1</tt:RelayOutputs></tt:DeviceIO></tt:Extension></tds:Capabilities></tds:GetCapabilitiesResponse></SOAP-ENV:Body></SOAP-ENV:Envelope>";

        public XMLGetCapabilitiesResponseParser() { }

        public List<Capability> Parse(string xml)
        {
            var doc = XDocument.Parse(xml);

            var capabilities = doc.Element(NameSpaces.SOAP_ENV + "Envelope")
                .Element(NameSpaces.SOAP_ENV + "Body")
                .Element(NameSpaces.tds + "GetCapabilitiesResponse")
                .Element(NameSpaces.tds + "Capabilities")
                .Descendants(NameSpaces.tt + "XAddr")
                .Select(p =>
                {
                    return
                        new Capability()
                        {
                            Name = p.Parent.Name,
                            XAddr = new Uri(p.Value)
                        };
                })
                .ToList();

            //foreach (var cap in capabilities)
            //{
            //    Debug.WriteLine(cap.ToString());
            //}

            return capabilities;
        }


    }

    public class Capability
    {
        public XName Name { get; set; }
        public Uri XAddr { get; set; }

        public override string ToString()
        {
            return $"{ Name.ToString() }  >>>>  { XAddr.ToString() }";
        }
    }
}

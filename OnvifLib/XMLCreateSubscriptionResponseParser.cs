using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;
using System.Xml.Serialization;
using OnvifLib.Models.CreateSubscriptionResponse;
using System.IO;

namespace OnvifLib
{
    public class XMLCreateSubscriptionResponseParser
    {
        public const string TestString1 = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:SOAP-ENC=\"http://www.w3.org/2003/05/soap-encoding\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" xmlns:wsdd=\"http://schemas.xmlsoap.org/ws/2005/04/discovery\" xmlns:chan=\"http://schemas.microsoft.com/ws/2005/02/duplex\" xmlns:wsa5=\"http://www.w3.org/2005/08/addressing\" xmlns:xmime=\"http://tempuri.org/xmime.xsd\" xmlns:xop=\"http://www.w3.org/2004/08/xop/include\" xmlns:wsrfbf=\"http://docs.oasis-open.org/wsrf/bf-2\" xmlns:wstop=\"http://docs.oasis-open.org/wsn/t-1\" xmlns:tt=\"http://www.onvif.org/ver10/schema\" xmlns:ns3=\"http://www.onvif.org/ver10/pacs\" xmlns:wsrfr=\"http://docs.oasis-open.org/wsrf/r-2\" xmlns:ns1=\"http://www.onvif.org/ver10/actionengine/wsdl\" xmlns:ns2=\"http://www.onvif.org/ver10/accesscontrol/wsdl\" xmlns:ns4=\"http://www.onvif.org/ver10/doorcontrol/wsdl\" xmlns:tad=\"http://www.onvif.org/ver10/analyticsdevice/wsdl\" xmlns:daae=\"http://www.onvif.org/ver20/analytics/wsdl/AnalyticsEngineBinding\" xmlns:dare=\"http://www.onvif.org/ver20/analytics/wsdl/RuleEngineBinding\" xmlns:tan=\"http://www.onvif.org/ver20/analytics/wsdl\" xmlns:dn=\"http://www.onvif.org/ver10/network/wsdl\" xmlns:ter=\"http://www.onvif.org/ver10/error\" xmlns:tds=\"http://www.onvif.org/ver10/device/wsdl\" xmlns:tev=\"http://www.onvif.org/ver10/events/wsdl\" xmlns:wsnt=\"http://docs.oasis-open.org/wsn/b-2\" xmlns:timg=\"http://www.onvif.org/ver20/imaging/wsdl\" xmlns:tls=\"http://www.onvif.org/ver10/display/wsdl\" xmlns:tmd=\"http://www.onvif.org/ver10/deviceIO/wsdl\" xmlns:tptz=\"http://www.onvif.org/ver20/ptz/wsdl\" xmlns:trc=\"http://www.onvif.org/ver10/recording/wsdl\" xmlns:trp=\"http://www.onvif.org/ver10/replay/wsdl\" xmlns:trt=\"http://www.onvif.org/ver10/media/wsdl\" xmlns:trv=\"http://www.onvif.org/ver10/receiver/wsdl\" xmlns:tse=\"http://www.onvif.org/ver10/search/wsdl\" xmlns:tns1=\"http://www.onvif.org/ver10/topics\" xmlns:tnshik=\"http://www.hikvision.com/2011/event/topics\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\"><SOAP-ENV:Header><wsa:MessageID>urn:uuid:27df1a92-6def-487c-ab1d-08abaa0b9084</wsa:MessageID>\r\n<wsa5:ReplyTo SOAP-ENV:mustUnderstand=\"true\"><wsa5:Address>http://www.w3.org/2005/08/addressing/anonymous</wsa5:Address>\r\n</wsa5:ReplyTo>\r\n<wsa5:To SOAP-ENV:mustUnderstand=\"true\">http://192.168.1.8/onvif/Events</wsa5:To>\r\n<wsa5:Action SOAP-ENV:mustUnderstand=\"true\">http://www.onvif.org/ver10/events/wsdl/EventPortType/CreatePullPointSubscriptionResponse</wsa5:Action>\r\n</SOAP-ENV:Header>\r\n<SOAP-ENV:Body><tev:CreatePullPointSubscriptionResponse><tev:SubscriptionReference><wsa5:Address>http://192.168.1.8/onvif/Events/Subscription?index=0</wsa5:Address>\r\n</tev:SubscriptionReference>\r\n<wsnt:CurrentTime>2020-02-16T15:35:57Z</wsnt:CurrentTime>\r\n<wsnt:TerminationTime>2020-02-16T15:45:57Z</wsnt:TerminationTime>\r\n</tev:CreatePullPointSubscriptionResponse>\r\n</SOAP-ENV:Body>\r\n</SOAP-ENV:Envelope>\r\n\r\n";

        public XMLCreateSubscriptionResponseParser() { }

        public Envelope Parse(string xml)
        {
            Envelope result;
            var doc = XDocument.Parse(xml);
            XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
            using (TextReader reader = new StringReader(xml))
            {
                result = (Envelope)serializer.Deserialize(reader);
            }
            return result;

        }
    }
}

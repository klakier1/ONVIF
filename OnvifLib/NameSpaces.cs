using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OnvifLib
{
    public static class NameSpaces
    {
        public static XNamespace SOAP_ENV = "http://www.w3.org/2003/05/soap-envelope";
        public static XNamespace SOAP_ENC = "http://www.w3.org/2003/05/soap-encoding";
        public static XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
        public static XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
        public static XNamespace wsa = "http://schemas.xmlsoap.org/ws/2004/08/addressing";
        public static XNamespace wsdd = "http://schemas.xmlsoap.org/ws/2005/04/discovery";
        public static XNamespace chan = "http://schemas.microsoft.com/ws/2005/02/duplex";
        public static XNamespace wsa5 = "http://www.w3.org/2005/08/addressing";
        public static XNamespace xmime = "http://tempuri.org/xmime.xsd";
        public static XNamespace xop = "http://www.w3.org/2004/08/xop/include";
        public static XNamespace wsrfbf = "http://docs.oasis-open.org/wsrf/bf-2";
        public static XNamespace wstop = "http://docs.oasis-open.org/wsn/t-1";
        public static XNamespace tt = "http://www.onvif.org/ver10/schema";
        public static XNamespace ns3 = "http://www.onvif.org/ver10/pacs";
        public static XNamespace wsrfr = "http://docs.oasis-open.org/wsrf/r-2";
        public static XNamespace ns1 = "http://www.onvif.org/ver10/actionengine/wsdl";
        public static XNamespace ns2 = "http://www.onvif.org/ver10/accesscontrol/wsdl";
        public static XNamespace ns4 = "http://www.onvif.org/ver10/doorcontrol/wsdl";
        public static XNamespace tad = "http://www.onvif.org/ver10/analyticsdevice/wsdl";
        public static XNamespace daae = "http://www.onvif.org/ver20/analytics/wsdl/AnalyticsEngineBinding";
        public static XNamespace dare = "http://www.onvif.org/ver20/analytics/wsdl/RuleEngineBinding";
        public static XNamespace tan = "http://www.onvif.org/ver20/analytics/wsdl";
        public static XNamespace dn = "http://www.onvif.org/ver10/network/wsdl";
        public static XNamespace ter = "http://www.onvif.org/ver10/error";
        public static XNamespace tds = "http://www.onvif.org/ver10/device/wsdl";
        public static XNamespace tev = "http://www.onvif.org/ver10/events/wsdl";
        public static XNamespace wsnt = "http://docs.oasis-open.org/wsn/b-2";
        public static XNamespace timg = "http://www.onvif.org/ver20/imaging/wsdl";
        public static XNamespace tls = "http://www.onvif.org/ver10/display/wsdl";
        public static XNamespace tmd = "http://www.onvif.org/ver10/deviceIO/wsdl";
        public static XNamespace tptz = "http://www.onvif.org/ver20/ptz/wsdl";
        public static XNamespace trc = "http://www.onvif.org/ver10/recording/wsdl";
        public static XNamespace trp = "http://www.onvif.org/ver10/replay/wsdl";
        public static XNamespace trt = "http://www.onvif.org/ver10/media/wsdl";
        public static XNamespace trv = "http://www.onvif.org/ver10/receiver/wsdl";
        public static XNamespace tse = "http://www.onvif.org/ver10/search/wsdl";
        public static XNamespace tns1 = "http://www.onvif.org/ver10/topics";
        public static XNamespace tnshik = "http://www.hikvision.com/2011/event/topics";
        public static XNamespace xs = "http://www.w3.org/2001/XMLSchema";
        public static XNamespace wsse = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        public static XNamespace wsu = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
    }
}
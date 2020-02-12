using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */

namespace OnvifLib.Models
{
    [XmlRoot(ElementName = "ReplyTo", Namespace = "http://www.w3.org/2005/08/addressing")]
    public class ReplyTo
    {
        [XmlElement(ElementName = "Address", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string Address { get; set; }
        [XmlAttribute(AttributeName = "mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public string MustUnderstand { get; set; }
    }

    [XmlRoot(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
    public class Action
    {
        [XmlAttribute(AttributeName = "mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public string MustUnderstand { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Header
    {
        [XmlElement(ElementName = "MessageID", Namespace = "http://schemas.xmlsoap.org/ws/2004/08/addressing")]
        public string MessageID { get; set; }
        [XmlElement(ElementName = "ReplyTo", Namespace = "http://www.w3.org/2005/08/addressing")]
        public ReplyTo ReplyTo { get; set; }
        [XmlElement(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
        public Action Action { get; set; }
    }

    [XmlRoot(ElementName = "Topic", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
    public class Topic
    {
        [XmlAttribute(AttributeName = "Dialect")]
        public string Dialect { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ProducerReference", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
    public class ProducerReference
    {
        [XmlElement(ElementName = "Address", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string Address { get; set; }
    }

    [XmlRoot(ElementName = "SimpleItem", Namespace = "http://www.onvif.org/ver10/schema")]
    public class SimpleItem
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "Source", Namespace = "http://www.onvif.org/ver10/schema")]
    public class Source
    {
        [XmlElement(ElementName = "SimpleItem", Namespace = "http://www.onvif.org/ver10/schema")]
        public List<SimpleItem> SimpleItem { get; set; }
    }

    [XmlRoot(ElementName = "Data", Namespace = "http://www.onvif.org/ver10/schema")]
    public class Data
    {
        [XmlElement(ElementName = "SimpleItem", Namespace = "http://www.onvif.org/ver10/schema")]
        public List<SimpleItem> SimpleItem { get; set; }
    }

    [XmlRoot(ElementName = "Message", Namespace = "http://www.onvif.org/ver10/schema")]
    public class Message
    {
        [XmlElement(ElementName = "Source", Namespace = "http://www.onvif.org/ver10/schema")]
        public Source Source { get; set; }
        [XmlElement(ElementName = "Data", Namespace = "http://www.onvif.org/ver10/schema")]
        public Data Data { get; set; }
        [XmlAttribute(AttributeName = "PropertyOperation")]
        public string PropertyOperation { get; set; }
        [XmlAttribute(AttributeName = "UtcTime")]
        public string UtcTime { get; set; }
    }

    [XmlRoot(ElementName = "Message", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
    public class Message2
    {
        [XmlElement(ElementName = "Message", Namespace = "http://www.onvif.org/ver10/schema")]
        public Message Message { get; set; }
    }

    [XmlRoot(ElementName = "NotificationMessage", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
    public class NotificationMessage
    {
        [XmlElement(ElementName = "Topic", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
        public Topic Topic { get; set; }
        [XmlElement(ElementName = "ProducerReference", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
        public ProducerReference ProducerReference { get; set; }
        [XmlElement(ElementName = "Message", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
        public Message2 Message2 { get; set; }
    }

    [XmlRoot(ElementName = "PullMessagesResponse", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
    public class PullMessagesResponse
    {
        [XmlElement(ElementName = "CurrentTime", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
        public string CurrentTime { get; set; }
        [XmlElement(ElementName = "TerminationTime", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
        public string TerminationTime { get; set; }
        [XmlElement(ElementName = "NotificationMessage", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
        public NotificationMessage NotificationMessage { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Body
    {
        [XmlElement(ElementName = "PullMessagesResponse", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
        public PullMessagesResponse PullMessagesResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Envelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public Header Header { get; set; }
        [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public Body Body { get; set; }
        [XmlAttribute(AttributeName = "SOAP-ENV", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string SOAPENV { get; set; }
        [XmlAttribute(AttributeName = "SOAP-ENC", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string SOAPENC { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "wsa", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsa { get; set; }
        [XmlAttribute(AttributeName = "wsdd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsdd { get; set; }
        [XmlAttribute(AttributeName = "chan", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Chan { get; set; }
        [XmlAttribute(AttributeName = "wsa5", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsa5 { get; set; }
        [XmlAttribute(AttributeName = "xmime", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xmime { get; set; }
        [XmlAttribute(AttributeName = "xop", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xop { get; set; }
        [XmlAttribute(AttributeName = "wsrfbf", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsrfbf { get; set; }
        [XmlAttribute(AttributeName = "wstop", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wstop { get; set; }
        [XmlAttribute(AttributeName = "tt", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tt { get; set; }
        [XmlAttribute(AttributeName = "ns3", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ns3 { get; set; }
        [XmlAttribute(AttributeName = "wsrfr", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsrfr { get; set; }
        [XmlAttribute(AttributeName = "ns1", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ns1 { get; set; }
        [XmlAttribute(AttributeName = "ns2", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ns2 { get; set; }
        [XmlAttribute(AttributeName = "ns4", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ns4 { get; set; }
        [XmlAttribute(AttributeName = "tad", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tad { get; set; }
        [XmlAttribute(AttributeName = "daae", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Daae { get; set; }
        [XmlAttribute(AttributeName = "dare", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Dare { get; set; }
        [XmlAttribute(AttributeName = "tan", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tan { get; set; }
        [XmlAttribute(AttributeName = "dn", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Dn { get; set; }
        [XmlAttribute(AttributeName = "ter", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ter { get; set; }
        [XmlAttribute(AttributeName = "tds", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tds { get; set; }
        [XmlAttribute(AttributeName = "tev", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tev { get; set; }
        [XmlAttribute(AttributeName = "wsnt", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsnt { get; set; }
        [XmlAttribute(AttributeName = "timg", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Timg { get; set; }
        [XmlAttribute(AttributeName = "tls", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tls { get; set; }
        [XmlAttribute(AttributeName = "tmd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tmd { get; set; }
        [XmlAttribute(AttributeName = "tptz", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tptz { get; set; }
        [XmlAttribute(AttributeName = "trc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Trc { get; set; }
        [XmlAttribute(AttributeName = "trp", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Trp { get; set; }
        [XmlAttribute(AttributeName = "trt", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Trt { get; set; }
        [XmlAttribute(AttributeName = "trv", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Trv { get; set; }
        [XmlAttribute(AttributeName = "tse", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tse { get; set; }
        [XmlAttribute(AttributeName = "tns1", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tns1 { get; set; }
        [XmlAttribute(AttributeName = "tnshik", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Tnshik { get; set; }
        [XmlAttribute(AttributeName = "xs", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xs { get; set; }
        [XmlAttribute(AttributeName = "wsse", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsse { get; set; }
        [XmlAttribute(AttributeName = "wsu", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Wsu { get; set; }
    }

}

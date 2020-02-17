/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System.Xml.Serialization;
using System.Collections.Generic;

namespace OnvifLib.Models.PullMessageResponse
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

    }

}

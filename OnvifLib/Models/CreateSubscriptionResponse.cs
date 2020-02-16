using System.Xml.Serialization;
using System.Collections.Generic;
using System;

/* 
Licensed under the Apache License, Version 2.0

http://www.apache.org/licenses/LICENSE-2.0
*/

namespace OnvifLib.Models.CreateSubscriptionResponse
{
    [XmlRoot(ElementName = "ReplyTo", Namespace = "http://www.w3.org/2005/08/addressing")]
    public class ReplyTo
    {
        [XmlElement(ElementName = "Address", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string Address { get; set; }
        [XmlAttribute(AttributeName = "mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public string MustUnderstand { get; set; }
    }

    [XmlRoot(ElementName = "To", Namespace = "http://www.w3.org/2005/08/addressing")]
    public class To
    {
        [XmlAttribute(AttributeName = "mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public string MustUnderstand { get; set; }
        [XmlText]
        public string Text { get; set; }
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
        [XmlElement(ElementName = "To", Namespace = "http://www.w3.org/2005/08/addressing")]
        public To To { get; set; }
        [XmlElement(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
        public Action Action { get; set; }
    }

    [XmlRoot(ElementName = "SubscriptionReference", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
    public class SubscriptionReference
    {
        private string _address;

        [XmlElement(ElementName = "Address", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                Uri = new Uri(_address);
            }
        }
        [XmlIgnore]
        public Uri Uri { get; set; }

    }

    [XmlRoot(ElementName = "CreatePullPointSubscriptionResponse", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
    public class CreatePullPointSubscriptionResponse
    {
        private string _currentTime;
        private string _terminationTime;

        [XmlIgnore]
        public DateTime CurrentTimeDT;
        [XmlIgnore]
        public DateTime TerminationTimeDT;

        [XmlElement(ElementName = "SubscriptionReference", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
        public SubscriptionReference SubscriptionReference { get; set; }
        [XmlElement(ElementName = "CurrentTime", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
        public string CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
                CurrentTimeDT = DateTime.Parse(_currentTime);
            }
        }
        [XmlElement(ElementName = "TerminationTime", Namespace = "http://docs.oasis-open.org/wsn/b-2")]
        public string TerminationTime
        {
            get
            {
                return _terminationTime;
            }
            set
            {
                _terminationTime = value;
                TerminationTimeDT = DateTime.Parse(_terminationTime);
            }
        }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Body
    {
        [XmlElement(ElementName = "CreatePullPointSubscriptionResponse", Namespace = "http://www.onvif.org/ver10/events/wsdl")]
        public CreatePullPointSubscriptionResponse CreatePullPointSubscriptionResponse { get; set; }
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


using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;

namespace OnvifLib
{
    public class XMLDateTimeParser
    {
        public XMLDateTimeParser()
        {

        }

        public DateTime Parse(string xml)
        {
            var doc = XDocument.Parse(xml);
            var xmlUTCDateTime = doc.Element(NameSpaces.SOAP_ENV + "Envelope")
                .Element(NameSpaces.SOAP_ENV + "Body")
                .Element(NameSpaces.tds + "GetSystemDateAndTimeResponse")
                .Element(NameSpaces.tds + "SystemDateAndTime")
                .Element(NameSpaces.tt + "UTCDateTime");

            var time = xmlUTCDateTime.Element(NameSpaces.tt + "Time");
            var date = xmlUTCDateTime.Element(NameSpaces.tt + "Date");

            var hour = time.Element(NameSpaces.tt + "Hour").Value;
            var minute = time.Element(NameSpaces.tt + "Minute").Value;
            var second = time.Element(NameSpaces.tt + "Second").Value;

            var year = date.Element(NameSpaces.tt + "Year").Value;
            var month = date.Element(NameSpaces.tt + "Month").Value;
            var day = date.Element(NameSpaces.tt + "Day").Value;

            return new DateTime(
                Int32.Parse(year),
                Int32.Parse(month),
                Int32.Parse(day),
                Int32.Parse(hour),
                Int32.Parse(minute),
                Int32.Parse(second));
        }

        public static XElement RemoveAllNamespaces(XElement e)
        {
            return new XElement(e.Name.LocalName,
              (from n in e.Nodes()
               select ((n is XElement) ? RemoveAllNamespaces(n as XElement) : n)),
                  (e.HasAttributes) ?
                    (from a in e.Attributes()
                     where (!a.IsNamespaceDeclaration)
                     select new XAttribute(a.Name.LocalName, a.Value)) : null);
        }
    }
}

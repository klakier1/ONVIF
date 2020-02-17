using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.Diagnostics;
using OnvifLib.Models.NameSpaces;

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

            var hour = Int32.Parse(time.Element(NameSpaces.tt + "Hour").Value);
            var minute = Int32.Parse(time.Element(NameSpaces.tt + "Minute").Value);
            var second = Int32.Parse(time.Element(NameSpaces.tt + "Second").Value);

            var year = Int32.Parse(date.Element(NameSpaces.tt + "Year").Value);
            var month = Int32.Parse(date.Element(NameSpaces.tt + "Month").Value);
            var day = Int32.Parse(date.Element(NameSpaces.tt + "Day").Value);

            if (hour < 0)
            {
                var dt = new DateTime(year, month, day, 0, minute, second);
                dt = dt.AddHours(hour);
                return dt;
            }

            return new DateTime(year, month, day, hour, minute, second);
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

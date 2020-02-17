using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class GetDateAndTime
    {
        private string _template = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><GetSystemDateAndTime xmlns=\"http://www.onvif.org/ver10/device/wsdl\"/></s:Body></s:Envelope>";

        public GetDateAndTime() { }

        public string ToXML()
        {
            return _template;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class SetDateAndTime
    {
        private UsernameTokenGenerator usernameTokenGenerator;
        private readonly string _template = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Header><Security s:mustUnderstand=\"1\" xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\"><UsernameToken><Username>{0}</Username><Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">{1}</Password><Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">{2}</Nonce><Created xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">{3}</Created></UsernameToken></Security></s:Header><s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><SetSystemDateAndTime xmlns=\"http://www.onvif.org/ver10/device/wsdl\"><DateTimeType>Manual</DateTimeType><DaylightSavings>true</DaylightSavings><TimeZone><TZ xmlns=\"http://www.onvif.org/ver10/schema\">WEuropeStandardTime-1DaylightTime,M3.5.0,M10.5.0/3</TZ></TimeZone><UTCDateTime><Time xmlns=\"http://www.onvif.org/ver10/schema\"><Hour>{4}</Hour><Minute>{5}</Minute><Second>{6}</Second></Time><Date xmlns=\"http://www.onvif.org/ver10/schema\"><Year>{7}</Year><Month>{8}</Month><Day>{9}</Day></Date></UTCDateTime></SetSystemDateAndTime></s:Body></s:Envelope>";

        public SetDateAndTime(UsernameTokenGenerator generator)
        {
            usernameTokenGenerator = generator;

        }

        public string ToXML(string created = null)
        {
            var token = usernameTokenGenerator.GetUsernameTokenData(created);
            var now = DateTime.UtcNow;
            return String.Format(_template, token.Login, token.PasswordDigest, token.Nounce, token.Created,
                now.Hour, now.Minute, now.Second, now.Year, now.Month, now.Day);
        }
    }
}

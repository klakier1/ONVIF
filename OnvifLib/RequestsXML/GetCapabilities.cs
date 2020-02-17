using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class GetCapabilities
    {
        private UsernameTokenGenerator usernameTokenGenerator;
        private Guid guid;
        private string _template = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\"><s:Header><Security s:mustUnderstand=\"1\" xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\"><UsernameToken><Username>{0}</Username><Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">{1}</Password><Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">{2}</Nonce><Created xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">{3}</Created></UsernameToken></Security></s:Header><s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><GetCapabilities xmlns=\"http://www.onvif.org/ver10/device/wsdl\"><Category>All</Category></GetCapabilities></s:Body></s:Envelope>";

        public GetCapabilities(UsernameTokenGenerator generator)
        {
            usernameTokenGenerator = generator;
            guid = Guid.NewGuid();
        }

        public string ToXML(string created = null)
        {
            var token = usernameTokenGenerator.GetUsernameTokenData(created);
            return String.Format(_template, token.Login, token.PasswordDigest, token.Nounce, token.Created);
        }
    }
}

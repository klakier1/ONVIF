using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class Unsubscribe
    {

        private UsernameTokenGenerator _usernameTokenGenerator;
        private string _address;
        private Guid _guid;
        private string _template = "<?xml version=\"1.0\"?><s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:a=\"http://www.w3.org/2005/08/addressing\"><s:Header><a:Action s:mustUnderstand=\"1\">http://docs.oasis-open.org/wsn/bw-2/SubscriptionManager/UnsubscribeRequest</a:Action><a:MessageID>urn:uuid:{0}</a:MessageID><a:ReplyTo><a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address></a:ReplyTo><Security xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" s:mustUnderstand=\"1\"><UsernameToken><Username>{1}</Username><Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">{2}</Password><Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">{3}</Nonce><Created xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">{4}</Created></UsernameToken></Security><a:To s:mustUnderstand=\"1\">{5}</a:To></s:Header><s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Unsubscribe xmlns=\"http://docs.oasis-open.org/wsn/b-2\"/></s:Body></s:Envelope>";
        public Unsubscribe(UsernameTokenGenerator generator, string address)
        {
            _usernameTokenGenerator = generator;
            _guid = Guid.NewGuid();
            _address = address;
            // uuid, login, password digest, nonce, created

        }

        public string ToXML()
        {
            var token = _usernameTokenGenerator.GetUsernameTokenData();
            return String.Format(_template, _guid, token.Login, token.PasswordDigest, token.Nounce, token.Created, _address);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class PullMessageRequest
    {

        private UsernameTokenGenerator _usernameTokenGenerator;
        private string _address;
        private Guid _guid;
        private string _template = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:a=\"http://www.w3.org/2005/08/addressing\"><s:Header><a:Action s:mustUnderstand=\"1\">http://www.onvif.org/ver10/events/wsdl/PullPointSubscription/PullMessagesRequest</a:Action><a:MessageID>urn:uuid:{0}</a:MessageID><a:ReplyTo><a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address></a:ReplyTo><Security s:mustUnderstand=\"1\" xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\"><UsernameToken><Username>{1}</Username><Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">{2}</Password><Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">{3}</Nonce><Created xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">{4}</Created></UsernameToken></Security><a:To s:mustUnderstand=\"1\">{5}</a:To></s:Header><s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><PullMessages xmlns=\"http://www.onvif.org/ver10/events/wsdl\"><Timeout>{6}</Timeout><MessageLimit>{7}</MessageLimit></PullMessages></s:Body></s:Envelope>";
        private string _timeout = "PT1M";
        private int _msgLimit = 1024;

        public PullMessageRequest(UsernameTokenGenerator generator, string address)
        {
            _usernameTokenGenerator = generator;
            _guid = Guid.NewGuid();
            _address = address;
            // uuid, login, password digest, nonce, created

        }

        public string ToXML()
        {
            var token = _usernameTokenGenerator.GetUsernameTokenData();
            return String.Format(_template, _guid, token.Login, token.PasswordDigest, token.Nounce, token.Created, _address, _timeout, _msgLimit);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class CreateSubscription
    {

        private UsernameTokenGenerator _usernameTokenGenerator;
        private string _address;
        private Guid _guid;
        private string _iniTerminationTime = "PT600S";
        private readonly string _template = "<s:Envelope xmlns:s=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:a=\"http://www.w3.org/2005/08/addressing\"><s:Header><a:Action s:mustUnderstand=\"1\">http://www.onvif.org/ver10/events/wsdl/EventPortType/CreatePullPointSubscriptionRequest</a:Action><a:MessageID>urn:uuid:{0}</a:MessageID><a:ReplyTo><a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address></a:ReplyTo><Security s:mustUnderstand=\"1\" xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\"><UsernameToken><Username>{1}</Username><Password Type=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">{2}</Password><Nonce EncodingType=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">{3}</Nonce><Created xmlns=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">{4}</Created></UsernameToken></Security><a:To s:mustUnderstand=\"1\">{5}</a:To></s:Header><s:Body xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><CreatePullPointSubscription xmlns=\"http://www.onvif.org/ver10/events/wsdl\"><InitialTerminationTime>{6}</InitialTerminationTime></CreatePullPointSubscription></s:Body></s:Envelope>";

        public string IniTerminationTime { get => _iniTerminationTime; set => _iniTerminationTime = value; }
        public string Address { get => _address; set => _address = value; }
        public Guid Guid { get => _guid; set => _guid = value; }
        public UsernameTokenGenerator UsernameTokenGenerator { get => _usernameTokenGenerator; set => _usernameTokenGenerator = value; }

        public CreateSubscription(UsernameTokenGenerator generator, string address)
        {
            UsernameTokenGenerator = generator;
            Guid = Guid.NewGuid();
            Address = address;
            // uuid, login, password digest, nonce, created

        }

        public string ToXML()
        {
            var token = UsernameTokenGenerator.GetUsernameTokenData();
            return String.Format(_template, Guid, token.Login, token.PasswordDigest, token.Nounce, token.Created, Address, IniTerminationTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace OnvifLib
{
    public class Get
    {
        private UsernameTokenGenerator usernameTokenGenerator;
        private Guid guid;
        private string _template = "";

        public Get(UsernameTokenGenerator generator)
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

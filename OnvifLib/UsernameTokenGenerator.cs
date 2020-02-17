using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OnvifLib
{
    public class UsernameTokenGenerator
    {
        string _login;
        string _pass;
        byte[] _nounce = new Byte[24];
        byte[] _tag = new Byte[] { 0x28, 0xb0, 0x0a, 0x20, 0x44 }; //as begin of nonce, then I know that its my packet

        public string Pass { get => _pass; set => _pass = value; }
        private byte[] Nounce { get => _nounce; set => _nounce = value; }
        public string Login { get => _login; set => _login = value; }

        public UsernameTokenGenerator(string login, string pass)
        {
            this.Login = login;
            this.Pass = pass;
            new Random().NextBytes(_nounce); //generate nounce
            Buffer.BlockCopy(_tag, 0, Nounce, 0, _tag.Length);
        }



        public void SetNounce(string nounce)
        {
            Nounce = Convert.FromBase64String(nounce);
        }

        public UsernameTokenData GetUsernameTokenData(string created = null)
        {
            if (created == null)
            {
                created = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            }
            byte[] buffCreated = Encoding.UTF8.GetBytes(created); //encode created

            byte[] buffPass = Encoding.UTF8.GetBytes(_pass); //encode password

            byte[] buffConcat = new byte[Nounce.Length + buffCreated.Length + buffPass.Length];
            Buffer.BlockCopy(Nounce, 0, buffConcat, 0, Nounce.Length);
            Buffer.BlockCopy(buffCreated, 0, buffConcat, Nounce.Length, buffCreated.Length);
            Buffer.BlockCopy(buffPass, 0, buffConcat, Nounce.Length + buffCreated.Length, buffPass.Length);

            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashed = sha.ComputeHash(buffConcat);
            string passwordDigest = Convert.ToBase64String(hashed);
            string base64nounce = Convert.ToBase64String(Nounce);

            Nounce[19]++; //change nounce for next generation, add one to bit 19 (or any other) every time

            return new UsernameTokenData()
            {
                PasswordDigest = passwordDigest,
                Created = created,
                Nounce = base64nounce,
                Login = this.Login
            };
        }
    }

    public class UsernameTokenData
    {
        public string Login { get; set; }
        public string Created { get; set; }
        public string Nounce { get; set; }
        public string PasswordDigest { get; set; }

        public override string ToString()
        {
            return $"{PasswordDigest} {Nounce} {Created} {Login}";
        }

    }
}




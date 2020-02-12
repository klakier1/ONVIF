using OnvifLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnvifTestApp
{
    class Program
    {
        static UsernameTokenGenerator Generator = new UsernameTokenGenerator("admin", "Dwapiatka25");

        static void Main(string[] args)
        {
            SetDateAndTime();
        }

        static void XMLProbeMatchChecker()
        {
            new XMLProbeMatchParser().Parse(XMLProbeMatchParser.TestString);
        }

        static void TokenChecker()
        {
            //string created = "2020-02-09T14:55:03.003Z";
            //string nonce = "sdx+I7tmu0GOZhvIdmdjrQEAAAAAAA==";
            //string passwordDigest = "6T1RFhTB6qXtigx0d6vQsC0AEz0=";

            //string created = "2020-02-10T11:09:26.249Z";
            //string nonce = "bkjy4daHn1uN8G04aqBSQLxG2MtBQtQb";
            //string passwordDigest = "89WVi717ArqeJqxZgHpPBXkUEdc=";

            string created = "2020-02-09T13:10:50.005Z";
            string nonce = "ezpvsVgPkky8BxGsQbhr+AEAAAAAAA==";
            string passwordDigest = "7cxOM6bvptyQu/3ytoYYvsABdSk=";

            string login = "admin";
            string pass = "Dwapiatka25";

            var gen = new UsernameTokenGenerator(login, pass);
            gen.SetNounce(nonce);
            var passwordDigestGenerated = gen.GetUsernameTokenData(created);

            Debug.WriteLine(passwordDigest + " " + nonce);
            Debug.WriteLine(passwordDigestGenerated);
            //89WVi717ArqeJqxZgHpPBXkUEdc= bkjy4daHn1uN8G04aqBSQLxG2MtBQtQb 2020-02-10T11:09:26.249Z admin
        }

        static void SetDateAndTime()
        {
            Debug.WriteLine(new SetDateAndTime(Generator).ToXML());
        }
    }
}

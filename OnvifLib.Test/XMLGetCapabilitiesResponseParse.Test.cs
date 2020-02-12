using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnvifLib;


namespace OnvifLib.Test
{
    [TestClass]
    public class XMLGetCapabilitiesResponseParseTest
    {
        [TestMethod]
        public void XMLGetCapabilitiesResponse_TestXML_returnMoreThenZeroElements()
        {
            var parser = new XMLGetCapabilitiesResponseParser();

            var list = parser.Parse(parser.TestString);

            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void PullMessageRequest_TestData_returnNotNull()
        {
            var generator = new UsernameTokenGenerator("admin", "Dwapiatka25");
            var address = "http://192.168.2.124/onvif/Events/Subscription?index=0";

            var request = new PullMessageRequest(generator, address);
            var xml =  request.ToXML();

            Debug.WriteLine(xml);

            Assert.IsNotNull(xml);
        }

        [DataRow(XMLPullMessagesResponseParser.TestString, "1")]
        //[DataRow(XMLPullMessagesResponseParse.TestString2, 3)]
        [DataTestMethod]
        public void XMLPullMessagesResponseParse_TestData_returnMoreThenZeroElements(string xml, string result)
        {
            var parser = new XMLPullMessagesResponseParser();

            var obj = parser.Parse(xml);

            Assert.AreEqual("1", result);
        }
    }
}

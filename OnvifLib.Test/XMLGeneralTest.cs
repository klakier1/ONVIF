using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnvifLib;


namespace OnvifLib.Test
{
    [TestClass]
    public class XMLGeneralTest
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
            var xml = request.ToXML();

            Debug.WriteLine(xml);

            Assert.IsNotNull(xml);
        }

        [DataRow(XMLPullMessagesResponseParser.TestString1, 3)]
        [DataRow(XMLPullMessagesResponseParser.TestString2, 1)]
        [DataRow(XMLPullMessagesResponseParser.TestString3, 1)]
        [DataRow(XMLPullMessagesResponseParser.TestString4, 0)]
        [DataTestMethod]
        public void XMLPullMessagesResponseParser_TestData_returnMoreThenZeroElements(string xml, int expected)
        {
            var parser = new XMLPullMessagesResponseParser();
            var actual = -1;

            var obj = parser.Parse(xml);
            if (obj.Body.PullMessagesResponse.NotificationMessage != null)
            {
                actual = obj.Body.PullMessagesResponse.NotificationMessage.Message2.Message.Source.SimpleItem.Count;
            }
            else
            {
                actual = 0;
            }

            Assert.AreEqual(expected, actual);
        }

        [DataRow(XMLCreateSubscriptionResponseParser.TestString1)]
        [DataTestMethod]
        public void XMLCreateSubscriptionResponseParser_TestData_returnSubscriptionReferenceNotNull(string xml)
        {
            var parser = new XMLCreateSubscriptionResponseParser();
            var value = false;

            var obj = parser.Parse(xml);
            if (obj.Body.CreatePullPointSubscriptionResponse != null)
            {
                value = obj.Body.CreatePullPointSubscriptionResponse.SubscriptionReference != null;
            }

            Assert.IsNotNull(value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnvifLib
{
    public class ConnectionTask
    {
        private HttpClient _httpClient = new HttpClient();
        private UsernameTokenGenerator _generator;
        private Camera _cam;
        private Task _task;
        private CancellationTokenSource _cancellationTokenSource;

        public Camera Cam { get => _cam; set => _cam = value; }
        public HttpClient HttpClient { get => _httpClient; set => _httpClient = value; }
        public UsernameTokenGenerator Generator { get => _generator; set => _generator = value; }


        public ConnectionTask(Camera cam, string login, string password)
        {
            Cam = cam;
            HttpClient.DefaultRequestHeaders.Connection.Add("Close");
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            HttpClient.DefaultRequestHeaders.ExpectContinue = false;
            Generator = new UsernameTokenGenerator(login, password);
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public void Connect()
        {
            _task = new Task(MainTask);
            _task.Start();
        }
        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public async void MainTask()
        {
            var token = _cancellationTokenSource.Token;

            //getTime
            String xml = new GetDateAndTime().ToXML();
            HttpContent content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(Cam.ProbeResult.XAddrs, content, token);
            var responseXml = await response.Content.ReadAsStringAsync();
            var dateTimeParser = new XMLDateTimeParser();
            DateTime cameraTime = dateTimeParser.Parse(responseXml);

            Console.WriteLine(cameraTime.ToString());

            //setTime
            string createdTime = cameraTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            xml = new SetDateAndTime(Generator).ToXML(createdTime);
            content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            response = await HttpClient.PostAsync(Cam.ProbeResult.XAddrs, content, token);
            responseXml = await response.Content.ReadAsStringAsync();

            //getTime
            xml = new GetDateAndTime().ToXML();
            content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            response = await HttpClient.PostAsync(Cam.ProbeResult.XAddrs, content, token);
            responseXml = await response.Content.ReadAsStringAsync();
            cameraTime = dateTimeParser.Parse(responseXml);

            Console.WriteLine("New Time");
            Console.WriteLine(cameraTime.ToString());

            //getCapabilities
            xml = new GetCapabilities(Generator).ToXML();
            content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            response = await HttpClient.PostAsync(Cam.ProbeResult.XAddrs, content, token);
            responseXml = await response.Content.ReadAsStringAsync();
            var capabilitiesParser = new XMLGetCapabilitiesResponseParser();
            var capabilities = capabilitiesParser.Parse(responseXml);
            Cam.Capability = capabilities;

            //check if has event capability
            var eventCapability = Cam.Capability.Where(q => q.Name.LocalName.Equals("Events")).FirstOrDefault();
            if (eventCapability == null)
                return;

            //createSubscription
            xml = new CreateSubscription(Generator, eventCapability.XAddr.AbsoluteUri)
            {
                IniTerminationTime = "PT600S"
            }
            .ToXML();

            content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            response = await HttpClient.PostAsync(eventCapability.XAddr, content, token);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Event creation failed => { response.StatusCode }");
                return;
            }
            responseXml = await response.Content.ReadAsStringAsync();
            var createSubscriptionResponse = new XMLCreateSubscriptionResponseParser().Parse(responseXml);

            var pullResponseParser = new XMLPullMessagesResponseParser();
            for (; ; )
            {
                xml = new PullMessageRequest(Generator, createSubscriptionResponse.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri).ToXML();
                content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
                response = await HttpClient.PostAsync(createSubscriptionResponse.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri, content, token);
                if(response.StatusCode != HttpStatusCode.OK)
                {
                    Debug.WriteLine($"Response Error >>>>>>>>>>>> {response.StatusCode} >> {response.Content.ToString()}");
                    continue;
                }
                responseXml = await response.Content.ReadAsStringAsync();
                var pullResponse = pullResponseParser.Parse(responseXml);

                var msg = pullResponse.Body.PullMessagesResponse.NotificationMessage;
                var item = msg.Message2.Message.Data.SimpleItem.First();

                //Console.Clear();
                Console.WriteLine(msg.Topic.Text);
                Console.WriteLine($"{item.Name} => {item.Value}");
            }
        }
    }
}

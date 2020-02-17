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
            _task = new Task(() =>
                {
                    try
                    {
                        MainTask();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                });
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
            DateTime cameraTime = await GetTime(token);

            Console.WriteLine($"Old Time { cameraTime.ToString() }");

            //setTime
            string createdTime = cameraTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            bool success = await SetTime(token, createdTime);
            if (!success) return;

            //getTime
            cameraTime = await GetTime(token);

            Console.WriteLine($"New Time { cameraTime.ToString() }");

            //getCapabilities
            Cam.Capability = await GetCapabilities(token);

            //check if has event capability
            var eventCapability = Cam.Capability.Where(q => q.Name.LocalName.Equals("Events")).FirstOrDefault();
            if (eventCapability == null)
                return;

            //createSubscription
            success = await CreateSubscription(token, Cam);
            if (!success) return;

            Debug.WriteLine($"Subscription { Cam.Subscription.Body.CreatePullPointSubscriptionResponse.CurrentTimeDT.ToString() } >> { Cam.Subscription.Body.CreatePullPointSubscriptionResponse.TerminationTimeDT.ToString() }");

            //pullMessageRequest
            for (; ; )
            {

                success = await PullMessageRequest(token, Cam);
                if (success)
                {
                    Models.PullMessageResponse.NotificationMessage msg = Cam.PullResponse.Body.PullMessagesResponse.NotificationMessage;
                    Models.PullMessageResponse.SimpleItem item = msg.Message2.Message.Data.SimpleItem.First();

                    Console.WriteLine(msg.Topic.Text);
                    Console.WriteLine($"{item.Name} => {item.Value}");
                }
                else
                {
                    await UnsubscribeRequest(token, Cam);
                    return;
                }
            }
        }

        async Task<DateTime> GetTime(CancellationToken token)
        {
            //getTime
            String xml = new GetDateAndTime().ToXML();
            HttpContent content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(Cam.ProbeResult.XAddrs, content, token);
            var responseXml = await response.Content.ReadAsStringAsync();
            var dateTimeParser = new XMLDateTimeParser();
            DateTime cameraTime = dateTimeParser.Parse(responseXml);
            return cameraTime;
        }

        async Task<bool> SetTime(CancellationToken token, string createdTime = null)
        {
            var xml = new SetDateAndTime(Generator).ToXML(createdTime);
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(Cam.ProbeResult.XAddrs, content, token);
            return response.IsSuccessStatusCode;
        }

        async Task<List<Capability>> GetCapabilities(CancellationToken token)
        {
            var xml = new GetCapabilities(Generator).ToXML();
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(Cam.ProbeResult.XAddrs, content, token);
            var responseXml = await response.Content.ReadAsStringAsync();
            var capabilitiesParser = new XMLGetCapabilitiesResponseParser();
            var capabilities = capabilitiesParser.Parse(responseXml);
            return capabilities;
        }

        async Task<bool> CreateSubscription(CancellationToken token, Camera cam)
        {
            //check if has event capability
            var eventCapability = Cam.Capability.Where(q => q.Name.LocalName.Equals("Events")).FirstOrDefault();
            if (eventCapability == null)
                return false;

            var xml = new CreateSubscription(Generator, eventCapability.XAddr.AbsoluteUri).ToXML();

            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(eventCapability.XAddr, content, token);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Event creation failed => { response.StatusCode }");
                return false;
            }
            var responseXml = await response.Content.ReadAsStringAsync();
            var createSubscriptionResponse = new XMLCreateSubscriptionResponseParser().Parse(responseXml);
            cam.Subscription = createSubscriptionResponse;
            return true;
        }

        async Task<bool> PullMessageRequest(CancellationToken token, Camera cam)
        {
            var pullResponseParser = new XMLPullMessagesResponseParser();
            var xml = new PullMessageRequest(Generator, cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri).ToXML();
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri, content, token);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Debug.WriteLine($"Response Error >>>>>>>>>>>> {DateTime.Now} >> {response.StatusCode} >> {response.Content.ToString()}");
                return false;
            }
            var responseXml = await response.Content.ReadAsStringAsync();
            cam.PullResponse = pullResponseParser.Parse(responseXml);

            return true;
        }

        async Task<bool> UnsubscribeRequest(CancellationToken token, Camera cam)
        {
            var xml = new Unsubscribe(Generator, cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri).ToXML();;
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri, content, token);
            return response.IsSuccessStatusCode;
        }

        async Task<bool> RenewRequest(CancellationToken token, Camera cam)
        {
            var xml = new RenewSubscription(Generator, cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri).ToXML(); ;
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri, content, token);
            if (!response.IsSuccessStatusCode) return false;
            var responseXml = await response.Content.ReadAsStringAsync();
            var renewResponse = new XMLRenewResponseParser().Parse(responseXml);
            cam.RenewResponse = renewResponse;
            return true;

        }
    }
}

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

        public delegate void ConnectionStateChanged(Camera cam);
        public event ConnectionStateChanged Disconnected;

        public ConnectionTask(Camera cam, string login, string password)
        {
            Cam = cam;
            HttpClient.DefaultRequestHeaders.Connection.Add("Close");
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            HttpClient.DefaultRequestHeaders.ExpectContinue = false;
            HttpClient.Timeout = new TimeSpan(0, 0, 30); // 30 seconds
            Generator = new UsernameTokenGenerator(login, password);
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public void Connect()
        {
            _task = new Task(async () =>
                {
                    try
                    {
                        await MainTask();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                    finally
                    {
                        //delete camera from the list
                        Disconnected?.Invoke(Cam);
                    }
                });
            _task.Start();
        }
        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public async Task MainTask()
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
            success = await GetCapabilities(token, Cam);
            if (!success) return;

            //check if has event capability
            var eventCapability = Cam.Capability.Where(q => q.Name.LocalName.Equals("Events")).FirstOrDefault();
            if (eventCapability == null)
                return;

            int subscriptionCounter = 0;

            for (; ; )
            {
                //createSubscription
                success = await CreateSubscription(token, Cam);
                if (!success)
                    return;

                string currentTime = Cam.Subscription.Body.CreatePullPointSubscriptionResponse.CurrentTimeDT.ToString();
                string terminationTime = Cam.Subscription.Body.CreatePullPointSubscriptionResponse.TerminationTimeDT.ToString();
                string address = Cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Address;
                Console.WriteLine($"Subscription { currentTime } >> { terminationTime } >> { address }");

                subscriptionCounter++; //increaa counter, count attemps to make subscription
                Debug.WriteLine($"Subscription counter { subscriptionCounter }");
                if (subscriptionCounter > 3) return; //after 3 fails, cancel task

                //pullMessageRequest
                for (; ; )
                {
                    if (token.IsCancellationRequested)
                        return;

                    success = await PullMessageRequest(token, Cam);
                    if (success)
                    {
                        subscriptionCounter = 0; //reset counter , if pullMessageResponse was ok reset counter

                        Models.PullMessageResponse.NotificationMessage msg = Cam.PullResponse.Body.PullMessagesResponse.NotificationMessage;
                        Models.PullMessageResponse.SimpleItem item = msg.Message2.Message.Data.SimpleItem.First();

                        //Console.WriteLine(msg.Topic.Text);
                        //Console.WriteLine($"{item.Name} => {item.Value}");

                        success = await RenewRequest(token, Cam);
                    }

                    if (!success)
                    {
                        await UnsubscribeRequest(token, Cam);
                        Console.WriteLine($"Unsubscribe >> { address }");
                        await Task.Delay(1000);
                        break;
                    }
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

        async Task<bool> GetCapabilities(CancellationToken token, Camera cam)
        {
            var xml = new GetCapabilities(Generator).ToXML();
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(cam.ProbeResult.XAddrs, content, token);
            if (!response.IsSuccessStatusCode) return false;
            var responseXml = await response.Content.ReadAsStringAsync();
            var capabilitiesParser = new XMLGetCapabilitiesResponseParser();
            var capabilities = capabilitiesParser.Parse(responseXml);
            cam.Capability = capabilities;
            return true;
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
            var xml = new PullMessageRequest(Generator, cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri).ToXML();
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri, content, token);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Response Error Pull Message>> {DateTime.Now} >> {response.StatusCode}");
                return false;
            }
            var responseXml = await response.Content.ReadAsStringAsync();
            var pullResponseParser = new XMLPullMessagesResponseParser();
            cam.PullResponse = pullResponseParser.Parse(responseXml);

            return true;
        }

        async Task<bool> UnsubscribeRequest(CancellationToken token, Camera cam)
        {
            var xml = new Unsubscribe(Generator, cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri).ToXML(); ;
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri, content, token);
            return response.IsSuccessStatusCode;
        }

        async Task<bool> RenewRequest(CancellationToken token, Camera cam)
        {
            var xml = new RenewSubscription(Generator, cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri).ToXML(); ;
            var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            var response = await HttpClient.PostAsync(cam.Subscription.Body.CreatePullPointSubscriptionResponse.SubscriptionReference.Uri.AbsoluteUri, content, token);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine($"Response Error Renew >> {DateTime.Now} >> {response.StatusCode}");
                return false;
            }
            var responseXml = await response.Content.ReadAsStringAsync();
            var renewResponse = new XMLRenewResponseParser().Parse(responseXml);
            cam.RenewResponse = renewResponse;
            return true;

        }
    }
}

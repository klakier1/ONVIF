using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnvifLib;
/*
 *  TODO
 *  -sprawdzac wszedzie czy status jest 200, jak nie to unsubscribe i od nowa subscribe
 */ 
namespace OnvifConnect
{
    class Program
    {
        private static readonly string _login = "admin";
        private static readonly string _password = "Dwapiatka25";

        private static string Login => _login;
        private static string Password => _password;

        static void Main(string[] args)
        {
            CameraFinder cameraFinder = new CameraFinder();
            cameraFinder.CameraFoundEvent += CameraFinder_CameraFoundEvent;
            cameraFinder.CameraUpgradeEvent += CameraFinder_CameraUpgradeEvent;
            cameraFinder.StartSearching();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            cameraFinder.StopSearching();
            cameraFinder.Cameras.ForEach(c => c.Cancel());
        }

        private static void CameraFinder_CameraUpgradeEvent(Camera cam)
        {
            //Debug.WriteLine($"Program => CameraUpgrageEvent \t=> {cam.ProbeResult.ToString()}");
            //Console.WriteLine($"Program => CameraUpgrageEvent \t=> {cam.ProbeResult.ToString()}");
        }

        private static void CameraFinder_CameraFoundEvent(Camera cam)
        {
            //Debug.WriteLine($"Program => CameraFoundEvent \t=> {cam.ProbeResult.ToString()}");
            //Console.WriteLine($"Program => CameraFoundEvent \t=> {cam.ProbeResult.ToString()}");

            cam.Connect(Login, Password);
        }

        static void Test1()
        {
            //string ip = "192.168.1.4";
            //string address = $"http://{ ip }/onvif/Events";
            //UsernameTokenGenerator generator = new UsernameTokenGenerator("admin", "Dwapiatka25");

            ////generator.SetNounce(@"0stEvo5ZTEil8MfjljQ+MgQAAAAAAA==");
            ////Console.WriteLine(generator.GetUsernameTokenData(@"2020-02-01T00:33:06.481Z").ToString());

            //address = $"http://{ ip }/onvif/Events";
            //string xml = new GetDateAndTime().ToXML();
            //var content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");

            ////datetime 1
            //var task = client.PostAsync(address, content);
            //var response = task.Result;
            //Debug.WriteLine("**** RESULT -> " + response.StatusCode + " ****");

            //var taskReadDate = response.Content.ReadAsStringAsync();
            //var responseXml = taskReadDate.Result;
            //DateTime cameraTime = new XMLDateTimeParser().Parse(responseXml);

            //Debug.WriteLine(cameraTime.ToString());



            ////get DNS
            //string created = cameraTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            //xml = new GetDNS(generator).ToXML(created);
            //content = new StringContent(xml, Encoding.UTF8, "application/soap+xml");
            //client.DefaultRequestHeaders.Connection.Add("Close");

            ////client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            ////client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            ////var qwe = new NameValueHeaderValue("action", "\"http://www.onvif.org/ver10/device/wsdl/GetDNS\"");
            ////content.Headers.ContentType.Parameters.Add(qwe);
            //task = client.PostAsync(address, content);
            //response = task.Result;
            //Debug.WriteLine("**** RESULT -> " + response.StatusCode + " ****");
            //Debug.Write(response.Content.ReadAsStringAsync().Result.ToString());
        }

        static void Test2()
        {

            string ipAddress = "239.255.255.250";
            int port = 3702;
            UdpClient client = new UdpClient();
            new Task(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    byte[] data = Encoding.ASCII.GetBytes(new ProbeXML().GetProbeDeviceXML());
                    client.Send(data, data.Length, new IPEndPoint(IPAddress.Parse(ipAddress), port));
                    Thread.Sleep(2000);
                }

            }).Start();
            new Task(() =>
            {
                IPAddress ip = IPAddress.Any;
                IPEndPoint recv = new IPEndPoint(IPAddress.Any, 55555);
                for (int i = 0; i < 50; i++)
                {
                    byte[] buffer = client.Receive(ref recv);
                    Console.WriteLine(Encoding.UTF8.GetString(buffer));
                }

            }).Start();



            Console.ReadKey();
        }

    }
}

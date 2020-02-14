using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnvifLib;

namespace OnvifConnect
{
    class CameraFinder
    {
        public List<Camera> Cameras = new List<OnvifLib.Camera>();
        private CancellationTokenSource Ts = new CancellationTokenSource();
        private const string _ipAddress = "239.255.255.250";
        private const int _port = 3702;
        private UdpClient _client;
        private IPEndPoint _broadcastEndPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), _port);

        public delegate void CameraFound(Camera cam);
        public event CameraFound CameraFoundEvent;
        public bool IsSearching = false;

        private Task _receiveTask;
        private Task _sendTask;


        public CameraFinder()
        {
            _client = new UdpClient(0);
        }

        public void StartSearching()
        {
            if (IsSearching == true)
                return;
            IsSearching = true;

            _sendTask = new Task(async () =>
            {
                try
                {
                    do
                    {
                        byte[] data = Encoding.ASCII.GetBytes(new ProbeXML().GetProbeDeviceXML());
                        await _client.SendAsync(data, data.Length, _broadcastEndPoint);
                        await Task.Delay(5000, Ts.Token);
                    } while (IsSearching);
                }
                catch (ObjectDisposedException e)
                {
                    IsSearching = false;
                    Debug.WriteLine("Socket Disposed");
                }
                catch (OperationCanceledException e)
                {
                    IsSearching = false;
                    Debug.WriteLine("Wysyłanie broadcastu zakonczone przez token");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Task Wysyłania: {e.Message}");
                }
            }, Ts.Token);

            _receiveTask = new Task(async () =>
            {

                do
                {
                    UdpReceiveResult receiveResult;
                    try
                    {
                        receiveResult = await _client.ReceiveAsync();
                    }
                    catch (ObjectDisposedException e)
                    {
                        IsSearching = false;
                        Debug.WriteLine("Socket Disposed");
                    }
                    catch (OperationCanceledException e)
                    {
                        IsSearching = false;
                        Debug.WriteLine("Odbieranie broadcastu zakonczone przez token");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Task Odbierania: {e.Message}");
                    }
                    string response = Encoding.UTF8.GetString(receiveResult.Buffer);
                    var parser = new XMLProbeMatchParser();
                    List<ProbeMatch> devices = parser.Parse(response);
                    foreach (ProbeMatch match in devices)
                    {
                        var device = new Camera()
                        {
                            ProbeResult = match
                        };

                        CameraFoundEvent?.Invoke(device);
                    }
                } while (IsSearching);

            }, Ts.Token);

            _sendTask.Start();
            _receiveTask.Start();
        }

        public void StopSearching()
        {
            Ts.Cancel();
            if (IsSearching == false)
                return;
            IsSearching = false;
            _client.Dispose();
        }


    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OnvifLib;

namespace OnvifLib
{
    public class CameraFinder
    {
        public List<Camera> Cameras = new List<Camera>();
        private CancellationTokenSource Ts = new CancellationTokenSource();
        private const string _ipAddress = "239.255.255.250";
        private const int _port = 3702;
        private UdpClient _client;
        private readonly IPEndPoint _broadcastEndPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), _port);

        public delegate void CameraFound(Camera cam);
        public event CameraFound CameraFoundEvent;
        public event CameraFound CameraUpgradeEvent;
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
                    byte[] dataProbeDevice = Encoding.ASCII.GetBytes(new ProbeXML().GetProbeDeviceXML());
                    byte[] dataProbeNetworkVideoTransmitter = Encoding.ASCII.GetBytes(new ProbeXML().GetProbeNetworkVideoTransmitterXML());
                    do
                    {
                        await _client.SendAsync(dataProbeNetworkVideoTransmitter, dataProbeNetworkVideoTransmitter.Length, _broadcastEndPoint);
                        await _client.SendAsync(dataProbeDevice, dataProbeDevice.Length, _broadcastEndPoint);
                        await Task.Delay(10000, Ts.Token);
                    } while (IsSearching);
                }
                catch (ObjectDisposedException)
                {
                    IsSearching = false;
                    Debug.WriteLine("Socket Disposed");
                }
                catch (OperationCanceledException)
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
                    catch (ObjectDisposedException)
                    {
                        IsSearching = false;
                        Debug.WriteLine("Socket Disposed");
                    }
                    catch (OperationCanceledException)
                    {
                        IsSearching = false;
                        Debug.WriteLine("Odbieranie broadcastu zakonczone przez token");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine($"Task Odbierania: {e.Message}");
                    }

                    OnRespone(receiveResult);

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

        private void OnRespone(UdpReceiveResult receiveResult)
        {
            string response = Encoding.UTF8.GetString(receiveResult.Buffer);
            var parser = new XMLProbeMatchParser();
            List<ProbeMatch> devices = parser.Parse(response);
            foreach (ProbeMatch match in devices)
            {
                var device = new Camera()
                {
                    ProbeResult = match
                };

                Camera temp = null; //to store camera if already exist on the list, to check if contains also type of device
                if (Cameras.Count(p =>
                {
                    var res = p.ProbeResult.XAddrs.Equals(device.ProbeResult.XAddrs);
                    if (res) temp = p;
                    return res;
                }) == 0) //if contains device with this XAddrs
                {
                    Cameras.Add(device);
                    CameraFoundEvent?.Invoke(device);
                }
                else
                {
                    if (!temp.ProbeResult.Types.Contains(device.ProbeResult.Types.First()))
                    {
                        temp.ProbeResult.Types.Add(device.ProbeResult.Types.First());
                        CameraUpgradeEvent?.Invoke(temp);
                    }
                    else
                    {
                        //Debug.WriteLine($"CameraFinder => Device is already on the list: {device.ProbeResult.ToString()}");
                    }
                }
            }
        }
    }
}


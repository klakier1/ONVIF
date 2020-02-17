using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnvifLib.Models.CreateSubscriptionResponse;
using OnvifLib.Models.PullMessageResponse;
using OnvifLib.Models.RenewResponse;

namespace OnvifLib
{
    public class Camera
    {
        private ProbeMatch _probeResult;
        private ConnectionTask _connection;
        private List<Capability> _capability;
        private Models.CreateSubscriptionResponse.Envelope _subscription;
        private Models.PullMessageResponse.Envelope _pullResponse;
        private Models.RenewResponse.Envelope _renewResponse;

        public ProbeMatch ProbeResult { get => _probeResult; set => _probeResult = value; }
        public ConnectionTask Connection { get => _connection; set => _connection = value; }
        public List<Capability> Capability { get => _capability; set => _capability = value; }
        public Models.CreateSubscriptionResponse.Envelope Subscription { get => _subscription; set => _subscription = value; }
        public Models.PullMessageResponse.Envelope PullResponse { get => _pullResponse; set => _pullResponse = value; }
        public Models.RenewResponse.Envelope RenewResponse { get => _renewResponse; set => _renewResponse = value; }

        public void Connect(string login, string pass)
        {
            Connection = new ConnectionTask(this, login, pass);
            Connection.Connect();
        }
        public void Cancel()
        {
            Connection.Cancel();
        }
    }
}

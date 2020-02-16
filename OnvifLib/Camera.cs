using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnvifLib
{
    public class Camera
    {
        private ProbeMatch _probeResult;
        private ConnectionTask _connection;
        private List<Capability> _capability;

        public ProbeMatch ProbeResult { get => _probeResult; set => _probeResult = value; }
        public ConnectionTask Connection { get => _connection; set => _connection = value; }
        public List<Capability> Capability { get => _capability; set => _capability = value; }

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

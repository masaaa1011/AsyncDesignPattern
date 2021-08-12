using AsyncDesignPattern.Common.Logger;
using SingleThreadedExecution.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleThreadedExecution
{
    public class UserThread : IUserThread
    {
        ILogger logger = new AsyncDesignPattern.Common.Logger.ConcoleLogger<UserThread>();

        private IGate _gate;
        private string _name;
        private string _address;
        public static UserThread Create(IGate gate, string name, string address) => new UserThread(gate, name, address);
        private UserThread(IGate gate, string name, string address)
        {
            _gate = gate;
            _name= name;
            _address = address;
        }

        public void Run()
        {
            logger.Logging($"{_name} BEGIN");
            Task.Run(() =>
            {
                while (true)
                {
                    _gate.Pass(_name, _address);
                }
            });
        }
    }
}

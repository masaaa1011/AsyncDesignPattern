using AsyncDesignPattern.Common.Logger;
using SingleThreadedExecution.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SingleThreadedExecution
{
    internal class Gate : IGate
    {
        ILogger logger = new AsyncDesignPattern.Common.Logger.ConcoleLogger<Gate>();
        private int _counter;
        private string _name = "Nobody";
        private string _address = "Nowhere";
        private object _lock = new object();
        public void Pass(string name, string address)
        {
            lock (_lock)
            {
                _counter++;
                this._name = name;
                Thread.Sleep(1000);
                this._address = address;
                Check();
            }
        }

        public void Check()
        {
            if (!_name.Equals(_address)) logger.Logging($" * ****BROKEN * **** {ToString()}");
            logger.Logging($"ckecked: {_name} / {_address}");
        }

        public override string ToString()
        {
            lock (_lock)
            {
                return $"{ _name} / { _address}";
            }
        }
    }
}

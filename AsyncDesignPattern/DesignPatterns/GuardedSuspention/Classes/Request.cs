using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardedSuspention.Classes
{
    public class Request : IRequest
    {
        public Request(string value) 
            => Value = value;
        public string Value { get; set; }
        void IRequest.Execute()
        {
            Console.WriteLine($"{Value}　ばぁ");
        }
    }

    public interface IRequest
    {
        void Execute();
    }
}

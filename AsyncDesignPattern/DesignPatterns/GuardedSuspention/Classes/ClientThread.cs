using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardedSuspention.Classes
{
    public class ClientThread : ITask
    {
        private IRequestPuttable m_RequestPutter;
        public ClientThread(IRequestPuttable queue)
            => m_RequestPutter = queue;

        public void ExecuteAsync()
        {
            var counter = 0;
            while (true)
            {
                counter++;
                IRequest r = new Request($"{counter} 回目の");
                m_RequestPutter.PutRequest(r);
            }
        }
    }
}

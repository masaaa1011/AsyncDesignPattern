using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardedSuspention.Classes
{
    public class ServerThread : ITask
    {
        private IRequestGetable m_RequestGetter;
        public ServerThread(IRequestGetable requestGetable)
            => m_RequestGetter = requestGetable;
        public void ExecuteAsync()
        {
            while (true)
            {
                IRequest r = m_RequestGetter.GetRequest();
                r.Execute();
            }
        }
    }
}

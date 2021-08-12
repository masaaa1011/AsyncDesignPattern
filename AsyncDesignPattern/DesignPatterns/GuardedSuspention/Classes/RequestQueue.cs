using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuardedSuspention.Classes
{
    public class RequestQueue : IRequestGetable, IRequestPuttable
    {
        private ConcurrentQueue<IRequest> m_queue = new ConcurrentQueue<IRequest>();
        private void Wait() => Thread.Sleep(1000);
        IRequest IRequestGetable.GetRequest()
        {
            while (m_queue.Count == 0)
                Wait();

            IRequest r = null;
            while (r == null)
                m_queue.TryDequeue(out r);

            return r;
        }

        void IRequestPuttable.PutRequest(IRequest request)
            => m_queue.Enqueue(request);
    }

    public interface IRequestGetable
    {
        IRequest GetRequest();
    }

    public interface IRequestPuttable
    {
        void PutRequest(IRequest request);
    }
}

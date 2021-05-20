using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Common.Result
{
    public class ProccessResult : IProccessResult
    {
        public object AsyncState => throw new NotImplementedException();

        public WaitHandle AsyncWaitHandle => throw new NotImplementedException();

        public bool CompletedSynchronously => throw new NotImplementedException();

        public bool IsCompleted => throw new NotImplementedException();

        public string JsonResult()
        {
            throw new NotImplementedException();
        }
    }
}

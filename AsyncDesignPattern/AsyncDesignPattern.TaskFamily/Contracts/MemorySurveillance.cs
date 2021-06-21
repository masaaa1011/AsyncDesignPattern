using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Contracts
{
    public class MemorySurveillance : ISurveillance
    {
        public bool Require()
        {
            return System.Environment.SystemPageSize < System.Environment.WorkingSet;
        }


        public bool Ensure()
        {
            return true;
        }

        public async void Invaritant(Func<bool> func, CancellationTokenSource cancellation)
        {

            while (true)
            {
                var isValidState = await Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    return func();
                });
                if (cancellation.IsCancellationRequested) break;
            }
        }
    }
}

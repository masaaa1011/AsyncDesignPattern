using AsyncDesignPattern.Common.Task;
using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.TaskFamily.Contracts;
using AsyncDesignPattern.TaskFamily.TaskHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Controller
{
    public class TaskHandler : ITaskHandler, ISurveillance
    {
        private ITaskHub _taskHub { get; set; } = TaskHub.TaskHub.Create();
        public TaskHandler()
        {
            
        }

        public void Handle(ITask task)
        {
            var tokenSource = new CancellationTokenSource();
            Invaritant(() => { return Require(); }, tokenSource);
            if (!Require()) throw new Exception("invalid state");

            _taskHub.Stack(task);


            if (!Ensure()) throw new Exception("invalid state");
            tokenSource.Cancel();
        }

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

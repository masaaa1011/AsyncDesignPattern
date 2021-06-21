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
    public class TaskHandler : ITaskHandler
    {
        public ITaskHub TaskHub { get; private set; } = TaskFamily.TaskHub.TaskHub.Create();
        public ISurveillanceCollection SurveillanceCollection { get; set; } = new SurveillanceCollection();

        public TaskHandler()
        {
        }

        public TaskHandler(ISurveillanceCollection surveillanceCollection)
        {
            SurveillanceCollection = surveillanceCollection;
        }

        public void Handle(ITask task)
        {
            var tokenSource = new CancellationTokenSource();
            SurveillanceCollection.survices.ForEach(f => { if(f.Require()) Console.WriteLine("not valid state"); });
            SurveillanceCollection.survices.ForEach(f => { Invaritant(() => { return Require(); }, tokenSource); });

            TaskHub.Stack(task);

            SurveillanceCollection.survices.ForEach(f => { if (f.Ensure()) Console.WriteLine("not valid state"); });
            tokenSource.Cancel();
        }

        public ITaskHandler AddSurveillance(ISurveillance surveillance)
        {
            SurveillanceCollection.AddSurveillance(surveillance);
            return this;
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

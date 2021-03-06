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
        public ISurveillanceCollection SurveillanceCollection { get; private set; } = new SurveillanceCollection();

        public ISurveillanceCollection AddSurveillance(ISurveillance surveillance)
        {
            SurveillanceCollection.AddSurveillance(surveillance);
            return SurveillanceCollection;
        }

        public TaskHandler()
        {
        }

        public TaskHandler(List<ISurveillance> surveillances)
        {
            SurveillanceCollection.Surveillances.AddRange(surveillances);
        }

        public void Handle(ITask task)
        {
            var tokenSource = new CancellationTokenSource();
            SurveillanceCollection.Surveillances.ForEach(f => { if(!f.Require()) Console.WriteLine("not valid state"); });
            SurveillanceCollection.Surveillances.ForEach((f) => { f.Invaritant(() => { return f.Require(); }, tokenSource); });

            TaskHub.Stack(task);

            SurveillanceCollection.Surveillances.ForEach((f) => { if (!f.Ensure()) Console.WriteLine("not valid state"); });
            tokenSource.Cancel();
        }
    }
}

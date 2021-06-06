using AsyncDesignPattern.Common.Proccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.TaskFamily.TaskHub
{
    public class TaskHub : ITaskHub
    {
        private static readonly ITaskHub _instance = new TaskHub();
        public static ITaskHub Create() => _instance;

        private TaskHub() { ProccessCollection = new Queue<ITask>(); }
        public Queue<ITask> ProccessCollection { get; private set; }
        private void Start()
        {
            ProccessCollection.Dequeue().ExecuteAsync();
        }

        public void Stack(ITask proccess)
        {

        }
    }
}

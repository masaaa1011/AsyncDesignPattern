using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.TaskFamily.TaskHub
{
    public interface ITaskHub
    {
        Queue<ITask> ProccessCollection { get; }
        virtual void Start() { }
        public void Stack(ITask task) { 
            ProccessCollection.Enqueue(task);
            Start();
        }
    }
}

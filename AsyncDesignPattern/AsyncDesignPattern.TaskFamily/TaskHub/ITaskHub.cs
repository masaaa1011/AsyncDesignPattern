using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.TaskFamily.TaskHub
{
    public interface ITaskHub
    {
        Queue<ITask> TaskCollection { get; }
        virtual void Start() { }
        public void Stack(ITask task) { 
            TaskCollection.Enqueue(task);
            Start();
        }
    }
}

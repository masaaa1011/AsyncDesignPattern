using AsyncDesignPattern.Common.Proccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.TaskFamily.TaskHub
{
    public interface ITaskHub
    {
        Queue<ITask> ProccessCollection { get; }
        virtual void Start() { }
        public void Stack(ITask proccess) { 
            ProccessCollection.Enqueue(proccess);
            Start();
        }
    }
}

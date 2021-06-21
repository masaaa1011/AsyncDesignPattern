using AsyncDesignPattern.Common.Task;
using AsyncDesignPattern.TaskFamily.Contracts;
using AsyncDesignPattern.TaskFamily.TaskHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Controller
{
    public interface ITaskHandler
    {
        ITaskHub TaskHub { get; }
        void Handle(ITask proccess);
    }
}

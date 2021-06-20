using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Controller
{
    public interface ITaskHandler
    {
        void Handle(ITask proccess);
    }
}

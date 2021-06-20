using AsyncDesignPattern.Common.Task;
using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.TaskFamily.TaskHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Controller
{
    public class TaskHandler : ITaskHandler
    {
        private ITaskHub _taskHub { get; set; } = TaskHub.TaskHub.Create();
        public TaskHandler()
        {
            
        }

        public void Handle(ITask task)
        {
            _taskHub.Stack(task);
        }
    }
}

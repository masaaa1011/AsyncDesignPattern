using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Controller
{
    public class TaskHandler : ITaskHandler
    {
        private readonly ITaskHandler _controller;

        public TaskHandler(ITaskHandler controller)
        {
            _controller = controller;
        }

        public void Handle(ITask proccess)
        {
            throw new NotImplementedException();
        }
    }
}

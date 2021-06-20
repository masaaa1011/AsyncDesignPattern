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
        public TaskHandler()
        {
            
        }

        public void Handle(ITask task)
        {
            throw new NotImplementedException();
        }
    }
}

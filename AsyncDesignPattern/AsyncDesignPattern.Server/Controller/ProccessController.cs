using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Server.Controller
{
    public class ProccessController : IProccessController
    {
        private readonly IRepository _repository;

        public ProccessController(IRepository repository)
        {
            _repository = repository;
        }

        public void Request(ITask proccess)
        {
            throw new NotImplementedException();
        }
    }
}

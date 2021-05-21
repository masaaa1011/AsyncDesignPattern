using AsyncDesignPattern.Server.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncDesignPattern.Server.Service
{
    public class AsyncProccessService : IAsyncService
    {
        private readonly IProccessController _controller;

        public AsyncProccessService(IProccessController controller)
        {
            _controller = controller;
        }

        public ActionResult ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}

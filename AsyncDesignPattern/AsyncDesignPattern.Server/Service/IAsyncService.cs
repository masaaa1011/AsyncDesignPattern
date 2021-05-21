using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncDesignPattern.Server.Service
{
    public interface IAsyncService
    {
        public ActionResult ExecuteAsync();
    }
}

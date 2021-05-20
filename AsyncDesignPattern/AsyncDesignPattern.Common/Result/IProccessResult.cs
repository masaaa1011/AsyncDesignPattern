using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Common.Result
{
    public interface IProccessResult : IAsyncResult
    {
        public string JsonResult();
    }
}

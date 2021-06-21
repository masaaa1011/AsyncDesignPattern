using AsyncDesignPattern.TaskFamily.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Contracts
{
    public interface ISurveillancable
    {
        ITaskHandler AddSurveillance(ISurveillance surveillance);
    }
}

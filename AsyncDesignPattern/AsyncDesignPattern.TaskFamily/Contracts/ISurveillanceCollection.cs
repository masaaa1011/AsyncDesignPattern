using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Contracts
{
    public interface ISurveillanceCollection
    {
        public List<ISurveillance> survices { get; }
        public ISurveillanceCollection AddSurveillance(ISurveillance surveillance);
    }
}

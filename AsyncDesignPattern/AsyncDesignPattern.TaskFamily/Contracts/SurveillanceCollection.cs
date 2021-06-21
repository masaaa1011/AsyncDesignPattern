using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Contracts
{
    public class SurveillanceCollection : ISurveillanceCollection
    {
        public List<ISurveillance> survices { get; private set; } = new List<ISurveillance>();

        public ISurveillanceCollection AddSurveillance(ISurveillance surveillance)
        {
            survices.Add(surveillance);
            return this;
        }
    }
}

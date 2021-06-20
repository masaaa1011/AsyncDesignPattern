using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.Contracts
{
    public interface ISurveillance
    {
        /// <summary>
        /// 事前制約
        /// </summary>
        /// <returns></returns>
        bool Require();

        /// <summary>
        /// 事後制約
        /// </summary>
        /// <returns></returns>
        bool Ensure();

        /// <summary>
        /// 状態監視
        /// </summary>
        void Invaritant(Func<bool> func, CancellationTokenSource cancellation);
    }
}

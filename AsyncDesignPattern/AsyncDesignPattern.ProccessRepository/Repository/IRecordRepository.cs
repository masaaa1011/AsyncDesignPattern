using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository
{
    public interface IRecordRepository
    {
        ITask GetByIdAsync();
        List<ITask> GetByNameAsync();
        void Add();
        void AddRange();
        void RemoveRange();
    }
}

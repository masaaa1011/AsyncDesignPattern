using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Repository.Dto;
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
        IRecord GetByIdAsync();
        List<IRecord> GetByNameAsync();
        void Add();
        void AddRange();
        void RemoveRange();
    }
}

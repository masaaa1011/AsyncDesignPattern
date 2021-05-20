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
    public class MockRecordRepository<T1, T2> : IRecordRepository where T1 : IEntity<T2> where T2 : IRecord
    {
        public IEntity<MyRecord> Entity { get; private set; }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void AddRange()
        {
            throw new NotImplementedException();
        }

        public ITask GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public List<ITask> GetByNameAsync()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange()
        {
            throw new NotImplementedException();
        }
    }
}

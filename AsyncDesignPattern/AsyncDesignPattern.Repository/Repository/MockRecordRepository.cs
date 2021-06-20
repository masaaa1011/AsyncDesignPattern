using AsyncDesignPattern.Common.Task;
using AsyncDesignPattern.Repository.Database.Tables;
using AsyncDesignPattern.Repository.Dto;
using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository
{
    public class MockRecordRepository : IRepository
    {
        public MockRecordRepository()
        {
            MockTable = new MockEntity();
        }

        public MockEntity MockTable { get; private set; }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void AddRange()
        {
            throw new NotImplementedException();
        }

        public IRecord GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public List<IRecord> GetByNameAsync()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange()
        {
            throw new NotImplementedException();
        }
    }
}

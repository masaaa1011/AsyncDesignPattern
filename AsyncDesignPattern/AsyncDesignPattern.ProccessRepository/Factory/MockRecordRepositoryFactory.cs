using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.Repository.Database.Tables;
using AsyncDesignPattern.Repository.Dto;
using AsyncDesignPattern.Repository.Entities;
using AsyncDesignPattern.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Factory
{
    public static class MockRecordRepositoryFactory
    {
        public static IRecordRepository Create() 
            => new MockRecordRepository<MockEntity, MockRecord>(new MockEntity(MockRecordTable.CrreateRecords().ToList()));
    }
}

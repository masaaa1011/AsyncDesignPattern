using AsyncDesignPattern.Repository.Database.Tables;
using AsyncDesignPattern.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Entities
{
    public class MockEntity : IEntity<MockRecord>
    {
        public MockEntity()
        {
            Records = new List<MockRecord>(MockRecordTable.CrreateRecords().ToList());
        }

        //public MockEntity(List<MockRecord> records)
        //{
        //    Records = records;
        //}

        public List<MockRecord> Records { get; private set; }
    }
}

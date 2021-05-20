using AsyncDesignPattern.Common.Enum;
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
    public static class RecordRepositoryFactory
    {
        public static IRecordRepository Create() => new MockRecordRepository<MyEntity<MyRecord>, MyRecord>();
    }
}

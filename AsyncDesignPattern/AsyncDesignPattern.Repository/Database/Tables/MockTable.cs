using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Database.Tables
{
    internal static class MockTable
    {
        internal static List<MockEntity> Records { get; set; } = CrreateRecords().ToList();
        private static MockEntity CreateSingleRecord(Guid id, string name) => 
            new MockEntity { Id = id, Name = name, IsDelete = 0.Equals(DateTime.Now.Millisecond % 2) ? true : false, Day = DateTime.Now };
        private static IEnumerable<MockEntity> CrreateRecords()
        {
            foreach (var i in 0..^10000) yield return CreateSingleRecord(Guid.NewGuid(), $"name_{i}");
        }
    }
}

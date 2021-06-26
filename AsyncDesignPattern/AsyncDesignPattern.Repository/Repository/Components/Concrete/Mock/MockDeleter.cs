using AsyncDesignPattern.Repository.Database.Tables;
using AsyncDesignPattern.Repository.Entities;
using AsyncDesignPattern.Repository.Repository.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository.Components.Concrete.Mock
{
    public class MockDeleter : IDelete<MockEntity>
    {
        public MockEntity Delete(MockEntity entity)
        {
            MockTable.Records.Remove(entity);
            return entity;
        }
    }
}

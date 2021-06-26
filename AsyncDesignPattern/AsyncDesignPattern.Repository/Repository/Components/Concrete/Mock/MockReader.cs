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
    public class MockReader : IRead<MockEntity>
    {
        public List<MockEntity> ReadAll()
            => MockTable.Records.Select(s => s).ToList();

        public MockEntity ReadOne(Guid id)
            => MockTable.Records.Where(w => w.Id.Equals(id)).FirstOrDefault();
    }
}

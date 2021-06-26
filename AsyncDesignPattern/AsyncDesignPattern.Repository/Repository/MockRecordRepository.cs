using AsyncDesignPattern.Common.Task;
using AsyncDesignPattern.Repository.Database.Tables;
using AsyncDesignPattern.Repository.Entities;
using AsyncDesignPattern.Repository.Repository.Components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository
{
    public class MockRecordRepository<TEntity> : IRepository where TEntity : IEntity
    {
        private ISave<TEntity> _saver;
        private IRead<TEntity> _reader;
        private IDelete<TEntity> _deleter;

        public MockRecordRepository() { }
        public MockRecordRepository<TEntity> UseSave(ISave<TEntity> saver) { _saver = saver; return this; }
        public MockRecordRepository<TEntity> UseRead(IRead<TEntity> reader) { _reader = reader; return this; }
        public MockRecordRepository<TEntity> UseDelete(IDelete<TEntity> deleter) { _deleter = deleter; return this; }

        public TEntity Save(TEntity entity)
            => _saver.Save(entity);

        public TEntity ReadOne(Guid entity)
            => _reader.ReadOne(entity);

        public List<TEntity> ReadAll()
            => _reader.ReadAll();

        public TEntity Delete(TEntity entity)
            => _deleter.Delete(entity);
    }
}

using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository.Components.Interfaces
{
    public interface IRead<IEntity>
    {
        public IEntity ReadOne(Guid id);
        public List<IEntity> ReadAll();
    }
}

using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository.Components.Interfaces
{
    public interface IDelete<IEntity>
    {
        public IEntity Delete(IEntity entity);
    }
}

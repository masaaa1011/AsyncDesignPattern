using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository.Components.Interfaces
{
    public interface ISave<IEntity>
    {
        public IEntity Save(IEntity entity);
    }
}

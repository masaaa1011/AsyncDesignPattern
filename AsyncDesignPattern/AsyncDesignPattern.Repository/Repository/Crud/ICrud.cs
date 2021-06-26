using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository.Crud
{
    public interface ISaver
    {
        IEntity Save(IEntity entity);
    }
    public interface IOneReader
    {
        IEntity ReadOne(Guid id);
    }
    public interface IAllReader
    {
        List<IEntity> ReadAll();
    }
    public interface IDeleter
    {
        IEntity Delete(IEntity entity);
    }
}

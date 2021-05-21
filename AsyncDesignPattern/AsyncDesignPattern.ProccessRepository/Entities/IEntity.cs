using AsyncDesignPattern.Repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Entities
{
    public interface IEntity<T> where T : IRecord
    {
        List<T> Records { get; }
    }
}

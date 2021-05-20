using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Dto
{
    public record MyRecord : IRecord
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

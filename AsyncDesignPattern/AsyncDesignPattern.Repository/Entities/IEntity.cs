﻿using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}

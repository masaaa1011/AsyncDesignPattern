﻿using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Repository.Database.Tables;
using AsyncDesignPattern.Repository.Dto;
using AsyncDesignPattern.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Repository
{
    public class MockRecordRepository<T1, T2> : IRepository where T1 : IEntity<T2>, new() where T2 : IRecord
    {
        public MockRecordRepository()
        {
            Entity = new T1();
        }

        //internal MockRecordRepository(T1 entity)
        //{
        //    Entity = entity;
        //}

        public T1 Entity { get; private set; }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void AddRange()
        {
            throw new NotImplementedException();
        }

        public IRecord GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public List<IRecord> GetByNameAsync()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange()
        {
            throw new NotImplementedException();
        }
    }
}

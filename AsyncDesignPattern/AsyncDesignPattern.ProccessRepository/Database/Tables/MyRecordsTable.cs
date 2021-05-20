﻿using AsyncDesignPattern.Repository.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Database.Tables
{
    static class RangeExtension
    {
        public static RangeEnumerator GetEnumerator(this Range range) => new RangeEnumerator(range);
        public struct RangeEnumerator : IEnumerator<int>
        {
            readonly int Max;
            readonly int Step;

            public int Current { get; private set; }
            object IEnumerator.Current => this.Current;

            public bool MoveNext()
            {
                if (this.Current != this.Max)
                {
                    this.Current += this.Step;
                    return true;
                }
                return false;
            }
            public void Dispose() { }
            public void Reset() => throw new NotSupportedException();

            public RangeEnumerator(Range range)
            {
                var step = range.End.Value < range.Start.Value ? -1 : 1;
                this.Current = range.Start.Value - (range.Start.IsFromEnd ? 0 : step);
                this.Max = range.End.Value - (range.End.IsFromEnd ? step : 0);
                this.Step = step;
            }
        }
    }
    public static class MyRecordsTable
    {
        public static MyRecord CreateSingleRevord(Guid id, string name) => new MyRecord { Id = id, Name = name };
        public static IEnumerable<MyRecord> CrreateRecords()
        {
            foreach (var i in 0..10) yield return CreateSingleRevord(Guid.NewGuid(), $"name_{i}");
        }
    }
}

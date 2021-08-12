using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Immutable.Classes
{
    [Serializable]
    public class Person : ICloneable
    {
        readonly string _name;
        public string Name => _name;
        readonly string _address;
        public string Addres => _address;

        public Person(string name, string address)
        {
            _name = name;
            _address = address;
        }

        public override string ToString()
            => $"[ Person: name = {_name }, address = {_address}]";

        public object Clone()
            => new Person(_name, _address);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    class Person
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public byte Age { get; private set; }

        public Person()
        {

        }

        public Person(string name, string lastname, byte age)
        {
            Name = name;
            LastName = lastname;
            Age = age;
        }
    }
}

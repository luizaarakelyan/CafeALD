using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    class User: Person
    {
        List<Cafe> favorite = new List<Cafe>();

        public User(string name, string lastname, byte age ): base(name, lastname, age)
        { }


    }
}

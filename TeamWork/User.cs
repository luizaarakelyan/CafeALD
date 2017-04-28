using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    class User: Person
    {
        public int ID { get; private set; }
        private static int idCount = 1;
        List<Cafe> favorite = new List<Cafe>();
        List<User> users = new List<User>();

        public User(string name, string lastname, byte age ): base(name, lastname, age)
        {
            users.Add(this);
            this.ID = idCount;
            idCount++;
        }
    }
}

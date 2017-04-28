using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    class Employee : Person
    {
        public decimal Salary { get; private set; }
        public Cafe WorkPlace { get; private set; }


        public Employee(string name, string lastname, byte age, decimal salary,Cafe workplace) : base(name, lastname, age)
        {
            Salary = salary;
            WorkPlace = workplace;
        }

    }
}

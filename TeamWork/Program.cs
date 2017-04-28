using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Cafe cafe1 = new Cafe("ALD", "Nalbandyan 14", "0552365980", new TimeSpan(9, 0, 0), new TimeSpan(22, 0, 0));
            User activeUser = new User("Luiza", "Arakelyan", 17);
            bool status = true;
            while (status)
            {
                string s = Console.ReadLine();
                switch (s)
                {
                    case "Rate":
                        Console.WriteLine("Cafe name: ");
                        string cafeName = Console.ReadLine();
                        Console.WriteLine("Enter your rate: ");
                        string cafeRate = Console.ReadLine();
                        for (int i = 0; i < Cafe.menu_am.Count; i++)
                        {
                            if (cafeName.Equals(Cafe.menu_am[i].Name))
                                Cafe.menu_am[i].Rate(int.Parse(cafeRate), activeUser);
                        }
                        Console.WriteLine(cafe1.ToString());
                        break;
                    case "Exit":
                        status = false;
                        break;
                    default:
                        Console.WriteLine("It is not command");
                        break;
                        
                }
            }
        }
    }
}

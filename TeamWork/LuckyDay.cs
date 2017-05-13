using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork

{
    class LuckyDay
    {
        private static Dictionary<Person, string> CommentBook = new Dictionary<Person, string>();
        private static List<LuckyDay> Cafes = new List<LuckyDay>();
        private static List<string> Adresses = new List<string>();
        private static List<string> Numbers = new List<string>();
        private static string Name
        { get { return "LuckyDay"; } }
        private static int Earning { get; set; }
        public string Adress { get; private set; }
        public string PhoneNumber { get; private set; }
        private static string Email { get { return "Lucky_Day@gmail.com"; } set {; } }
        private static string Website { get { return "www.LuckyDay.com"; } set {; } }
        private static string Fax { get { return "245896325"; } set {; } }
        private static TimeSpan Open = new TimeSpan(10, 0, 0);
        private static TimeSpan Close = new TimeSpan(20, 0, 0);
        public static Dictionary<string, int> Menu = new Dictionary<string, int>();
        public static double Rating { get; private set; }
        private static int rateCount = 1;



        public LuckyDay(string adress, string phone_number)
        {
            Rating = 5;
            Adress = adress;
            PhoneNumber = phone_number;
            Numbers.Add(PhoneNumber);
            Cafes.Add(this);
            Adresses.Add(Adress);
        }

        public LuckyDay()
        { }

        public static void Earn(int money)
        {
            Earning += money;
        }

        public static void CafesReader()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\Cafes.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] str = s.Split();
                        LuckyDay cafe = new LuckyDay(str[0], str[1]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Cafes.txt:");
                Console.WriteLine(e.Message);
            }
        }

        public static void CafesWriter()
        {
            try
            {
                File.WriteAllText(@"..\..\Cafes.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\Cafes.txt"))
                {
                    foreach (LuckyDay item in Cafes)
                        sw.WriteLine(item.Adress + " " + item.PhoneNumber);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't write in file Cafes.txt:");
                Console.WriteLine(e.Message);
            }
        }


        public static void MenuReader()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\Menu.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] keyvalue = s.Split();
                        Menu.Add(keyvalue[0], int.Parse(keyvalue[1]));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Menu:");
                Console.WriteLine(e.Message);
            }

        }

        private static void MenuWriter()
        {
            try
            {
                File.WriteAllText(@"..\..\Menu.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\Menu.txt"))
                {
                    foreach (KeyValuePair<string, int> item in Menu)
                        sw.WriteLine(item.Key + " " + item.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't write in file Menu.txt:");
                Console.WriteLine(e.Message);
            }
        }


        public static void CommentBookReader()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\CommentBook.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] str = s.Split();
                        Person person = new Person(str[0], str[1]);
                        CommentBook.Add(person, str[2]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read CommentBook.txt:");
                Console.WriteLine(e.Message);
            }

        }

        private static void CommentBookWriter()
        {
            try
            {
                File.WriteAllText(@"..\..\CommentBook.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\CommentBook.txt"))
                {
                    foreach (KeyValuePair<Person, string> item in CommentBook)
                        sw.WriteLine(item.Key.Name + " " + item.Key.LastName + " " + item.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't write in file CommentBook.txt:");
                Console.WriteLine(e.Message);
            }
        }



        public static string ShowMenu()
        {
            string output = "";
            foreach (KeyValuePair<string, int> item in Menu)
            {
                output += item.Key + " " + item.Value + "\n";
            }
            return output;
        }

        public static void AddMenu(string item, int price)
        {
            Menu.Add(item, price);
            MenuWriter();
        }

        public static void DeleteFromMenu(string item)
        {
            Menu.Remove(item);
            MenuWriter();
        }

        public static void Rate(int rate)
        {
            Rating = (Rating * rateCount + rate) / (rateCount + 1);
            rateCount++;
        }

        public static string Status()
        {
            if (DateTime.Now.TimeOfDay >= Open && DateTime.Now.TimeOfDay <= Close)
                return "Open now";
            else
                return "Close now";
        }


        public static string AboutUs()
        {
            string output = "";
            output += String.Format("\nCafes complex LuckyDay\nWorking times:   {0}  -  {1}\n{2}\nRating: {3}\nWebsite: {4}", Open, Close, Status(), Rating, Website);
            return output;
        }

        public static string Feedback()
        {
            string output = Name + "\n" + "\n";
            output += "Fax: \n" + Fax + "\n" + "\n" + "Email adress: \n" + Email + "\n" + "\n";
            output += "Adresses and numbers\n\n";
            for (int i = 0; i < Cafes.Count; i++)
            {
                output += Cafes[i].Adress + "  " + Cafes[i].PhoneNumber + "\n";
            }
            return output;
        }

        public static void LeaveReview(Person person, string comment)
        {

            if (!CommentBook.ContainsKey(person))      // if the name and lastname of person aren't existing in dictionary
            {
                CommentBook.Add(person, comment);
                CommentBookWriter();
            }
            else
            {   // make random number in range 2-70, concatenate to person's name 
                // and lastname, and after it add to dictionary, to escape an exception
                // of adding existing key to dictionary
                Random rnd = new Random();
                int count = rnd.Next(2, 70);
                person.Name = person.Name + count.ToString();
                person.LastName = person.LastName + count.ToString();
                try
                {
                    CommentBook.Add(person, comment);
                    CommentBookWriter();
                }
                catch (Exception)
                {
                    Console.WriteLine("random number repeated, please enter other command.");
                }
            }
        }

        public static string ShowReview()
        {

            string output = "";
            foreach (KeyValuePair<Person, string> entry in CommentBook)
            {
                output += entry.Key.Name + " " + entry.Key.LastName + "  " + "Review:  " + entry.Value + "\n";
            }
            return output;
        }

        public static bool IsValidNumber(string str)            // must be unique number
        {
            bool isvalid = true;
            for (int i = 0; i < Cafes.Count; i++)
            {
                if (Cafes[i].PhoneNumber.Equals(str))
                {
                    isvalid = false;
                    break;
                }
            }
            return isvalid;
        }

        public static LuckyDay Search(string str)
        {
            LuckyDay cafe = new LuckyDay();
            for (int i = 0; i < Cafes.Count; i++)
            {
                string cafeAdress = Cafes[i].Adress;
                if (Cafes[i].Adress.ToLower().Contains(str.ToLower()))
                {
                    cafe = Cafes[i];
                    break;
                }
                cafeAdress = "";
            }
            return cafe;
        }

    }
}

struct Person
{
    public string Name { get; set; }
    public string LastName { get; set; }

    public Person(string name, string lastname)
    {
        Name = name;
        LastName = lastname;
    }
}




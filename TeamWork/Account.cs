using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TeamWork
{
    class Account
    {
        public static List<Account> Accounts = new List<Account>();
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string CardN { get; private set; }
        public int Balance { get; private set; }

        public Account(string name, string lastname, string login, string password, string card_n, int balance)
        {
            Name = name;
            LastName = lastname;
            Login = login;
            Password = password;
            Balance = balance;
            CardN = card_n;
            Accounts.Add(this);
        }


        public Account()
        { }


        public void SetPassword(string pass)
        {
            Password = pass;
        }

        public static void AccountReader()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\UserAccounts.txt"))
                {
                    string s = "";
                    string[] values = new string[6];
                    while ((s = sr.ReadLine()) != null)
                    {

                        values = s.Split();
                        string name = values[0];
                        string lastName = values[1];
                        string login = values[2];
                        string password_coded = values[3];
                        string password = DecodingPassword(password_coded);
                        string cardNumber = values[4];
                        int balance = int.Parse(values[5]);
                        Account account = new Account(name, lastName, login, password, cardNumber, balance);

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read UserAccounts.txt:");
                Console.WriteLine(e.Message);
            }
        }


        public static void AccountWriter()
        {
            try
            {
                File.WriteAllText(@"..\..\UserAccounts.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\UserAccounts.txt"))
                {
                    foreach (Account item in Accounts)
                    {
                        string new_pass = CodingPassword(item.Password);
                        sw.WriteLine(item.Name + " " + item.LastName + " "
                            + item.Login + " " + new_pass + " " + item.CardN + " " + item.Balance);

                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Can't write in file UserAccounts.txt:");
                Console.WriteLine(e.Message);
            }
        }

        public int ShowBalance()
        {
            return this.Balance;
        }

        public void PayForOrder(int money)
        {
            Balance -= money;
            AccountWriter();
        }

        public static int MakeOrder(string items)
        {
            int sum = 0;
            // if its lucky day of week, we will discount all menu items -20% 
            if (DateTime.Now.DayOfWeek.ToString().Equals("Friday"))
            {
                string[] str = items.Trim().ToLower().Split();
                for (int i = 0; i < str.Length; i++)
                {
                    if (LuckyDay.Menu.ContainsKey(str[i]) == true)
                    {
                        sum += LuckyDay.Menu[str[i]] - (LuckyDay.Menu[str[i]] * 20 / 100);
                    }
                }
            }
            else
            {
                string[] str = items.Trim().ToLower().Split();
                for (int i = 0; i < str.Length; i++)
                {
                    if (LuckyDay.Menu.ContainsKey(str[i]) == true)
                    {
                        sum += LuckyDay.Menu[str[i]];
                    }
                }
            }
            return sum;

        }

        public void AddMoney(int money)
        {
            this.Balance += money;
            AccountWriter();
        }

        public static bool IsValidLogin(string login)
        {
            bool isvalid = true;
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].Login.Equals(login))
                {
                    isvalid = false;
                    break;
                }
            }
            return isvalid;
        }

        public static bool IsValidPassword(string password)
        {
            bool isvalid = false;
            if (password.Length >= 8 && password.Length <= 30 && IsContain(password))
                isvalid = true;
            return isvalid;
        }

        private static bool IsContain(string str)           // password must contain at least one digit
        {
            bool isvalid = false;
            if (str.Contains('0') || str.Contains('1') || str.Contains('2') || str.Contains('3')
                || str.Contains('4') || str.Contains('5') || str.Contains('6') || str.Contains('7')
                || str.Contains('8') || str.Contains('9'))
                isvalid = true;
            return isvalid;
        }

        public static bool IsValidCard(string str)          // valid card's number must contain 16 digits
        {
            bool isvalid = true;
            if (str.Length != 16)
                return false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] - 48 > 9 || str[i] - 48 < 0)
                {
                    isvalid = false;
                    break;
                }
            }
            return isvalid;
        }

        public static bool IsValidCard2(string str)     // and be unique
        {
            bool isvalid = true;
            for (int i = 0; i < Accounts.Count; i++)
            {
                if (Accounts[i].CardN.Equals(str))
                {
                    isvalid = false;
                    break;
                }
            }
            return isvalid;
        }

        public void ShowAccountInfo()
        {
            Console.WriteLine("Your login: {0}\nYour card number: {1}\nYour balance: {2}", Login, CardN, Balance);
        }


        private static string CodingPassword(string password)
        {
            string codePassword = "";
            int j = 0;
            for (int i = 0; i < password.Length; i++)
            {

                if (j * 2 < 10)
                {
                    string str = "" + j * 2;
                    codePassword += password[i] + str;
                    str = "";
                    j++;
                }
                else
                {
                    j = 0;
                    string str = "" + j * 2;
                    codePassword += password[i] + str;
                    str = "";
                    j = 1;
                }
            }
            return codePassword;
        }

        public static string DecodingPassword(string pass)
        {
            string realPassword = "";
            for (int i = 0; i < pass.Length; i++)
            {
                if (i % 2 == 0)
                {
                    realPassword += "" + pass[i];
                    continue;

                }
                else
                {
                    realPassword += "";
                }

            }

            return realPassword;
        }
    }

}



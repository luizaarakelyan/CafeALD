using System;
using System.Device.Location;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TeamWork
{
    class Program
    {
        static void Main(string[] args)
        {

            LuckyDay.CafesReader();
            Account.AccountReader();
            Street.StreetsReader();
            LuckyDay.CommentBookReader();
            LuckyDay.MenuReader();

            Console.WriteLine("Good day.\nEnter help if it's your first time here.");

            Account acc = null;

            bool isactive = false;

            bool status = true;
            while (status)
            {
                string command = Console.ReadLine().Trim().ToLower();
                switch (command)
                {
                    case "help":
                        HelpVirtual();
                        if (isactive)
                        {
                            if (acc.Login.Equals("ADMIN"))
                                AdminAbilities();
                        }
                        break;

                    case "create an account":
                        if (isactive == false)
                        {
                            Console.WriteLine("Please enter your name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Please enter your last name: ");
                            string lastname = Console.ReadLine();

                            string card_number = "";
                            bool cardvalid = false;
                            for (int i = 0; i < 5; i++)               // every time we will give to user 5 chances to enter valid input
                            {
                                Console.WriteLine("Enter please your card number: ");
                                card_number = Console.ReadLine();
                                if (Account.IsValidCard(card_number) && Account.IsValidCard2(card_number))
                                {
                                    cardvalid = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect card number, or your it isn't your card number. Try again.");
                                    continue;
                                }
                            }
                            if (cardvalid == false)
                            {
                                Console.WriteLine("Card number must contain only 16 digits.\nPlease enter command");
                                continue;
                            }

                            string login = "";
                            bool logvalid = false;
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine("Enter your login: ");
                                login = Console.ReadLine();
                                if (Account.IsValidLogin(login))
                                {
                                    logvalid = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("This login is already existing, try again");
                                    continue;
                                }
                            }
                            if (logvalid == false)
                            {
                                Console.WriteLine("Think of unique login.\nPlease enter command");
                                continue;
                            }

                            string password = "";
                            bool pasvalid = false;
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine("Enter password: ");
                                password = Console.ReadLine();
                                if (Account.IsValidPassword(password))
                                {
                                    pasvalid = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Password length must be in 8-30 range and containing at least one digit");
                                    continue;
                                }
                            }
                            if (pasvalid == false)
                            {
                                Console.WriteLine("Think of correct password.\nPlease enter command");
                                continue;
                            }

                            string code = "";
                            Console.WriteLine("Please enter the code to end creating an account.");
                            string result = "";
                            bool validcode = false;
                            for (int j = 0; j < 3; j++)
                            {
                                result = "";
                                Random rand = new Random();
                                for (int i = 0; i < 4; i++)
                                {
                                    int compNumber = rand.Next(0, 10);
                                    result += compNumber;
                                }
                                Console.WriteLine("CODE: " + result);
                                code = Console.ReadLine();
                                if (code != result)
                                {
                                    Console.WriteLine("You entered wrong code.Try again.");
                                    continue;
                                }
                                else
                                {
                                    validcode = true;
                                    break;
                                }
                            }

                            if (validcode == false)
                            {
                                Console.WriteLine("You couldn't enter code right.\nPlease enter command.");
                                continue;
                            }

                            string balance = "";
                            bool validbalance = false;
                            int balanceint = 0;
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    Console.WriteLine("Enter balance in your card:");
                                    balance = Console.ReadLine();
                                    balanceint = int.Parse(balance);
                                    if (balanceint < 0)
                                    {
                                        Console.WriteLine("Balance must be positive");
                                        continue;
                                    }
                                    else
                                    {
                                        validbalance = true;
                                        break;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Enter integer number.");
                                    continue;
                                }
                            }
                            if (validbalance == false)
                            {
                                Console.WriteLine("You couldn't enter valid balance.\nPlease enter command.");
                                continue;
                            }

                            acc = new Account(name, lastname, login, password, card_number, balanceint);
                            Account.AccountWriter();
                            Console.WriteLine("{0}, your account have been created.\nYou already can log in to your account", name);

                        }
                        else
                        {
                            Console.WriteLine("Log out to create new account.");

                        }
                        break;
                    ////// end create an account

                    case "log in":
                        if (isactive == false)
                        {
                            Console.WriteLine("Please enter your login: ");
                            bool logexists = false;
                            string username = "";
                            for (int j = 0; j < 5; j++)
                            {
                                username = Console.ReadLine();
                                for (int i = 0; i < Account.Accounts.Count; i++)
                                {
                                    if (Account.Accounts[i].Login.Equals(username))
                                    {
                                        logexists = true;
                                        acc = Account.Accounts[i];
                                        break;
                                    }
                                }
                                if (logexists == false)
                                {
                                    Console.WriteLine("You entered wrong login.\nTry again.");
                                    continue;
                                }
                                else break;
                            }
                            if (logexists == false)
                            {
                                Console.WriteLine("Login is not existing or you entered wrong login.\nPlease enter command");
                                continue;
                            }

                            string user_password = "";
                            bool passvalid = false;
                            Console.WriteLine("Please enter your password: ");
                            for (int i = 0; i < 5; i++)
                            {
                                user_password = "";
                                while (true)
                                {
                                    ConsoleKeyInfo ch = Console.ReadKey(true);
                                    if (ch.Key == ConsoleKey.Enter)
                                        break;
                                    else
                                    {
                                        user_password += ch.KeyChar;
                                        Console.Write("*");
                                    }
                                }
                                int j;
                                for (j = 0; j < Account.Accounts.Count; j++)
                                {
                                    if (Account.Accounts[j].Login.Equals(username))
                                    {

                                        if ((Account.Accounts[j].Password).Equals(user_password))
                                        {
                                            passvalid = true;
                                            break;
                                        }
                                    }
                                }
                                if (passvalid == false)
                                {
                                    Console.WriteLine("\nPlease enter correct password.");
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("\n{0} loged in", Account.Accounts[j].Login);
                                    isactive = true;
                                    break;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("You already loged in, if you want to log in to another acc, log out from yours.");
                            continue;
                        }
                        // end 5 times for
                        break;
                    ////log in end

                    case "log out":
                        if (isactive == true)
                        {
                            try
                            {
                                isactive = false;
                                Console.WriteLine("{0} loged out.", acc.Login);
                                acc = new Account();
                            }
                            catch (Exception)           // unplanned exception
                            {
                                Console.WriteLine("Oops!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You already are loged out.");
                        }
                        break;

                    case "show menu":
                        if (isactive == true)
                        {
                            string menu = LuckyDay.ShowMenu();
                            Console.WriteLine(menu);
                        }
                        else
                        {
                            Console.WriteLine("Log in to see menu.");
                        }
                        break;

                    case "change password":
                        if (isactive == true)
                        {
                            bool current = false;
                            for (int i = 0; i < 5; i++)                     // right current password
                            {
                                Console.WriteLine("Enter your current password");
                                string password = Console.ReadLine();
                                if (acc.Password.Equals(password))
                                {
                                    current = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Enter your current password right.\nTry again");
                                    continue;
                                }
                            }
                            if (current == false)
                            {
                                Console.WriteLine("You couldn't enter current password right.\nPlease eneter command.");
                                continue;
                            }

                            bool newpass = false;
                            for (int j = 0; j < 5; j++)             // new password validation
                            {
                                Console.WriteLine("Enter new password: ");
                                string new_password = Console.ReadLine();
                                if (Account.IsValidPassword(new_password))
                                {
                                    Console.WriteLine("Enter new password one more time: ");
                                    string second_password = Console.ReadLine();
                                    if (second_password.Equals(new_password))
                                    {
                                        newpass = true;
                                        acc.SetPassword(new_password);
                                        Account.AccountWriter();
                                        Account.AccountReader();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("New passwords arn't matching. Try again.");
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("New password must be in 8-30 range and contain at least one digit.");
                                    continue;
                                }
                            }
                            if (newpass == false)
                            {
                                Console.WriteLine("You could not change your password.\nPlease enter command.");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Your password has been changed.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to change your password.");
                            continue;
                        }
                        break;

                    case "rate":
                        if (isactive == true)
                        {
                            bool israted = false;
                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    Console.WriteLine("Your rate: ");
                                    int rate = int.Parse(Console.ReadLine());
                                    if (rate >= 1 && rate <= 5)
                                    {
                                        LuckyDay.Rate(rate);
                                        Console.WriteLine("Your rate was added");
                                        israted = true;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Rating must be in 1-5 range");
                                        continue;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("rate must be an integer");
                                    continue;
                                }
                            }
                            if (israted == false)
                            {
                                Console.WriteLine("Your rate is not satisfying to our standarts.\nPlease enter command.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to rate");
                        }
                        break;

                    case "about cafe":
                        Console.WriteLine(LuckyDay.AboutUs());
                        break;

                    case "show my balance":
                        if (isactive == true)
                        {
                            Console.WriteLine("Your balance: {0}", acc.ShowBalance());
                        }
                        else
                        {
                            Console.WriteLine("Log in to see your balance");
                        }
                        break;

                    case "add money":
                        if (isactive == true)
                        {

                            for (int i = 0; i < 5; i++)
                            {
                                try
                                {
                                    Console.WriteLine("Enter your fee: ");
                                    int money = int.Parse(Console.ReadLine());
                                    if (money > 0)
                                    {
                                        acc.AddMoney(money);
                                        Console.WriteLine("Money was added.");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter positive number");
                                        continue;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Enter integer money.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to add money");
                        }
                        break;

                    case "leave review":
                        if (isactive == true)
                        {
                            Console.WriteLine("Enter your name: ");
                            string firstname = Console.ReadLine();
                            Console.WriteLine("Enter your lastname");
                            string last = Console.ReadLine();
                            Person pers = new Person(firstname, last);
                            string comment = "";
                            bool isspace = true;
                            for (int i = 0; i < 5; i++)
                            {
                                Console.WriteLine("Enter your comment(with underscores): ");
                                comment = Console.ReadLine();
                                if (comment.Contains(" "))
                                {
                                    Console.WriteLine("Enter comment without spaces,  enter with underscores.Try again");
                                    continue;
                                }
                                else
                                {
                                    isspace = false;
                                    break;
                                }
                            }
                            if (isspace == true)
                            {
                                Console.WriteLine("You couldn't enter comment without spaces.\nPlease enter command.");
                                continue;
                            }
                            LuckyDay.LeaveReview(pers, comment);
                            Console.WriteLine("Thank you for review");
                        }
                        else
                        {
                            Console.WriteLine("Log in to leave review.");
                        }
                        break;

                    case "show reviews":
                        if (isactive == true)
                        {
                            Console.WriteLine(LuckyDay.ShowReview());
                        }
                        else
                        {
                            Console.WriteLine("Log in to see reviews");
                        }
                        break;

                    case "feedback":
                        if (isactive == true)
                        {
                            string info = LuckyDay.Feedback();
                            Console.WriteLine(info);
                        }
                        else
                        {
                            Console.WriteLine("Log in to see feedback info");
                        }
                        break;

                    case "add to menu":
                        if (isactive == true)
                        {
                            if (acc.Login.Equals("ADMIN"))
                            {
                                bool foodvalid = false;
                                string food = "";
                                for (int i = 0; i < 5; i++)
                                {
                                    Console.WriteLine("Enter food name(if it contains space please write with underscore(_)):");
                                    food = Console.ReadLine().ToLower();
                                    if (food.Contains(" "))
                                    {
                                        Console.WriteLine("Enter without space");
                                        continue;
                                    }
                                    else if (LuckyDay.Menu.ContainsKey(food))
                                    {
                                        Console.WriteLine("This item is already in menu. Enter other item.");
                                        continue;
                                    }
                                    else
                                    {
                                        foodvalid = true;
                                        break;
                                    }
                                }
                                if (foodvalid == false)
                                {
                                    Console.WriteLine("This item is not satisfying to our standarts.\nPlease enter command");
                                    continue;
                                }

                                int price = 0;
                                bool validprice = false;
                                for (int i = 0; i < 5; i++)
                                {
                                    try
                                    {
                                        Console.WriteLine("Enter food price: ");
                                        price = int.Parse(Console.ReadLine());
                                        if (price < 0)
                                        {
                                            Console.WriteLine("Enter positive integer number.Try again.");
                                            continue;
                                        }
                                        else
                                        {
                                            validprice = true;
                                            break;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Price must be posotive integer number.Try again");
                                        continue;
                                    }
                                }
                                if (validprice == false)
                                {
                                    Console.WriteLine("You couldn't enter valid price.\nPlease enter command.");
                                    continue;
                                }
                                LuckyDay.AddMenu(food, price);
                                Console.WriteLine("New item has been added");
                            }
                            else
                            {
                                Console.WriteLine("Log in as admin to make such change.\nPlease enter command.");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to add to menu.\nPlease enter command.");
                            continue;
                        }
                        break;

                    case "delete from menu":
                        if (isactive == true)
                        {
                            if (acc.Login.Equals("ADMIN"))
                            {
                                bool isdeleted = false;
                                for (int i = 0; i < 5; i++)
                                {

                                    try
                                    {
                                        Console.WriteLine("Enter food name(if it contains space please write with underscore(_)):");
                                        string fooddelete = Console.ReadLine().ToLower();
                                        if (LuckyDay.Menu.ContainsKey(fooddelete))
                                        {
                                            LuckyDay.DeleteFromMenu(fooddelete);
                                            Console.WriteLine("{0} was deleted from menu", fooddelete);
                                            isdeleted = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Menu doesn't contain {0}. Try again.", fooddelete);
                                            continue;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Menu doesn't contain such item.Try again.");
                                        continue;
                                    }
                                    if (isdeleted == true)
                                        break;
                                }
                                if (isdeleted == false)
                                {
                                    Console.WriteLine("You couldn't delete item from menu.\nPlease enter command.");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Log in as admin to make such change.\nPlease enter command.");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to delete item from menu.");
                            continue;
                        }
                        break;

                    case "add cafe":
                        if (isactive == true)
                        {
                            if (acc.Login.Equals("ADMIN"))
                            {
                                string phoneNumber = "";
                                bool validadress = false;
                                bool validnumber = false;
                                double latitude = 0;
                                double longitude = 0;
                                GeoCoordinate geo = null;
                                string adress = "";
                                Regex rgx1 = new Regex("^[a-zA-Z]+$");      // for adress
                                Regex rgx2 = new Regex("^[0-9]+$");         // for phone numbers(must contain only digits)
                                for (int i = 0; i < 5; i++)
                                {
                                    try
                                    {
                                        Console.WriteLine("Please enter the street in which you want to open new branch");
                                        adress = Console.ReadLine();
                                        if (rgx1.IsMatch(adress))
                                        {
                                            validadress = true;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Street name must contain only letters.");
                                            continue;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Wrong format of adress.Try again.");
                                        continue;
                                    }
                                }

                                if (validadress == false)
                                {
                                    Console.WriteLine("You couldn't enter valid adress.\nPlease enter command.");
                                    continue;
                                }

                                if (Street.IsExist(adress) == false)
                                {
                                    bool validcoord = false;
                                    Console.WriteLine("There is no branch in {0}, please enter coordinates.", adress);
                                    for (int i = 0; i < 5; i++)
                                    {
                                        try
                                        {
                                            Console.WriteLine("latitude:");
                                            latitude = double.Parse(Console.ReadLine());
                                            Console.WriteLine("longitude:");
                                            longitude = double.Parse(Console.ReadLine());
                                            if (longitude >= 10 && longitude <= 85 && latitude >= 10 && longitude <= 85)
                                            {
                                                geo = new GeoCoordinate(latitude, longitude);
                                                if (!Street.ExistGeo(geo))
                                                {
                                                    validcoord = true;
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("You entered coordinates of existing street.Try again");
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Coordinates must be in 10 - 85 range.Try again.");
                                                continue;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("Enter double coordinates.Try again.");
                                            continue;
                                        }
                                    }
                                    if (validcoord == false)
                                    {
                                        Console.WriteLine("You couldn't enter right coordinates.\nPlease enter command.");
                                        continue;
                                    }
                                }
                                for (int i = 0; i < 5; i++)
                                {
                                    try
                                    {
                                        Console.WriteLine("Enter phone number:");
                                        phoneNumber = Console.ReadLine();
                                        if (phoneNumber.Length == 9 && rgx2.IsMatch(phoneNumber) && LuckyDay.IsValidNumber(phoneNumber))
                                        {
                                            validnumber = true;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Phone number length must be 9, and contain only digits, and must be unique.Try again.");
                                            continue;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Enter right phone number.Try again.(catch)");
                                        continue;
                                    }
                                }
                                if (validnumber == false)
                                {
                                    Console.WriteLine("You couldn't enter valid phone number.\nPlease enter command.");
                                    continue;
                                }
                                if (!Street.IsExist(adress))            // if adress is not existing in Streets.txt, will add street name and its coordinates
                                {
                                    Street.streets.Add(adress, geo);
                                    Street.StreetWriter();
                                }
                                Random rnd = new Random();              // if that adress exists, will concatenate "_" and random number as a full adress
                                int num = rnd.Next(1, 70);
                                LuckyDay cafe = new LuckyDay(adress + "_" + num.ToString(), phoneNumber);
                                LuckyDay.CafesWriter();
                                Console.WriteLine("Branch has been opened.");
                            }
                            else
                            {
                                Console.WriteLine("Only admin can open new branch of cafes complex.\nPlease enter command.");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to open new branch.\nPlease enter command.");
                            continue;
                        }
                        break;

                    case "make order":
                        if (isactive == true)
                        {
                            Regex rgx = new Regex("^[a-zA-Z]+$");               // adress must contain only letters
                            string adress = "";
                            bool validadress = false;
                            try
                            {
                                Console.WriteLine("What do you want?");
                                Console.WriteLine();
                                Console.WriteLine(LuckyDay.ShowMenu());
                                Console.WriteLine("\nEnter foodnames seperated by space.");
                                string items = Console.ReadLine();
                                int sum = Account.MakeOrder(items);
                                if (DateTime.Now.DayOfWeek.ToString().Equals("Friday"))
                                    Console.WriteLine("CONGRATUALTIONS!!! Today is the lucky day for you! Because we will discount all items of our menu -20%");
                                Console.WriteLine("Your bill is: " + sum);
                                if (sum != 0)
                                {
                                    Console.WriteLine("We inclused to bill only items which were in menu.");
                                    Console.WriteLine();
                                    if (acc.Balance >= sum)
                                    {
                                        for (int i = 0; i < 5; i++)
                                        {
                                            Console.WriteLine("Enter your adress.");
                                            adress = Console.ReadLine();
                                            if (rgx.IsMatch(adress))
                                            {
                                                validadress = true;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enter only name of street.Try again.");
                                                continue;
                                            }
                                        }
                                        if (validadress == false)
                                        {
                                            Console.WriteLine("You couldn't enter right adress.\nPlease enter command.");
                                            continue;
                                        }
                                        bool existcoord = false;
                                        double longitude = 0;
                                        double latitude = 0;
                                        GeoCoordinate geo = null;
                                        if (!Street.IsExist(adress))
                                        {
                                            for (int i = 0; i < 5; i++)
                                            {
                                                Console.WriteLine("Enter your coordinates:");
                                                try
                                                {
                                                    Console.WriteLine("latitude:");
                                                    latitude = double.Parse(Console.ReadLine());
                                                    Console.WriteLine("longitude:");
                                                    longitude = double.Parse(Console.ReadLine());
                                                    // 10 - 85 is the range of coordinates where cafe can send order
                                                    if (longitude >= 10 && longitude <= 85 && latitude >= 10 && longitude <= 85)
                                                    {
                                                        geo = new GeoCoordinate(latitude, longitude);
                                                        existcoord = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Coordinates must be in 10 - 85 range.Try again.");
                                                        continue;
                                                    }
                                                }
                                                catch (Exception)
                                                {
                                                    Console.WriteLine("Enter double cooridinates.Try again.)");
                                                    continue;
                                                }
                                            }
                                            if (existcoord == false)
                                            {
                                                Console.WriteLine("You couldn't enter right coordinates.\nPlease enter command.");
                                                continue;
                                            }
                                            acc.PayForOrder(sum);
                                            LuckyDay.Earn(sum);
                                            Console.WriteLine(Street.MinimalDistance(geo));
                                        }
                                        else
                                        {
                                            // if in entered adress exists a branche, order will be sent from the  branche in entered adress
                                            Console.WriteLine("Bill has been paid from your card. We will send your order from {0}", adress);
                                            acc.PayForOrder(sum);
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("You can't pay bill. So enter command add money, and after it you can make order.");
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You ordered nothing, or we have no your ordered items.\nPlease enter command");
                                    continue;
                                }
                            }
                            catch (Exception e)                 // unplaned exception
                            {
                                Console.WriteLine("Oops!");
                                Console.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Log in to make order.");
                            continue;
                        }
                        break;


                    case "search":                         // will search cafe branch by street, give adress and phone_number
                        Console.WriteLine("Enter word: ");
                        string str = Console.ReadLine();
                        if (str.Length >= 3)
                        {
                            LuckyDay cafe = LuckyDay.Search(str);
                            if (cafe.Adress != null)
                                Console.WriteLine(cafe.Adress + " " + cafe.PhoneNumber);
                            else
                            {
                                Console.WriteLine("There is no cafe in your entered adress");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter longer word.\nPlease enter command.");
                            continue;
                        }
                        break;

                    case "acc info":
                        if (isactive == true)
                        {
                            acc.ShowAccountInfo();
                        }
                        break;

                    case "exit":
                        status = false;
                        break;


                    default:
                        Console.WriteLine("Invalid command. Enter help to see command list.");
                        break;
                }
            }
        }

        static void HelpVirtual()
        {
            Console.WriteLine(
                "valid commands at website:\n" +
                "log in                 : To log in\n" +
                "log out                : To log out from account\n" +
                "create an account      : To create an account\n" +
                "change password        : To change password of account\n" +
                "show my balance        : Shows money at your card\n" +
                "show menu              : To show menu of cafe\n" +
                "make order             : To make order at cafe\n" +
                "add money              : To add money to card\n" +
                "acc info               : Shows main info about your acc\n" +
                "rate                   : To rate a cafe\n" +
                "leave review           : To leave opinion about cafe\n" +
                "show reviews           : Shows all reviews about cafe\n" +
                "about cafe             : Shows main info about cafe\n" +
                "feedback               : Shows numbers, fax, email of cafe\n" +
                "search                 : Shows info about LuckyDay by writing one part of the name of cafe\n"
                );
        }

        static void AdminAbilities()
        {
            Console.WriteLine(
                "advantages of admin:\n" +
                "add cafe           : To open new branch\n" +
                "add to menu        : To add new item to menu\n" +
                "delete from menu   : To delete item from menu\n"
                );
        }

    }
}

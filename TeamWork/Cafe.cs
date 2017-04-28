using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    class Cafe: IComparable<Cafe>
    {
        List<Cafe> menu_am = new List<Cafe>();
        public Dictionary<string, int> Menu { get; set; }
        public string Name { get; private set; }
        public string Adress { get; private set; }
        public TimeSpan Open = new TimeSpan();
        public TimeSpan Close = new TimeSpan();
        public string Phone { get; private set; }
        public double Rating { get; private set; }
        private int rateCount;


        public Cafe(string name, string adress, string phone, TimeSpan open, TimeSpan close)
        {
            Name = name;
            Adress = adress;
            Phone = phone;
            Open = open;
            Close = close;
            menu_am.Add(this);
        }

        public void Rate(int rate)
        {
            if (rate > 5)
                rate = 5;
            if (rate < 1)
                rate = 1;
            Rating = (Rating * rateCount + rate) / (rateCount + 1);
            rateCount++;
        }

        private string Status()
        {
            if (DateTime.Now.TimeOfDay >= Open && DateTime.Now.TimeOfDay <= Close)
                return "Open now";
            else
                return "Close now";
        }

        

        public override string ToString()
        {
            return string.Format("Cafe {0}\nAdress {1}\nOpen status: {2}   {3} - {4}\nRating {5}", Name, Adress, Status(), Open, Close, Rating);
        }

        public int  CompareTo(Cafe cafe)
        {
            if (this.Rating > cafe.Rating)
                return 1;
            else if (this.Rating == cafe.Rating)
                return 0;
            else
                return -1;
        }

        
    }
}

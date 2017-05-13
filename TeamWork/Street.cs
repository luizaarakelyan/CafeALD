using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork
{
    class Street
    {

        public static GeoCoordinate Geo = new GeoCoordinate();
        public static Dictionary<string, GeoCoordinate> streets = new Dictionary<string, GeoCoordinate>();

        public Street()
        { }

        public static void StreetsReader()
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"..\..\Streets.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Geo = new GeoCoordinate();
                        string[] str = s.Split();
                        Geo.Latitude = double.Parse(str[1]);
                        Geo.Longitude = double.Parse(str[2]);
                        streets.Add(str[0], Geo);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read Streets.txt:");
                Console.WriteLine(e.Message);
            }
        }



        public static void StreetWriter()
        {
            try
            {
                File.WriteAllText(@"..\..\Streets.txt", "");
                using (StreamWriter sw = new StreamWriter(@"..\..\Streets.txt"))
                {
                    foreach (KeyValuePair<string, GeoCoordinate> item in streets)
                        sw.WriteLine(item.Key + " " + item.Value.Latitude + " " + item.Value.Longitude);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can't write in file Streets.txt:");
                Console.WriteLine(e.Message);
            }

        }


        public static string MinimalDistance(GeoCoordinate geo)
        {
            GeoCoordinate userCoord = new GeoCoordinate();
            userCoord.Latitude = geo.Latitude;
            userCoord.Longitude = geo.Longitude;

            double min = 0;
            int i = 0;
            string output = "";
            foreach (KeyValuePair<string, GeoCoordinate> item in streets)
            {
                if (i == 0)
                {
                    min = item.Value.GetDistanceTo(userCoord);
                    i++;
                    continue;
                }
                if (item.Value.GetDistanceTo(userCoord) < min)
                {
                    min = item.Value.GetDistanceTo(userCoord);
                    output = item.Key;
                }
                i++;
            }
            return String.Format("We will send your order from {0}", output);
        }



        public static bool IsExist(string adress)         // checking existance of adress
        {
            bool isExist = false;
            foreach (string item in streets.Keys)
            {
                if (adress.Equals(item))
                {
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }

        public static bool ExistGeo(GeoCoordinate geo)      // checking existance of coordinates
        {
            bool isexist = false;
            foreach (KeyValuePair<string, GeoCoordinate> coord in streets)
            {
                if (coord.Value.Equals(geo))
                {
                    isexist = true;
                    break;
                }
            }
            return isexist;
        }
    }
}

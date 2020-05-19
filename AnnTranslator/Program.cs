using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;

namespace AnnTranslator
{
    class Translator
    {
        
        string dirtyShopping;
        List<string> cleanShopping;
        List<string> categories = new List<string>() { "FRUIT N VEG", "FRUIT AND VEG", "BAKERY", 
                                                        "MEATS", "DAIRY", "PANTRY", 
                                                        "TOBY", "HOUSEHOLD", "HOUSE", "FROZEN",
                                                        "MINERALS"}; 

        public void Cleaner(string dirty)
        {
            dirty = dirty.ToUpper();

            dirty = dirty.Replace("CD", "");
            dirty = dirty.Replace(".", "");
            dirty = dirty.Replace(",", "");
            dirty = dirty.Replace("DAN", "");
            dirty = dirty.Replace("THANKS", "");
            dirty = dirty.Replace("TKS", ""); 
            dirty = dirty.Replace("CHECK", "");
            dirty = dirty.Replace("DATES", "");
            dirty = dirty.Replace("DATE", "");
            dirty = dirty.Replace("PKTS", "");
            dirty = dirty.Replace("PKT", "");
            dirty = dirty.Replace("DOZ", "12");
            dirty = dirty.Replace("PLEASE", "");
            dirty = dirty.Replace("PLS", "");
            dirty = dirty.Replace("ETC", "");
            dirty = dirty.Replace("/", "\n*");

            dirty = dirty.ToLower();

            foreach (string cat in categories)
                dirty = dirty.Replace(cat.ToLower(), "\n\n"+cat+ "\n\t");

            dirty = dirty.Replace("*  ", "");
            dirty = dirty.Replace("* ", "");
            dirty = dirty.Replace("*", "");
            dirty = dirty.Replace("    ", " ");
            dirty = dirty.Replace("   ", " ");
            dirty = dirty.Replace("  ", " ");

            

            cleanShopping = Seperator(dirty);
        }


        public List<string> Seperator(string clean)
        {
            List<string> seperatedShopping;

            seperatedShopping = clean.Split("\n").ToList();

            for (int i = 0; i < seperatedShopping.Count; i++)
            {
                if (seperatedShopping[i].Contains("\t"))
                    seperatedShopping[i] = seperatedShopping[i].Replace("\t","\n");
                
                if (seperatedShopping[i] == "s")
                    seperatedShopping.RemoveAt(i);
                else if (seperatedShopping[i] == "\r")
                    seperatedShopping.RemoveAt(i);
                else if (seperatedShopping[i] == "")
                    seperatedShopping.RemoveAt(i);
                else if (Char.IsWhiteSpace(seperatedShopping[i].ElementAt(0)))
                    seperatedShopping[i] = seperatedShopping[i].Substring(1);
            }

                return seperatedShopping;
        }

        public void Printer(List<string> cleanList)
        {
            foreach(string item in cleanList)
                Console.WriteLine(item);
        }

        static void Main()
        {
            Translator t1 = new Translator();
            string path = @"C:\Users\dnlma\Desktop\dirtyShop.txt";

            Console.WriteLine("Formatted shopping list: ");
            t1.dirtyShopping = File.ReadAllText(path);

            //Console.WriteLine(t1.dirtyShopping);

            t1.Cleaner(t1.dirtyShopping);
            t1.Printer(t1.cleanShopping);

            Console.ReadKey();

        }
    }
}

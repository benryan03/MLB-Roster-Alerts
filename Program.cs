using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLB_Trade_Alerts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MLB TRADE ALERTS");

            string currentDate = DateTime.Now.ToString("MM/dd/yy"); ;
            Console.WriteLine(currentDate + "\n");

            //Console.WriteLine("Enter team: ");
            //string Team = Console.ReadLine();
            //Console.WriteLine(Team);

            // Load roster move webpage
            var url = "https://www.mlb.com/redsox/roster/transactions/2020/09/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            // Scrape roster move dates and descriptions from 'roster_table' on the webpage
            var scrapedElements = doc.DocumentNode.SelectNodes("//table[@class='roster__table']/tbody/tr/td");

            List<string> datesList = new List<string>();
            List<string> tradesList = new List<string>();

            // Populate dates in datesList and roster moves in tradesList
            // datesList[0] is the date for the roster move in tradesList[0]
            int iteration = 0;
            foreach (var x in scrapedElements)
            {
                iteration++;
                if (iteration % 2 != 0) // Odd - element is date
                {
                    datesList.Add(x.InnerText);
                }
                else // Even - element is roster move
                {
                    tradesList.Add(x.InnerText);
                }    
            }

            // Find roster moves from today, and print them
            // Time zones may be an issue here
            int tradeQuantity = tradesList.Count;
            for (int x = 0; x < tradeQuantity; x++)
            {
                if (datesList[x] == currentDate)
                {
                    Console.WriteLine(datesList[x]);
                    Console.WriteLine(tradesList[x]);
                }
            }
        }
    }
}

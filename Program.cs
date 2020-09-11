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
            // Parse arguments
            // If no arguments given, default team is all MLB teams
            string team = "mlb";
            if (args.Length != 0)
            {
                team = args[0].Substring(1);
            }

            string yesterdayDate = DateTime.Now.AddDays(-1).ToString("MM/dd/yy"); ;

            Console.WriteLine(team.ToUpper() + " TRADE ALERTS"); // DEBUG
            Console.WriteLine(yesterdayDate + "\n"); // DEBUG

            // Load roster move webpage
            string url = "https://www.mlb.com/" + team + "/roster/transactions/2020/09/";
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

            // Find roster moves from yesterday, and print them
            // Time zones may be an issue here
            int tradeQuantity = tradesList.Count;
            for (int x = 0; x < tradeQuantity; x++)
            {
                if (datesList[x] == yesterdayDate)
                {
                    Console.WriteLine(tradesList[x]);
                }
            }
        }
    }
}

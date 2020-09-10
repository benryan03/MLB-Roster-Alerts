using HtmlAgilityPack;
using System;

namespace MLB_Trade_Alerts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MLB TRADE ALERTS");
            Console.WriteLine(DateTime.Now);

            //Console.WriteLine("Enter team: ");
            //string Team = Console.ReadLine();
            //Console.WriteLine(Team);

            var url = "https://www.mlb.com/redsox/roster/transactions/2020/09/";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var nodes = doc.DocumentNode.SelectNodes("//table[@class='roster__table']/tbody/tr/td");

            foreach (var x in nodes)
            {
                Console.WriteLine(x.InnerText);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Net.Mail;
using System.Net;


namespace MLB_Trade_Alerts
{
    class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments
            if (AreArgsValid(args) == false)
            {
                // ERROR: an argument is invalid or missing
                return;
            }

            // Arguments are valid - parse them
            string team = args[0];
            string toAddressString = args[1];
            string fromAddressString = args[2];
            string fromPasswordString = @args[3];

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

            string emailBody = "";

            // Find roster moves from yesterday, and print them
            // Time zones may be an issue here
            int tradeQuantity = tradesList.Count;
            for (int x = 0; x < tradeQuantity; x++)
            {
                if (datesList[x] == yesterdayDate)
                {
                    Console.WriteLine(tradesList[x]);
                    emailBody += tradesList[x] + "\n";
                }
            }


            MailAddress toAddress = new MailAddress(toAddressString);
            MailAddress fromAddress = new MailAddress(fromAddressString, team + " Roster Alerts");
            string fromPassword = fromPasswordString;
            string subject = team + " roster moves from " + yesterdayDate;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = emailBody
            })
            {
                smtp.Send(message);
            }
        }

        static bool AreArgsValid(string[] args)
        {
            if (args[0] == null)
            {
                // Error: no team
                return false;
            }

            if (args[1] != null)
            {
                if (IsValidEmail(args[1]) == false)
                {
                    // Error: invalid TO address
                    return false;
                }
            }
            else
            {
                // Error: missing TO address
                return false;
            }

            if (args[2] != null)
            {
                if (IsValidEmail(args[2]) == false)
                {
                    // Error: invalid FROM address
                    return false;
                }
            }
            else
            {
                // Error: missing FROM address
                return false;
            }

            if (args[3] == null)
            {
                // Error: missing FROM password
                return false;
            }

            return true;
        }

        // Thanks to https://stackoverflow.com/questions/498400/
        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

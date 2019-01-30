using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Flights
{
    class Program
    {
        static void Main(string[] args)
        {

            GetHtmlAsync();

            Console.ReadLine();
        }

        private static async void GetHtmlAsync()
        {
            var url = "https://newmood.lt/moterims/products?sort=-latest&new=true&ap=1%2C47";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);


            var flightsList = htmlDocument.DocumentNode.Descendants("ul")
            .Where(node => node.GetAttributeValue("id", "")
            .Equals("available-flight")).ToList();


            Console.WriteLine();
        }
    }
}

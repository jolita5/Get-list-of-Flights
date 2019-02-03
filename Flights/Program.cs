using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Flights
{
    class Program
    {
        static void Main(string[] args)
        {
            int pageNr = 0;
            GetHtmlAsync(pageNr);



            Console.ReadLine();
        }



        private static async void GetHtmlAsync(int pageNr)
        {
            var url = "https://newmood.lt/moterims/products?sort=-latest&new=true&ap=1%2C47";


            if (pageNr != 0)
            {
                url = "https://newmood.lt/moterims/products?page" + pageNr + "sort=-latest&new=true&ap=1%2C47";

            }


            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);



            var clothesList = htmlDocument.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("id", "")
            .Equals("new-product-list")).ToList();


            var productList = htmlDocument.DocumentNode.Descendants("div")
           .Where(node => node.GetAttributeValue("class", "")
           .Contains("col-xs-6 col-md-4 col-sm-4 col-lg-4 product-block")).ToList();


            foreach (var item in productList)
            {
                Console.WriteLine(item.GetAttributeValue("col-xs-6 col-md-4 col-sm-4 col-lg-4 product-block", ""));

                Console.WriteLine(item.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("product")).FirstOrDefault().InnerText.Trim('\n'));

                GetHtmlAsync(++pageNr);


            }



            Console.WriteLine();

            Console.WriteLine($"Count of products: {productList.Count}.");


        }


    }


}

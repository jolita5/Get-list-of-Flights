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


        private static string GetUrl(int pageNr)
        {
            pageNr = 0;

            string url = null;

            if (pageNr != 0)
            {
                url = "https://newmood.lt/moterims/products?page=" + pageNr + "&sort=-latest&ap=1%2C47&new=true";
            }


            return url = "https://newmood.lt/moterims/products?sort=-latest&new=true&ap=1%2C47";


        }



        private static async void GetHtmlAsync(int pageNr)
        {
            pageNr = 0;

            var raknings = GetUrl(pageNr);


            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(GetUrl(pageNr));

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);



            var clothesList = htmlDocument.DocumentNode.Descendants("div")
            .Where(node => node.GetAttributeValue("id", "")
            .Equals("new-product-list")).ToList();

            var productList = htmlDocument.DocumentNode.Descendants("div")
           .Where(node => node.GetAttributeValue("class", "")
           .Contains("col-xs-6 col-md-4 col-sm-4 col-lg-4 product-block")).ToList();

            bool check = true;

            while (productList.Count > 0)
            {
                foreach (var item in productList)
                {



                    Console.WriteLine(item.GetAttributeValue("brand", ""));

                    Console.WriteLine(
                        item.Descendants("div")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Equals("title")).FirstOrDefault().InnerText.Trim('\r', '\n', 't'));

                    Console.WriteLine(
                        Regex.Match(
                            item.Descendants("div")
                            .Where(node => node.GetAttributeValue("class", "")
                            .Equals("right text-right price")).FirstOrDefault().InnerText.Trim('\r', '\n', 't')
                        , @"\d+.\d+"));

                    //Console.WriteLine(
                    //    item.Descendants("div")
                    //    .Where(node => node.GetAttributeValue("class", "")
                    //    .Equals("size")).FirstOrDefault().InnerText.Trim('\r', '\n', 't'));

                    Console.WriteLine(item.Descendants("a").FirstOrDefault().GetAttributeValue("href", ""));


                    raknings = GetUrl(++pageNr);

                    
                }


            }

            Console.WriteLine();

            Console.WriteLine($"Count of products: {productList.Count}.");


        }
    }


}

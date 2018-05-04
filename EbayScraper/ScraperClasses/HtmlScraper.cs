using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EbayScraper.Models;
using HtmlAgilityPack;
using ScrapySharp;
using ScrapySharp.Extensions;

namespace EbayScraper.ScraperClasses
{
    public static class HtmlScraper
    {
        
        public static JsonContainer BeginEbayScrape(string searchurl)
        {
            var jsoncontainer = new JsonContainer();
            var web = new HtmlWeb();
            var document = web.Load(searchurl);
            var nodes = document.DocumentNode.Descendants().Where(d =>
                d.Attributes.Contains("class") &&
                (d.Attributes["class"].Value.Split(' ').Contains("s-Item") || d.Attributes["class"].Value.Split(' ').Contains("sresult")) &&
                !d.Attributes["class"].Value.Split(' ').Contains("shic"));


            foreach (HtmlNode node in nodes)
            {
                var imageUrl = node.Descendants().Where(d => d.Attributes.Contains("class") &&
                                                             d.Attributes["class"].Value.Contains("img")).CssSelect("img").First().Attributes["src"].Value;
                var price = node.Descendants().CssSelect("li")
                    .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lvprice prc"))
                    .CssSelect("span").First().InnerText.Replace("\t", String.Empty).Replace("\n", String.Empty).Replace("\r", String.Empty);

                var title = node.Descendants().First(d => d.Attributes.Contains("class") && 
                                                          d.Attributes["class"].Value.Contains("lvtitle")).InnerText.
                    Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty);


                  System.Diagnostics.Debug.WriteLine(imageUrl + "   " + price + "   " + title);

                jsoncontainer.addItem(imageUrl, title, price);
            }
            return jsoncontainer;
        }
    }
}
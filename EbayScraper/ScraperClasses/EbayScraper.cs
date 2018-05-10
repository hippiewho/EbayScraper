using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EbayScraper.Models;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace EbayScraper.ScraperClasses
{
    public class EbayScraper : HtmlScraper
    {
        protected override IEnumerable<HtmlNode> GetWebPageNodes(HtmlDocument document)
        {
            return document.DocumentNode.Descendants().Where(d =>
                d.Attributes.Contains("class") &&
                (d.Attributes["class"].Value.Split(' ').Contains("s-Item") || d.Attributes["class"].Value.Split(' ').Contains("sresult") || d.Attributes["class"].Value.Split(' ').Contains("s-item")) );
        }

        protected override void ParseWebPageNode(JsonContainer jsoncontainer, HtmlNode node)
        {
            var imageUrl = node.Descendants().Where(d => d.Attributes.Contains("class") &&
                                                         d.Attributes["class"].Value.Contains("img") ).CssSelect("img").First().Attributes["src"].Value;
            var price = node.Descendants().CssSelect("li")
                .Where(d => d.Attributes.Contains("class") && (d.Attributes["class"].Value.Contains("lvprice prc") || d.Attributes["class"].Value.Contains("s-item__detail--primary")))
                .CssSelect("span").First().InnerText.Replace("\t", String.Empty).Replace("\n", String.Empty).Replace("\r", String.Empty);

            var title = node.Descendants().First(d => d.Attributes.Contains("class") &&
                                                      (d.Attributes["class"].Value.Contains("lvtitle") || d.Attributes["class"].Value.Contains("s-item__title")) ).InnerText.
                Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty);


            System.Diagnostics.Debug.WriteLine(imageUrl + "   " + price + "   " + title);

            jsoncontainer.addItem(imageUrl, title, price);
        }

        protected override async Task<HtmlDocument> LoadWebpage(string searchurl, HtmlWeb web)
        {
            return await Task.Run(() => web.Load(searchurl));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace Scraper.ScraperClasses
{
    public class EbayScraper : HtmlScraper
    {
        protected override IEnumerable<HtmlNode> GetWebPageNodes(HtmlDocument document)
        {
            return document.DocumentNode.Descendants().Where(d =>
                d.Attributes.Contains("class") &&
                (d.Attributes["class"].Value.Split(' ').Contains("s-Item") || d.Attributes["class"].Value.Split(' ').Contains("sresult") || d.Attributes["class"].Value.Split(' ').Contains("s-item")) );
        }

        protected override string ParseTitle(HtmlNode node)
        {
            var title = node.Descendants().CssSelect("img").First().Attributes["alt"].Value;
            return title;
        }

        protected override string ParsePrice(HtmlNode node)
        {
            return node.Descendants().CssSelect("span").First(d => d.InnerText.Contains("$")).InnerText
                .Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty);
        }

        protected override string ParseImageUrl(HtmlNode node)
        {
            var imageUrl = node.Descendants().CssSelect("img").First().Attributes["src"].Value;
            try
            {
                imageUrl = node.Descendants().CssSelect("img").First().Attributes["data-src"].Value;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("data-src does not exist.");
            }

            return imageUrl;
        }

    }
}
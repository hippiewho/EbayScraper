using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace Scraper.ScraperClasses.Scrapers
{
    public class AmazonScraper : HtmlScraper
    {
        protected override IEnumerable<HtmlNode> GetWebPageNodes(HtmlDocument document)
        {
            return document.DocumentNode.Descendants().Where(d =>
                d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("result_") );
        }

        protected  override string ParseImageUrl(HtmlNode node)
        {
            return node.Descendants().CssSelect("img").First().Attributes["src"].Value;
        }

        protected override string ParsePrice(HtmlNode node)
        {
            return node.Descendants().CssSelect("span").First(d => d.InnerText.Contains("$")).InnerText
                .Replace("\t", String.Empty).Replace("\r", String.Empty).Replace("\n", String.Empty);
        }

        protected override string ParseTitle(HtmlNode node)
        {
            return node.Descendants().CssSelect("h2").First().InnerText;
        }
    }
}
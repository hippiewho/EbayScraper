using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EbayScraper.Models;
using EbayScraper.ScraperClasses.Interfaces;
using HtmlAgilityPack;
using ScrapySharp;
using ScrapySharp.Extensions;

namespace EbayScraper.ScraperClasses
{
    public abstract class HtmlScraper : IScraper
    {

        public async Task<JsonContainer> BeginScrapeAsync(string searchurl)
        {
            var jsoncontainer = new JsonContainer();
            var web = new HtmlWeb();
            HtmlDocument document = await LoadWebpage(searchurl, web);
            IEnumerable<HtmlNode> nodes = GetWebPageNodes(document);

            foreach (HtmlNode node in nodes)
            {
                ParseWebPageNode(jsoncontainer, node);
            }
            return jsoncontainer;
        }

        protected abstract IEnumerable<HtmlNode> GetWebPageNodes(HtmlDocument document);

        protected abstract void ParseWebPageNode(JsonContainer jsoncontainer, HtmlNode node);

        protected abstract Task<HtmlDocument> LoadWebpage(string searchurl, HtmlWeb web);
    }
}
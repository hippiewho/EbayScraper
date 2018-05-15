using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Scraper.Models;
using Scraper.ScraperClasses.Interfaces;
using ScrapySharp.Extensions;

namespace Scraper.ScraperClasses
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

        protected void ParseWebPageNode(JsonContainer jsoncontainer, HtmlNode node)
        {
            try
            {
                string title = ParseTitle(node);
                string price = ParsePrice(node);
                string imageurl = ParseImageUrl(node);
                jsoncontainer.AddItem(imageurl, title, price);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("Error occured while parsing an item node.");
            }

        }
        protected async Task<HtmlDocument> LoadWebpage(string searchurl, HtmlWeb web)
        {
            return await Task.Run(() => web.Load(searchurl));
        }

        protected abstract string ParseImageUrl(HtmlNode node);
        protected abstract string ParsePrice(HtmlNode node);

        protected abstract string ParseTitle(HtmlNode node);

        protected abstract IEnumerable<HtmlNode> GetWebPageNodes(HtmlDocument document);

    }
}
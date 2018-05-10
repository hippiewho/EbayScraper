using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EbayScraper.Models;
using HtmlAgilityPack;
using ScrapySharp;
using ScrapySharp.Extensions;

namespace EbayScraper.ScraperClasses
{
    public class HtmlConnection
    {
        private string EbayBaseUrl { get; } = "https://www.ebay.com/sch/i.html?_nkw=";
        private JsonContainer _jsonContainer;

        public async Task<string> ParseEbay(string id)
        {
            string term = id;
            var searchurl = EbayBaseUrl + term;
            EbayScraper ebayScraper = new EbayScraper();
            _jsonContainer = await ebayScraper.BeginScrapeAsync(searchurl);
            return _jsonContainer.ConvertListToJsonString();
        }
    }
}
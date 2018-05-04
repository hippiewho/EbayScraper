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
    public class HtmlConnection
    {
        private string EbayBaseUrl { get; } = "https://www.ebay.com/sch/i.html?_nkw=";
        private JsonContainer _jsonContainer;

        public string ParseEbay(string id)
        {
            string term = id;
            var searchurl = EbayBaseUrl + term;

            _jsonContainer = HtmlScraper.BeginEbayScrape(searchurl);
            
            return _jsonContainer.ConvertListToJsonString();
        }

        private string ParseEbayListings(string term)
        {
            throw new NotImplementedException();
        }
    }
}
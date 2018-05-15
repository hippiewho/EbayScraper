using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Scraper.Models;

namespace Scraper.ScraperClasses.Interfaces
{
    interface IScraper
    {
        Task<JsonContainer> BeginScrapeAsync(string searchurl);
    }
}

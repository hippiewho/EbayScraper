using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbayScraper.Models;
using HtmlAgilityPack;

namespace EbayScraper.ScraperClasses.Interfaces
{
    interface IScraper
    {
        Task<JsonContainer> BeginScrapeAsync(string searchurl);
    }
}

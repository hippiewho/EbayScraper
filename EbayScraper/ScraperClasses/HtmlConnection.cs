using System;
using System.Threading.Tasks;
using Scraper.Models;
using Scraper.ScraperClasses.Scrapers;

namespace Scraper.ScraperClasses
{
    public class HtmlConnection
    {
        private string EbayBaseUrl { get; } = "https://www.ebay.com/sch/i.html?_nkw=";
        private string AmazonBaseUrl { get; } = "https://www.amazon.com/s?field-keywords=";

        private JsonContainer _jsonContainer;

        public async Task<string> ParseEbay(string term, string scraper)
        {
            try
            {
                if (scraper.Equals("ebay"))
                {
                    await EbayScrape(term);
                }
                else if (scraper.Equals("amazon"))
                {
                    await AmazonScrape(term);
                }
                return _jsonContainer.ConvertListToJsonString();
            }
            catch (Exception)
            {
                Console.WriteLine("Could Not Create JSON Object.");
                throw;
            }

        }

        private async Task AmazonScrape(string term)
        {
            var amazonnsearchurl = AmazonBaseUrl + term;
            AmazonScraper amazonScraper = new AmazonScraper();
            _jsonContainer = await amazonScraper.BeginScrapeAsync(amazonnsearchurl);
        }

        private async Task EbayScrape(string term)
        {
            var ebaysearchurl = EbayBaseUrl + term;
            EbayScraper ebayScraper = new EbayScraper();
            _jsonContainer = await ebayScraper.BeginScrapeAsync(ebaysearchurl);
        }
    }
}
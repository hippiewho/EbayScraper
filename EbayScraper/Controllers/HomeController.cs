using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Scraper.ScraperClasses;

namespace Scraper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> Scrape(string term, string scraper)
        {
            HtmlConnection htmlConnection = new HtmlConnection();
            string jsonreturn = await htmlConnection.ParseEbay(term, scraper);
            return jsonreturn;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EbayScraper.ScraperClasses;

namespace EbayScraper.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> Scrape(string id)
        {
            HtmlConnection htmlConnection = new HtmlConnection();
            string jsonreturn = await htmlConnection.ParseEbay(id);
            return jsonreturn;
        }
    }
}
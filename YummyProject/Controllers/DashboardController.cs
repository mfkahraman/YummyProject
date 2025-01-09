using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;

namespace YummyProject.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        YummyContext db = new YummyContext();
        public ActionResult Index()
        {
            ViewBag.soupCount = db.Products.Where(x => x.CategoryId == 1).Count();
            ViewBag.mostExpensive = db.Products.OrderByDescending(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            ViewBag.avgPrice = db.Products.Average(x => x.Price);
            ViewBag.cheapestProduct = db.Products.OrderBy(x=>x.Price).Select(x=>x.ProductName).FirstOrDefault();
            return View();
        }
    }
}
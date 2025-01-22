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
            ViewBag.productCount = db.Products.Count();
            ViewBag.mostExpensive = db.Products.OrderByDescending(x => x.Price).Select(x => x.ProductName).FirstOrDefault();
            ViewBag.avgPrice = db.Products.Average(x => x.Price);
            ViewBag.cheapestProduct = db.Products.OrderBy(x=>x.Price).Select(x=>x.ProductName).FirstOrDefault();
            var values = db.Products.OrderByDescending(x => x.ProductId).Take(10).ToList();
            return View(values);
        }
    }
}
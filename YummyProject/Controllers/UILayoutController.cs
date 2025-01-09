using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;

namespace YummyProject.Controllers
{
    public class UILayoutController : Controller
    {
        // GET: UILayout
        YummyContext db = new YummyContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SocialMedias()
        {
            var values = db.SocialMedias.ToList();
            return PartialView(values);
        }
    }
}
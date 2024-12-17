using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class FeatureController : Controller
    {
        YummyContext db = new YummyContext();
        public ActionResult Index()
        {
            var values = db.Features.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddFeature()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFeature(Feature model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            db.Features.Add(model);
            var dbresult = db.SaveChanges();
            if(dbresult == 0)
            {
                ViewBag.error = "Dbye kayıt edilirken sorun oluştu";
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFeature(int id)
        {
            var itemToDelete = db.Features.Find(id);
            db.Features.Remove(itemToDelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    public class ChefSocialController : Controller
    {
        private readonly YummyContext db = new YummyContext();
        public ActionResult Index(int? page)
        {
            int pageSize = 10; // Number of items per page
            int pageNumber = (page ?? 1); // Default to page 1 if no page is specified

            var products = db.ChefSocials.OrderBy(p => p.ChefSocialId).ToPagedList(pageNumber, pageSize);
            return View(products);
        }

        [HttpGet]
        public ActionResult AddChefSocial()
        {
            ViewBag.Chefs = new SelectList(db.Chefs, "ChefId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddChefSocial(ChefSocial model)
        {
            switch (model.SocialMediaName.ToLower())
            {
                case "twitter":
                    model.Icon = "bi bi-twitter-x";
                    break;
                case "facebook":
                    model.Icon = "bi bi-facebook";
                    break;
                case "instagram":
                    model.Icon = "bi bi-instagram";
                    break;
                case "linkedin":
                    model.Icon = "bi bi-linkedin";
                    break;
                default:
                    ModelState.AddModelError("SocialMediaName", "Geçersiz sosyal medya adı.");
                    ViewBag.Error = "Geçersiz sosyal medya adı.";
                    ViewBag.Chefs = new SelectList(db.Chefs, "ChefId", "Name");
                    ViewBag.SocialMediaNames = new SelectList(new[] { "twitter", "facebook", "instagram", "linkedin" });
                    return View(model);
            }

            if (ModelState.IsValid)
            {
                db.ChefSocials.Add(model);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Kayıt eklendi.";
                return RedirectToAction("Index");
            }

            ViewBag.Chefs = new SelectList(db.Chefs, "ChefId", "Name");
            ViewBag.SocialMediaNames = new SelectList(new[] { "twitter", "facebook", "instagram", "linkedin" });
            ViewBag.Error = "Lütfen tüm alanları doğru şekilde doldurun.";
            return View(model);
        }


        public ActionResult UpdateChefSocial(int id)
        {
            var value = db.ChefSocials.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }

            ViewBag.SocialMediaNames = new SelectList(new[] { "twitter", "facebook", "instagram", "linkedin" }, value.SocialMediaName);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateChefSocial(ChefSocial model)
        {
            switch (model.SocialMediaName.ToLower())
            {
                case "twitter":
                    model.Icon = "bi bi-twitter-x";
                    break;
                case "facebook":
                    model.Icon = "bi bi-facebook";
                    break;
                case "instagram":
                    model.Icon = "bi bi-instagram";
                    break;
                case "linkedin":
                    model.Icon = "bi bi-linkedin";
                    break;
                default:
                    ModelState.AddModelError("SocialMediaName", "Geçersiz sosyal medya adı.");
                    ViewBag.Error = "Geçersiz sosyal medya adı.";
                    ViewBag.SocialMediaNames = new SelectList(new[] { "twitter", "facebook", "instagram", "linkedin" }, model.SocialMediaName);
                    return View(model);
            }

            if (ModelState.IsValid)
            {
                var itemToUpdate = db.ChefSocials.Find(model.ChefSocialId);
                if (itemToUpdate == null)
                {
                    ModelState.AddModelError("", "Güncellenecek kayıt bulunamadı.");
                    ViewBag.Error = "Güncellenecek kayıt bulunamadı.";
                    ViewBag.SocialMediaNames = new SelectList(new[] { "twitter", "facebook", "instagram", "linkedin" }, model.SocialMediaName);
                    return View(model);
                }

                db.Entry(itemToUpdate).CurrentValues.SetValues(model);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Kayıt güncellendi.";
                return RedirectToAction("Index");
            }

            ViewBag.SocialMediaNames = new SelectList(new[] { "twitter", "facebook", "instagram", "linkedin" }, model.SocialMediaName);
            ViewBag.Error = "Lütfen tüm alanları doğru şekilde doldurun.";
            return View(model);
        }


        private string SaveImage(HttpPostedFileBase file, string folderPath)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".gif")
            {
                return null;
            }

            string fileName = Guid.NewGuid() + fileExtension;
            string rootPath = "/wwwroot/images/";
            string fullPath = rootPath + folderPath;
            string serverFolderPath = Server.MapPath(fullPath);
            if (!Directory.Exists(serverFolderPath))
            {
                Directory.CreateDirectory(serverFolderPath);
            }
            string path = Path.Combine(serverFolderPath, fileName);
            file.SaveAs(path);

            return fullPath + fileName;
        }

        private bool DeleteOldImage(string imageUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    var oldImagePath = Server.MapPath(imageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                        return true;
                    }
                }
            }
            catch
            {
            }
            return false;
        }

    }
}
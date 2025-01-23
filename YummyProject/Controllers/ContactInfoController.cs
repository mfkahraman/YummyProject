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
    public class ContactInfoController : Controller
    {
        private readonly YummyContext db = new YummyContext();

        public ActionResult Index(int? page)
        {
            int pageSize = 5; // Number of items per page
            int pageNumber = (page ?? 1); // Default to page 1 if no page is specified

            var products = db.ContactInfos.OrderBy(p => p.ContactInfoId).ToPagedList(pageNumber, pageSize);
            return View(products);
        }

        [HttpGet]
        public ActionResult AddContactInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddContactInfo(ContactInfo model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ContactInfos.Add(model);
                    int dbresult = db.SaveChanges();
                    TempData["SuccessMessage"] = "Kayıt eklendi.";

                    if (dbresult > 0)
                    {
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", "Veritabanına kayıt sırasında bir sorun oluştu.");
                    ViewBag.Error = "Kayıt işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin.";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Beklenmeyen bir hata oluştu: " + ex.Message);
                    ViewBag.Error = "Beklenmeyen bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Error = "Lütfen tüm alanları doğru şekilde doldurun.";
            }

            return View(model);
        }


        public ActionResult UpdateContactInfo(int id)
        {
            var value = db.ContactInfos.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }

            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateContactInfo(ContactInfo model)
        {
            if (ModelState.IsValid)
            {
                var itemToUpdate = db.Categories.Find(model.ContactInfoId);
                if (itemToUpdate == null)
                {
                    ModelState.AddModelError("", "Güncellenecek kayıt bulunamadı.");
                    ViewBag.Error = "Güncellenecek kayıt bulunamadı.";
                    return View(model);
                }

                db.Entry(itemToUpdate).CurrentValues.SetValues(model);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Kayıt güncellendi.";
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Lütfen tüm alanları doğru şekilde doldurun.";
            return View(model);
        }

        public ActionResult DeleteContactInfo(int id)
        {
            var itemToDelete = db.ContactInfos.Find(id);
            if (itemToDelete != null)
            {
                try
                {
                    db.ContactInfos.Remove(itemToDelete);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Silme işlemi sırasında bir hata oluştu:" + ex.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Silinecek kayıt bulunamadı.";
            }

            TempData["SuccessMessage"] = "Kayıt silindi.";
            return RedirectToAction("Index");
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
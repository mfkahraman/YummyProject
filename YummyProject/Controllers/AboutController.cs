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
    [Authorize]
    public class AboutController : Controller
    {
        private readonly YummyContext db = new YummyContext();
        public ActionResult Index()
        {
            var values = db.Abouts.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About model, HttpPostedFileBase imageFile, HttpPostedFileBase imageFile2)
        {
            //ImageUrl için
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                var imagePath = SaveImage(imageFile, "abouts/");
                if (imagePath == null)
                {
                    ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png veya .gif dosyaları yükleyebilirsiniz.");
                    ViewBag.Error = "Görsel yüklenemedi. Lütfen geçerli bir dosya formatı seçin.";
                    return View(model);
                }
                model.ImageUrl = imagePath;
            }

            //ImageUrl2 için
            if (imageFile2 != null && imageFile2.ContentLength > 0)
            {
                var imagePath2 = SaveImage(imageFile2, "abouts/");
                if (imagePath2 == null)
                {
                    ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png veya .gif dosyaları yükleyebilirsiniz.");
                    ViewBag.Error = "Görsel yüklenemedi. Lütfen geçerli bir dosya formatı seçin.";
                    return View(model);
                }
                model.ImageUrl2 = imagePath2;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Abouts.Add(model);
                    int dbresult = db.SaveChanges();

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


        public ActionResult UpdateAbout(int id)
        {
            var value = db.Abouts.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateAbout(About model, HttpPostedFileBase imageFile, HttpPostedFileBase imageFile2)
        {
            var itemToUpdate = db.Abouts.Find(model.AboutId);

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Güncellenecek kayıt bulunamadı.");
                ViewBag.Error = "Güncellenecek kayıt bulunamadı.";
                return View(model);
            }

            itemToUpdate.Item1 = model.Item1;
            itemToUpdate.Item2 = model.Item2;
            itemToUpdate.Item3 = model.Item3;
            itemToUpdate.PhoneNumber = model.PhoneNumber;
            itemToUpdate.Description = model.Description;
            itemToUpdate.VideoUrl = model.VideoUrl;

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                DeleteOldImage(itemToUpdate.ImageUrl);

                var imagePath = SaveImage(imageFile, "abouts/");
                if (imagePath == null)
                {
                    ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png veya .gif dosyaları yükleyebilirsiniz.");
                    ViewBag.Error = "Görsel yüklenemedi. Lütfen geçerli bir dosya formatı seçin.";
                    return View(model);
                }

                itemToUpdate.ImageUrl = imagePath;
            }

            if (imageFile2 != null && imageFile2.ContentLength > 0)
            {
                DeleteOldImage(itemToUpdate.ImageUrl2);

                var imagePath2 = SaveImage(imageFile2, "abouts/");
                if (imagePath2 == null)
                {
                    ModelState.AddModelError("ImageUrl2", "Sadece .jpg, .jpeg, .png veya .gif dosyaları yükleyebilirsiniz.");
                    ViewBag.Error = "Görsel yüklenemedi. Lütfen geçerli bir dosya formatı seçin.";
                    return View(model);
                }

                itemToUpdate.ImageUrl2 = imagePath2;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu: " + ex.Message);
                    ViewBag.Error = "Beklenmeyen bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Error = "Lütfen tüm alanları doğru şekilde doldurun.";
            }

            return View(model);
        }

        public ActionResult DeleteAbout(int id)
        {
            var itemToDelete = db.Abouts.Find(id);
            if (itemToDelete != null)
            {
                try
                {
                    DeleteOldImage(itemToDelete.ImageUrl);
                    DeleteOldImage(itemToDelete.ImageUrl2);
                    db.Abouts.Remove(itemToDelete);
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
                TempData["ErrorMessage"] = "Silinecek entity bulunamadı.";
            }

            TempData["SuccessMessage"] = "Entity silindi.";
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
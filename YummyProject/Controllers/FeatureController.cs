﻿using System;
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
    public class FeatureController : Controller
    {
        private readonly YummyContext db = new YummyContext();
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
        public ActionResult AddFeature(Feature model, HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                var imagePath = SaveImage(imageFile, "features/");
                if (imagePath == null)
                {
                    ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png veya .gif dosyaları yükleyebilirsiniz.");
                    ViewBag.Error = "Görsel yüklenemedi. Lütfen geçerli bir dosya formatı seçin.";
                    return View(model);
                }
                model.ImageUrl = imagePath;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Features.Add(model);
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


        public ActionResult UpdateFeature(int id)
        {
            var value = db.Features.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateFeature(Feature model, HttpPostedFileBase imageFile)
        {
            var itemToUpdate = db.Features.Find(model.FeatureId);

            if (itemToUpdate == null)
            {
                ModelState.AddModelError("", "Güncellenecek kayıt bulunamadı.");
                ViewBag.Error = "Güncellenecek kayıt bulunamadı.";
                return View(model);
            }

            itemToUpdate.Title = model.Title;
            itemToUpdate.Description = model.Description;
            itemToUpdate.VideoUrl = model.VideoUrl;

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                DeleteOldImage(itemToUpdate.ImageUrl);

                var imagePath = SaveImage(imageFile, "features/");
                if (imagePath == null)
                {
                    ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png veya .gif dosyaları yükleyebilirsiniz.");
                    ViewBag.Error = "Görsel yüklenemedi. Lütfen geçerli bir dosya formatı seçin.";
                    return View(model);
                }

                itemToUpdate.ImageUrl = imagePath;
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

        public ActionResult DeleteFeature(int id)
        {
            var itemToDelete = db.Features.Find(id);
            if (itemToDelete != null)
            {
                try
                {
                    DeleteOldImage(itemToDelete.ImageUrl);
                    db.Features.Remove(itemToDelete);
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
                TempData["ErrorMessage"] = "Silinecek feature bulunamadı.";
            }

            TempData["SuccessMessage"] = "Feature silindi.";
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
﻿using PagedList;
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
    public class ProductController : Controller
    {
        private readonly YummyContext db = new YummyContext();

        public ActionResult Index(int? page)
        {
            int pageSize = 5; 
            int pageNumber = (page ?? 1);

            var products = db.Products.OrderBy(p => p.ProductId).ToPagedList(pageNumber, pageSize);
            return View(products);
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product model, HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                var imagePath = SaveImage(imageFile, "products/");
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
                    db.Products.Add(model);
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


        public ActionResult UpdateProduct(int id)
        {
            var value = db.Products.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", value.CategoryId);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product model, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var itemToUpdate = db.Products.Find(model.ProductId);
                if (itemToUpdate == null)
                {
                    ModelState.AddModelError("", "Güncellenecek kayıt bulunamadı.");
                    ViewBag.Error = "Güncellenecek kayıt bulunamadı.";
                    return View(model);
                }

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    DeleteOldImage(itemToUpdate.ImageUrl);

                    var imagePath = SaveImage(imageFile, "products/");
                    if (imagePath == null)
                    {
                        ModelState.AddModelError("ImageUrl", "Sadece .jpg, .jpeg, .png veya .gif dosyaları yükleyebilirsiniz.");
                        ViewBag.Error = "Görsel yüklenemedi. Lütfen geçerli bir dosya formatı seçin.";
                        return View(model);
                    }

                    model.ImageUrl = imagePath;
                }
                else
                {
                    model.ImageUrl = itemToUpdate.ImageUrl;
                }

                db.Entry(itemToUpdate).CurrentValues.SetValues(model);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Kayıt güncellendi.";
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", model.CategoryId);
            ViewBag.Error = "Lütfen tüm alanları doğru şekilde doldurun.";
            return View(model);
        }

        public ActionResult DeleteProduct(int id)
        {
            var itemToDelete = db.Products.Find(id);
            if (itemToDelete != null)
            {
                try
                {
                    DeleteOldImage(itemToDelete.ImageUrl);
                    db.Products.Remove(itemToDelete);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;
using System.Data.Entity;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly YummyContext db = new YummyContext();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult DefaultFeature()
        {
            var values = db.Features.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultAbout()
        {
            var values = db.Abouts.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultProduct()
        {
            var values = db.Categories.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultWhyUs()
        {
            return PartialView();
        }

        public PartialViewResult DefaultTestimonial()
        {
            var values = db.Testimonials.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultEvent()
        {
            var values = db.Events.ToList();
            return PartialView(values);
        }
        public PartialViewResult DefaultChef()
        {
            var values = db.Chefs.Include(c => c.ChefSocials).ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultBooking()
        {
            ViewBag.Message = TempData["Booking"];
            return PartialView();
        }

        [HttpPost]
        public ActionResult BookTable(Booking booking)
        {
            booking.IsApproved = false;
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();

                TempData["Booking"] = "Rezervasyon talebiniz tarafımıza ulaştı. Rezervasyonunuz tarafımızdan onaylandığında eposta ile bilgilendirileceksiniz";
                return RedirectToAction("Index", booking);
            }

            return View("Index", booking);
        }

        public PartialViewResult DefaultGallery()
        {
            var values = db.PhotoGalleries.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultContact()
        {
            var values = db.ContactInfos.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultMessage()
        {
            ViewBag.Message = TempData["Message"];
            return PartialView();
        }

        [HttpPost]
        public ActionResult SendMessage(Message message)
        {
            message.IsRead = false;
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();

                TempData["Message"] = "Mesajınız gönderildi";
                return RedirectToAction("Index", message);
            }

            return RedirectToAction("Index", message);
        }

    }
}
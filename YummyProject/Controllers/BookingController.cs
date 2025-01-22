using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;

namespace YummyProject.Controllers
{
    public class BookingController : Controller
    {
        private readonly YummyContext db = new YummyContext();
        public ActionResult PendingReservations()
        {
            var values = db.Bookings.Where(x => x.IsApproved == false).ToList();
            return View(values);
        }

        public ActionResult ApprovedReservations()
        {
            var values = db.Bookings.Where(x => x.IsApproved == true).ToList();
            return View(values);
        }

        public ActionResult ApproveReservation(int id)
        {
            var booking = db.Bookings.Find(id);
            booking.IsApproved = true;
            db.SaveChanges();
            TempData["SuccessMessage"] = "Rezervasyon başarılı şekilde onaylandı!";
            return RedirectToAction("PendingReservations");
        }

    }
}
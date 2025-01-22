using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyProject.Context;

namespace YummyProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly YummyContext db = new YummyContext();
        // GET: Message
        public ActionResult UnreadMessages()
        {
            var values = db.Messages.Where(x => x.IsRead == false).ToList();
            return View(values);
        }

        public ActionResult ReadMessages()
        {
            var values = db.Messages.Where(x => x.IsRead == true).ToList();
            return View(values);
        }

        public ActionResult MarkAsRead(int id)
        {
            var message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            message.IsRead = true;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Mesaj başarıyla okundu olarak işaretlendi!";
            return RedirectToAction("UnreadMessages");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YummyProject.Context;
using YummyProject.Models;

namespace YummyProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        YummyContext db = new YummyContext();
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Admin model, string returnUrl)
        {
            var admin = db.Admins.FirstOrDefault(x=> x.Username == model.Username && x.Password == model.Password);
            if (admin == null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya Şifre hatalı");
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(admin.Username, false);
            Session["currentUser"] = admin.Username;

            if (returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index","DashBoard");
        }
    }
}
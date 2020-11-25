using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.Security;

namespace WebApplication1.Areas.admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: admin/Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Alogin(kullanicilar kullaniciFormu, string ReturnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View("index", kullaniciFormu);
            }

            string sifre1 = Sifrele.MD5Olustur(kullaniciFormu.sifre);

            using (mobilyaEntities db = new mobilyaEntities())
            {
                var kullaniciVarmi = db.kullanicilar.FirstOrDefault(x => x.kad == kullaniciFormu.kad && x.sifre == sifre1);

                if (kullaniciVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullaniciVarmi.kad, kullaniciFormu.BeniHatirla);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session.Add("kad", kullaniciVarmi.kad);
                        return RedirectToAction("/index", "urunler");
                    }


                }

                ViewBag.Hata = "!!Kullanıcı adı veya şifre hatalı!!";
                return View("index");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index");
        }
    }
}
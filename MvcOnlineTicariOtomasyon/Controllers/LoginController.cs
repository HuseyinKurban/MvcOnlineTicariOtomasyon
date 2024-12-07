using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Context c=new Context();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PartialRegister()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult PartialRegister(Cariler p)
        {
            p.Durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return PartialView();
        }
        [HttpGet]
        public ActionResult PartialCariLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PartialCariLogin(Cariler p)
        {
           var bilgiler=c.Carilers.FirstOrDefault(x=>x.CariMail==p.CariMail && x.CariSifre==p.CariSifre);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"] = bilgiler.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();

        public ActionResult Index(string p)
        {
            var values = c.KargoDetays.Where(x => (p == null || x.TakipKodu.Contains(p))).ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {

            return View();
        }


        [HttpPost]
        public ActionResult YeniKargo(KargoDetay p)
        {
            string randomString = Guid.NewGuid().ToString().Substring(0, 10);
            p.TakipKodu = randomString;

            c.KargoDetays.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
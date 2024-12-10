using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c=new Context();

        public ActionResult Index()
        {
            var values=c.KargoDetays.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            List<SelectListItem> deger1 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd+" "+x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();
        }

        [HttpPost]
        public ActionResult YeniKargo(KargoDetay p)
        {
            c.KargoDetays.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
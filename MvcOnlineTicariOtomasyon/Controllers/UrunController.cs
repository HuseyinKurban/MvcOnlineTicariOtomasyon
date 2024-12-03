using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun

        Context c = new Context();

        public ActionResult Index()
        {
            var values = c.Uruns.Where(x => x.Durum == true).ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text=x.KategoriAd,
                                               Value=x.Kategoriid.ToString()
                                           } ).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            p.Durum = true;
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.Kategoriid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urundeger=c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }

        
        public ActionResult UrunGuncelle(Urun p)
        {
            var u = c.Uruns.Find(p.Urunid);
            u.UrunAd = p.UrunAd;
            u.Marka = p.Marka;
            u.Stok = p.Stok;
            u.AlisFiyat = p.AlisFiyat;
            u.SatisFiyat= p.SatisFiyat;
            u.Durum = p.Durum;
            u.UrunGorsel = p.UrunGorsel;
            u.Kategoriid = p.Kategoriid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
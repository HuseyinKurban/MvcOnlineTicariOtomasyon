using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();

        public ActionResult Index()
        {
            var values = c.SatisHarekets.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            ViewBag.cari = deger1;



            List<SelectListItem> deger2 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.personel = deger2;

            List<SelectListItem> deger3 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.Urunid.ToString()
                                           }).ToList();
            ViewBag.urun = deger3;

            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            decimal toplamt = 0;
            decimal fiyat = 0;
            int adet = 0;


            adet = s.Adet;

            var urun = c.Uruns.Where(x => x.Urunid == s.Urunid).FirstOrDefault();
            fiyat = urun.SatisFiyat;


            toplamt = adet * fiyat;
            s.ToplamTutar = toplamt;
            s.Fiyat = fiyat;
            s.Tarih = DateTime.Now;

            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.Cariid.ToString()
                                           }).ToList();
            ViewBag.cari = deger1;



            List<SelectListItem> deger2 = (from x in c.Personels.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.Personelid.ToString()
                                           }).ToList();
            ViewBag.personel = deger2;

            List<SelectListItem> deger3 = (from x in c.Uruns.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.Urunid.ToString()
                                           }).ToList();
            ViewBag.urun = deger3;

            var deger = c.SatisHarekets.Find(id);
            return View("SatisGetir", deger);
        }

        public ActionResult SatisGuncelle(SatisHareket p)
        {
            decimal toplamt = 0;
            decimal fiyat = 0;
            int adet = 0;

            adet = p.Adet;

            var urun = c.Uruns.Where(x => x.Urunid == p.Urunid).FirstOrDefault();
            fiyat = urun.SatisFiyat;

            toplamt = adet * fiyat;
            var u = c.SatisHarekets.Find(p.Satisid);

            u.ToplamTutar = toplamt;
            u.Fiyat = fiyat;
            u.Tarih = DateTime.Now;
            u.Adet = adet;
            u.Cariid = p.Cariid;
            u.Personelid = p.Personelid;
            u.Urunid = p.Urunid;
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SatisDetay(int id)
        {
            var degerler=c.SatisHarekets.Where(x=>x.Satisid==id).ToList();
            return View(degerler);
        }

    }
}
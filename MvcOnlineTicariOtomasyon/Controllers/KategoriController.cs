﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();

        public ActionResult Index(int sayfa=1)
        {
            var values = c.Kategoris.ToList().ToPagedList(sayfa,10);
            return View(values);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);  
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori=c.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }

        public ActionResult KategoriGuncelle(Kategori k)
        {
            var ktg=c.Kategoris.Find(k.Kategoriid);
            ktg.KategoriAd=k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Deneme()
        {
            sinif3 cs= new sinif3();
            cs.Kategoriler = new SelectList(c.Kategoris, "Kategoriid", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");
            return View(cs);
        }

        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.Kategoriid equals y.Kategoriid
                               where x.Kategori.Kategoriid == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.Urunid.ToString()
                               }).ToList();

            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}
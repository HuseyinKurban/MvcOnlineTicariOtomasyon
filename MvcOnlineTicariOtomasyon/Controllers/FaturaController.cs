﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();

        public ActionResult Index()
        {
            var values = c.Faturalars.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            //fatura sıra nonun en sondaki değerini seç
            var sirano = c.Faturalars.OrderByDescending(x => x.FaturaSıraNo).Select(x => x.FaturaSıraNo).FirstOrDefault();

            //sirano boş işe değer olarak 100000 den başlat
            if (string.IsNullOrEmpty(sirano))
            {
                sirano = "100000";
            }

            //sqlde char 6 olarak tutuldugu için gelen değeri convert et ve arttır
            int siranoInt = Convert.ToInt32(sirano);
            siranoInt++;

            //sonra tekrar stringe çevir 
            sirano = siranoInt.ToString();
            f.FaturaSıraNo = sirano;

            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir", fatura);
        }

        public ActionResult FaturaGuncelle(Faturalar f)
        {
            //fatura kalemdeki fatura id, faturaidden gelen idye eşit ise tutarlarını topla ve faturadaki toplama aktar
            var toplam = c.FaturaKalems.Where(x => x.Faturaid == f.Faturaid)
             .Select(y => y.Tutar)//burada tutarı seçiyoruz
           .DefaultIfEmpty(0) // eğer bişey yoksa 0 dönsün
            .Sum();//toplama


            var fat = c.Faturalars.Find(f.Faturaid);
            fat.FaturaSeriNo = f.FaturaSeriNo;
            fat.Tarih = f.Tarih;
            fat.VergiDairesi = f.VergiDairesi;
            fat.Saat = f.Saat;
            fat.TeslimEden = f.TeslimEden;
            fat.TeslimAlan = f.TeslimAlan;
            fat.Toplam = toplam;

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaDetay(int id)
        {
            var ad = c.Faturalars.Where(x => x.Faturaid == id).Select(x => x.TeslimAlan).FirstOrDefault();
            ViewBag.Musteri = ad;

            var fatura = c.Faturalars.Where(x => x.Faturaid == id).Select(y => y.Faturaid).FirstOrDefault();
            ViewBag.f = fatura;

            //tutar değeri yoksa eğer 0 yaz
            var ftoplam=c.FaturaKalems.Where(x=>x.Faturaid==id).Sum(y=>(decimal?)y.Tutar)??0;
            ViewBag.ftoplam = ftoplam.ToString("C2");

            var fkalem=c.FaturaKalems.Where(x => x.Faturaid == id).Count();
            //eger fkalemden gelen değer varsa onu yaz yoksa 0 yaz
            ViewBag.fkalem = fkalem > 0 ? fkalem : 0;

            var values = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(values);
        }


        [HttpGet]
        public ActionResult YeniKalem()
        {

            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem f,int id)
        {
            int miktars = 0;
            decimal tutars = 0;
            decimal birimfiyats = 0;

            miktars = f.Miktar;
            birimfiyats = f.BirimFiyat;

            tutars = miktars * birimfiyats;

            var fatura = c.Faturalars.Where(x => x.Faturaid == id).Select(y => y.Faturaid).FirstOrDefault();
            f.Faturaid = fatura;

            f.Tutar = tutars;
            c.FaturaKalems.Add(f);
            c.SaveChanges();
            return RedirectToAction("FaturaDetay", "Fatura", new { id = f.Faturaid });

        }

        public ActionResult Dinamik()
        {
            Sinif4 cs= new Sinif4();
            cs.deger1=c.Faturalars.ToList();
            cs.deger2=c.FaturaKalems.ToList();

            return View(cs);
        }
       
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo,DateTime Tarih,string VergiDairesi,string Saat ,string TeslimEden,string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            f.Toplam = decimal.Parse(Toplam);

            c.Faturalars.Add(f);

            foreach (var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.FaturaKalemid; 
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);

        }
    }
}
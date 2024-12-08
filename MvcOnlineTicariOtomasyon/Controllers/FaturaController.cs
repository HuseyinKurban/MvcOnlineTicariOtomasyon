using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
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

    }
}
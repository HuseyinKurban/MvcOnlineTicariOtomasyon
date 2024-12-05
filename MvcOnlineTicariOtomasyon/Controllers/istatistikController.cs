using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c=new Context();

        public ActionResult Index()
        {
            var deger1= c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 = c.Personels.Count().ToString();
            ViewBag.d3 = deger3;

            var deger4= c.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;

            var deger5 = c.Uruns.Sum(x=>x.Stok).ToString();
            ViewBag.d5 = deger5;

            var deger6 = c.Uruns.Select(x => x.Marka).Distinct().Count();
            ViewBag.d6 = deger6;

            var deger7 = c.Uruns.Count(x => x.Stok <= 50).ToString();
            ViewBag.d7 = deger7;

            var deger8 = c.Uruns.OrderByDescending(x => x.SatisFiyat).Select(y => y.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;

            var deger9 = c.Uruns.OrderBy(x => x.SatisFiyat).Select(y => y.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;

            //key burda seçilen alan yani marka
            var deger10 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y=>y.Key).FirstOrDefault();
            ViewBag.d10 = deger10;

            var deger11 = c.Uruns.Count(x => x.UrunAd=="Buzdolabı").ToString();
            ViewBag.d11 = deger11;

            var deger12 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString(); ;
            ViewBag.d12 = deger12;

            var deger13 = c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            var ad = c.Uruns.Where(x => x.Urunid == deger13).Select(y => y.UrunAd).FirstOrDefault();
            ViewBag.d13 = ad;

            //iç içe sorgu deger13 ün
            //var deger20 =c.Uruns.Where(u=>u.Urunid==(c.SatisHarekets.GroupBy(x=>x.Urunid).OrderByDescending(x=>x.Count()).Select(y=>y.Key).FirstOrDefault())).Select(x=>x.UrunAd).FirstOrDefault().ToString();
            //ViewBag.d20 = deger20;

            var deger14 = c.SatisHarekets.Sum(x=>x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;

            DateTime bugun = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x=>x.Tarih==bugun).ToString();
            ViewBag.d15 = deger15;

            var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => (decimal?)y.ToplamTutar) ?? 0;
            ViewBag.d16 = deger16;

            return View();
        }
    }
}
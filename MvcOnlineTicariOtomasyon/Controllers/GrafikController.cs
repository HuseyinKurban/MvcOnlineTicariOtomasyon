using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context c = new Context();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Index2()
        {
            var grafikciz = new Chart(width: 600, height: 600);
            grafikciz.AddTitle(text: "Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Beyaz Eşya", "Telefon", "Bilgisayar" }, yValues: new[] { 85, 78, 65 }).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();

            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(x => yvalue.Add(x.Stok));

            var grafik = new Chart(width: 1200, height: 1000).AddTitle("Stoklar").AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);

            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");

        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult Index5()
        {
            return View();
        }

        public List<sinif1> Urunlistesi()
        {


            List<sinif1> snf = new List<sinif1>();
            snf.Add(new sinif1()
            {
                urunad = "Bilgisayar",
                stok = 120
            });

            snf.Add(new sinif1()
            {
                urunad = "Beyaz Eşya",
                stok = 140
            });

            snf.Add(new sinif1()
            {
                urunad = "Mobilya",
                stok = 70
            });

            snf.Add(new sinif1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 180
            });

            snf.Add(new sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 90
            });

            return snf;
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet);
        }

        public List<Sinif2> Urunlistesi2()
        {

            List<Sinif2> snf = new List<Sinif2>();
            using (var context = new Context())
            {
                snf = c.Uruns.Select(x => new Sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }

                return snf;
        }

        public ActionResult VisualizeUrunResult2()
        {
            return Json(Urunlistesi2(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index6()
        {
            return View();
        }

        public ActionResult Index7()
        {
            return View();
        }
    }
}
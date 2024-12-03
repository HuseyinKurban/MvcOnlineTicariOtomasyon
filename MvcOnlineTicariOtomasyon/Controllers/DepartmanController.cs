using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();

        public ActionResult Index(int sayfa = 1)
        {
            var values = c.Departmans.Where(x => x.Durum == true).ToList().ToPagedList(sayfa, 10);
            return View(values);
        }

        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            d.Durum = true;
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            var departman = c.Departmans.Find(id);
            departman.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanGetir(int id)
        {
            var dpt = c.Departmans.Find(id);
            return View("DepartmanGetir", dpt);
        }

        public ActionResult DepartmanGuncelle(Departman d)
        {
            var p = c.Departmans.Find(d.Departmanid);
            p.DepartmanAd = d.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int id)
        {
            var values = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.DepartmanAd = values;
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
            return View(degerler);
        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            var values = c.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            var values1 = c.Personels.Where(x => x.Personelid == id).Select(y => y.Departman.DepartmanAd).FirstOrDefault();
            ViewBag.PersonelAdSoyad = values;
            ViewBag.DepartmanPersonel = values1;
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            return View(degerler);

        }

    }
}
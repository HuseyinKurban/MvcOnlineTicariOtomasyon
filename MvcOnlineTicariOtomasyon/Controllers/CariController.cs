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
    public class CariController : Controller
    {
        // GET: Cari
        Context c=new Context();

        public ActionResult Index(int sayfa=1)
        {
            var degerler=c.Carilers.Where(x=>x.Durum==true).ToList().ToPagedList(sayfa, 10);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler p)
        {
            p.Durum = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult CariSil(int id)
        {
            var values = c.Carilers.Find(id);
            values.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
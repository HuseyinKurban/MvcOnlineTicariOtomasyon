using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c=new Context();
        
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.m = mail;
            return View(degerler);
        }

        [HttpPost]
        public ActionResult Index2(Cariler p)
        {
           
            var cari = c.Carilers.Where(x => x.Cariid == p.Cariid).FirstOrDefault();
            cari.CariAd=p.CariAd;
            cari.CariSoyad=p.CariSoyad;
            cari.CariSehir=p.CariSehir;
            cari.CariMail=p.CariMail;
            cari.CariSifre =p.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();

            var siparis=c.SatisHarekets.Where(x=>x.Cariid==id).ToList();

            return View(siparis);
        }
    }
}
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{

    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();

        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.Where(x => x.CariMail == mail).ToList();
            ViewBag.m = mail;

            var mailid=c.Carilers.Where(x=>x.CariMail==mail).Select(x=>x.Cariid).FirstOrDefault();

            var toplamsatis=c.SatisHarekets.Where(x=>x.Cariid==mailid).Count();
            ViewBag.Toplamsatis=toplamsatis;

            var toplamfiyat= c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => (decimal?)y.ToplamTutar) ?? 0;
            ViewBag.Toplamfiyat = toplamfiyat.ToString("C2");

            return View(degerler);
        }

        [HttpPost]
        public ActionResult Index2(Cariler p)
        {

            var cari = c.Carilers.Where(x => x.Cariid == p.Cariid).FirstOrDefault();
            cari.CariAd = p.CariAd;
            cari.CariSoyad = p.CariSoyad;
            cari.CariSehir = p.CariSehir;
            cari.CariMail = p.CariMail;
            cari.CariSifre = p.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();

            var siparis = c.SatisHarekets.Where(x => x.Cariid == id).ToList();

            return View(siparis);
        }

        public ActionResult GelenMesajlar()
        {

            var mail = (string)Session["CariMail"];

            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.Alici = gelensayisi;

            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.Gonderici = gidensayisi;

            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x=>x.Mesajid).ToList();    
            return View(mesajlar);
        }

        public ActionResult GidenMesajlar()
        {

            var mail = (string)Session["CariMail"];

            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.Alici = gelensayisi;

            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.Gonderici = gidensayisi;

            var mesajlar = c.Mesajlars.Where(x => x.Gönderici == mail).OrderByDescending(x => x.Mesajid).ToList();
            return View(mesajlar);
        }

        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];

            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.Alici = gelensayisi;

            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.Gonderici = gidensayisi;

            var mesaj=c.Mesajlars.Where(x=>x.Mesajid==id).ToList();

            return View(mesaj);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {

            var mail = (string)Session["CariMail"];

            var gelensayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.Alici = gelensayisi;

            var gidensayisi = c.Mesajlars.Count(x => x.Gönderici == mail).ToString();
            ViewBag.Gonderici = gidensayisi;

            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar p)
        {
            var mail = (string)Session["CariMail"];
            p.Gönderici = mail;
            p.Tarih=DateTime.Now;
            c.Mesajlars.Add(p);
            c.SaveChanges();
            return RedirectToAction("GidenMesajlar");
        }

        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));
            return View(k.ToList());
        }

        public ActionResult KargoDetay(string id)
        {
            ViewBag.Cari = c.KargoDetays.Where(x => x.TakipKodu == id).Select(x => x.Alici).FirstOrDefault();
            var values = c.KargoTakips.Where(x => x.Takipkodu == id).ToList();
            return View(values);
        }

        public ActionResult LogOut()
        {
            //oturumdan cık
            FormsAuthentication.SignOut();
            //istekleri terket
            Session.Abandon();
            //beni logine geri yönlendir
            return RedirectToAction("Index","Login");
        }


    }
}
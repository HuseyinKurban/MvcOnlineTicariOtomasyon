using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.IO;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c=new Context();
        
        public ActionResult Index(int sayfa=1)
        {
            var values=c.Personels.ToList().ToPagedList(sayfa,10);
            return View(values);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            //isteklerim arasında bir dosya mevcutsa
            if(Request.Files.Count>0)
            {
                //dosya adini bu şekilde aldık
                string dosyaadi=Path.GetFileName(Request.Files[0].FileName);
                //dosya uzantisini aldık
                string uzanti=Path.GetExtension(Request.Files[0].FileName);

                string yol = "~/Image/" + dosyaadi + uzanti;
                //almıs oldugumuz resmi klasör içine atıyoruz
                //sunucu içerisindeki maplenen yol değişkenine
                Request.Files[0].SaveAs(Server.MapPath(yol));

                //veritabanına kaydetmek için
                p.PersonelGorsel="/Image/"+dosyaadi+uzanti;
            }
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var prs = c.Personels.Find(id);
            return View("PersonelGetir",prs);
        }

        public ActionResult PersonelGuncelle(Personel p)
        {
            //isteklerim arasında bir dosya mevcutsa
            if (Request.Files.Count > 0)
            {
                //dosya adini bu şekilde aldık
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                //dosya uzantisini aldık
                string uzanti = Path.GetExtension(Request.Files[0].FileName);

                string yol = "~/Image/" + dosyaadi + uzanti;
                //almıs oldugumuz resmi klasör içine atıyoruz
                //sunucu içerisindeki maplenen yol değişkenine
                Request.Files[0].SaveAs(Server.MapPath(yol));

                //veritabanına kaydetmek için
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }

            var t=c.Personels.Find(p.Personelid);
            t.PersonelAd=p.PersonelAd;
            t.PersonelSoyad=p.PersonelSoyad;
            t.PersonelGorsel=p.PersonelGorsel;
            t.Departmanid=p.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListe(int sayfa=1)
        {
            var values = c.Personels.ToList().ToPagedList(sayfa, 9);
            return View(values);
        }
    }
}
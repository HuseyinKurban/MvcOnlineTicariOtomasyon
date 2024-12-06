using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c=new Context();
        UrunDetayList cs=new UrunDetayList();

        public ActionResult Index(int id)
        {
            cs.Deger1=c.Uruns.Where(x=>x.Urunid==id).ToList();
            cs.Deger2=c.UDetays.Where(x=>x.UDetayid==id).ToList();
           
            return View(cs);
        }
    }
}
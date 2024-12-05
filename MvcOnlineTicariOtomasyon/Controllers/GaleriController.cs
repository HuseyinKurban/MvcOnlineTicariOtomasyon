using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context c=new Context();

        public ActionResult Index()
        {
            var values=c.Uruns.ToList();
            return View(values);
        }
    }
}
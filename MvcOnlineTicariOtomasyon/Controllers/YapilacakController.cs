﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        // GET: Yapilacak
        Context c=new Context();

        public ActionResult Index()
        {
            var deger1=c.Carilers.Count().ToString();
            ViewBag.d1=deger1;

            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 = c.Faturalars.Count().ToString();
            ViewBag.d3 = deger3;

            var deger4 = c.FaturaKalems.Count().ToString();
            ViewBag.d4 = deger4;


            var deger=c.Yapilacaks.ToList();
            return View(deger);
        }

    }
}
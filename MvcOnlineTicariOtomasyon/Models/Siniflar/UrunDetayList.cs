using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class UrunDetayList
    {
        public IEnumerable<Urun> Deger1 { get; set; }
        public IEnumerable<UDetay> Deger2 { get; set; }

    }
}
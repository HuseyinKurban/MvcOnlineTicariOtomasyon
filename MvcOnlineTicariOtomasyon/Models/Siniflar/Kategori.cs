using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Kategori
    {
        [Key]
        public int Kategoriid { get; set; }
        public string KategoriAd { get; set; }
        //Kategoride birden fazla ürün yer alabilir
        public ICollection<Urun> Uruns { get; set; }
    }
}
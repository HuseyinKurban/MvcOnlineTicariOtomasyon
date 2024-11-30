using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        public int Satisid { get; set; }
        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }

        //ürün
        public  ICollection<Urun> Uruns { get; set; }
        //cari
        public ICollection<Cariler> Carilers { get; set; }
        //personel
        public ICollection<Personel> Personels { get; set; }

        
    }
}
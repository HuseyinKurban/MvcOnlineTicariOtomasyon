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
        public  Urun Urun { get; set; }
        //cari
        public Cariler Cariler { get; set; }
        //personel
        public Personel Personel { get; set; }
   
    }
}
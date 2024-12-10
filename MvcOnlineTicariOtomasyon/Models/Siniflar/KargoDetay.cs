using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class KargoDetay
    {
        [Key]
        public int KargoDetayid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string Aciklama { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string TakipKodu { get; set; }

        public DateTime Tarih { get; set; }

        public int Personelid { get; set; }
        public virtual Personel Personel { get; set; }

        public int Cariid { get; set; }
        public virtual Cariler Cari { get; set; }

    }
}
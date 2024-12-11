using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Mesajlar
    {
        [Key]
        public int Mesajid { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(100)]
        public string Gönderici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Alici { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        public string Konu { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(2000)]
        public string icerik { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime Tarih { get; set; }
    }
}
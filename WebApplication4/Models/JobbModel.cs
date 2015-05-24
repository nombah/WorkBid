using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebApplication4.Models
{
    public class Jobb
    {
        public int ID { get; set; }
        public string Titel { get; set;}
        public string Ort { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }
        public double Pris { get; set; }
        public virtual List<CommentModel> Comments { get; set; }
        public virtual List<ButModel> Bud { get; set; }
        public string Image { get; set; }
    }
}
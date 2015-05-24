using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class ButModel
    {
        public int butModelId { get; set; }
        [Display(Name = "Bud")]
        public string But { get; set; }

        [Display(Name = "Jobb")]
        public int? jobbId { get; set; }
        [Display(Name = "Jobb")]
        public virtual Jobb jobb { get; set; }       
        public int? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
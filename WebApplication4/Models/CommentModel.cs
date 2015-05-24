using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class CommentModel
    {
        public int CommentModelId { get; set; }
        public string Comment { get; set; }

        public int? jobbId { get; set; }
        public virtual Jobb jobb { get; set; }

        public int? UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
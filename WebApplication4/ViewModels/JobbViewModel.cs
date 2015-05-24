using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication4.ViewModels
{
    public class JobbViewModel
    {
       public List<Jobb> Jobbs { get; set; }
       public ApplicationUser User { get; set; }
       public List<ApplicationUser> Users { get; set; }
    }
}
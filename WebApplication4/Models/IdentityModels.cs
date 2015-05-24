using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using WebApplication4.Controllers;

namespace WebApplication4.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    { 
        public string Information { get; set; }
        public int PhonenNumber { get; set; }
        public string Adress { get; set; }
        public string Role { get; set; }
        public List<CommentModel> Comment { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {         
        }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<Jobb> Jobb { get; set; }
        public DbSet<ButModel> Bud { get; set; }
    }
}
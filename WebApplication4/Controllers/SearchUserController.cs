using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using WebApplication4.ViewModels;


namespace WebApplication4.Controllers
{

    public class SearchUserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /SearchUser/
        public ActionResult Index(string SearchUser)
        {
            var JobbViewModel = new JobbViewModel();
            
            var UserResult = from m in db.Users
                             select m;
            if (!String.IsNullOrEmpty(SearchUser))
            {
                UserResult = UserResult.Where(s => s.UserName.Contains(SearchUser));
            }
            JobbViewModel.Users = UserResult.ToList();
            return View(JobbViewModel);
        }
	}
}
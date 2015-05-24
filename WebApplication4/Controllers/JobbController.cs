using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using WebApplication4.ViewModels;
using System.IO;
namespace WebApplication4.Controllers
{

    public class JobbController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Jobb/
        public ActionResult Index(string searchString)
        {
            var jobb = from m in db.Jobb
                         select m;          
            if (!String.IsNullOrEmpty(searchString))
            {
                jobb = jobb.Where(s => s.Titel.Contains(searchString));
            }        
            var user = db.Users.Find(User.Identity.GetUserId());
            var JobbViewModel = new JobbViewModel();
            JobbViewModel.Jobbs = jobb.ToList();
            JobbViewModel.User = user;
            
            return View(JobbViewModel);
        }
        // GET: /Jobb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobb jobb = db.Jobb.Find(id);
            if (jobb == null)
            {
                return HttpNotFound();
            }
            return View(jobb);
        }

        // GET: /Jobb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Jobb/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Titel,Ort,Datum,Pris")] Jobb jobb)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                        file.SaveAs(path);
                        jobb.Image = "/Content/Images/" + fileName;
                    }
                }
                var userprofile = db.Users.Find(User.Identity.GetUserId());
                jobb.User = userprofile;
                db.Jobb.Add(jobb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobb);
        }

        // GET: /Jobb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobb jobb = db.Jobb.Find(id);
            if (jobb == null)
            {
                return HttpNotFound();
            }
            return View(jobb);
        }

        // POST: /Jobb/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Titel,Ort,Datum,Pris")] Jobb jobb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobb);
        }

        // GET: /Jobb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobb jobb = db.Jobb.Find(id);
            if (jobb == null)
            {
                return HttpNotFound();
            }
            return View(jobb);
        }

        // POST: /Jobb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobb jobb = db.Jobb.Find(id);
            db.Jobb.Remove(jobb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddComment()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

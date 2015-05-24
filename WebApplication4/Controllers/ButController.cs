using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication4.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication4.Controllers
{
    public class ButController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /But/
        public ActionResult Index()
        {
            var bud = db.Bud.Include(b => b.jobb);
            return View(bud.ToList());
        }

        // GET: /But/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ButModel butmodel = db.Bud.Find(id);
            if (butmodel == null)
            {
                return HttpNotFound();
            }
            return View(butmodel);
        }

        // GET: /But/Create
        public ActionResult Create()
        {
            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel");
            return View();
        }

        // POST: /But/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="butModelId,But,jobbId,UserId")] ButModel butmodel)
        {
            if (ModelState.IsValid)
            {               
                var userprofile = db.Users.Find(User.Identity.GetUserId());
                butmodel.User = userprofile;
                db.Bud.Add(butmodel);
                db.SaveChanges();
                return RedirectToAction("Index","jobb");
            }

            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel", butmodel.jobbId);
            return View(butmodel); 
 
      
        }

        // GET: /But/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ButModel butmodel = db.Bud.Find(id);
            if (butmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel", butmodel.jobbId);
            return View(butmodel);
        }

        // POST: /But/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="butModelId,But,jobbId,UserId")] ButModel butmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(butmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel", butmodel.jobbId);
            return View(butmodel);
        }

        // GET: /But/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ButModel butmodel = db.Bud.Find(id);
            if (butmodel == null)
            {
                return HttpNotFound();
            }
            return View(butmodel);
        }

        // POST: /But/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ButModel butmodel = db.Bud.Find(id);
            db.Bud.Remove(butmodel);
            db.SaveChanges();
            return RedirectToAction("Index");
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

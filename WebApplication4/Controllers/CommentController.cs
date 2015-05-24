using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication4.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Comment/
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.jobb);
            return View(comments.ToList());
        }

        // GET: /Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.Comments.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        // GET: /Comment/Create
        public ActionResult Create()
        {
            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel");
            return View();
        }

        // POST: /Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CommentModelId,Comment,jobbId,UserId")] CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                var userprofile = db.Users.Find(User.Identity.GetUserId());
                commentmodel.User = userprofile;
                db.Comments.Add(commentmodel);
                db.SaveChanges();
                return RedirectToAction("Index","Jobb");
            }

            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel", commentmodel.jobbId);
            return View(commentmodel);
        }

        // GET: /Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.Comments.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel", commentmodel.jobbId);
            return View(commentmodel);
        }

        // POST: /Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CommentModelId,Comment,jobbId,UserId")] CommentModel commentmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jobbId = new SelectList(db.Jobb, "ID", "Titel", commentmodel.jobbId);
            return View(commentmodel);
        }

        // GET: /Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentmodel = db.Comments.Find(id);
            if (commentmodel == null)
            {
                return HttpNotFound();
            }
            return View(commentmodel);
        }

        // POST: /Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentmodel = db.Comments.Find(id);
            db.Comments.Remove(commentmodel);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormGenerator.Models;
using SelectList = System.Web.Mvc.SelectList;

namespace FormGenerator.Controllers
{
    public class UserSubmissionsController : Controller
    {
        private FormGeneratorEntities db = new FormGeneratorEntities();

        // GET: UserSubmissions
        public ActionResult Index(int id)
        {
            var userSubmissions = db.UserSubmissions.Where(u => u.FormId==id);
            return View(userSubmissions.ToList());
        }

        // GET: UserSubmissions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmission userSubmission = db.UserSubmissions.Find(id);
            if (userSubmission == null)
            {
                return HttpNotFound();
            }
            return View(userSubmission);
        }

        // GET: UserSubmissions/Create
        public ActionResult Create()
        {
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name");
            return View();
        }

        // POST: UserSubmissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SubmissionTime,FormId")] UserSubmission userSubmission)
        {
            if (ModelState.IsValid)
            {
                db.UserSubmissions.Add(userSubmission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", userSubmission.FormId);
            return View(userSubmission);
        }

        // GET: UserSubmissions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmission userSubmission = db.UserSubmissions.Find(id);
            if (userSubmission == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", userSubmission.FormId);
            return View(userSubmission);
        }

        // POST: UserSubmissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SubmissionTime,FormId")] UserSubmission userSubmission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSubmission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", userSubmission.FormId);
            return View(userSubmission);
        }

        // GET: UserSubmissions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmission userSubmission = db.UserSubmissions.Find(id);
            if (userSubmission == null)
            {
                return HttpNotFound();
            }
            return View(userSubmission);
        }

        // POST: UserSubmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            UserSubmission userSubmission = db.UserSubmissions.Find(id);
            db.UserSubmissions.Remove(userSubmission);
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

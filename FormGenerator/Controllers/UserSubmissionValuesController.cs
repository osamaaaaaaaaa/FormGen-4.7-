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
    [Authorize]
    public class UserSubmissionValuesController : Controller
    {
        private FormGeneratorEntities db = new FormGeneratorEntities();

        // GET: UserSubmissionValues
        public ActionResult Index(int id)
        {
            var userSubmissionValues = db.UserSubmissionValues.Where(u => u.SubmissionId == id);
            return View(userSubmissionValues.ToList());
        }

        // GET: UserSubmissionValues/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmissionValue userSubmissionValue = db.UserSubmissionValues.Find(id);
            if (userSubmissionValue == null)
            {
                return HttpNotFound();
            }
            return View(userSubmissionValue);
        }

        // GET: UserSubmissionValues/Create
        public ActionResult Create()
        {
            ViewBag.FieldId = new SelectList(db.FormFields, "Id", "Caption");
            ViewBag.SubmissionId = new SelectList(db.UserSubmissions, "Id", "Id");
            return View();
        }
        public static String GetFieldCaption(long id)
        {
            FormGeneratorEntities db = new FormGeneratorEntities();
            FormField formField = db.FormFields.Where(x => x.Id == id).First();
            return formField.Caption;
        }

            // POST: UserSubmissionValues/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubmissionId,FieldId,SubmittedValue")] UserSubmissionValue userSubmissionValue)
        {
            if (ModelState.IsValid)
            {
                db.UserSubmissionValues.Add(userSubmissionValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldId = new SelectList(db.FormFields, "Id", "Caption", userSubmissionValue.FieldId);
            ViewBag.SubmissionId = new SelectList(db.UserSubmissions, "Id", "Id", userSubmissionValue.SubmissionId);
            return View(userSubmissionValue);
        }

        // GET: UserSubmissionValues/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmissionValue userSubmissionValue = db.UserSubmissionValues.Find(id);
            if (userSubmissionValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldId = new SelectList(db.FormFields, "Id", "Caption", userSubmissionValue.FieldId);
            ViewBag.SubmissionId = new SelectList(db.UserSubmissions, "Id", "Id", userSubmissionValue.SubmissionId);
            return View(userSubmissionValue);
        }

        // POST: UserSubmissionValues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubmissionId,FieldId,SubmittedValue")] UserSubmissionValue userSubmissionValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSubmissionValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FieldId = new SelectList(db.FormFields, "Id", "Caption", userSubmissionValue.FieldId);
            ViewBag.SubmissionId = new SelectList(db.UserSubmissions, "Id", "Id", userSubmissionValue.SubmissionId);
            return View(userSubmissionValue);
        }

        // GET: UserSubmissionValues/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSubmissionValue userSubmissionValue = db.UserSubmissionValues.Find(id);
            if (userSubmissionValue == null)
            {
                return HttpNotFound();
            }
            return View(userSubmissionValue);
        }

        // POST: UserSubmissionValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            UserSubmissionValue userSubmissionValue = db.UserSubmissionValues.Find(id);
            db.UserSubmissionValues.Remove(userSubmissionValue);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormGenerator.Models;
using SelectList = FormGenerator.Models.SelectList;

namespace FormGenerator.Controllers
{
    public class SelectListsController : Controller
    {
        private FormGeneratorEntities db = new FormGeneratorEntities();

        // GET: SelectLists
        public ActionResult Index()
        {
            return View(db.SelectLists.ToList());
        }

        // GET: SelectLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectList selectList = db.SelectLists.Find(id);
            if (selectList == null)
            {
                return HttpNotFound();
            }
            return View(selectList);
        }

        // GET: SelectLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SelectLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] SelectList selectList)
        {
            if (ModelState.IsValid)
            {
                db.SelectLists.Add(selectList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(selectList);
        }

        // GET: SelectLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectList selectList = db.SelectLists.Find(id);
            if (selectList == null)
            {
                return HttpNotFound();
            }
            return View(selectList);
        }

        // POST: SelectLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] SelectList selectList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(selectList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(selectList);
        }

        // GET: SelectLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectList selectList = db.SelectLists.Find(id);
            if (selectList == null)
            {
                return HttpNotFound();
            }
            return View(selectList);
        }

        // POST: SelectLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SelectList selectList = db.SelectLists.Find(id);
            db.SelectLists.Remove(selectList);
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

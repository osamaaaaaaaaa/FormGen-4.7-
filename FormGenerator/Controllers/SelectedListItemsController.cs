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
    public class SelectedListItemsController : Controller
    {
        private FormGeneratorEntities db = new FormGeneratorEntities();

        // GET: SelectedListItems
        public ActionResult Index()
        {
            var selectedListItems = db.SelectedListItems.Include(s => s.SelectList);
            return View(selectedListItems.ToList());
        }

        // GET: SelectedListItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectedListItem selectedListItem = db.SelectedListItems.Find(id);
            if (selectedListItem == null)
            {
                return HttpNotFound();
            }
            return View(selectedListItem);
        }

        // GET: SelectedListItems/Create
        public ActionResult Create()
        {
            ViewBag.ListId = new SelectList(db.SelectLists, "Id", "Name");
            return View();
        }

        // POST: SelectedListItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,IsActive,ListId,Name")] SelectedListItem selectedListItem)
        {
            if (ModelState.IsValid)
            {
                db.SelectedListItems.Add(selectedListItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListId = new SelectList(db.SelectLists, "Id", "Name", selectedListItem.ListId);
            return View(selectedListItem);
        }

        // GET: SelectedListItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectedListItem selectedListItem = db.SelectedListItems.Find(id);
            if (selectedListItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListId = new SelectList(db.SelectLists, "Id", "Name", selectedListItem.ListId);
            return View(selectedListItem);
        }

        // POST: SelectedListItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,IsActive,ListId,Name")] SelectedListItem selectedListItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(selectedListItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListId = new SelectList(db.SelectLists, "Id", "Name", selectedListItem.ListId);
            return View(selectedListItem);
        }

        // GET: SelectedListItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectedListItem selectedListItem = db.SelectedListItems.Find(id);
            if (selectedListItem == null)
            {
                return HttpNotFound();
            }
            return View(selectedListItem);
        }

        // POST: SelectedListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SelectedListItem selectedListItem = db.SelectedListItems.Find(id);
            db.SelectedListItems.Remove(selectedListItem);
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

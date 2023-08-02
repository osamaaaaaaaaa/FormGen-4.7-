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
    public class FormFieldsController : Controller
    {
        private FormGeneratorEntities db = new FormGeneratorEntities();

        // GET: FormFields
        public  ActionResult Index(int id)
        {

            ViewData["Formid"] = id;
            ViewData["FormName"] = db.Forms.Where(x => x.Id.Equals(id));
            ViewData["Selectedlist"] = new SelectList(db.SelectLists, "Id", "Name"); ;
            ViewData["Formlist"] = new SelectList(db.Forms.Where(x => x.Id == id), "Id", "Name");
            ViewData["FieldTypeId"] = new SelectList(db.FieldTypes, "Id", "Name");


            var formGeneratorContext = db.FormFields.Where(x => x.FormId == id);
            return View( formGeneratorContext.ToList());
        }

        [AllowAnonymous]
        public ActionResult UserView(int id)
        {
            var dataToPass = db.FormFields.Where(f => f.IsActive && f.FormId == id).OrderBy(f => f.FieldOrder);

            return PartialView(dataToPass);
        }

        [AllowAnonymous]
        public ActionResult UserSubmmitAction(List<String> Name, int FormId, List<int> FieldTypeId, [Bind(Include ="Id,SubmissionTime,FormId")] UserSubmission userSubmission)

        {
            userSubmission.SubmissionTime = DateTime.Now;
            userSubmission.FormId = FormId;

            db.UserSubmissions.Add(userSubmission);
            db.SaveChanges();
            var usersubId = userSubmission.Id;




            for (int i = 0; i < Name.Count; i++)
            {

                UserSubmissionValue userSubmissionValue = new UserSubmissionValue();
                userSubmissionValue.SubmissionId = usersubId;
                userSubmissionValue.FieldId = FieldTypeId[i];
                userSubmissionValue.SubmittedValue = Name[i];
                db.UserSubmissionValues.Add(userSubmissionValue);
                db.SaveChanges();

            }
            return PartialView("Thanks");

        }


        public ActionResult UserViewSubmission(int id)
        {
            var dataToPass = db.FormFields.Where(f => f.IsActive && f.FormId == id).OrderBy(f => f.FieldOrder);
            // ViewBag.FormTitle = dataToPass.FirstOrDefault().Form.Name;
            return View("~/Views/Home/test.cshtml");
        }


        // GET: FormFields/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormField formField = db.FormFields.Find(id);
            if (formField == null)
            {
                return HttpNotFound();
            }
            return View(formField);
        }

        // GET: FormFields/Create
        public ActionResult Create(int id)
        {
            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name");
            ViewBag.FormId = new SelectList(db.Forms.Where(m=>m.Id==id), "Id", "Name");
            ViewBag.SelectListId = new SelectList(db.SelectLists, "Id", "Name");
            return View();
        }

        // POST: FormFields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Caption,TextBefore,TextAfter,FieldTypeId,SelectListId,FieldOrder,FormId,IsActive,MaxValue,MinValue,MaxLength,MinLength,DefaultValue")] FormField formField)
        {
            db.FormFields.Add(formField);
            db.SaveChanges();
            return RedirectToAction("Index",new{id=formField.FormId });
            if (ModelState.IsValid)
            {
              
            }

            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name", formField.FieldTypeId);
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", formField.FormId);
            ViewBag.SelectListId = new SelectList(db.SelectLists, "Id", "Name", formField.SelectListId);
            return View(formField);
        }

        // GET: FormFields/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormField formField = db.FormFields.Find(id);
            if (formField == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name", formField.FieldTypeId);
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", formField.FormId);
            ViewBag.SelectListId = new SelectList(db.SelectLists, "Id", "Name", formField.SelectListId);
            return View(formField);
        }

        // POST: FormFields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Caption,TextBefore,TextAfter,FieldTypeId,SelectListId,FieldOrder,FormId,IsActive,MaxValue,MinValue,MaxLength,MinLength,DefaultValue")] FormField formField)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formField).State = EntityState.Modified;
                db.SaveChanges();
                    return RedirectToAction(nameof(Index), new { Id = formField.FormId });

            }
            ViewBag.FieldTypeId = new SelectList(db.FieldTypes, "Id", "Name", formField.FieldTypeId);
            ViewBag.FormId = new SelectList(db.Forms, "Id", "Name", formField.FormId);
            ViewBag.SelectListId = new SelectList(db.SelectLists, "Id", "Name", formField.SelectListId);
            return View(formField);
        }

        // GET: FormFields/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormField formField = db.FormFields.Find(id);
            if (formField == null)
            {
                return HttpNotFound();
            }
            return View(formField);
        }

        // POST: FormFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FormField formField = db.FormFields.Find(id);
            db.FormFields.Remove(formField);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = formField.FormId });

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

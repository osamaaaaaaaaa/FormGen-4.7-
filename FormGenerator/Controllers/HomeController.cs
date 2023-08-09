using FormGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace FormGenerator.Controllers
{
    public class HomeController : Controller
    {

        private FormGeneratorEntities db = new FormGeneratorEntities();

        public ActionResult Index()
        {
           ViewBag.formscount= db.Forms.ToList().Count;
            ViewBag.formssubcount = db.UserSubmissions.ToList().Count;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
 

            if (Membership.ValidateUser(model.UserName.ToString(), model.Password.ToString()))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
               
                return RedirectToAction("Index");
            
            }
       


            return View();

        }
      public ActionResult Signout() {
            FormsAuthentication.SignOut();
           
            return RedirectToAction("Index");

        }
    }
}
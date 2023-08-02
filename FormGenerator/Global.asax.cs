using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using FormGenerator.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace FormGenerator
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (Membership.FindUsersByName("Administrator").Count == 0)
                Membership.CreateUser("Administrator", "oM_12345678");
        }
    }
}

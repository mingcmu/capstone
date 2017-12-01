using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity; //data entity added
using Inspinia_MVC5_SeedProject.Models; //Models added

namespace Inspinia_MVC5_SeedProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Database.SetInitializer<NameOfDbContext>(new DropCreateDatabaseIfModelChanges<NameOfDbContext>());
            Database.SetInitializer<OurDBContext>(new DropCreateDatabaseIfModelChanges<OurDBContext>());
        }
    }
}

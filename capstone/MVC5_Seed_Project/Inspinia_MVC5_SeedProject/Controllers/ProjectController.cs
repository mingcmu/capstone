using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models; //using model

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.projectList.ToList());
            }
            //return View();
        }


        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Projects project)
        {
            if (ModelState.IsValid)
            {
                using (OurDBContext db = new OurDBContext())
                {
                    db.projectList.Add(project);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = project.ProjectName + "Project creation is done";
            }
            return View();
        }

    }
}

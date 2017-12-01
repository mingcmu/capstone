using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models; //using model

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [Authorize]
    public class AssessmentController : Controller
    {
        // GET: Assessment
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.assessmentList.ToList());
            }
            //return View();
        }

        // GET: Assessment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(Assessments assessment)
        {
            if (ModelState.IsValid)
            {
                using (OurDBContext db = new OurDBContext())
                {
                    db.assessmentList.Add(assessment);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = assessment.Name + "Assessment creation is done";
            }
            return View();
        }
    }
}
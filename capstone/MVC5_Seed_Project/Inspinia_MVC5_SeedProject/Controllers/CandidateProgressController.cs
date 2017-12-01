using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models; //using model

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class CandidateProgressController : Controller
    {
        // GET: CandidateProgress
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.candidateProgressList.ToList());
            }
            //return View();
        }
    }
}
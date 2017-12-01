using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models; //using model
using Inspinia_MVC5_SeedProject.Helpers;
using System.Threading.Tasks;
using System.Text;
using System;
using Inspinia_MVC5_SeedProject.Controllers.Requests;
using Newtonsoft.Json;

namespace Inspinia_MVC5_SeedProject.Controllers
{

    public class CandidateController : Controller
    {
        // GET: Candidate
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.candidateAccount.ToList());
            }
            //return View();
        }

        // GET: Candidate
        public ActionResult Dashboard()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.candidateAccount.ToList());
            }
            //return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        
        
        [HttpGet]
        public string TestTrackProgress() {
            
            Client client = new Client();
            string receiptId = "XXXX";
            
            RequestController rc = new RequestController();
            // ReturnType returnType = rc.AssessmentStatusRequest(client, receiptId);
            
            ReturnType returnType = new ReturnType
            {
                Status = "80"
            };
            
            Console.Write(Json(returnType));

            string json = JsonConvert.SerializeObject(returnType);
            return json;
        }

        
        [HttpPost]
        public ActionResult Register(Candidates candidate)
        {
            if (ModelState.IsValid)
            {
                // New workflow:
                // 1. Send XML request and check OK or Not, get urls info
                // 2. Store all info(candidatw) + urls
                // 3. Configure email content and send
                
                // 4. Need to this in batch register as well


                string id = candidate.UserID;
                //tianyi's code is filled here.
                //List<string> urls = new List<string>();
                //urls.Add("http1");
                //urls.Add("http2");
                //urls.Add("http3");
                
                // Sending XML request and retieve urls
                using (var db = new OurDBContext())
                {
                    var client = db.clientAccount.Find(candidate.Requestor);
                    if (client != null)
                    {
                        var request = new RequestController();
                        ReturnType returnType = request.AssessmentOrderRequest(client, candidate, "Test pool 1", "None call back");
                        db.candidateAccount.Add(candidate);                                             
                        db.candidateProgressList.Add(new CandidateProgress
                        {
                            ReciptID = returnType.ReceiptId,
                            UserID = id,
                            AssessmentURL = returnType.Url,
                            Progress = "0" //initialize progress as 0
                        });                   
                        db.SaveChanges();

                        //var progress = db.candidateProgressList.Last();
                        //Console.Write("URL: "+progress.AssessmentURL);
                        //send a registration email once changes are saved in the database
                        CustomEmail regMail = new CustomEmail();
                        StringBuilder mailMessage = new StringBuilder();
                        mailMessage.Append("Hi, " + candidate.FirstName + ":\n");
                        mailMessage.Append("Welcome to our platform! The followings are the assessments you need to take:\n");
                      
                            mailMessage.Append(returnType.Url + "\n");
                       
                        mailMessage.Append("Good Luck!");

                        regMail.sendMail(candidate.Email, "Select International Registration", mailMessage.ToString());

                        ModelState.Clear();
                        ViewBag.Message = candidate.FirstName + "" + candidate.LastName + "registration done";

                    }

                }

            }
            return View();
        }
        /*
        //Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        */
        [AllowAnonymous]
        public ActionResult Login(Candidates can)
        {
            using (OurDBContext db = new OurDBContext())
            {
                var usr = db.candidateAccount.Where(u => u.UserName == can.UserName && u.Password == can.Password).FirstOrDefault();
                System.Console.WriteLine("name: " + can.UserName);
                System.Console.WriteLine("pw: " + can.Password);
                
                if (usr != null)
                {
                    //Session["UserId"] = can.UserID.ToString();
                    Session["UserName"] = can.UserName.ToString();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Participant", "Home", new { area = "" });
                }
                else {
                    //ModelState.AddModelError("", "usr is null");
                }
            }

            return View();
        }
        

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            } else
            {
                return RedirectToAction("Login");
            }
        }
    }
}
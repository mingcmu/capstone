using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Assessments
    {
        [Key]
        public String AssessmentID { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }
        public string URL { get; set; }
    }
}
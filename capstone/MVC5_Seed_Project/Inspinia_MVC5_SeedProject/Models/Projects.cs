using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Projects
    {
        [Key]
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Assessment1 { get; set; }
        public string Assessment2 { get; set; }
        public string Assessment3 { get; set; }
        public string Assessment4 { get; set; }
        public string Assessment5 { get; set; }
    }
}
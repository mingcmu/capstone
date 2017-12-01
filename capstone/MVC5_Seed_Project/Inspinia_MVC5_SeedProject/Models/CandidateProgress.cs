using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class CandidateProgress
    { 
        [Key]
        public string ReciptID { get; set; }
        public string UserID { get; set; }
        public string AssessmentURL { get; set; }
        public string Progress { get; set; } 
    }
}
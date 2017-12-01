using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class UserProgress
    {
        public string UserId { get; set; }
        public string AssessmentURl { get; set; }
        public string Progress { get; set; } 
    }
}
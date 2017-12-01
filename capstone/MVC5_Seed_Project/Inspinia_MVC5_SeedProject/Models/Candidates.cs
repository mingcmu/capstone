using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Inspinia_MVC5_SeedProject.Models
{
    public class Candidates
    {   
        public string UserID { get; set; } //EmployeeNumber
        public string FirstName { get; set; } //LastName 
        public string LastName { get; set; } //FirstName
        [Key]
        public string UserName { get; set; } 
        public string Email { get; set; } //CandidateEmail
        public string Requestor { get; set; } //Requestor, // it means ClientCode @Clients Model
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        public string ProjectId { get; set; }
    }
}
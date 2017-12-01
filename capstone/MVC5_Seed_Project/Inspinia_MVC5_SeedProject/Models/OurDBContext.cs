using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; //data entity added

namespace Inspinia_MVC5_SeedProject.Models
{
    public class OurDBContext: DbContext //DbContext
    {
        public DbSet<Candidates> candidateAccount { get; set; }
        public DbSet<Client> clientAccount { get; set; }
        public DbSet<Projects> projectList { get; set; }
        public DbSet<Assessments> assessmentList { get; set; }
        public DbSet<CandidateProgress> candidateProgressList { get; set; }
    }
}
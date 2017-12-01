using System.ComponentModel.DataAnnotations;


namespace Inspinia_MVC5_SeedProject.Models
{
    public class Client
    {
        [Key]
        public string ClientCode { get; set; } //Requestor
        public string ProviderKey { get; set; }
        public string CustomerNumber { get; set; } 
        public string CallBackUri { get; set; }
    }
}
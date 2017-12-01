using System.Collections.Generic;

namespace Inspinia_MVC5_SeedProject.Controllers.Requests
{
    public class ReturnType
    {
        public bool ResponseStatus { get; set; }
        public string ReceiptId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string StatusDate { get; set; }
    }
}
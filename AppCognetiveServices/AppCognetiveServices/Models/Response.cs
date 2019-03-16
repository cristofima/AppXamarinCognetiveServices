using System.Collections.Generic;

namespace AppCognetiveServices.Models
{
    public class Response
    {
        public List<Document> documents { get; set; }
        public List<object> errors { get; set; }
    }
}
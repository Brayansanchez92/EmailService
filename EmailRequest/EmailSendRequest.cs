using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.EmailRequest
{
    public class EmailSendRequest 
    {
        [Required][MaxLength(255)]
        public string To { get; set; }
                    
        public string Subject { get; set; }
        
        public string PlainTextContent { get; set; }

        public string HtmlContent { get; set; }
        
    }  
}

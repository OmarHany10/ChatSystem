using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.DTOs
{
    public class RequestDTO
    {
        public string Username {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime SentDate { get; set; }
    }
}

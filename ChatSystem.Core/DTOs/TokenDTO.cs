using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSystem.Core.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string? Message { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}

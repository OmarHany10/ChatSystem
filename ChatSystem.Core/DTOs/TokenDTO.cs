using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatSystem.Core.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string? Message { get; set; }
        public DateTime ExpireDate { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiresOn { get; set; }
    }
}

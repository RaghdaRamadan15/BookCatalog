using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Dtos
{
    public class AuthResult
    {
       
        public string? Token { get; set; }  
        public DateTime? Expiration { get; set; }
        public List<string>? Errors { get; set; }
    }
}

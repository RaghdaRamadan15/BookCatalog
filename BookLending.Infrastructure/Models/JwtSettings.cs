using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Infrastructure.Models
{
    public class JwtSettings
    {
        public string SecritKey { get; set; }  
        public string IssuerIP { get; set; } 
        public string AudienceIP { get; set; } 
    }
}

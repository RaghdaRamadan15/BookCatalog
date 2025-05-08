using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Dtos
{
    public class SendingEmail
    {
        public string MemberEmail { get; set; }
        public string MemberName { get; set; }
        public string BookName { get; set; }


    }
}

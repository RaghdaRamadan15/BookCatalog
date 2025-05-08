using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Dtos
{
    public class BorrowingBook
    {
        public string BookName {  get; set; }
        public string UserName { get; set; }
        public string CaseBook {  get; set; }

    }
}

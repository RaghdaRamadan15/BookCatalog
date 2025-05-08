using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Dtos
{
    public class CreateBrowingDto
    {
        [Required]
        public string UserId {  get; set; }
        [Required]
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public bool IsReturned { get; set; }=false;
    }
}

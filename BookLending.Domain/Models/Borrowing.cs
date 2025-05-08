using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookLending.Domain.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int  BookId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Column(TypeName = "date")]
        public DateTime BorrowDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime DueDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ReturnDate { get; set; }  
        public bool IsReturned { get; set; }
        public virtual Book Book { get; set; }  
        public virtual IdentityUser User { get; set; }
    }
}

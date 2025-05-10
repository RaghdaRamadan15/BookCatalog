using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Dtos
{
    public class CreatRequstBrorrow
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}

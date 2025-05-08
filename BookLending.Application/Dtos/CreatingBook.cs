using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Dtos
{
    public class CreatingBook
    {
        [Required(ErrorMessage ="please enter 3 letter at lest")]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must more then 1 or equel 1")]
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }= false;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class BookingUpdateDto
    {
        [Required(ErrorMessage = "Ticket type is required")]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }
    }
}

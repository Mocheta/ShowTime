using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class BookingCreateDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TicketId { get; set; }
    }
}

using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class BookingGetDto
    {
        public int UserId { get; set; }
        public int FestivalId { get; set; }
        public int TicketId { get; set; }
        public int quantity { get; set; }
        public Ticket Ticket { get; set; } = null!;
        public string Type { get; set; } = string.Empty;


    }
}

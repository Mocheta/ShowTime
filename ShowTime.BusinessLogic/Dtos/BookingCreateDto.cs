using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class BookingCreateDto
    {
        public int UserId { get; set; }
        public int FestivalId { get; set; }
        public int TicketId { get; set; }

        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}

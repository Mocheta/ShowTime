using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int FestivalId { get; set; }
        public int UserId { get; set; }
        public string type { get; set; } = string.Empty; 
        
        public int TicketId { get; set; }

        public Festival Festival { get; set; } = null!;
        public User User { get; set; } = null!;
        public Ticket Ticket { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
        public int FestivalId { get; set; }
        public int Quantity { get; set; }
        public Festival Festival { get; set; } = null!;
        public Booking Booking { get; set; } = null!;
    }
}

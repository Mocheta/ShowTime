using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class TicketCreateDto
    {
        [Required]
        public int FestivalId { get; set; }
        [Required]
        public String Name { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}

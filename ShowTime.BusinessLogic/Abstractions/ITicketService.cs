using ShowTime.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface ITicketService
    {
        Task UpdateTicketAsync(int ticketId, TicketCreateDto editedTicket);
    }
}

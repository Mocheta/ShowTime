using ShowTime.BusinessLogic.Dtos;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepo<Ticket> _ticketRepository;
        public TicketService(IRepo<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task UpdateTicketAsync(int ticketId, TicketCreateDto editedTicket)
        {
            try
            {
                var ticket = await _ticketRepository.GetByIdAsync(ticketId);
                if (ticket == null) return;
                ticket.Name = editedTicket.Name;
                ticket.Type = editedTicket.Type;
                ticket.Quantity = editedTicket.Quantity;
                ticket.Price = editedTicket.Price;
                await _ticketRepository.UpdateAsync(ticket);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

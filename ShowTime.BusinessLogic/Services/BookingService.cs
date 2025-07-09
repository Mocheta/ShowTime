using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;

namespace ShowTime.BusinessLogic.Services
{
    public class BookingService : IBookingService
    {
        public async Task CreateBookingAsync(int userId, int festivalId, int ticketId, string type, int price)
        {
            try {
                var booking = new Booking
                {
                    UserId = userId,
                    FestivalId = festivalId,
                    TicketId = ticketId,
                    type = type,
                    quantity = 1 
                };
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while creating the booking.", ex);
            }
        }
    }
}

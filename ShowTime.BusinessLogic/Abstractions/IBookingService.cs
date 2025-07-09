using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IBookingService
    {
        Task CreateBookingAsync(int userId, int festivalId, int ticketId, string type, int price);
    }
}

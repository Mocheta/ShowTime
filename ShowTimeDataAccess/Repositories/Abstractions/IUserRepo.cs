using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Repositories.Abstractions
{
    public interface IUserRepo : IRepo<User>
    {
        public Task<User?> GetByEmailAsync(string email);
        public Task<IList<Booking>> GetBookingsByUserIdAsync(int userId);
        public Task<IList<Ticket>> GetTicketsByUserIdAsync(int userId);
        public Task DeleteUserBookingsAsync(int userId);
    }
}

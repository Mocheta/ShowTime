using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Repositories.Implementations
{
    public class UserImplement : GenericImplement<User>, IUserRepo
    {
        private readonly ShowTimeDBContext _context;

        private readonly DbSet<User> _users;

        public UserImplement(ShowTimeDBContext context) : base(context)
        {
            _context = context;
            _users = context.Set<User>();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                Console.WriteLine($"Repository: Looking for email: '{email}'");

                var user = await _users
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    Console.WriteLine("Exact match failed, trying normalized search...");
                    user = await _users
                        .SingleOrDefaultAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower());
                }

                Console.WriteLine($"Repository: Found user: {user != null}");
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve user with email {email}: {ex.Message}");
            }
        }
        public async Task<IList<Booking>> GetBookingsByUserIdAsync(int userId)
        {
            return await _users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Bookings)
                .ToListAsync();
        }
        public async Task<IList<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            return await _users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Tickets)
                .ToListAsync();
        }
        public async Task DeleteUserBookingsAsync(int userId)
        {
            var user = await _users.Include(u => u.Bookings).FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                _context.Bookings.RemoveRange(user.Bookings);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"User with ID {userId} not found.");
            }
        }
    }
}

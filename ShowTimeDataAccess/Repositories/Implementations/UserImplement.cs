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
            try
            {
                return await _context
                    .Set<Booking>()
                    .Where(b => b.UserId == userId)
                    .Include(b => b.Ticket)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve bookings for user ID {userId}: {ex.Message}");
            }
        }

        public async Task<IList<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            try
            {
                return await _context
                    .Set<Booking>()
                    .Where(b => b.UserId == userId)
                    .Select(b => b.Ticket)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve tickets for user ID {userId}: {ex.Message}");
            }
        }
        public async Task DeleteUserBookingAsync(int userId, int ticketId)
        {
            try
            {
                var bookings = await _context
                    .Set<Booking>()
                    .Include(b => b.Ticket)
                    .Where(b => b.UserId == userId && b.TicketId == ticketId)
                    .FirstOrDefaultAsync();

                if (bookings != null)
                {
                    bookings.Ticket.Quantity += 1;
                    _context.Set<Booking>().Remove(bookings);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error trying to delete user booking with ID {userId}: {e.Message}");
            }
        }
        public async Task BookTicketAsync(int userId, int ticketId)
        {
            try
            {
                var ticket = await _context
                    .Set<Ticket>()
                    .FirstOrDefaultAsync(t => t.Id == ticketId);

                if (ticket == null || ticket.Quantity <= 0)
                {
                    throw new Exception("Ticket not available");
                }

                var booking = new Booking
                {
                    UserId = userId,
                    TicketId = ticketId,
                    FestivalId = ticket.FestivalId  // ADD THIS LINE!
                };

                ticket.Quantity -= 1;
                await _context.Set<Booking>().AddAsync(booking);
                await _context.SaveChangesAsync();

                Console.WriteLine($"✅ Booking created: UserId={userId}, TicketId={ticketId}, FestivalId={ticket.FestivalId}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Error booking ticket: {e.Message}");
                throw new Exception($"Error trying to book ticket with ID {ticketId} for user ID {userId}: {e.Message}");
            }
        }
        public async Task<int> GetUserIdByEmailAsync(string? email)
        {
            try
            {
                var user = await _users
                    .FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                return user.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve user ID with email {email}: {ex.Message}");
            }
        }

    }
}

using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task RegisterAsync(RegisterDto registerDto);
        Task BookTicketAsync(int userId, int ticketId);
        Task<List<BookingGetDto>> GetUserBookings(int userId);
        Task<List<TicketGetDto>> GetUserTickets(int userId);
        Task DeleteUserBookingAsync(int userId, int ticketId);
        Task<int> GetUserIdByEmailAsync(string? email);
    }
}

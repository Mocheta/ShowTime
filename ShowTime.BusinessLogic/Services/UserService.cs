using Microsoft.AspNetCore.Identity;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace ShowTime.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepo userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepo = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepo.GetByEmailAsync(loginDto.Email);

            if (user == null)
            {
                Console.WriteLine("User is null");
                throw new Exception("Invalid credentials");
            }
            else
            {
                Console.WriteLine($"User found: {user.Email}");
            }

            if (user.Password == null)
            {   
                Console.WriteLine("User password is null");
                throw new Exception("Invalid credentials");
            }
            else
            {
                Console.WriteLine($"Stored hash: {user.Password}");
            }

            Console.WriteLine($"Entered password: {loginDto.Password}");

            var passwordValid = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password) == PasswordVerificationResult.Success;

            if (!passwordValid)
            {
                Console.WriteLine("Password invalid");
                throw new Exception("Invalid credentials");
            }

            return new LoginResponseDto
            {
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }


        public async Task RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepo.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var newUser = new User
            {
                Email = registerDto.Email,
                Role = (int)registerDto.Role,
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, registerDto.Password);
            await _userRepo.AddAsync(newUser);
        }
        public async Task<int> GetUserIdByEmailAsync(string? email)
        {
            try
            {
                return await _userRepo.GetUserIdByEmailAsync(email);
            }
            catch (Exception e)
            {
                throw new Exception($"Error occured while trying to get user id by email: {e.Message}");
            }
        }
        public async Task BookTicketAsync(int userId, int ticketId)
        {
            try
            {
                await _userRepo.BookTicketAsync(userId, ticketId);
            }
            catch (Exception e)
            {
                throw new Exception($"Error occured while trying to book ticket: {e.Message}");
            }
        }
        public async Task<List<BookingGetDto>> GetUserBookings(int userId)
        {
            try
            {
                var bookings = await _userRepo.GetBookingsByUserIdAsync(userId);
                return bookings.Select(userBooking => new BookingGetDto()
                {
                    Id = userBooking.Id,
                    UserId = userBooking.UserId,
                    TicketId = userBooking.TicketId
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Error occured while trying to get user bookings: {e.Message}");
            }
        }
        public async Task<List<TicketGetDto>> GetUserTickets(int userId)
        {
            try
            {
                var tickets = await _userRepo.GetTicketsByUserIdAsync(userId);
                return tickets.Select(t => new TicketGetDto()
                {
                    Id = t.Id,
                    FestivalId = t.FestivalId,
                    Name = t.Name,
                    Price = t.Price,
                    Quantity = t.Quantity,
                    Type = t.Type
                }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Error occured while trying to get user {userId} tickets: {e.Message}");
            }
        }
        public async Task DeleteUserBookingAsync(int userId, int ticketId)
        {
            await _userRepo.DeleteUserBookingAsync(userId, ticketId);
        }
    }
}
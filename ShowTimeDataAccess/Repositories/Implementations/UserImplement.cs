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
        private readonly DbSet<User> _users;

        public UserImplement(ShowTimeDBContext context) : base(context)
        {
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
    }
}

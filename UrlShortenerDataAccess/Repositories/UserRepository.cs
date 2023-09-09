using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DataAccess;
using UrlShortener.ObjectModel;
using UrlShortenerDataAccess.Repositories.Abstraction;

namespace UrlShortenerDataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;   
        }

        public async Task<User> Create(User user)
        {
            if(user.Id == Guid.Empty)
            {
                user.Id = Guid.NewGuid();
            }
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

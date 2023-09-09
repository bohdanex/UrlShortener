using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel;

namespace UrlShortenerDataAccess.Repositories.Abstraction
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
    }
}

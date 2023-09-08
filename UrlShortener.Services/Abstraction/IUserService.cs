using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel;

namespace UrlShortener.Services.Abstraction
{
    public interface IUserService
    {
        Task Create(User user);
        Task<User> GetByEmail(string email);
    }
}

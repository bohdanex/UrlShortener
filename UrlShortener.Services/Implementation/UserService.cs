using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel;
using UrlShortener.ObjectModel.DTO;
using UrlShortener.Services.Abstraction;
using UrlShortenerDataAccess.Repositories.Abstraction;

namespace UrlShortener.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Create(User user)
        {
            return await userRepository.Create(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await userRepository.GetByEmail(email);
        }
    }
}

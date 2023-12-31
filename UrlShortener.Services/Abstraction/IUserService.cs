﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ObjectModel;
using UrlShortener.ObjectModel.DTO;

namespace UrlShortener.Services.Abstraction
{
    public interface IUserService
    {
        Task<User> Create(User user);
        Task<User> GetByEmail(string email);
        Task<User> GetById(Guid id);
    }
}

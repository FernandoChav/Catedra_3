using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catedra3.src.Models;

namespace catedra3.src.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User user);

        Task<User> GetByEmail(string email);

        
    }
}
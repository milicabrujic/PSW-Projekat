using PSW_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        User GetUserByPassword(string password);
        List<User> GetAll();
    }
}

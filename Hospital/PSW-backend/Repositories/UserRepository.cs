using PSW_backend.Models;
using PSW_backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public User GetUserByUsername(string username)
        {
            return _applicationDbContext.Users.FirstOrDefault(user => user.Username.Equals(username));
        }

        public User GetUserByPassword(string password)
        {
            return _applicationDbContext.Users.FirstOrDefault(user => user.Password.Equals(password));
        }

        public List<User> GetAll()
        {
            return _applicationDbContext.Users.ToList();
        }
    }
}

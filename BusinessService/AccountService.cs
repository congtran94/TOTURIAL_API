using BusinessService.Interface;
using GOSDataModel;
using GOSDataModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessService
{
    public class AccountService: Repository<AspNetUsers>, IAccountService
    {
        public GOSContext Context;
        public AccountService(GOSContext _context) : base(_context)
        {
            Context = _context;
        }
        public User Authenticate(string userName, string password)
        {
            IEnumerable<AspNetUsers> users= Find(s => s.UserName == userName && s.PasswordHash == password).ToList();
            if (users != null &&  users.Any())
            {
                var user  = users.FirstOrDefault();
                return new User()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash
                };
            }
                
            return null;
        }
        public bool Create(User user, string Password)
        {
            return true;
        }
        public IEnumerable<User> GetAll()
        {
            return new List<User>();
        }
        public User GetById(int Id)
        {
            return new User();
        }
        public bool Update(User user)
        {
            return true;
        }
        public  bool Delete(int Id)
        {
            return true;
        }
    }
}

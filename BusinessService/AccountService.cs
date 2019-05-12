using BusinessService.Interface;
using GOSDataModel;
using GOSDataModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService
{
    public class AccountService: Repository<AccountService>, IAccountService
    {
        public GOSContext Context;
        public AccountService(GOSContext _context) : base(_context)
        {
            Context = _context;
        }
        public User Authenticate(string userName, string Password)
        {
            return new User();
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

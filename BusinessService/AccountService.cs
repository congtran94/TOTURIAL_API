using BusinessService.Interface;
using GOSDataModel;
using GOSDataModel.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessService
{
    public class AccountService: Repository<User>, IAccountService
    {
        public AccountService(GOSContext _context) : base(_context)
        {
            Context = _context;
        }
        public User Authenticate(string userName, string password)
        {
            IEnumerable<User> users= Find(s => s.UserName == userName && s.PasswordHash == password);
            if (users != null &&  users.Any())
            {
                var user  = users.FirstOrDefault();
                return new User()
                {
                    Id  = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash
                };
            }
            return null;
        }
        public bool Create(User user)
        {
            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                return false;
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.PasswordHash, out passwordHash, out passwordSalt);
            user.PasswordHash = Encoding.UTF8.GetString(passwordHash, 0, passwordHash.Length);
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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

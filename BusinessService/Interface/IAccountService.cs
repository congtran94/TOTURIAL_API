using GOSDataModel.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService.Interface
{
    public interface IAccountService
    {
        User Authenticate(string UserName, string Password);
        bool Create(User user, string Password);
        IEnumerable<User> GetAll();
        User GetById(int Id);
        bool Update(User user);
        bool Delete(int Id);
        
    }
}

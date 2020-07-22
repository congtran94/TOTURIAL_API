using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessService.Models
{
    public partial class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set;}
        public string UserName { get; set; }
        public  string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public partial class LoginModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}

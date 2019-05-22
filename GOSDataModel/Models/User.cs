using System;
using System.Collections.Generic;
using System.Text;

namespace GOSDataModel.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set;}
        public string UserName { get; set; }
        public  string PasswordHash { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

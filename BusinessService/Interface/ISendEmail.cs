using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Interface
{
    public interface ISendEmail
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessService;
using BusinessService.Interface;
using GOSDataModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TOTURIAL_API.Models;

namespace TOTURIAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;
        private readonly EmailSettings _emailSettings;
        
        public EmailController(IRepositoryWrapper repoWrapper,IOptions<EmailSettings> emailSettings)
        {
            _repoWrapper = repoWrapper;
            _emailSettings = emailSettings.Value;
        }
        [HttpPost]
        public ActionResult SendEmail([FromBody] EmailInfo email)
        {
            Task sendEmail =  _repoWrapper.SendEmail.SendEmailAsync(email.ReceiveEmail, email.Subject, email.Body);
            return Ok();
        }
    }
}
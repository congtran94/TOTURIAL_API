using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GOSDataModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessService.Interface;
using TOTURIAL_API.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;
using TOTURIAL_API.Models;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace TOTURIAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigins")]
    public class AccountController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;
        private readonly AppSettings _appSettings;
        private IMapper _mapper;
        public AccountController(IRepositoryWrapper repoWrapper, IOptions<AppSettings> appSettings/*, IMapper mapper*/)
        {
            _repoWrapper = repoWrapper;
            _appSettings = appSettings.Value;
            //_mapper = mapper;
        }
        [EnableCors("AllowOrigins")]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginModel userLogin)
        {
            var user = _repoWrapper.AccountService.Authenticate(userLogin.UserName, userLogin.PassWord);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim( ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserModel userRegiter)
        {
            // map dto to entity
            if(userRegiter == null || string.IsNullOrEmpty(userRegiter.UserName))
                return  BadRequest(new { message = "Username or password is incorrect" });
            var user = _mapper.Map<User>(userRegiter);
            //var user = userRegiter;
            try
            {
                // save 
                _repoWrapper.AccountService.Create(user);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
      
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _repoWrapper.AccountService.GetAll();
            //var userDtos = _mapper.Map<IList<User>>(users);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _repoWrapper.AccountService.GetById(id);
           // var userDto = _mapper.Map<User>(user);
            return Ok(user);
        }
      
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]User userDto)
        {
            // map dto to entity and set id
            //var user = _mapper.Map<User>(userDto);
            var user = userDto;
            user.Id = id;

            try
            {
                // save 
                _repoWrapper.AccountService.Update(user);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repoWrapper.AccountService.Delete(id);
            return Ok();
        }
        
    }
}

using Application.Application;
using Application.Interfaces;
using Entities.Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Presentation.Models;
using Presentation.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _IUserApplication;
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly IConfiguration _Configuration;

        public UserController(IUserApplication IUserApplication, UserManager<User> UserManager, SignInManager<User> SignInManager, IConfiguration Configuration)
        {
            _IUserApplication = IUserApplication;
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Configuration = Configuration;
        }

        //[AllowAnonymous]
        //[Produces("application/json")]
        //[HttpPost("/api/CreateToken")]
        //public async Task<IActionResult> CreateToken([FromBody] LoginDTO login)
        //{
        //    if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
        //        return Unauthorized();

        //    var result = await _IUserApplication.ExistUser(login.Email, login.Password);
        //    if (result)
        //    {
        //        var token = new TokenJWTBuilder()
        //         .AddSecurityKey(JwtSecurityKey.Create(_Configuration.GetSection("Settings:Secret").Value))
        //         .AddSubject("Empresa - Canal Dev Net Core")
        //         .AddIssuer("Study.Securiry.Bearer")
        //         .AddAudience("Study.Securiry.Bearer")
        //         .AddClaim("UserID", "a")
        //         .AddExpiry(5)
        //         .Builder();

        //        return Ok(token.value);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[AllowAnonymous]
        //[Produces("application/json")]
        //[HttpPost("/api/AddUser")]
        //public async Task<IActionResult> AddUser([FromBody] UserDTO user)
        //{
        //    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        //        return Ok("Por gentileza digite os dados corretamente!");

        //    var result = await _IUserApplication.AddUser(user.NameCompany, user.CpfCnpj, user.Email, user.Password, user.Age, user.DDD, user.Phone, user.CellPhone, user.Role);
        //    if (result)
        //        return Ok("Usuario Adicionado com Sucesso!");
        //    else
        //        return Ok("Erro ao Adicionar Usuario!");

        //}

        /// <summary>
        /// Generete JWT Token
        /// </summary>
        /// <param name="Login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] LoginDTO login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                return Unauthorized();

            var result = await _SignInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var entity = await _IUserApplication.GetUser(login.Email);

                var token = new TokenJWTBuilder()
                 .AddSecurityKey(JwtSecurityKey.Create(_Configuration.GetSection("Settings:Secret").Value))
                 .AddSubject(entity.Id)
                 .AddIssuer(_Configuration.GetSection("Settings:Issuer").Value)
                 .AddAudience(_Configuration.GetSection("Settings:Audience").Value)
                 .AddClaim("userID", entity.Id)
                 .AddClaim("role", entity.Role.ToString())
                 .AddExpiry(5)
                 .Builder();

                return Ok(token.value);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateUserIdentity")]
        public async Task<IActionResult> CreateUserIdentity([FromBody] UserDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return Unauthorized();

            var entity = new User
            {
                NameCompany = user.NameCompany,
                CpfCnpj = user.CpfCnpj,
                Email = user.Email,
                PasswordHash = user.Password,
                Age = user.Age,
                DDD = user.DDD,
                Phone = user.Phone,
                CellPhone = user.CellPhone,
                Role = RoleEnum.Client,
                UserName = user.Email
            };
            var result = await _UserManager.CreateAsync(entity, user.Password);

            if (result.Errors.Any())
            {
                return Ok(result.Errors);
            }

            var userId = await _UserManager.GetUserIdAsync(entity);
            var code = await _UserManager.GenerateEmailConfirmationTokenAsync(entity);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var resultCode = await _UserManager.ConfirmEmailAsync(entity, code);

            if (resultCode.Succeeded)
                return Ok("Usuario Adicionado com Sucesso!");
            else
                return Ok("Erro ao confirmar usuario!");
        }

        [Authorize(Roles = "Admin")]
        [Produces("application/json")]
        [HttpPost("/api/Admin")]
        public async Task<String> Admin()
        {
            return "Admin";
        }

        [Authorize(Roles = "Client")]
        [Produces("application/json")]
        [HttpPost("/api/Client")]
        public async Task<String> Client()
        {
            return "Cliente";
        }

    }
}

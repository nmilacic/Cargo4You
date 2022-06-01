using Cargo4You.Data.Database.Cargo4You.Context;
using Cargo4You.Services;
using Cargo4You.Services.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cargo4You.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(
            Courier4YouContext context, UserService userService
           )
        {
            this.userService = userService;
        }
        [HttpPost("UserRegistration", Name = "UserRegistration")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UserRegistration(RegistrationData registrationData)
        {
            return Ok( await userService.Registration(registrationData));

        }

        [HttpPost("Login", Name = "Login")]
        [ProducesResponseType(typeof(LoginData), 200)]
        public async Task<IActionResult> Login(LoginRequest login)
        {
            return Ok(await userService.Login(login));
        }
    }
}

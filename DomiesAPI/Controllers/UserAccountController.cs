using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DomiesAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {

        //private readonly DomiesContext _context;
        //private ApiResponse _response;
        private IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            //_context = context;
            //_response = apiResponse;
            _userAccountService = userAccountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userdto)
        {
            if (ModelState.IsValid)
            {
                string result = await _userAccountService.RegisterUser(userdto);
                if (result == "Użytkownik z danym mailem już istnieje.")
                {
                    return BadRequest(new { message = result });
                }
                else
                {
                    return Ok(new { token = result, user = userdto.Email });
                }
            }
            return BadRequest(new { message = "Wystąpił błąd w rejestracji." });

            //await _userService.RegisterUser(userdto);
            //    return Ok();
                
            

            //return BadRequest(new { message = "Wystąpił błąd w rejestracji." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                string result = await _userAccountService.Login(loginDto);
                if (result == "Niepoprawny email lub hasło")
                {
                    return BadRequest(new { message = result });
                }
                else
                {
                    return Ok(new { token = result, user = loginDto.Email });
                }
            }
            return BadRequest(new { message = "Wystąpił błąd w rejestracji." });
        }

        
    }
}

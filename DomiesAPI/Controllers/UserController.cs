using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DomiesAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {

        private ApiResponse _response;
        private IUserService _userService;

        public UserController(DomiesContext context, IUserService userService)
        {
            //_context = context;
            _response = new ApiResponse();
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {

            //if (ModelState.IsValid)
            //{
            //    string result = await _offerService.GetOffers();
            //    if (result == "Niepoprawny email lub hasło")
            //    {
            //        return BadRequest(new { message = result });
            //    }
            //    else
            //    {
            //        return Ok(new { token = result, user = userdto.Email });
            //    }
            //}
            //return BadRequest(new { message = "Wystąpił błąd w rejestracji." });

            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            var users = await _userService.GetUsers(userEmail);
            _response.Result = users;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        [HttpGet("{email}")]
        public async Task<IActionResult> GetById(string email)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            if (email == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var user = await _userService.GetUserById(email, userEmail);


            if (user == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = user;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);


        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);

            try
            {
                var createdUser = await _userService.CreateUser(userDto, userEmail);

                if (createdUser == null)
                {
                    return BadRequest(_response);
                }

                _response.Result = createdUser;
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);

            }

        }

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateApplication(string email, [FromBody] UserDto userDto)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            try
            {
                var updatedUser = await _userService.UpdateUser(email, userDto, userEmail);


                if (updatedUser == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "No changes were made to the application." });
                }

                _response.Result = updatedUser;
                _response.StatusCode = HttpStatusCode.OK;
                //_response.Result = new { message = "Offer updated successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);

            }

        }

    }
}

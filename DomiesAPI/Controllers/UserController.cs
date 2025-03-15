using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            _response = new ApiResponse();
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            try
            {
                var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
                var users = await _userService.GetUsers(userEmail);
                _response.Result = users;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas zmiany roli użytkownika.", ex);
            }     
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
            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas zmiany roli użytkownika.", ex);

            }


        }

        [HttpPut("userRole/{email}")]
        public async Task<IActionResult> DowngradingToUserRole(string email)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            try
            {
                var updatedUserRole = await _userService.ChangeRole(email, 1, userEmail);

                if (updatedUserRole == null)
                {
                    return Ok(new { message = "Brak zmian roli użytkownika." });
                }

                _response.Result = updatedUserRole;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas zmiany roli użytkownika.", ex);

            }

        }

        [HttpPut("managerRole/{email}")]
        public async Task<IActionResult> PromotionToManagerRole(string email)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            try
            {
                var updatedUserRole = await _userService.ChangeRole(email, 2, userEmail);

                if (updatedUserRole == null)
                {
                    return Ok(new { message = "Brak zmian roli użytkownika." });
                }

                _response.Result = updatedUserRole;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas zmiany roli użytkownika.", ex);

            }

        }


        [HttpDelete("{email}")]
        public async Task<ActionResult<ApiResponse>> DeleteUser(string email)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            try
            {
                var userToDelete = await _userService.DeleteUserById(email, userEmail);


                if (userToDelete == null)
                {
                    return Ok(new { message = "Nie usunięto żadnego użytkownka." });
                }

                _response.Result = userToDelete;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Wystąpił błąd podczas usuwania.", ex);

            }

        }

    }
}

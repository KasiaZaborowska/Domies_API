using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DomiesAPI.Controllers
{
    [Route("api/application")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        //private readonly DomiesContext _context;
        private ApiResponse _response;
        private IApplicationService _applicationService;

        public ApplicationController(DomiesContext context, IApplicationService applicationService)
        {
            //_context = context;
            _response = new ApiResponse();
            _applicationService = applicationService;
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
            var applications = await _applicationService.GetApplications(userEmail);
            _response.Result = applications;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            var application = await _applicationService.GetApplicationById(id, userEmail);


            if (application == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = application;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);


        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationDto applicationDto)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);

            try
            {
                var createdApplication = await _applicationService.CreateApplication(applicationDto, userEmail);

                if (createdApplication == null)
                {
                    return BadRequest(_response);
                }

                _response.Result = createdApplication;
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);

            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] ApplicationDto applicationDto)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            try
            {
                var updatedApplication = await _applicationService.UpdateApplication(id, applicationDto, userEmail);


                if (updatedApplication == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "No changes were made to the application." });
                }

                _response.Result = updatedApplication;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            try
            {
                var applicationToDelete = await _applicationService.DeleteApplicationById(id, userEmail);


                if (applicationToDelete == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "No delete were made to the application." });
                }

                _response.Result = applicationToDelete;
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

using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace DomiesAPI.Controllers
{
    [Route("api/facility")]
    [ApiController]
    //[Authorize]
    public class FacilityController : ControllerBase
    {
        private readonly DomiesContext _context;
        private ApiResponse _response;
        private IFacilityService _facilityService;
        public FacilityController(DomiesContext context, IFacilityService facilityService)
        {
            _context = context;
            _response = new ApiResponse();
            _facilityService = facilityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            try
            {
                //var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
                var facilities = await _facilityService.GetFacilities();
                _response.Result = facilities;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania udogodnień.", ex);

            }
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                //var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var facility = await _facilityService.GetFacilityById(id);


                if (facility == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = facility;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółów udogodnienia.", ex);

            }
        }
    }
}

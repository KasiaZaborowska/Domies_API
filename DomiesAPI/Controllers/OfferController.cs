using DomiesAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DomiesAPI.Controllers
{
    [Route("api/offer")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly DomiesContext _context;
        private ApiResponse _response;

        public OfferController(DomiesContext context)
        {
            _context = context;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            _response.Result = _context.Offers;
            
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }











        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Offer type = _context.Offers.FirstOrDefault(u => u.Id == id);

            if (type == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = type;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

    }
}

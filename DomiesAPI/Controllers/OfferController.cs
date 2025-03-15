using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DomiesAPI.Controllers
{
    [Route("api/offer")]
    [ApiController]

    public class OfferController : ControllerBase
    {
        //private readonly DomiesContext _context;
        private ApiResponse _response;
        private IOfferService _offerService;

        public OfferController(DomiesContext context, IOfferService offerService)
        {
            //_context = context;
            _response = new ApiResponse();
            _offerService = offerService;
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

            try
            {
                var offers = await _offerService.GetOffers();
                _response.Result = offers;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania ofert.", ex);

            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var offer = await _offerService.GetOfferById(id);


                if (offer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = offer;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółów oferty.", ex);

            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddOffer([FromForm] OfferDto offerDto)
        {
            try
            {
                var createdOffer = await _offerService.CreateOffer(offerDto);
                var file = offerDto.File;
                if (createdOffer == null)
                {
                    return BadRequest(_response);
                }

                _response.Result = createdOffer;
                _response.StatusCode = HttpStatusCode.Created;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas dodawania oferty.", ex);

            }

        }

        [HttpPatch("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateOffer(int id, [FromForm] OfferDto offerDto)
        {
            try
            {
                var updatedOffer = await _offerService.UpdateOffer(id, offerDto);


                if (updatedOffer == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "Brak zmian." });
                }

                _response.Result = updatedOffer;
                _response.StatusCode = HttpStatusCode.OK;
                //_response.Result = new { message = "Offer updated successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas aktualizacji oferty.", ex);

            }

        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            try
            {
                var offerToDelete = await _offerService.DeleteOfferById(id);


                if (offerToDelete == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "Nie usunięto żadnej oferty." });
                }

                _response.Result = offerToDelete;
                _response.StatusCode = HttpStatusCode.OK;
                //_response.Result = new { message = "Offer updated successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas usuwania oferty.", ex);

            }
        }

    }
}

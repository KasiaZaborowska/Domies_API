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
    [Route("api/opinion")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        //private readonly DomiesContext _context;
        private ApiResponse _response;
        private IOpinionService _opinionService;

        public OpinionController(DomiesContext context, IOpinionService opinionService)
        {
            //_context = context;
            _response = new ApiResponse();
            _opinionService = opinionService;
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

            var opinions = await _opinionService.GetOpinions();
            _response.Result = opinions;
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
            var opinion = await _opinionService.GetOpinionById(id);


            if (opinion == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = opinion;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);


        }

        [HttpPost]
        public async Task<IActionResult> AddOpinion([FromBody] OpinionDto opinionDto)
        {
            try
            {
                var createdApplication = await _opinionService.CreateOpinion(opinionDto);

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

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOpinion(int id, [FromBody] OpinionDto opinionDto)
        {
            try
            {
                var updatedApplication = await _opinionService.UpdateOpinion(id, opinionDto);


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
        public async Task<IActionResult> DeleteOpinion(int id)
        {
            try
            {
                var opinionToDelete = await _opinionService.DeleteOpinionById(id);


                if (opinionToDelete == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "No delete were made to the application." });
                }

                _response.Result = opinionToDelete;
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

using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DomiesAPI.Controllers
{
    [Route("api/animal")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly DomiesContext _context;
        private ApiResponse _response;
        private IAnimalService _animalService;
        public AnimalController(DomiesContext context, IAnimalService animalService)
        {
            _context = context;
            _response = new ApiResponse();
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var animalTypes = await _animalService.GetAnimals();
            _response.Result = animalTypes;
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
            var animalType = await _animalService.GetAnimalById(id);


            if (animalType == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = animalType;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateTypeOfAnimal([FromBody] AnimalDto animalDto)
        {
            try
            {
                var createdAnimalType = await _animalService.CreateAnimal(animalDto);

                if (createdAnimalType == null)
                {
                    return BadRequest(_response);
                }

                _response.Result = createdAnimalType;
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
        public async Task<ActionResult<ApiResponse>> UpdateTypeOfAnimal(int id, [FromBody] AnimalDto animalDto)
        {
            try
            {
                var updatedAnimalType = await _animalService.UpdateAnimal(id, animalDto);


                if (updatedAnimalType == null)
                {
                    return Ok(new { message = "No changes were made to the animal type." });
                }

                _response.Result = updatedAnimalType;
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

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteTypeOfAnimal(int id)
        {
            try
            {
                var animalTypeToDelete = await _animalService.DeleteAnimalById(id);


                if (animalTypeToDelete == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "No delete were made to the offer." });
                }

                _response.Result = animalTypeToDelete;
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

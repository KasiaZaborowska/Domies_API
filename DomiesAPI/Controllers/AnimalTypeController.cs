using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DomiesAPI.Controllers
{
    [Route("api/animaltype")]
    [ApiController]
    
    public class AnimalTypeController : ControllerBase
    {
        private readonly DomiesContext _context;
        private ApiResponse _response;
        private IAnimalTypeService _animalTypeService;
        public AnimalTypeController(DomiesContext context, IAnimalTypeService animalTypeService)
        {
            _context = context;
            _response = new ApiResponse();
            _animalTypeService = animalTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            try
            {
                var animalTypes = await _animalTypeService.GetAnimalTypes();
                _response.Result = animalTypes;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania typów zwierząt.", ex);
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
                var animalType = await _animalTypeService.GetAnimalTypesById(id);


                if (animalType == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = animalType;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowycyh typów zwierząt.", ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse>> CreateTypeOfAnimal([FromBody] AnimalTypeDto animalTypeDto)
        {
            try
            {
                var createdAnimalType = await _animalTypeService.CreateAnimalType(animalTypeDto);

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
                throw new ApplicationException("Błąd podczas tworzenia typu zwierzęcia.", ex);

            }
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Manager")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse>> UpdateTypeOfAnimal(int id, [FromBody] AnimalTypeDto typeOfAnimalDto)
        {
            try
            {
                var updatedAnimalType = await _animalTypeService.UpdateAnimalType(id, typeOfAnimalDto);


                if (updatedAnimalType == null)
                {
                    return Ok(new { message = "Brak zmian." });
                }

                _response.Result = updatedAnimalType;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas aktualizacji typu zwierzęcia.", ex);

            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Manager")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApiResponse>> DeleteTypeOfAnimal(int id)
        {
            try
            {
                var animalTypeToDelete = await _animalTypeService.DeleteAnimalTypeById(id);


                if (animalTypeToDelete == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "Nie usunięto żadnego typu zwierzęcia." });
                }

                _response.Result = animalTypeToDelete;
                _response.StatusCode = HttpStatusCode.OK;
                //_response.Result = new { message = "Offer updated successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas usuwania typu zwierzęcia.", ex);

            }

        }
    }
}

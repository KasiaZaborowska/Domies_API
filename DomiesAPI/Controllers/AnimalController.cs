﻿using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace DomiesAPI.Controllers
{
    [Route("api/animal")]
    [ApiController]
    [Authorize]
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
            try
            {
                var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
                var animals = await _animalService.GetAnimals(userEmail);
                _response.Result = animals;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania zwierząt.", ex);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                var animalType = await _animalService.GetAnimalById(id, userEmail);


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
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji o zwierzęciu.", ex);
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<ActionResult<ApiResponse>> CreateTypeOfAnimal([FromBody] AnimalDto animalDto)
        {
            try
            {
                var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
                var createdAnimalType = await _animalService.CreateAnimal(animalDto, userEmail);

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
                throw new ApplicationException("Błąd podczas tworzenia zwierzęcia.", ex);

            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateTypeOfAnimal(int id, [FromBody] AnimalDto animalDto)
        {
            try
            {
                var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
                var updatedAnimalType = await _animalService.UpdateAnimal(id, animalDto, userEmail);
               

                if (updatedAnimalType == null)
                {
                    return Ok(new { message = "Brak zmian." });
                }

                _response.Result = updatedAnimalType;
                _response.StatusCode = HttpStatusCode.OK;
                //_response.Result = new { message = "Offer updated successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas aktualizacji zwierzaka.", ex);

            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteTypeOfAnimal(int id)
        {
            try
            {
                var userEmail = IUserAccountService.getLoggedInUserEmail(HttpContext);
                var animalTypeToDelete = await _animalService.DeleteAnimalById(id, userEmail);
                

                if (animalTypeToDelete == null)
                {
                    //_response.StatusCode = HttpStatusCode.NotFound;
                    //return NotFound(_response);
                    return Ok(new { message = "Nie usunięto żadnego zwierzaka." });
                }

                _response.Result = animalTypeToDelete;
                _response.StatusCode = HttpStatusCode.OK;
                //_response.Result = new { message = "Offer updated successfully." };
                return Ok(_response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas usuwania zwierzaka.", ex);

            }

        }
    }
}

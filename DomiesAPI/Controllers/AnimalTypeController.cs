using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
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
        public AnimalTypeController(DomiesContext context)
        {
            _context = context;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            _response.Result = _context.AnimalTypes;
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

            AnimalType type = _context.AnimalTypes.FirstOrDefault(u =>u.Id == id);

            if (type == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Result = type;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateTypeOfAnimal([FromBody] AnimalTypeDto typeOfAnimalDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AnimalType typeOfAnimal = new()
                    {
                        Type = typeOfAnimalDto.Type
                    };
                    _context.AnimalTypes.Add(typeOfAnimal);
                    await _context.SaveChangesAsync();

                    _response.Result = typeOfAnimal;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "Invalid model state." };
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateTypeOfAnimal(int id, [FromBody] AnimalTypeDto typeOfAnimalDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (typeOfAnimalDto == null || id != typeOfAnimalDto.AnimalTypeId)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    AnimalType typeOfAnimalfromDb = await _context.AnimalTypes.FindAsync(id);
                    if (typeOfAnimalfromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    typeOfAnimalfromDb.Type = typeOfAnimalDto.Type;

                    _context.AnimalTypes.Update(typeOfAnimalfromDb);
                    await _context.SaveChangesAsync();

                    _response.Result = typeOfAnimalfromDb;
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "Invalid model state." };
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteTypeOfAnimal(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    AnimalType typeOfAnimalfromDb = await _context.AnimalTypes.FindAsync(id);

                    if (typeOfAnimalfromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        return BadRequest();
                    }

                    _context.AnimalTypes.Remove(typeOfAnimalfromDb);
                    await _context.SaveChangesAsync();

                    _response.Result = typeOfAnimalfromDb;
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "Invalid model state." };
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

    }
}

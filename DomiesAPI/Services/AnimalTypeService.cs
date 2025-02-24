using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IAnimalTypeService
    {
        Task<List<AnimalTypeDto>> GetAnimalTypes();
        Task<AnimalTypeDto> GetAnimalTypesById(int id);
        Task<AnimalTypeDto> CreateAnimalType(AnimalTypeDto animalTypeDto);
        Task<AnimalTypeDto> UpdateAnimalType(int id, AnimalTypeDto animalTypeDto);

        Task<bool> DeleteAnimalTypeById(int id);
    }
    public class AnimalTypeService : IAnimalTypeService
    {
        private readonly DomiesContext _context;
        public AnimalTypeService(DomiesContext context) 
        {
            _context = context;
        }

        public async Task<List<AnimalTypeDto>> GetAnimalTypes()
        {
            try
            {
                if (_context.AnimalTypes == null)
                {
                    throw new ApplicationException("Tablica AnimalTypes nie istnieje.");
                }

                var animalTypes = await _context.AnimalTypes
                     .Select(t => new AnimalTypeDto
                     {
                         AnimalTypeId = t.Id,
                         Type = t.Type,
                     })
                    .ToListAsync();
                
                return animalTypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<AnimalTypeDto> GetAnimalTypesById(int id)
        {
            try
            {
                var animalType = await _context.AnimalTypes
                    .Where(t => t.Id == id)
                     .Select(t => new AnimalTypeDto
                     {
                         AnimalTypeId = t.Id,
                         Type = t.Type
                     })
                    .FirstOrDefaultAsync();
                Console.WriteLine(animalType);
                return animalType;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<AnimalTypeDto> CreateAnimalType(AnimalTypeDto animalTypeDto)
        {
            try
            {
                var animalTypeEntity = new AnimalType
                {
                    Type = animalTypeDto.Type

                };

                _context.AnimalTypes.Add(animalTypeEntity);
                await _context.SaveChangesAsync();

                return new AnimalTypeDto
                {
                    Type = animalTypeEntity.Type

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas tworzenia typu zwierzęcia", ex);
            }
        }

        public async Task<AnimalTypeDto> UpdateAnimalType(int id, AnimalTypeDto animalTypeDto)
        {
            try
            {
                var animalTypeEntity = await _context.AnimalTypes
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (animalTypeEntity == null)
                {
                    return null;
                }

                if (!string.IsNullOrEmpty(animalTypeDto.Type) && animalTypeEntity.Type != animalTypeDto.Type)
                {
                    animalTypeEntity.Type = animalTypeDto.Type;


                }

                await _context.SaveChangesAsync();

                return new AnimalTypeDto
                {
                    Type = animalTypeEntity.Type

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas edytowania typu zwierzęcia", ex);
            }
        }

        public async Task<bool> DeleteAnimalTypeById(int id)
        {
            try
            {
                var animalTypeToDelete = await _context.AnimalTypes
                     .FirstOrDefaultAsync(o => o.Id == id);

                if (animalTypeToDelete == null)
                {
                    return false;
                }
                _context.AnimalTypes.Remove(animalTypeToDelete);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas usuwania typu zwierzęcia", ex);
            }
        }


    }
}

using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IAnimalService
    {
        Task<List<AnimalDto>> GetAnimals();
        Task<AnimalDto> GetAnimalById(int id);
        Task<string> CreateAnimal(AnimalDto animalDto);
        Task<string> UpdateAnimal(int id, AnimalDto animalDto);

        Task<bool> DeleteAnimalById(int id);
    }
    public class AnimalService : IAnimalService
    {
        private readonly DomiesContext _context;
        public AnimalService(DomiesContext context) 
        {
            _context = context;
        }

        public async Task<List<AnimalDto>> GetAnimals()
        {
            try
            {
                if (_context.AnimalTypes == null)
                {
                    throw new ApplicationException("Tablica Animal nie jest zainicjalizowana.");
                }

                var animals = await _context.Animals
                     .Select(a => new AnimalDto
                     {
                         PetName = a.PetName,
                         SpecificDescription = a.SpecificDescription,
                         Owner = a.Owner,
                         AnimalType = a.AnimalType,
                     })
                    .ToListAsync();
                
                return animals;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<AnimalDto> GetAnimalById(int id)
        {
            try
            {
                var animal = await _context.Animals
                    .Where(a => a.Id == id)
                     .Select(a => new AnimalDto
                     {
                         PetName = a.PetName,
                         SpecificDescription = a.SpecificDescription,
                         Owner = a.Owner,
                         AnimalType = a.AnimalType,
                     })
                    .FirstOrDefaultAsync();
                Console.WriteLine(animal);
                return animal;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<string> CreateAnimal(AnimalDto animalDto)
        {
            try
            {
                var animalEntity = new Animal
                {
                    PetName = animalDto.PetName, 
                    SpecificDescription = animalDto.SpecificDescription, 
                    Owner = animalDto.Owner,
                    AnimalType = animalDto.AnimalType,

                };

                _context.Animals.Add(animalEntity);
                await _context.SaveChangesAsync();

                return "Utworzono.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas tworzenia", ex);
            }
        }

        public async Task<string> UpdateAnimal(int id, AnimalDto animalDto)
        {
            try
            {
                var animalEntity = await _context.Animals
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (animalEntity == null)
                {
                    return null;
                }

                if (!string.IsNullOrEmpty(animalDto.PetName) && animalEntity.PetName != animalDto.PetName)
                {
                    animalEntity.PetName = animalDto.PetName;
                }
                if (!string.IsNullOrEmpty(animalDto.SpecificDescription) && animalEntity.SpecificDescription != animalDto.SpecificDescription)
                {
                    animalEntity.SpecificDescription = animalDto.SpecificDescription;
                }

                await _context.SaveChangesAsync();

                return "Edytowano.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas edytowania", ex);
            }
        }

        public async Task<bool> DeleteAnimalById(int id)
        {
            try
            {
                var animalToDelete = await _context.Animals
                     .FirstOrDefaultAsync(o => o.Id == id);

                if (animalToDelete == null)
                {
                    return false;
                }
                _context.Animals.Remove(animalToDelete);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }


    }
}

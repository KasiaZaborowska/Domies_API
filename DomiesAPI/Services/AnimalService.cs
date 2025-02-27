using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DomiesAPI.Services
{
    public interface IAnimalService
    {
        Task<List<AnimalDto>> GetAnimals(String userEmail);
        Task<AnimalDto> GetAnimalById(int id, String userEmail);
        Task<string> CreateAnimal(AnimalDto animalDto, String userEmail);
        Task<string> UpdateAnimal(int id, AnimalDto animalDto, String userEmail);

        Task<bool> DeleteAnimalById(int id, String userEmail);
    }
    public class AnimalService : IAnimalService
    {
        private readonly DomiesContext _context;
        public AnimalService(DomiesContext context) 
        {
            _context = context;
        }

        public async Task<List<AnimalDto>> GetAnimals(String userEmail)
        {
            try
            {
                if (_context.AnimalTypes == null)
                {
                    throw new ApplicationException("Tablica Animal nie jest zainicjalizowana.");
                }

                var animals = await _context.Animals
                     .Include(a => a.AnimalTypeNavigation)
                     .Where(a => a.Owner == userEmail)
                     .Select(a => new AnimalDto
                     {
                         Id = a.Id,
                         PetName = a.PetName,
                         SpecificDescription = a.SpecificDescription,
                         Owner = userEmail,
                         AnimalType = a.AnimalType,
                         Type = a.AnimalTypeNavigation.Type,
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

        public async Task<AnimalDto> GetAnimalById(int id, String userEmail)
        {
            try
            {
                var animal = await _context.Animals
                    .Where(a => a.Owner == userEmail)
                    .Where(a => a.Id == id)
                     .Select(a => new AnimalDto
                     {
                         Id = a.Id,
                         PetName = a.PetName,
                         SpecificDescription = a.SpecificDescription,
                         Owner = a.Owner,
                         AnimalType = a.AnimalType,
                         Type = a.AnimalTypeNavigation.Type,
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

        public async Task<string> CreateAnimal(AnimalDto animalDto, String userEmail)
        {
            try
            {
                var typ = await _context.AnimalTypes.FirstOrDefaultAsync(t => t.Id == animalDto.AnimalType);
                

                if ((typ == null))
                {
                    return "Nie poprawny typ.";
                }
                var animalEntity = new Animal
                {
                    PetName = animalDto.PetName,
                    SpecificDescription = animalDto.SpecificDescription,
                    Owner = userEmail,
                    AnimalType = animalDto.AnimalType,
                    // = typ,

                    AnimalTypeNavigation = typ

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

        public async Task<string> UpdateAnimal(int id, AnimalDto animalDto, String userEmail)
        {
            try
            {
                var animalEntity = await _context.Animals
                    .Where(a => a.Owner == userEmail)
                    .FirstOrDefaultAsync(t => t.Id == id);

                var typ = await _context.AnimalTypes.FirstOrDefaultAsync(t => t.Id == animalDto.AnimalType);


                if ((animalEntity == null))
                {
                    return "Dane zwierzę nie istnieje.";
                }

                animalEntity.PetName = animalDto.PetName;
                animalEntity.SpecificDescription = animalDto.SpecificDescription;
                Console.WriteLine(animalEntity.AnimalType); 
                Console.WriteLine(animalEntity.AnimalTypeNavigation);
                if (animalDto.AnimalType == 0)
                {
                    animalEntity.AnimalTypeNavigation = animalEntity.AnimalTypeNavigation;
                }
                else
                {
                    animalEntity.AnimalType = animalDto.AnimalType;
                    animalEntity.AnimalTypeNavigation = typ;
                   
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

        public async Task<bool> DeleteAnimalById(int id, String userEmail)
        {
            try
            {
                var animalToDelete = await _context.Animals
                    .Where(a => a.Owner == userEmail)
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

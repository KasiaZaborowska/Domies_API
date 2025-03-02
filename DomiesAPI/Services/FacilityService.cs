using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IFacilityService
    {
        Task<List<FacilityDto>> GetFacilities();
        Task<FacilityDto> GetFacilityById(int id);
        //Task<AnimalTypeDto> CreateAnimalType(AnimalTypeDto animalTypeDto);
        //Task<AnimalTypeDto> UpdateAnimalType(int id, AnimalTypeDto animalTypeDto);

        //Task<bool> DeleteAnimalTypeById(int id);
    }
    public class FacilityService : IFacilityService
    {
        private readonly DomiesContext _context;
        public FacilityService(DomiesContext context)
        {
            _context = context;
        }

        public async Task<List<FacilityDto>> GetFacilities()
        {
            try
            {
                if (_context.Facilities == null)
                {
                    throw new ApplicationException("Tablica Facilities nie istnieje.");
                }

                var facilities = await _context.Facilities
                     .Select(t => new FacilityDto
                     {
                         Id = t.Id,
                         FacilitiesType = t.FacilitiesType,
                         FacilitiesDescription = t.FacilitiesDescription,
                     })
                    .ToListAsync();

                return facilities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<FacilityDto> GetFacilityById(int id)
        {
            try
            {
                var facility = await _context.Facilities
                    .Where(t => t.Id == id)
                     .Select(t => new FacilityDto
                     {
                         Id = t.Id,
                         FacilitiesType = t.FacilitiesType,
                         FacilitiesDescription = t.FacilitiesDescription,
                     })
                    .FirstOrDefaultAsync();
                Console.WriteLine(facility);
                return facility;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }




        //public async Task<AnimalTypeDto> CreateAnimalType(AnimalTypeDto animalTypeDto)
        //{
        //    try
        //    {
        //        var animalTypeEntity = new AnimalType
        //        {
        //            Type = animalTypeDto.Type

        //        };

        //        _context.AnimalTypes.Add(animalTypeEntity);
        //        await _context.SaveChangesAsync();

        //        return new AnimalTypeDto
        //        {
        //            Type = animalTypeEntity.Type

        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        //        throw new ApplicationException("Błąd podczas tworzenia typu zwierzęcia", ex);
        //    }
        //}

        //public async Task<AnimalTypeDto> UpdateAnimalType(int id, AnimalTypeDto animalTypeDto)
        //{
        //    try
        //    {
        //        var animalTypeEntity = await _context.AnimalTypes
        //            .FirstOrDefaultAsync(t => t.Id == id);

        //        if (animalTypeEntity == null)
        //        {
        //            return null;
        //        }

        //        if (!string.IsNullOrEmpty(animalTypeDto.Type) && animalTypeEntity.Type != animalTypeDto.Type)
        //        {
        //            animalTypeEntity.Type = animalTypeDto.Type;


        //        }

        //        await _context.SaveChangesAsync();

        //        return new AnimalTypeDto
        //        {
        //            Type = animalTypeEntity.Type

        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        //        throw new ApplicationException("Błąd podczas edytowania typu zwierzęcia", ex);
        //    }
        //}

        //public async Task<bool> DeleteAnimalTypeById(int id)
        //{
        //    try
        //    {
        //        var animalTypeToDelete = await _context.AnimalTypes
        //             .FirstOrDefaultAsync(o => o.Id == id);

        //        if (animalTypeToDelete == null)
        //        {
        //            return false;
        //        }
        //        _context.AnimalTypes.Remove(animalTypeToDelete);
        //        await _context.SaveChangesAsync();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        //        throw new ApplicationException("Błąd podczas usuwania typu zwierzęcia", ex);
        //    }
        //}


    }
}

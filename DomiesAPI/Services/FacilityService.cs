using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IFacilityService
    {
        Task<List<FacilityDto>> GetFacilities();
        Task<FacilityDto> GetFacilityById(int id);
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
    }
}

using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using System;

namespace DomiesAPI.Services
{
    public interface IApplicationService
    {
        Task<List<ApplicationDtoRead>> GetApplications(String userEmail);
        Task<ApplicationDto> GetApplicationById(int id, String userEmail);
        Task<string> CreateApplication(ApplicationDto applicationDto, String userEmail);
        Task<string> UpdateApplication(int id, ApplicationDto applicationDto, String userEmail);
       // Task<ApplicationDto> UpdateApplication(int id, ApplicationDto applicationDto);
        Task<bool> DeleteApplicationById(int id, String userEmail);
    }
    public class ApplicationService : IApplicationService
    {
        private readonly DomiesContext _context;
        public ApplicationService(DomiesContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationDtoRead>> GetApplications(String userEmail)
        {
            try
            {
                if (_context.Applications == null)
                {
                    throw new ApplicationException("Applications table is not initialized.");
                }

                var applicationsDto = await _context.Applications
                    //.Include(o => o.Address)
                    
                    .Where(a => a.ToUser == userEmail)
                    .Include(o => o.Animals)
                     .Select(a => new ApplicationDtoRead
                     {
                         Id = a.Id,
                         DateStart = a.DateStart,
                         DateEnd = a.DateEnd,
                         OfferId = a.OfferId,
                         ToUser = userEmail,
                         ApplicationDateAdd = a.ApplicationDateAdd,
                         //City = o.Address.City,

                         //Animals = string.Join(", ",
                         //   o.OfferAnimalTypes
                         //   .Select(oat => oat.AnimalType.Type)),

                         //Animals = string.Join(", ",
                         //   a.Animals
                         //   .Select(oat => oat.AnimalType.Type)),

                         Animals = a.Animals != null
                         ? a.Animals
                         .AsEnumerable()
                         .Select(
                            an => new AnimalDto
                            {
                                PetName = an.PetName,
                                SpecificDescription = an.SpecificDescription,
                                AnimalType = an.AnimalType,
                                Type = an.AnimalTypeNavigation.Type,

                            }).ToList()
                            : null,
                         // ta łączona s  READ = o. ICOLLECTION OFFER ANIMAL TYPEs
                     })
                    .ToListAsync();
                Console.WriteLine(applicationsDto);
                return applicationsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<ApplicationDto> GetApplicationById(int id, String userEmail)
        {
            try
            {
                var applicationDto = await _context.Applications
                    .Where(a => a.ToUser == userEmail)
                    .Where(a => a.Id == id)
                    //.Include(o => o.Address)
                     .Select(a => new ApplicationDto
                     {
                         Id = a.Id,
                         DateStart = a.DateStart,
                         DateEnd = a.DateEnd,
                         OfferId = a.OfferId,
                         ToUser = userEmail,
                         ApplicationDateAdd = a.ApplicationDateAdd,
                     })
                    .FirstOrDefaultAsync();
                Console.WriteLine(applicationDto);
                return applicationDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<string> CreateApplication(ApplicationDto applicationDto, String userEmail)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var applicationEntity = new Application
                {
                    DateStart = applicationDto.DateStart,
                    DateEnd = applicationDto.DateEnd,
                    OfferId = applicationDto.OfferId,
                    ToUser = applicationDto.ToUser,
                    ApplicationDateAdd = applicationDto.ApplicationDateAdd,
                };

                

                if (applicationDto.AnimalsInt != null && applicationDto.AnimalsInt.Any())
                {
                    var animalToApplication = await _context.Animals
                        //.Where(at => at.Type.Equals("dog") || at.Type.Equals("cat"))
                        .Where(at => applicationDto.AnimalsInt.Contains(at.Id))
                        //.Select(at => at.Id)
                        .ToListAsync();

                    //applicationEntity.Animals = animalToApplication;

                    foreach (var animal in animalToApplication)
                    {
                        applicationEntity.Animals.Add(animal);
                        animal.Applications.Add(applicationEntity);
                    }
 
                }

                _context.Applications.Add(applicationEntity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return "Stworzono aplikację.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas tworzenia aplikacji", ex);
            }
        }

        public async Task<string> UpdateApplication(int id, ApplicationDto applicationDto, String userEmail)
        {
            try
            {
                var applicationEntity = await _context.Applications
                    .Where(a => a.ToUser == userEmail)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (applicationEntity == null)
                {
                    return null;
                }

                applicationEntity.DateStart = applicationDto.DateStart;
                applicationEntity.DateEnd = applicationDto.DateEnd;



                //var userEmail = applicationEntity.ToUser;
                //applicationEntity.ToUser = userEmail;



                await _context.SaveChangesAsync();

                return "Edytowano aplikację.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas edytowania oferty", ex);
            }
        }

        public async Task<bool> DeleteApplicationById(int id, String userEmail)
        {
            try
            {
                var applicationToDelete = await _context.Applications
                     //.Include(o => o.Address)
                     .Where(a => a.ToUser == userEmail)
                     .FirstOrDefaultAsync(o => o.Id == id);

                if (applicationToDelete == null)
                {
                    return false;
                }
                _context.Applications .Remove(applicationToDelete);
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

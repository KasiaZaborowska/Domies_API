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
        Task<ApplicationDtoRead> GetApplicationById(int id, String userEmail);
        Task<string> ChangeApplicationStatus(int id, string newStatus, String userEmail);
        Task<string> CreateApplication(ApplicationDto applicationDto, String userEmail);
        Task<string> UpdateApplication(int id, ApplicationDto applicationDto, String userEmail);
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

                    .Where(a => a.Applicant == userEmail)
                    .Include(o => o.Animals)
                     .Select(a => new ApplicationDtoRead
                     {
                         Id = a.Id,
                         DateStart = a.DateStart,
                         DateEnd = a.DateEnd,
                         OfferId = a.OfferId,
                         Applicant  = userEmail,
                         ApplicationStatus = a.ApplicationStatus,
                         ApplicationDateAdd = a.ApplicationDateAdd,
                         Note = a.Note,
                         Opinions = a.Opinions.Select(opinion => new OpinionDto
                         {
                             Id = opinion.Id,
                             Rating = opinion.Rating,
                             Comment = opinion.Comment,
                             ApplicationId = opinion.ApplicationId,
                             UserEmail = opinion.UserEmail,
                             OpinionDateAdd = opinion.OpinionDateAdd,
                         }).ToList(),
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
                         // ta łączona s  READ = o. 
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

        public async Task<ApplicationDtoRead> GetApplicationById(int id, String userEmail)
        {
            try
            {
                var applicationDto = await _context.Applications
                    .Where(a => a.Applicant  == userEmail)
                    .Where(a => a.Id == id)
                     //.Include(o => o.Address)
                     .Select(a => new ApplicationDtoRead
                     {
                         Id = a.Id,
                         DateStart = a.DateStart,
                         DateEnd = a.DateEnd,
                         OfferId = a.OfferId,
                         Applicant  = userEmail,
                         ApplicationStatus = a.ApplicationStatus,
                         ApplicationDateAdd = a.ApplicationDateAdd,
                         Note = a.Note,
                         Opinions = a.Opinions.Select(opinion => new OpinionDto
                         {
                             Id = opinion.Id,
                             Rating = opinion.Rating,
                             Comment = opinion.Comment,
                             ApplicationId = opinion.ApplicationId,
                             UserEmail = opinion.UserEmail,
                             OpinionDateAdd = opinion.OpinionDateAdd,
                         }).ToList()
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
                    Applicant = userEmail,
                    ApplicationStatus = "Oczekująca",
                    ApplicationDateAdd = DateTime.Now,
                    Note = applicationDto.Note,
                };



                if (applicationDto.Animals != null && applicationDto.Animals.Any())
                {
                    var animalToApplication = await _context.Animals
                        .Where(at => applicationDto.Animals.Contains(at.Id))
                        .ToListAsync();

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
        public async Task<string> ChangeApplicationStatus(int id, string newStatus, String userEmail)
        {
            try
            {
                var applicationEntity = await _context.Applications
                    .Include(a => a.Offer)
                    .Where(a => a.Id == id && userEmail == a.Offer.Host)
                    .FirstOrDefaultAsync();

                if (applicationEntity == null ) {
                    return "Nie ma takiej aplikacji dla aktualnie zalogowanego użytkownika.";
                }

                applicationEntity.ApplicationStatus = newStatus;
                await _context.SaveChangesAsync();


                Console.WriteLine(applicationEntity);
                return $"Status aplikacji zmieniony na {newStatus}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd", ex);
            }
        }


        public async Task<string> UpdateApplication(int id, ApplicationDto applicationDto, String userEmail)
        {
            try
            {
                var applicationEntity = await _context.Applications
                    .Where(a => a.Applicant  == userEmail)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (applicationEntity == null)
                {
                    return null;
                }

                applicationEntity.DateStart = applicationDto.DateStart;
                applicationEntity.DateEnd = applicationDto.DateEnd;
                applicationEntity.Note = applicationDto.Note;
                applicationEntity.ApplicationStatus = applicationDto.ApplicationStatus;



                //var userEmail = applicationEntity.Applicant ;
                //applicationEntity.Applicant  = userEmail;



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
                     .Where(a => a.Applicant  == userEmail)
                     .FirstOrDefaultAsync(o => o.Id == id);

                if (applicationToDelete == null)
                {
                    return false;
                }
                _context.Applications.Remove(applicationToDelete);
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

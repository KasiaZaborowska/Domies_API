using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IApplicationService
    {
        Task<List<ApplicationDto>> GetApplications();
        Task<ApplicationDto> GetApplicationById(int id);
        Task<ApplicationDto> CreateApplication(ApplicationDto applicationDto);
       // Task<ApplicationDto> UpdateApplication(int id, ApplicationDto applicationDto);
        Task<bool> DeleteApplicationById(int id);
    }
    public class ApplicationService : IApplicationService
    {
        private readonly DomiesContext _context;
        public ApplicationService(DomiesContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationDto>> GetApplications()
        {
            try
            {
                if (_context.Applications == null)
                {
                    throw new ApplicationException("Applications table is not initialized.");
                }

                var applicationsDto = await _context.Applications
                    //.Include(o => o.Address)
                     .Select(a => new ApplicationDto
                     {
                         Id = a.Id,
                         DateStart = a.DateStart,
                         DateEnd = a.DateEnd,
                         OfferId = a.OfferId,
                         ToUser = a.ToUser,
                         ApplicationDateAdd = a.ApplicationDateAdd,
                         //City = o.Address.City,
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

        public async Task<ApplicationDto> GetApplicationById(int id)
        {
            try
            {
                var applicationDto = await _context.Applications
                    .Where(a => a.Id == id)
                    //.Include(o => o.Address)
                     .Select(a => new ApplicationDto
                     {
                         Id = a.Id,
                         DateStart = a.DateStart,
                         DateEnd = a.DateEnd,
                         OfferId = a.OfferId,
                         ToUser = a.ToUser,
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

        public async Task<ApplicationDto> CreateApplication(ApplicationDto applicationDto)
        {
            try
            {
                var applicationEntity = new Application
                {
                    DateStart = applicationDto.DateStart,
                    DateEnd = applicationDto.DateEnd,
                    OfferId = applicationDto.OfferId,
                    ToUser = applicationDto.ToUser,
                    //AddressId = offerDto.AddressId,
                    ApplicationDateAdd = applicationDto.ApplicationDateAdd,
                    //Address = new Address
                    //{
                    //    Country = offerDto.Country,
                    //    City = offerDto.City,
                    //    Street = offerDto.Street,
                    //    PostalCode = offerDto.PostalCode,
                    //}

                };

                _context.Applications.Add(applicationEntity);
                await _context.SaveChangesAsync();

                return new ApplicationDto
                {
                    DateStart = applicationEntity.DateStart,
                    DateEnd = applicationEntity.DateEnd,
                    OfferId = applicationEntity.OfferId,
                    ToUser = applicationEntity.ToUser,
                    ApplicationDateAdd = applicationEntity.ApplicationDateAdd,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas tworzenia aplikacji", ex);
            }
        }

        //public async Task<ApplicationDto> UpdateApplication(int id, ApplicationDto applicationDto)
        //{
        //    try
        //    {
        //        var applicationEntity = await _context.Applications
        //            //.Include(o => o.Address)
        //            .FirstOrDefaultAsync(o => o.Id == id);

        //        if (applicationEntity == null)
        //        {
        //            return null;
        //        }

        //        if (!string.IsNullOrEmpty(applicationEntity.DateStart) && applicationEntity.DateStart != applicationEntity.DateStart)
        //        {
        //            applicationEntity.DateStart = applicationEntity.DateStart;
        //        }

        //        if (!string.IsNullOrEmpty(applicationEntity.Photo) && applicationEntity.Photo != applicationEntity.Photo)
        //        {
        //            applicationEntity.Photo = applicationEntity.Photo;
        //        }

        //        if (!string.IsNullOrEmpty(offerDto.Description) && offerEntity.Description != offerDto.Description)
        //        {
        //            offerEntity.Description = offerDto.Description;
        //        }

        //        //offerEntity.Title = offerDto.Title;
        //        //offerEntity.Photo = offerDto.Photo;
        //        //offerEntity.Description = offerDto.Description;


        //        if (offerEntity.Address != null)
        //        {
        //            if (!string.IsNullOrEmpty(offerDto.Country) && offerEntity.Address.Country != offerDto.Country)
        //            {
        //                offerEntity.Address.Country = offerDto.Country;
        //            }
        //            if (!string.IsNullOrEmpty(offerDto.City) && offerEntity.Address.City != offerDto.City)
        //            {
        //                offerEntity.Address.City = offerDto.City;
        //            }
        //            if (!string.IsNullOrEmpty(offerDto.Street) && offerEntity.Address.Street != offerDto.Street)
        //            {
        //                offerEntity.Address.Street = offerDto.Street;
        //            }
        //            if (!string.IsNullOrEmpty(offerDto.PostalCode) && offerEntity.Address.PostalCode != offerDto.PostalCode)
        //            {
        //                offerEntity.Address.PostalCode = offerDto.PostalCode;
        //            }
        //            //offerEntity.Address.Country = offerDto.Country;
        //            //offerEntity.Address.City = offerDto.City;
        //            //offerEntity.Address.Street = offerDto.Street;
        //            //offerEntity.Address.PostalCode = offerDto.PostalCode;
        //        }

        //        await _context.SaveChangesAsync();

        //        return new OfferDto
        //        {
        //            Title = offerEntity?.Title,
        //            Photo = offerEntity?.Photo,
        //            Description = offerEntity?.Description,

        //            Country = offerEntity.Address?.Country,
        //            City = offerEntity.Address?.City,
        //            Street = offerEntity.Address?.Street,
        //            PostalCode = offerEntity.Address?.PostalCode,

        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        //        throw new ApplicationException("Błąd podczas edytowania oferty", ex);
        //    }
        //}

        public async Task<bool> DeleteApplicationById(int id)
        {
            try
            {
                var applicationToDelete = await _context.Applications
                    //.Include(o => o.Address)
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

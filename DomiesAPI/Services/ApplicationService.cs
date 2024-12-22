using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IApplicationService
    {
        Task<List<OfferDto>> GetOffers();
        Task<OfferDto> GetOfferById(int id);
        Task<OfferDto> CreateOffer(OfferDto offerDto);
        Task<OfferDto> UpdateOffer(int id, OfferDto offerDto);
        Task<bool> DeleteOfferById(int id);
    }
    public class ApplicationService 
    {
        private readonly DomiesContext _context;
        public ApplicationService(DomiesContext context)
        {
            _context = context;
        }

        public async Task<List<OfferDto>> GetOffers()
        {
            try
            {
                if (_context.Offers == null)
                {
                    throw new ApplicationException("Offers table is not initialized.");
                }

                var offersDto = await _context.Offers
                    .Include(o => o.Address)
                     .Select(o => new OfferDto
                     {
                         Id = o.Id,
                         Title = o.Title,
                         Photo = o.Photo,
                         Description = o.Description,
                         Host = o.Host,
                         AddressId = o.AddressId,
                         DateAdd = o.DateAdd,
                         Country = o.Address.Country,
                         City = o.Address.City,
                         Street = o.Address.Street,
                         PostalCode = o.Address.PostalCode,
                     })
                    .ToListAsync();
                Console.WriteLine(offersDto);
                return offersDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<OfferDto> GetOfferById(int id)
        {
            try
            {
                var offerDto = await _context.Offers
                    .Where(o => o.Id == id)
                    .Include(o => o.Address)
                     .Select(o => new OfferDto
                     {
                         Id = o.Id,
                         Title = o.Title,
                         Photo = o.Photo,
                         Description = o.Description,
                         Host = o.Host,
                         AddressId = o.AddressId,
                         DateAdd = o.DateAdd,
                         Country = o.Address.Country,
                         City = o.Address.City,
                         Street = o.Address.Street,
                         PostalCode = o.Address.PostalCode,
                     })
                    .FirstOrDefaultAsync();
                Console.WriteLine(offerDto);
                return offerDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<OfferDto> CreateOffer(OfferDto offerDto)
        {
            try
            {
                var offerEntity = new Offer
                {
                    Title = offerDto.Title,
                    Photo = offerDto.Photo,
                    Description = offerDto.Description,
                    Host = offerDto.Host,
                    //AddressId = offerDto.AddressId,
                    DateAdd = offerDto.DateAdd,
                    Address = new Address
                    {
                        Country = offerDto.Country,
                        City = offerDto.City,
                        Street = offerDto.Street,
                        PostalCode = offerDto.PostalCode,
                    }

                };

                _context.Offers.Add(offerEntity);
                await _context.SaveChangesAsync();

                return new OfferDto
                {
                    Title = offerEntity.Title,
                    Photo = offerEntity.Photo,
                    Description = offerEntity.Description,
                    Host = offerEntity.Host,
                    //AddressId = offerEntity.AddressId,
                    DateAdd = offerEntity.DateAdd,
                    Country = offerEntity.Address.Country,
                    City = offerEntity.Address.City,
                    Street = offerEntity.Address.Street,
                    PostalCode = offerEntity.Address.PostalCode,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas tworzenia oferty", ex);
            }
        }

        public async Task<OfferDto> UpdateOffer(int id, OfferDto offerDto)
        {
            try
            {
                var offerEntity = await _context.Offers
                    .Include(o => o.Address)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (offerEntity == null)
                {
                    return null;
                }

                if (!string.IsNullOrEmpty(offerDto.Title) && offerEntity.Title != offerDto.Title)
                {
                    offerEntity.Title = offerDto.Title;
                }

                if (!string.IsNullOrEmpty(offerDto.Photo) && offerEntity.Photo != offerDto.Photo)
                {
                    offerEntity.Photo = offerDto.Photo;
                }

                if (!string.IsNullOrEmpty(offerDto.Description) && offerEntity.Description != offerDto.Description)
                {
                    offerEntity.Description = offerDto.Description;
                }

                //offerEntity.Title = offerDto.Title;
                //offerEntity.Photo = offerDto.Photo;
                //offerEntity.Description = offerDto.Description;


                if (offerEntity.Address != null)
                {
                    if (!string.IsNullOrEmpty(offerDto.Country) && offerEntity.Address.Country != offerDto.Country)
                    {
                        offerEntity.Address.Country = offerDto.Country;
                    }
                    if (!string.IsNullOrEmpty(offerDto.City) && offerEntity.Address.City != offerDto.City)
                    {
                        offerEntity.Address.City = offerDto.City;
                    }
                    if (!string.IsNullOrEmpty(offerDto.Street) && offerEntity.Address.Street != offerDto.Street)
                    {
                        offerEntity.Address.Street = offerDto.Street;
                    }
                    if (!string.IsNullOrEmpty(offerDto.PostalCode) && offerEntity.Address.PostalCode != offerDto.PostalCode)
                    {
                        offerEntity.Address.PostalCode = offerDto.PostalCode;
                    }
                    //offerEntity.Address.Country = offerDto.Country;
                    //offerEntity.Address.City = offerDto.City;
                    //offerEntity.Address.Street = offerDto.Street;
                    //offerEntity.Address.PostalCode = offerDto.PostalCode;
                }

                await _context.SaveChangesAsync();

                return new OfferDto
                {
                    Title = offerEntity?.Title,
                    Photo = offerEntity?.Photo,
                    Description = offerEntity?.Description,

                    Country = offerEntity.Address?.Country,
                    City = offerEntity.Address?.City,
                    Street = offerEntity.Address?.Street,
                    PostalCode = offerEntity.Address?.PostalCode,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas edytowania oferty", ex);
            }
        }

        public async Task<bool> DeleteOfferById(int id)
        {
            try
            {
                var offerToDelete = await _context.Offers
                    .Include(o => o.Address)
                     .FirstOrDefaultAsync(o => o.Id == id);

                if (offerToDelete == null)
                {
                    return false;
                }
                _context.Offers .Remove(offerToDelete);
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

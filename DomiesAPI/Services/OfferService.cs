using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IOfferService
    {
        Task<List<OfferDto>> GetOffers();
        Task<List<OfferDto>> GetOfferById(int id);
        Task<OfferDto> CreateOffer(OfferDto offerDto);
    }
    public class OfferService : IOfferService
    {
        private readonly DomiesContext _context;
        public OfferService(DomiesContext context)
        {
            _context = context;
        }

        public async Task<List<OfferDto>> GetOffers()
        {
            try
            {
                if (_context.Offers == null)
                {
                    throw new ApplicationException("Cars table is not initialized.");
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

        public async Task<List<OfferDto>> GetOfferById(int id)
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
                    .ToListAsync();
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

    }
}

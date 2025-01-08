using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DomiesAPI.Services
{
    public interface IOfferService
    {
        Task<List<OfferDto>> GetOffers();
        Task<OfferDto> GetOfferById(int id);
        Task<string> CreateOffer(OfferDto offerDto);
        Task<string> UpdateOffer(int id, OfferDto offerDto);
        Task<bool> DeleteOfferById(int id);
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
                    throw new ApplicationException("Offers table is not initialized.");
                }


                var offersDto = await _context.Offers
                    .Include(o => o.Address)
                    .Include(o => o.Photo)
                     .Select(o => new OfferDto
                     {
                         Id = o.Id,
                         Name = o.Name,
                         Description = o.Description,
                         Host = o.Host,
                         //AddressId = o.AddressId,
                         DateAdd = o.DateAdd,
                         Price = o.Price,
                         Photo = o.Photo != null
                            ? $"data:{o.Photo.Type};base64,{Convert.ToBase64String(o.Photo.BinaryData)}"
                            : null,

                         Country = o.Address.Country,
                         City = o.Address.City,
                         Street = o.Address.Street,
                         PostalCode = o.Address.PostalCode,


                         OfferAnimalTypes = string.Join(", ",
                            o.OfferAnimalTypes
                            .Select(oat => oat.AnimalType.Type)),


                         //OfferAnimalTypes = o.OfferAnimalType != null 
                         //? o.OfferAnimalType
                         //.AsEnumerable()
                         //.Select(
                         //   oat => new AnimalTypeDto
                         //   {
                         //       AnimalTypeId = oat.AnimalTypeId,
                         //       Type = oat.AnimalType.Type,
                         //   }).ToList()
                         //   : null,
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
                    .Include(o => o.Photo)
                     .Select(o => new OfferDto
                     {
                         Id = o.Id,
                         Name = o.Name,
                         Description = o.Description,
                         Host = o.Host,
                         //AddressId = o.AddressId,
                         DateAdd = o.DateAdd,
                         Price = o.Price,
                         Photo = o.Photo != null
                            ? $"data:{o.Photo.Type};base64,{Convert.ToBase64String(o.Photo.BinaryData)}"
                            : null,

                         Country = o.Address.Country,
                         City = o.Address.City,
                         Street = o.Address.Street,
                         PostalCode = o.Address.PostalCode,


                         OfferAnimalTypes = string.Join(", ",
                            o.OfferAnimalTypes
                            .Select(oat => oat.AnimalType.Type)),

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

        public async Task<string> CreateOffer(OfferDto offerDto)
        {
            if (offerDto.File == null || offerDto.File.Length == 0)
            {
                return "nie przesłano pliku.";
            }


            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                using var memoryStream = new MemoryStream();
                await offerDto.File.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                var photo = new Photo
                {
                    BinaryData = fileBytes,
                    Name = offerDto.File.FileName,
                    Extension = Path.GetExtension(offerDto.File.FileName),
                    Type = offerDto.File.ContentType
                };

                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();

                //Console.WriteLine($"Wystąpilo photo : {photo.Id}");

                var newOffer = new Offer
                {
                    Name = offerDto.Name,
                    Description = offerDto.Description,
                    Host = offerDto.Host,
                    Price = offerDto.Price,
                    DateAdd = offerDto.DateAdd,
                    PhotoId = photo.Id,
                    Address = new Address
                    {
                        Country = offerDto.Country,
                        City = offerDto.City,
                        Street = offerDto.Street,
                        PostalCode = offerDto.PostalCode,
                    }
                };

                _context.Offers.Add(newOffer);
                await _context.SaveChangesAsync();

                if (offerDto.OfferAnimalTypes != null && offerDto.OfferAnimalTypes.Any())
                {
                    var animalTypeToOffer = await _context.AnimalTypes
                        //.Where(at => at.Type.Equals("dog") || at.Type.Equals("cat"))
                        .Where(at => offerDto.OfferAnimalTypes.Contains(at.Type))
                        .Select(at => at.Id)
                        .ToListAsync();

                    foreach (var animalId in animalTypeToOffer)
                    {
                        var newAnimalTypeToOffer = new OfferAnimalType
                        {
                            OfferId = newOffer.Id,
                            AnimalTypeId = animalId,
                        };
                        _context.OfferAnimalTypes.Add(newAnimalTypeToOffer);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return "Utworzono.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas tworzenia oferty", ex);
            }
        }

        public async Task<string> UpdateOffer(int id, OfferDto offerDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var offerEntity = await _context.Offers
                    .Include(o => o.Address)
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (offerEntity == null)
                {
                    return "null";
                }

                if (!string.IsNullOrEmpty(offerDto.Name) && offerEntity.Name != offerDto.Name)
                {
                    offerEntity.Name = offerDto.Name;
                }

                if (offerDto.File == null || offerDto.File.Length == 0)
                {
                     Console.WriteLine("nie zamieniamy pliku.");
                }
                else
                {
                        using var memoryStream = new MemoryStream();
                        await offerDto.File.CopyToAsync(memoryStream);
                        var fileBytes = memoryStream.ToArray();

                        var photo = new Photo
                        {
                            BinaryData = fileBytes,
                            Name = offerDto.File.FileName,
                            Extension = Path.GetExtension(offerDto.File.FileName),
                            Type = offerDto.File.ContentType
                        };

                        _context.Photos.Add(photo);
                        await _context.SaveChangesAsync();
                    
                }

               

                if (!string.IsNullOrEmpty(offerDto.Description) && offerEntity.Description != offerDto.Description)
                {
                    offerEntity.Description = offerDto.Description;
                }
                if (offerEntity.Price != offerDto.Price)
                {
                    offerEntity.Price = offerDto.Price;
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

                if (offerDto.OfferAnimalTypes != null && offerDto.OfferAnimalTypes.Any())
                {
                    var animalTypeToOffer = await _context.AnimalTypes
                        //.Where(at => at.Type.Equals("dog") || at.Type.Equals("cat"))
                        .Where(at => offerDto.OfferAnimalTypes.Contains(at.Type))
                        .Select(at => at.Id)
                        .ToListAsync();

                    foreach (var animalId in animalTypeToOffer)
                    {
                        var newAnimalTypeToOffer = new OfferAnimalType
                        {
                            OfferId = offerEntity.Id,
                            AnimalTypeId = animalId,
                        };
                        _context.OfferAnimalTypes.Add(newAnimalTypeToOffer);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                await _context.SaveChangesAsync();

                return "Utworzono.";
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

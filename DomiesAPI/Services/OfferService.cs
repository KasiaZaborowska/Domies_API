using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace DomiesAPI.Services
{
    public interface IOfferService
    {
        Task<List<OfferDtoRead>> GetOffers();
        Task<OfferDtoRead> GetOfferById(int id);
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

        public async Task<List<OfferDtoRead>> GetOffers()
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
                    .Include(o => o.Applications)
                     //.Include(o => o.OfferFacilities)

                     .Select(o => new OfferDtoRead
                     {
                         Id = o.Id,
                         Name = o.Name,
                         OfferDescription = o.OfferDescription,
                         PetSitterDescription = o.PetSitterDescription,
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

                         Facilities = o.Facilities.Select(f => new FacilityDto
                         {
                             Id = f.Id,
                             FacilitiesType = f.FacilitiesType,
                             FacilitiesDescription = f.FacilitiesDescription,

                         }).ToList(),

                         Applications = o.Applications.Select(a => new ApplicationDtoRead
                         {
                             Id = a.Id,
                             DateStart = a.DateStart,
                             DateEnd = a.DateEnd,
                             OfferId = a.OfferId,
                             Applicant = a.Applicant,
                             Note = a.Note,
                             Animals = a.Animals.Select(animals => new AnimalDto
                             {
                                 Id = animals.Id,
                                 PetName = animals.PetName,
                                 SpecificDescription = animals.SpecificDescription,
                                 AnimalType = animals.AnimalType,
                                 Type = animals.AnimalTypeNavigation.Type,
                             }).ToList(),
                             Opinions = a.Opinions.Select(opinion => new OpinionDto
                             {
                                 Id = opinion.Id,
                                 Rating = opinion.Rating,
                                 Comment = opinion.Comment,
                                 ApplicationId = opinion.ApplicationId,
                                 UserEmail = opinion.UserEmail
                             }).ToList()
                         }).ToList(),

                         //Applications = o.Applications

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

        public async Task<OfferDtoRead> GetOfferById(int id)
        {
            try
            {
                var offerDto = await _context.Offers
                    .Where(o => o.Id == id)
                    .Include(o => o.Address)
                    .Include(o => o.Photo)
                    .Include(o => o.Applications)
                    .ThenInclude(o => o.Animals)
                    .Include(o => o.Applications)
                    .ThenInclude(o => o.Opinions)
                     .Select(o => new OfferDtoRead
                     {
                         Id = o.Id,
                         Name = o.Name,
                         OfferDescription = o.OfferDescription,
                         PetSitterDescription = o.PetSitterDescription,
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

                         Facilities = o.Facilities.Select(f => new FacilityDto
                         {
                             Id = f.Id,
                             FacilitiesType = f.FacilitiesType,
                             FacilitiesDescription = f.FacilitiesDescription,

                         }).ToList(),

                         OfferAnimalTypes = string.Join(", ",
                            o.OfferAnimalTypes
                            .Select(oat => oat.AnimalType.Type)),

                         Applications = o.Applications.Select(a => new ApplicationDtoRead
                         {
                             Id = a.Id,
                             DateStart = a.DateStart,
                             DateEnd = a.DateEnd,
                             OfferId = a.OfferId,
                             Applicant = a.Applicant,
                             Note = a.Note,
                             ApplicationDateAdd = a.ApplicationDateAdd,
                             ApplicationStatus = a.ApplicationStatus,
                             Animals = a.Animals.Select(animals => new AnimalDto
                             {
                                 Id = animals.Id,
                                 PetName = animals.PetName,
                                 SpecificDescription = animals.SpecificDescription,
                                 AnimalType = animals.AnimalType,
                                 Type = animals.AnimalTypeNavigation.Type,
                             }).ToList(),
                             Opinions = a.Opinions.Select(opinion => new OpinionDto
                             {
                                 Id = opinion.Id,
                                 Rating = opinion.Rating,
                                 Comment = opinion.Comment,
                                 ApplicationId = opinion.ApplicationId,
                                 UserEmail = opinion.UserEmail,
                                 OpinionDateAdd = opinion.OpinionDateAdd,
                             }).ToList()
                         }).ToList(),

                         //OpinionsList = o.Applications.Select(a => new ApplicationDtoRead
                         //{
                         //    Id = a.Id,
                         //    OfferId = a.OfferId,
                         //    Applicant = a.Applicant,
                         //    Opinions = a.Opinions.Select(opinion => new OpinionDto
                         //    {
                         //        Id = opinion.Id,
                         //        Rating = opinion.Rating,
                         //        Comment = opinion.Comment,
                         //        ApplicationId = opinion.ApplicationId,
                         //        UserEmail = opinion.UserEmail
                         //    }).ToList()
                         //}).ToList(),



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

                //if (offerDto.Facilities != null && offerDto.Facilities.Any())
                //{ return "brak udogodnien."; }


                var facilitiesToOffer = await _context.Facilities
                        //.Include(f => offerDto.Facilities)
                        .Where(f => offerDto.Facilities.Contains(f.Id))
                        //.Select(f => f.Id)
                        .ToListAsync();
               



                var newOffer = new Offer
                {
                    Name = offerDto.Name,
                    OfferDescription = offerDto.OfferDescription,
                    PetSitterDescription = offerDto.PetSitterDescription,
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
                    },
                    Facilities = facilitiesToOffer

                };

                _context.Offers.Add(newOffer);
                await _context.SaveChangesAsync();

                if (offerDto.OfferAnimalTypes != null && offerDto.OfferAnimalTypes.Any())
                {
                    var animalTypeToOffer = await _context.AnimalTypes
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
                    .Include(o => o.OfferAnimalTypes)
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
                    if(offerEntity.PhotoId != null)
                    {
                        var photoEntity = await _context.Photos.FindAsync(offerEntity.PhotoId);
                        _context.Photos.Remove(photoEntity);
                    }
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

                    offerEntity.PhotoId = photo.Id;


                }

               

                if (!string.IsNullOrEmpty(offerDto.OfferDescription) && offerEntity.OfferDescription != offerDto.OfferDescription)
                {
                    offerEntity.OfferDescription = offerDto.OfferDescription;
                }
                if (!string.IsNullOrEmpty(offerDto.PetSitterDescription) && offerEntity.PetSitterDescription != offerDto.PetSitterDescription)
                {
                    offerEntity.PetSitterDescription = offerDto.PetSitterDescription;
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
                        .Where(at => offerDto.OfferAnimalTypes.Contains(at.Type))
                        .Select(at => at.Id)  
                        .ToListAsync();



                    foreach (var oldOfferAnimalType in offerEntity.OfferAnimalTypes)
                    {                       
                        if (!animalTypeToOffer.Any(
                                    animalId =>
                                        oldOfferAnimalType.OfferId == id
                                        && oldOfferAnimalType.AnimalTypeId == animalId
                                        )
                            )
                            _context.OfferAnimalTypes.Remove(oldOfferAnimalType);
                    }

                    foreach (var animalId in animalTypeToOffer)
                    {
                        var newAnimalTypeToOffer = new OfferAnimalType
                        {
                            OfferId = offerEntity.Id,
                            AnimalTypeId = animalId,
                        };
                        if ( !await _context.OfferAnimalTypes
                                .AnyAsync( 
                                    existing => 
                                        newAnimalTypeToOffer.OfferId == existing.OfferId
                                        && newAnimalTypeToOffer.AnimalTypeId == existing.AnimalTypeId 
                                        )
                            )
                        _context.OfferAnimalTypes.Add(newAnimalTypeToOffer);
                    }

                    if (offerDto.Facilities != null && offerDto.Facilities.Any())
                    {

                        var currentFacilities = await _context.Offers
                            .Where(of => of.Id == offerEntity.Id)
                            .Include(of => of.Facilities)
                            .FirstOrDefaultAsync();

                        if (currentFacilities != null)
                        {
                            currentFacilities.Facilities.Clear();
                        }

                        var facilitiesToOffer = await _context.Facilities
                       .Where(f => offerDto.Facilities.Contains(f.Id))
                       .ToListAsync();

                        
                        offerEntity.Facilities = facilitiesToOffer;

                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

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
                if (offerToDelete.PhotoId != null)
                {
                    var photoEntity = await _context.Photos.FindAsync(offerToDelete.PhotoId);
                    _context.Photos.Remove(photoEntity);
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

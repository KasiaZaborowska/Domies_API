using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace DomiesAPI.Services
{
    public interface IOpinionService
    {
        Task<List<OpinionDto>> GetOpinions();
        Task<OpinionDto> GetOpinionById(int id);
        Task<OpinionDto> CreateOpinion(OpinionDto opinionDto);
        Task<OpinionDto> UpdateOpinion(int id, OpinionDto opinionDto);
        Task<bool> DeleteOpinionById(int id);
    }
    public class OpinionService : IOpinionService
    {
        private readonly DomiesContext _context;
        public OpinionService(DomiesContext context)
        {
            _context = context;
        }

        public async Task<List<OpinionDto>> GetOpinions()
        {
            try
            {
                if (_context.Opinions == null)
                {
                    throw new ApplicationException("opinions table is not initialized.");
                }

                var opinionsDto = await _context.Opinions
                     .Select(o => new OpinionDto
                     {
                         Id = o.Id,
                         Rating = o.Rating,
                         Comment = o.Comment,
                         ApplicationId = o.ApplicationId,
                         UserEmail = o.UserEmail, // ZMIAENA NWEFAWe
                         OpinionDateAdd = o.OpinionDateAdd,
                     })
                    .ToListAsync();
                Console.WriteLine(opinionsDto);
                return opinionsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<OpinionDto> GetOpinionById(int id)
        {
            try
            {
                var opinionDto = await _context.Opinions
                    .Where(o => o.Id == id )
                     .Select(o => new OpinionDto
                     {
                         Id = o.Id,
                         Rating = o.Rating,
                         Comment = o.Comment,
                         ApplicationId = o.ApplicationId,
                         UserEmail = o.UserEmail,
                         OpinionDateAdd = o.OpinionDateAdd,
                     })
                    .FirstOrDefaultAsync();
                Console.WriteLine(opinionDto);
                return opinionDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<OpinionDto> CreateOpinion(OpinionDto opinionDto)
        {
            try
            {
                var opinionEntity = new Opinion
                {
                    Rating = opinionDto.Rating,
                    Comment = opinionDto.Comment,
                    ApplicationId = opinionDto.ApplicationId,
                    UserEmail = opinionDto.UserEmail,
                    OpinionDateAdd = opinionDto.OpinionDateAdd,

                };

                _context.Opinions.Add(opinionEntity);
                await _context.SaveChangesAsync();

                return new OpinionDto
                {
                    Rating = opinionEntity.Rating,
                    Comment = opinionEntity.Comment,
                    ApplicationId = opinionEntity.ApplicationId,
                    UserEmail = opinionEntity.UserEmail,
                    OpinionDateAdd = DateTime.Now,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas tworzenia oferty", ex);
            }
        }

        public async Task<OpinionDto> UpdateOpinion(int id, OpinionDto opinionDto)
        {
            try
            {
                var opinionEntity = await _context.Opinions
                    .FirstOrDefaultAsync(o => o.Id == id);

                if (opinionEntity == null)
                {
                    return null;
                }

                if (opinionEntity.Rating != opinionDto.Rating)
                {
                    opinionEntity.Rating = opinionDto.Rating;
                }

                if (!string.IsNullOrEmpty(opinionDto.Comment) && opinionEntity.Comment != opinionDto.Comment)
                {
                    opinionEntity.Comment = opinionDto.Comment;
                }


                //offerEntity.Title = offerDto.Title;
                //offerEntity.Photo = offerDto.Photo;
                //offerEntity.Description = offerDto.Description;


                await _context.SaveChangesAsync();

                return new OpinionDto
                {
                    Rating = opinionDto.Rating,
                    Comment = opinionDto.Comment,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas edytowania opinii", ex);
            }
        }

        public async Task<bool> DeleteOpinionById(int id)
        {
            try
            {
                var opinionToDelete = await _context.Opinions
                     .FirstOrDefaultAsync(o => o.Id == id);

                if (opinionToDelete == null)
                {
                    return false;
                }
                _context.Opinions.Remove(opinionToDelete);
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

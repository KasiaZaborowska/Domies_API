using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DomiesAPI.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers(String userEmail);
        Task<UserDto> GetUserById(string email, String userEmail);
        Task<string> ChangeRole(string email, int newRole, String userEmail);
        //Task<string> CreateUser(UserDto userDto, String userEmail);
        //Task<string> UpdateUser(string email, UserDto userDto, String userEmail);
        // Task<ApplicationDto> UpdateApplication(int id, ApplicationDto applicationDto);
        Task<bool> DeleteUserById(string email, String userEmail);
    }

    public class UserService : IUserService
    {
        private readonly DomiesContext _context;
        public UserService(DomiesContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetUsers(String userEmail)
        {
            try
            {
                if (_context.Applications == null)
                {
                    throw new ApplicationException("Users table is not initialized.");
                }

                var userDto = await _context.Users
                    .Include(u => u.Role)
                    .Where(u => u.Role.Name != "Admin")
                     .Select(u => new UserDto
                     {
                         Email = u.Email,
                         FirstName = u.FirstName,
                         LastName = u.LastName,
                         PhoneNumber = u.PhoneNumber,
                         RoleId = u.RoleId,
                         RoleName = u.Role.Name,
                         DateAdd = u.DateAdd,
                     })
                    .ToListAsync();
                Console.WriteLine(userDto);
                return userDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        public async Task<UserDto> GetUserById(string email, String userEmail)
        {
            try
            {
                var userDto = await _context.Users
                    .Include(u => u.Role)
                    .Where(a => a.Email == email)
                     .Select(u => new UserDto
                     {
                         Email = u.Email,
                         FirstName = u.FirstName,
                         LastName = u.LastName,
                         PhoneNumber = u.PhoneNumber,
                         RoleId = u.RoleId,
                         RoleName = u.Role.Name,
                         DateAdd = u.DateAdd,
                     })
                    .FirstOrDefaultAsync();
                Console.WriteLine(userDto);
                return userDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

        //public async Task<string> CreateUser(UserDto userDto, String userEmail)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync();

        //    try
        //    {
        //        var userEntity = new User
        //        {
        //            Email = userDto.Email,
        //            FirstName = userDto.FirstName,
        //            LastName = userDto.LastName,
        //            PhoneNumber = userDto.PhoneNumber,
        //            RoleId = userDto.RoleId,
        //            DateAdd = DateTime.Now,
        //        };


        //        _context.Users.Add(userEntity);
        //        await _context.SaveChangesAsync();
        //        await transaction.CommitAsync();

        //        return "Stworzono aplikację.";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        //        throw new ApplicationException("Błąd podczas tworzenia aplikacji", ex);
        //    }
        //}

        //public async Task<string> UpdateUser(string email, UserDto userDto, String userEmail)
        //{
        //    try
        //    {
        //        var userEntity = await _context.Users
        //            .FirstOrDefaultAsync(u => u.Email == email);

        //        if (userEntity == null)
        //        {
        //            return null;
        //        }

        //        userEntity.Email = userDto.Email;
        //        userEntity.FirstName = userDto.FirstName;
        //        userEntity.LastName = userDto.LastName;
        //        userEntity.PhoneNumber = userDto.PhoneNumber;
        //        userEntity.RoleId = userDto.RoleId;

        //        await _context.SaveChangesAsync();

        //        return "Edytowano.";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Wystąpił błąd: {ex.Message}");
        //        throw new ApplicationException("Błąd podczas edytowania oferty", ex);
        //    }
        //}


        public async Task<string> ChangeRole(string email, int newRole, String userEmail)
        {
            try
            {
                var userEntity = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (userEntity == null)
                {
                    return "Nie ma takiego użytkownika.";
                }

                userEntity.RoleId = newRole;

                await _context.SaveChangesAsync();

                return "Zmieniono rolę.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas zmiany roli", ex);
            }
        }


        public async Task<bool> DeleteUserById(string email, String userEmail)
        {
            try
            {
                var userToDelete = await _context.Users
                     //.Include(o => o.Address)
                     .Where(a => a.Email == email)
                     .Where(u => u.Role.Name != "Admin")
                     .FirstOrDefaultAsync(o => o.Email == email);

                if (userToDelete == null)
                {
                    return false;
                }
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    var animalsToDelete = _context.Animals.Where(p => p.Owner == email).ToList();

                    _context.Animals.RemoveRange(animalsToDelete);

                    _context.SaveChanges();

                    _context.Users.Remove(userToDelete);
                    await _context.SaveChangesAsync();

  
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);
            }
        }

    }

}

﻿using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DomiesAPI.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers(String userEmail);
        Task<UserDto> GetUserById(string email, String userEmail);
        Task<string> ChangeRole(string email, int newRole, String userEmail);
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


        public async Task<bool> DeleteUserById(String email, String userEmail)
        {
            try
            {
                var userToDelete = await _context.Users
                     //.Where(a => a.Email.Equals(email))
                     .Where(u => !u.Role.Name.Equals("Admin"))
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

                    await _context.SaveChangesAsync();

                    var opinionsToDelete = _context.Opinions.Where(p => p.UserEmail == email).ToList();

                    _context.Opinions.RemoveRange(opinionsToDelete);

                    await _context.SaveChangesAsync();

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

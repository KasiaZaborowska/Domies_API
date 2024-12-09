using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DomiesAPI.Services
{
    public interface IUserService
    {
        Task<string> RegisterUser(UserDto userDto);
        Task<string> Login(UserDto userDto);
    }
    public class UserService : IUserService
    {
        private readonly DomiesContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        public UserService(DomiesContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<string> RegisterUser(UserDto userDto)
        {
            //try
            //{
                var roleExists = _context.Roles.Any(r => r.RoleId == userDto.RoleId);
                if (!roleExists)
                {
                    throw new Exception($"Rola z Id {userDto.RoleId} nie istnieje.");
                }
                var newUser = new User()
                {
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    RoleId = userDto.RoleId
                };
                var hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

                newUser.Password = hashedPassword;
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return $"Nowy użytkownik utworzony {newUser.Email}";

            //} catch (Exception ex) 
            //{
            //    // Logowanie błędu
            //    Console.WriteLine(ex.Message);
            //    return ex.Message;
            //}

            
            
        }

        public async Task<string> Login(UserDto userDto)
        {
            try
            {
                var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == userDto.Email);

                Console.WriteLine(user.Role.Name);

                if (user == null)
                {
                    return "Niepoprawny email lub hasło";
                }

                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, userDto.Password);
                if (result == PasswordVerificationResult.Failed)
                {
                    return "Niepoprawny email lub hasło";
                }
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                };


                var jwtSettings = _configuration.GetSection("JwtSettings");
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["JwtSecretKey"]));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToInt32(jwtSettings["JwtExpireDays"]));
                var token = new JwtSecurityToken(jwtSettings["JwtIssuer"],
                     jwtSettings["JwtIssuer"],
                    claims,
                    expires: expires,
                    signingCredentials: cred
                    );

                var tokenHandler = new JwtSecurityTokenHandler();
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
    }
}

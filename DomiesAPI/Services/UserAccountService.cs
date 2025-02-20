using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DomiesAPI.Services
{
    public interface IUserAccountService
    {
        static string getLoggedInUserEmail(HttpContext httpContext)
        {
            var userEmail = httpContext?.User.FindFirst("Email")?.Value;
            return userEmail ?? string.Empty;
        }
        Task<string> RegisterUser(UserDto userDto, string verificationToken);
        Task<string> Login(LoginDto loginDto);
        Task<string> VerifyUserEmail(string token);
    }
    public class UserAccountService : IUserAccountService
    {
        private readonly DomiesContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        public UserAccountService(DomiesContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        

        public async Task<string> RegisterUser(UserDto userDto, string verificationToken)
        {
            try
            
            {
           User userFromDb = _context.Users
           .FirstOrDefault(u => u.Email.ToLower() == userDto.Email.ToLower());

            if (userFromDb != null)
            {
                throw new ArgumentException("Użytkownik z danym mailem już istnieje.");
                
            }

            var roleExists = _context.Roles.Any(r => r.RoleId == userDto.RoleId);
                if (!roleExists)
                {
                    throw new ArgumentException($"Rola z Id {userDto.RoleId} nie istnieje.");
                }

                var newUser = new User()
                {
                    Email = userDto.Email,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    PhoneNumber = userDto.PhoneNumber,
                    RoleId = 1,
                    IsEmailVerified = false,
                    EmailVerificationToken = verificationToken,
                };
                var hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

                newUser.Password = hashedPassword;
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return $"Nowy użytkownik utworzony {newUser.Email}";

            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Błąd podczas rejestracji", ex);
                
            }



        }

        public async Task<string> VerifyUserEmail(string Token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == Token);
            if (user == null)
                return "Niepoprawny token";

            user.IsEmailVerified = true;
            await _context.SaveChangesAsync();

            return "Twoje konto zostało zweryfikowane!";
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = _context.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
            {
                throw new ArgumentException("Użytkownik nie istnieje.");
            }

            if (user.IsEmailVerified == false)
            {
                throw new ArgumentException("Zweryfikuj swoje konto.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Niepoprawny email lub hasło.");
            }
            var claims = new List<Claim>()
            {
                new Claim("Email", user.Email.ToString()),
                new Claim("FirstName",user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Role", user.Role.Name),
                //new Claim(ClaimTypes.NameIdentifier, user.Email.ToString()),
                //new Claim("FirstName",user.FirstName),
                //new Claim("LastName", user.LastName),
                ////new Claim(ClaimTypes.Name ,$"{user.FirstName} {user.LastName}"),
                //new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
            };

            //var identity = new ClaimsIdentity(claims, "login");
            //var principal = new ClaimsPrincipal(identity);
                


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
    }
}

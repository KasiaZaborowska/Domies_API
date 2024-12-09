using DomiesAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DomiesAPI
{
    public class DomiesSeeder
    {
        private readonly DomiesContext _context;
        public DomiesSeeder(DomiesContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                //var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                //if (pendingMigrations != null && pendingMigrations.Any())   // czy tab z migracjami jest pusta
                //{
                //    _dbContext.Database.Migrate();
                //}

                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }

                if (!_context.Addresses.Any())
                {
                    var address = GetAddress();
                    _context.Addresses.AddRange(address);
                    _context.SaveChanges();
                }

                if (!_context.Users.Any())
                {
                    var users = GetUsers();
                    _context.Users.AddRange(users);
                    _context.SaveChanges();
                }


                if (!_context.Offers.Any())
                {
                    var offers = GetOffers();
                    _context.Offers.AddRange(offers);
                    _context.SaveChanges();
                }

            }
            else
            {
                Console.WriteLine("Roles already exists.");
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };

            return roles;
        }

        private IEnumerable<Address> GetAddress()
        {
            var address = new List<Address>()
            {
                new Address()
                {
                    Country = "Polska",
                    City = "Warszawa",
                    Street = "Aleje Jerozolimskie",
                    PostalCode = "42-100"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Łódź",
                    Street = "Struga 3",
                    PostalCode = "42-313"

                }
            };

            return address;
        }

        private IEnumerable<Offer> GetOffers()
        {
            var offers = new List<Offer>()
            {
                new Offer()
                {
                    Title = "Kasia",
                    Photo = "photo",
                    Description ="Description Description Description",
                    AddressId = 1,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                }
            };

            return offers;
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Email = "email@gmail.com",
                    FirstName ="Natalia",
                    LastName = "Nowak",
                    Password = "password",
                    RoleId = 1,
                    DateAdd= DateTime.Now,
                }
            };

            return users;
        }


    }
}

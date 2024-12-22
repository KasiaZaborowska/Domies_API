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
                    Street = "Juliana Konstantego Ordona 9",
                    PostalCode = "01-224"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Warszawa",
                    Street = "Marcina Kasprzaka 96",
                    PostalCode = "01-234"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Warszawa",
                    Street = "Nowolipie",
                    PostalCode = "00-150"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Warszawa",
                    Street = "Nowowiejska 26A",
                    PostalCode = "00-911"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Łódź",
                    Street = "Wodna 38",
                    PostalCode = "90-046"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Łódź",
                    Street = "Siedlecka 3",
                    PostalCode = "93-138"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Łódź",
                    Street = "Wierzbowa 42",
                    PostalCode = "90-001"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Poznań",
                    Street = "Świętego Jerzego 3",
                    PostalCode = "61-546"

                },
                new Address()
                {
                    Country = "Polska",
                    City = "Poznań",
                    Street = "Dolna Wilda 40",
                    PostalCode = "61-552"

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
                    Photo = "/Images/offer_1.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2011,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Magdalena",
                    Photo = "/Images/offer_2.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2012,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Piotrek",
                    Photo = "/Images/offer_3.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2013,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Kasia",
                    Photo = "/Images/offer_4.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2014,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Magda",
                    Photo = "/Images/offer_5.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2015,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Karol",
                    Photo = "/Images/offer_6.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2016,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Krzysztof",
                    Photo = "/Images/offer_7.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2017,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Ola",
                    Photo = "/Images/offer_8.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2018,
                    Host = "email@gmail.com",
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Title = "Kamila",
                    Photo = "/Images/offer_9.jpg",
                    Description ="Cześć! Jestem osobą, która uczy się zaocznie i pracuje zdalnie, co daje mi możliwość poświęcenia dużo czasu na opiekę nad Twoim pupilem. Mam już duże doświadczenie w opiece nad zwierzętami, ponieważ mam dwa kotki i pieska, które są spokojne i przyjaźnie nastawione do innych zwierząt. Mieszkam w domu z dużym ogródkiem, więc Twój pupil będzie miał dużo przestrzeni do zabawy i wypoczynku. Okolica jest cicha i spokojna, a w pobliżu znajdują się liczne parki oraz ścieżki leśne, które chętnie pokażę Twojemu zwierzakowi.\r\n\r\nZależy mi na tym, aby Twój pupil czuł się u mnie jak w domu, więc zapewnię mu komfort i troskliwą opiekę. Wierzę, że będzie chętnie wracał!\r\n\r\nZajmuję się także zwierzakami starszymi i chorymi, które wymagają specjalnej opieki, podawania leków czy wykonywania innych procedur pielęgnacyjnych. Mam duże serce i pełne zrozumienie dla ich potrzeb, traktując każdego pupila jak członka rodziny. Serdecznie zapraszam do kontaktu!",
                    AddressId = 2019,
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

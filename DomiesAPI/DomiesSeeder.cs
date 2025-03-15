using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;

namespace DomiesAPI
{
    public class DomiesSeeder
    {
        private readonly DomiesContext _context;
        private readonly IWebHostEnvironment _env;

        public DomiesSeeder(DomiesContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //public async Task SeedPhotos()
        //{
        //    if (_context.Database.CanConnect())
        //    {
        //        var photos = await GetPhotos();

        //        if (photos.Any())
        //        {
        //            _context.Photos.AddRange(photos);
        //            await _context.SaveChangesAsync();
        //            Console.WriteLine($"{photos.Count()} zdjęć zostało dodanych do bazy!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Nie dodano żadnych zdjęć.");
        //        }
        //    }
        //}

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

                if (!_context.Facilities.Any())
                {
                    var facilities = GetFacilities();
                    _context.Facilities.AddRange(facilities);
                    _context.SaveChanges();
                }

                if (!_context.AnimalTypes.Any())
                {
                    var animalTypes = GetAnimalTypes();
                    _context.AnimalTypes.AddRange(animalTypes);
                    _context.SaveChanges();
                }

                if (!_context.Users.Any())
                {
                    var users = GetUsers();
                    _context.Users.AddRange(users);
                    _context.SaveChanges();
                }
                if (!_context.Addresses.Any())
                {
                    var address = GetAddress();
                    _context.Addresses.AddRange(address);
                    _context.SaveChanges();
                }

                if (!_context.Animals.Any())
                {
                    var animals = GetAnimals();
                    _context.Animals.AddRange(animals);
                    _context.SaveChanges();
                }

                if (!_context.Photos.Any())
                {
                    var photos = GetPhotos();
                    if (photos.Any())
                    {
                        _context.Photos.AddRange(photos);
                        _context.SaveChanges();
                        Console.WriteLine($"{photos.Count()} zdjęć zostało dodanych do bazy!");
                    }
                    else
                    {
                        Console.WriteLine("Nie dodano żadnych zdjęć.");
                    }
                }
                

                if (!_context.Offers.Any())
                {
                    var offers = GetOffers();
                    _context.Offers.AddRange(offers);
                    _context.SaveChanges();
                    
                }
                if (!_context.Applications.Any())
                {
                    var appliccations = GetApplications();
                    _context.Applications.AddRange(appliccations);
                    _context.SaveChanges();     
                }
               
                
                if (!_context.Opinions.Any())
                {
                    var opinions = GetOpinions();
                    _context.Opinions.AddRange(opinions);
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
                    //RoleId = 1,
                    Name = "User"
                },
                new Role()
                {
                    //RoleId = 2,
                    Name = "Manager"
                },
                new Role()
                {
                    //RoleId = 3,
                    Name = "Admin"
                },
            };

            return roles;
        }
        private IEnumerable<Facility> GetFacilities()
        {
            var facilities = new List<Facility>()
            {
                new Facility()
                {
                    //Id = 1,
                    FacilitiesType = "home_type",
                    FacilitiesDescription = "Dom"
                },
                 new Facility()
                {
                     //Id = 2,
                    FacilitiesType = "home_type",
                    FacilitiesDescription = "Duży ogród"
                },
                new Facility()
                {
                    //Id = 3,
                    FacilitiesType = "no_restrictions ",
                    FacilitiesDescription = "Pozwalam wejść na kanapę"
                },
                 new Facility()
                {
                     //Id = 4,
                    FacilitiesType = "environment",
                    FacilitiesDescription = "Brak małych dzieci"
                },new Facility()
                {
                    //Id = 5,
                    FacilitiesType = "environment",
                    FacilitiesDescription = "Brak toksycznych roślin"
                },
                 new Facility()
                {
                     //Id = 6,
                    FacilitiesType = "environment",
                    FacilitiesDescription = "Opiekun niepalący"
                },
            };

            return facilities;
        }
        private IEnumerable<AnimalType> GetAnimalTypes()
        {
            var animalTypes = new List<AnimalType>()
            {
                new AnimalType()
                {
                    //Id = 1,
                    Type = "Pies",
                },
                new AnimalType()
                {
                    //Id = 2,
                    Type = "Kot",
                },
                new AnimalType()
                {
                    //Id = 3,
                    Type = "Gryzoń",
                },
                new AnimalType()
                {
                    //Id = 4,
                    Type = "Gad",
                },
                new AnimalType()
                {
                    //Id = 5,
                    Type = "Płaz",
                },
                new AnimalType()
                {
                    //Id = 6,
                    Type = "Rybki",
                },
            };

            return animalTypes;
        }
        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            { 
                new User()
                {
                    Email = "admin@gmail.com",
                    FirstName ="Admin",
                    LastName = "Admin",
                    Password = "passwordAdmin",
                    PhoneNumber = "353456339",
                    IsEmailVerified = true,
                    EmailVerificationToken = "t3jc93t3cf-htyrjhar2zer1qkqd-pxh8hwbv",
                    RoleId = 3,
                    DateAdd= DateTime.Now,
                },
                new User()
                {
                    Email = "moderator@gmail.com",
                    FirstName ="Moderator",
                    LastName = "Moderator",
                    Password = "passwordModerator",
                    PhoneNumber = "900456323",
                    IsEmailVerified = true,
                    EmailVerificationToken = "86o57jysqg-gdvuct0sqhzc7-7d4819j7d8gf",
                    RoleId = 2,
                    DateAdd= DateTime.Now,
                },
                new User()
                {
                    Email = "test@gmail.com",
                    FirstName ="Natalia",
                    LastName = "Nowak",
                    PhoneNumber = "800455789",
                    IsEmailVerified = true,
                    EmailVerificationToken = "00e2f-7d3mjq6zlsj1u-3cu4c4oi1-tcmjqi1",
                    Password = "password",
                    RoleId = 1,
                    DateAdd= DateTime.Now,
                },
                new User()
                {
                    Email = "petsitter@gmail.com",
                    FirstName ="Krzysztof",
                    LastName = "Kaczmarek",
                    PhoneNumber = "555455733",
                    IsEmailVerified = true,
                    EmailVerificationToken = "agjhd-4cb3tkqp94-zpjttuzkkfgorxjvyepw",
                    Password = "password",
                    RoleId = 1,
                    DateAdd= DateTime.Now,
                },
                new User()
                {
                    Email = "petowner@gmail.com",
                    FirstName ="Kornelia",
                    LastName = "Mucha",
                    PhoneNumber = "336775733",
                    IsEmailVerified = true,
                    EmailVerificationToken = "xqtelts-elgzaku805mew6ct-4myovssvh3g",
                    Password = "password",
                    RoleId = 1,
                    DateAdd= DateTime.Now,
                },
            };

            return users;
        }

        private IEnumerable<Animal> GetAnimals()
        {
            var animals = new List<Animal>()
            {
                new Animal()
                {
                    PetName = "Max",
                    SpecificDescription = "Max to przyjazny i łagodny golden retriever, który uwielbia towarzystwo ludzi. Ma dużo energii i potrzebuje codziennych spacerów oraz zabaw. Jest dobrze wychowany, zna podstawowe komendy i dogaduje się z dziećmi. Szuka opiekuna, który zapewni mu aktywność i czułość.",
                    Owner = "petowner@gmail.com",
                    AnimalType = 1

                },
                 new Animal()
                {
                    PetName = "Borys",
                    SpecificDescription = "Borys to przyjazny i łagodny golden retriever, który uwielbia towarzystwo ludzi. Ma dużo energii i potrzebuje codziennych spacerów oraz zabaw. Jest dobrze wychowany, zna podstawowe komendy i dogaduje się z dziećmi. Szuka opiekuna, który zapewni mu aktywność i czułość.",
                    Owner = "test@gmail.com",
                    AnimalType = 1

                },
                 new Animal()
                {
                    PetName = "Mars",
                    SpecificDescription = "Mars to przyjazny i łagodny golden retriever, który uwielbia towarzystwo ludzi. Ma dużo energii i potrzebuje codziennych spacerów oraz zabaw. Jest dobrze wychowany, zna podstawowe komendy i dogaduje się z dziećmi. Szuka opiekuna, który zapewni mu aktywność i czułość.",
                    Owner = "petsitter@gmail.com",
                    AnimalType = 1

                },
                new Animal()
                {
                    PetName = "Luna",
                    SpecificDescription = "Luna to piękna husky, pełna energii i ciekawości świata. Uwielbia długie spacery i bieganie, ale bywa uparta, dlatego potrzebuje doświadczonego opiekuna. Nie lubi samotności, więc najlepiej odnajdzie się u osoby aktywnej, która ma czas na wspólne przygody.",
                    Owner = "petowner@gmail.com",
                    AnimalType = 1

                },new Animal()
                {   
                    PetName = "Milo",
                    SpecificDescription = "Milo to spokojny kot, który ceni sobie wygodę i domowe ciepło. Jest przyjazny, ale niezależny – lubi pieszczoty, ale tylko wtedy, gdy ma na to ochotę. Idealny dla osoby, która szuka towarzysza na spokojne wieczory. Szuka opiekuna, który zadba o jego miseczkę i wygodne legowisko.",
                    Owner = "petowner@gmail.com",
                    AnimalType = 2

                },
                new Animal()
                {
                    PetName = "Koko",
                    SpecificDescription = "Koko to uroczy królik o miękkim futerku i dużych, opadających uszach. Jest łagodny i towarzyski, lubi głaskanie i zabawę. Potrzebuje opiekuna, który zapewni mu przestrzeń do skakania i odpowiednią dietę pełną świeżych warzyw. Idealny dla miłośnika małych zwierząt.",
                    Owner = "petowner@gmail.com",
                    AnimalType = 3

                },
                new Animal()
                {
                    PetName = "Spike",
                    SpecificDescription = "Spike to spokojna agama, która lubi ciepło i odpoczynek pod lampą UV. Nie wymaga dużo uwagi, ale potrzebuje odpowiednich warunków w terrarium. To świetny wybór dla kogoś, kto chce mieć egzotycznego i łatwego w utrzymaniu podopiecznego.",
                    Owner = "petowner@gmail.com",
                    AnimalType = 4

                },new Animal()
                {
                    PetName = "Tito",
                    SpecificDescription = "Tito to fascynujący wodny stworek o uroczym wyglądzie i spokojnym usposobieniu. Jest łatwy w utrzymaniu, ale wymaga czystej wody i odpowiedniej temperatury. Idealny dla kogoś, kto lubi obserwować niezwykłe zwierzęta i dbać o ich środowisko.",
                    Owner = "petowner@gmail.com",
                    AnimalType = 5

                },
                new Animal()
                {
                    PetName = "Bąbel",
                    SpecificDescription = "Bąbel i Płotka to dwie złote rybki, które dodają uroku każdemu akwarium. Są spokojne, ale potrzebują odpowiednio dużego zbiornika i regularnej pielęgnacji. Szukają opiekuna, który zadba o ich wodne królestwo i odpowiednie karmienie.",
                    Owner = "petowner@gmail.com",
                    AnimalType = 6

                },
            };

            return animals;
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

        static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length / 2)
                             .Select(i => Convert.ToByte(hex.Substring(i * 2, 2), 16))
                             .ToArray();
        }
        private  IEnumerable<Photo> GetPhotos()
        {
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            var photoNames = new List<string> { 
                "01_man_with_cat.jpg", 
                "02_girl_with_cat.jpg", 
                "03_girl_with_cat_yellow.jpg", 
                "04_girl_with_dog.jpg",
                "05_girl_with_dog_sunset.jpg",
                "06_lady_exotic_animals.jpg",
                "07_spiders.jpg",
                "08_fishes.jpg"
            };

            var photos = new List<Photo>();

            foreach (var photoName in photoNames)
            {
                var imagePath = Path.Combine(imagesPath, photoName);

                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"Plik nie istnieje {imagePath}");
                    continue;
                }
                byte[]? bytes = null;

                try
                {
                    bytes = File.ReadAllBytes(imagePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas odczytu pliku {photoName}: {ex.Message}");
                }

                if (photoName == null || photoName.Length == 0)
                {
                    Console.WriteLine($"Plik {photoName} jest pusty lub nie udało się go wczytać.");
                    continue;
                }
                photos.Add(new Photo
                {
                    BinaryData = bytes,
                    Name = photoName,
                    Extension = Path.GetExtension(photoName),
                    Type = "image/jpeg"
                });



            }

            return photos;
        }

        private List<Facility> FetchFacilityList(List<int> ids)
        {
            return _context.Facilities
                //.AsNoTracking() 
                .Where(f => ids.Contains(f.Id))
                .ToList();
        }

        private IEnumerable<Offer> GetOffers()
        {
            

            var offers = new List<Offer>()
            { 
                
                new Offer()
                {
                    Name = "Adam",
                    OfferDescription  ="Jeśli szukasz troskliwego i doświadczonego opiekuna dla swojego kota, to dobrze trafiłeś! Jestem miłym i odpowiedzialnym miłośnikiem kotów, który chętnie zaopiekuje się Twoim pupilem podczas Twojej nieobecności. Mój kot jest bardzo towarzyski i uwielbia kontakt z innymi zwierzętami, dlatego może stanowić doskonałe towarzystwo dla Twojego pupila. Oferuję kompleksową opiekę, w tym karmienie, czyszczenie kuwety oraz długie sesje zabaw i pieszczot. W razie potrzeby mogę podawać lekarstwa lub zadbać o specjalną dietę Twojego kota. Jestem dostępny zarówno na długoterminową, jak i krótkoterminową opiekę, a także mogę odwiedzać Twojego kota w jego domu. Zapewniam codzienne aktualizacje i zdjęcia, abyś miał pewność, że Twój kot jest w najlepszych rękach.",
                    PetSitterDescription = "Od zawsze uwielbiam koty i ich towarzystwo. W moim domu mieszka niezwykle przyjazny i kontaktowy kot, który chętnie wita nowych przyjaciół i lubi zabawy z innymi futrzanymi towarzyszami. Mam doświadczenie w opiece nad kotami o różnych temperamentach - od energicznych i ciekawskich po spokojne i wycofane. Znam się na kociej diecie, pielęgnacji oraz zdrowiu, a w razie potrzeby współpracuję z weterynarzem. Jestem osobą cierpliwą, odpowiedzialną i dbającą o potrzeby każdego zwierzaka. Opieka nad kotami to dla mnie nie tylko obowiązek, ale i prawdziwa pasja. Uwielbiam spędzać czas na wspólnych zabawach, drapaniu za uchem i zapewnianiu im poczucia bezpieczeństwa..",
                    AddressId = 1,
                    Host = "petsitter@gmail.com",
                    Price = 129.99m,
                    PhotoId = 1,
                    Facilities = FetchFacilityList(new List<int> { 3, 4, 5, 6 }),
                    //Facilities = [3, 4, 5, 6],
                    OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 1, AnimalTypeId = 2 }
                    },
                    //OfferAnimalTypes = ["Kot"],
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Name = "Marta",
                    OfferDescription  = "Cześć! Nazywam się Marta i jestem cichą, spokojną osobą, która z przyjemnością zajmuje się kotami i gryzoniami. Posiadam kota, który jest bardzo przyjacielski, ale również ceni sobie ciszę i spokój. Oferuję profesjonalną i troskliwą opiekę, w tym regularne karmienie, sprzątanie klatek i kuwet, a także długie sesje pieszczot i zabawy. Mam doświadczenie w opiece nad kotami oraz małymi zwierzętami, takimi jak chomiki, świnki morskie, myszki czy szczury. Zadbam o odpowiednie warunki dla Twojego pupila, a także dostosuję się do jego indywidualnych potrzeb. Jeśli Twój zwierzak wymaga specjalnej diety lub przyjmowania leków, mogę się tym zająć.",
                    PetSitterDescription = "Jestem osobą spokojną, cichą i bardzo ciepłą wobec zwierząt. Od dziecka kocham koty i gryzonie, a opieka nad nimi to dla mnie czysta przyjemność. Wiem, że każde zwierzę ma swój unikalny charakter i potrzeby, dlatego zawsze dostosowuję się do jego temperamentu. Potrafię zdobyć zaufanie nawet najbardziej nieufnych czworonogów, dając im czas i przestrzeń do oswojenia się. Moje podejście to spokój, cierpliwość i delikatność..",
                    AddressId = 2,
                    Host = "petsitter@gmail.com",
                    Price = 100.00m,
                    PhotoId = 2,
                    Facilities = FetchFacilityList(new List<int> { 3, 4, 5, 6 }),
                    //Facilities = [3, 4, 5, 6],
                    OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 2, AnimalTypeId = 2 },
                            new OfferAnimalType { OfferId = 2, AnimalTypeId = 3 },
                            new OfferAnimalType { OfferId = 2, AnimalTypeId = 6 },
                    },
                    //Facilities = [3, 4, 5, 6],
                    //OfferAnimalTypes = [ "Kot", "Gryzoń", "Rybki"],
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Name = "Julia",
                    OfferDescription  = "Cześć! Nazywam się Julia i mam doświadczenie w opiece nad kotami, psami, gryzoniami oraz rybkami. Moja oferta jest kompleksowa i obejmuje opiekę nad wieloma gatunkami zwierząt, zarówno domowymi, jak i wodnymi. Zajmuję się zwierzętami z pełnym zaangażowaniem, dbając o ich potrzeby fizyczne i emocjonalne. Mój kot to bardzo przyjazne stworzenie, które uwielbia spędzać czas z innymi zwierzętami, co sprawia, że idealnie nadaje się do kontaktu z nowymi towarzyszami. Jeśli masz kota, psa, gryzonia czy rybki, z przyjemnością się nimi zaopiekuję. Moje doświadczenie pozwala mi na dostosowanie opieki do indywidualnych potrzeb każdego zwierzaka. Dla psów oferuję regularne spacery i zabawy, które pozwalają im na aktywność fizyczną oraz spalenie energii. Kotom zapewnię spokój i odpowiednią przestrzeń do odpoczynku oraz zabawy, dbając o ich komfort w każdym aspekcie. Gryzonie, takie jak chomiki czy świnki morskie, będą miały zapewnione odpowiednie warunki do życia oraz czas na interakcje i zabawę.",
                    PetSitterDescription = "Jestem studentką weterynarii, więc mam solidną wiedzę na temat potrzeb różnych zwierząt. Moja edukacja daje mi szeroką bazę teoretyczną oraz praktyczną, dzięki czemu jestem w stanie skutecznie dbać o zdrowie i dobrostan zwierząt. Pasjonuję się zarówno zwierzętami lądowymi, jak i wodnymi, co pozwala mi zapewnić kompleksową opiekę wszystkim gatunkom. Od kilku lat zajmuję się zwierzętami domowymi, co daje mi cenne doświadczenie w zakresie ich pielęgnacji i wychowywania. Prowadzę także kilka akwariów, dzięki czemu wiem, jak odpowiednio dbać o rybki i inne wodne stworzenia. Moje doświadczenie obejmuje szeroki zakres opieki – od karmienia, przez pielęgnację, aż po zabawę. Każde zwierzę traktuję indywidualnie, bo wiem, że każde ma swoje unikalne potrzeby. Moje podejście jest pełne pasji i zaangażowania, co sprawia, że zwierzęta czują się ze mną bezpiecznie.",
                    AddressId = 3,
                    Host = "petsitter@gmail.com",
                    Price = 149.99m,
                    PhotoId = 3,
                    Facilities = FetchFacilityList(new List<int> {1, 3, 4, 5, 6}),
                    //Facilities = [1, 3, 4, 5, 6],
                     OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 3, AnimalTypeId = 1 },
                            new OfferAnimalType { OfferId = 3, AnimalTypeId = 2 },
                            new OfferAnimalType { OfferId = 3, AnimalTypeId = 3 },
                            new OfferAnimalType { OfferId = 3, AnimalTypeId = 6 },
                    },
                    //OfferAnimalTypes = ["Pies", "Kot", "Gryzoń", "Rybki"],
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Name = "Ania",
                    OfferDescription  = "Witam! Jestem Ania i posiadam psa, który jest moim wiernym towarzyszem. Zajmę się Twoim psem z pełnym zaangażowaniem, oferując spacery, zabawę oraz odpowiednią dietę. Moja oferta jest skierowana do właścicieli psów, którzy chcą, aby ich pupile czuli się komfortowo podczas ich nieobecności. Oferuję opiekę przez całą dobę, dbając o to, aby Twój pies miał wystarczająco dużo ruchu i interakcji, które są niezbędne dla jego zdrowia i dobrego samopoczucia. Wiem, jak ważne jest, by psy miały regularne spacery, dzięki którym będą mogły się wyszaleć i poznać nowe zapachy. Podchodzę do każdego psa indywidualnie, bo wiem, że każdy ma inne potrzeby. Oferuję również zabawę, która pomoże Twojemu psu się zrelaksować i spalić nadmiar energii. Kiedy Twój pies będzie ze mną, zapewnię mu także odpowiednią dietę dostosowaną do jego wieku i wymagań zdrowotnych. Zadbam o to, by czuł się kochany i bezpieczny, a także miał wszystko, czego potrzebuje do dobrego samopoczucia. Spędzając czas z psem, staram się budować z nim więź opartą na zaufaniu, aby czuł się komfortowo w moim towarzystwie. Wiem, jak ważne jest, by psy miały poczucie bezpieczeństwa, dlatego tworzymy razem przestrzeń, w której Twój pupil poczuje się jak w domu. Dzięki mojej opiece, Twój pies będzie miał zapewniony pełen komfort, a Ty będziesz spokojny o jego dobrostan podczas Twojej nieobecności.",
                    PetSitterDescription = "Jestem młodą, energiczną osobą, która uwielbia psy. Na co dzień pracuję w firmie marketingowej, ale spędzam każdą wolną chwilę z moim psem, który jest moim najlepszym przyjacielem. Posiadam doświadczenie w opiece nad psami i wiem, jak zadbać o ich potrzeby emocjonalne i fizyczne. Dzięki mojemu podejściu, zwierzęta czują się swobodnie i bezpiecznie. W pracy z psami kieruję się cierpliwością, empatią i szacunkiem do ich natury. Wiem, jak ważne są codzienne spacery, zabawy i regularna interakcja, by pies czuł się szczęśliwy i zdrowy. Posiadam także wiedzę na temat ich diet i potrafię dostosować ją do indywidualnych potrzeb każdego psa. Uwielbiam spędzać czas na świeżym powietrzu, co pozwala mi również na aktywności fizyczne z psem. Często korzystam z okazji, by przejść się z psem na długie spacery, w trakcie których mamy okazję na wspólne zabawy i relaks. Zajmowanie się psem to dla mnie prawdziwa pasja, dlatego podchodzę do tego z pełnym zaangażowaniem.",
                    AddressId = 4,
                    Host = "petsitter@gmail.com",
                    Price = 89.99m,
                    PhotoId = 4,
                    Facilities = FetchFacilityList(new List<int> {1, 2, 3, 4, 5}),
                    //Facilities = [1, 2, 3, 4, 5],
                    OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 4, AnimalTypeId = 1 },
                    },
                    //OfferAnimalTypes = ["Pies"],
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Name = "Kasia",
                    OfferDescription  = "Cześć, nazywam się Kasia i jestem osobą, która posiada zarówno psa, jak i koty, co sprawia, że mam duże doświadczenie w opiece nad zwierzętami. Moja oferta jest skierowana do właścicieli psów i kotów, którzy szukają profesjonalnej opieki dla swoich pupili. Zajmuję się wszystkimi zwierzętami, dbając o ich potrzeby fizyczne i emocjonalne. Zawsze staram się zapewnić im jak najlepszą opiekę, dostosowując się do ich indywidualnych potrzeb. Twój pies będzie miał zapewnione codzienne spacery i zabawę, które pozwolą mu spalić energię i poznać nowe otoczenie. Dzięki moim spacerom i interakcjom, Twój pies będzie miał pełną radość z aktywności na świeżym powietrzu. Dla kotów tworzę spokojną atmosferę, w której będą mogły odpoczywać i bawić się w zgodzie z ich naturalnym rytmem. Koty uwielbiają poczucie bezpieczeństwa, dlatego staram się zapewnić im miejsce, gdzie będą mogły czuć się komfortowo i bezpiecznie. Moja opieka nad zwierzętami obejmuje również karmienie, pielęgnację, a także kontrolowanie ich stanu zdrowia. Posiadam doświadczenie w opiece nad różnymi rasami psów oraz kotów, dzięki czemu wiem, jak dostosować opiekę do potrzeb konkretnego zwierzaka. Zawsze kieruję się miłością do zwierząt i ich dobrem, a moja opieka jest pełna zaangażowania i troski. Jeśli chcesz, by Twój pupil czuł się komfortowo i kochany, z pewnością mogę Ci pomóc. Dzięki mojej ofercie, Twoje zwierzęta będą miały zapewnioną najlepszą opiekę, a Ty będziesz spokojny o ich dobrostan. Zajmuję się nie tylko codziennymi obowiązkami, ale również staram się budować więź z pupilem, by czuł się jak w domu. Gwarantuję, że Twój pies lub kot będzie traktowany z pełnym szacunkiem i troską.",
                    PetSitterDescription = "Jestem studentką informatyki, co daje mi dużą elastyczność w organizowaniu swojego czasu, dzięki czemu mogę zaoferować pełną opiekę nad Twoimi zwierzętami. W wolnym czasie zajmuję się moimi zwierzętami, które są moją prawdziwą pasją. Od najmłodszych lat byłam otoczona zwierzętami, co pozwoliło mi zdobyć doświadczenie w ich pielęgnacji oraz wychowywaniu. Zajmuję się psami i kotami od zawsze, dzięki czemu wiem, jak rozumieć ich potrzeby. Moje podejście do zwierząt opiera się na cierpliwości, zrozumieniu oraz trosce o ich emocjonalne i fizyczne potrzeby. Każde zwierzę traktuję indywidualnie, bo wiem, że każde z nich ma inny charakter i wymagania. Mam doświadczenie zarówno w opiece nad małymi, jak i dużymi psami, a także kotami o różnych temperamentach. W wolnym czasie chętnie uczę się nowych metod wychowawczych oraz poszerzam swoją wiedzę o zwierzętach, aby jak najlepiej im służyć. Zajmowanie się zwierzętami to nie tylko moja praca, ale także hobby, które daje mi ogromną satysfakcję. Uwielbiam spędzać czas na świeżym powietrzu, biorąc udział w spacerach z psami lub obserwując, jak koty bawią się w ogrodzie. Moje doświadczenie pozwala mi na odpowiednią pielęgnację, zarówno jeżeli chodzi o zdrowie, jak i higienę zwierząt. Dbałość o ich dobrostan jest dla mnie priorytetem.Jeśli szukasz opiekuna, który nie tylko zaspokoi potrzeby Twojego zwierzaka, ale także stworzy z nim prawdziwą więź, jestem odpowiednią osobą! ",
                    AddressId = 5,
                    Host = "petsitter@gmail.com",
                    Price = 99.99m,
                    PhotoId = 5,
                    Facilities = FetchFacilityList(new List<int> {1, 3, 4, 5, 6}),
                    //Facilities = [1, 3, 4, 5, 6],
                    OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 5, AnimalTypeId = 1 },
                            new OfferAnimalType { OfferId = 5, AnimalTypeId = 2 },
                    },
                    //OfferAnimalTypes = ["Pies", "Kot"],
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Name = "Ewa",
                    OfferDescription  = "Cześć! Nazywam się Ewa i jestem osobą pełną energii, która uwielbia zajmować się zwierzętami. Moje doświadczenie obejmuje szeroką gamę gatunków, od kotów i psów, po płazy, gady, gryzonie i rybki. Zajmuję się zwierzętami z pełnym zaangażowaniem, dbając o ich fizyczne i emocjonalne potrzeby. Moja oferta jest kompleksowa – zajmuję się karmieniem, pielęgnacją, zabawą oraz dbam o odpowiednie warunki do życia dla każdego zwierzaka. Każdy gatunek ma swoje unikalne potrzeby, dlatego dostosowuję opiekę indywidualnie, zapewniając komfort i bezpieczeństwo. Zadbam o Twoje zwierzęta, niezależnie od tego, czy potrzebują codziennych spacerów, odpowiedniego terrarium, czy po prostu towarzystwa i zabawy. W przypadku kotów dbam o spokój w ich otoczeniu, tworząc im bezpieczne miejsce do odpoczynku. Gryzonie, takie jak króliki czy świnki morskie, będą miały zapewnioną odpowiednią przestrzeń do zabawy i odpoczynku, a rybki będą miały czystą wodę oraz spokój w swoim akwarium. Płazy i gady wymagają specjalistycznej opieki, którą mogę im zapewnić – odpowiednia temperatura, wilgotność oraz pożywienie to podstawa. Mam odpowiednie doświadczenie i umiejętności, by zapewnić Twoim zwierzętom pełną troskę. Każde zwierzę traktuję indywidualnie, bo wiem, że wymagają one innego podejścia. Zajmuję się nimi, jakby były moimi własnymi, dbając o ich dobrostan na każdym etapie. Gwarantuję, że Twój pupil poczuje się komfortowo i bezpiecznie, a Ty będziesz spokojny, wiedząc, że jest w dobrych rękach.",
                    PetSitterDescription = "Jestem energiczną osobą, która kocha zwierzęta. Od najmłodszych lat miałam kontakt z różnymi gatunkami zwierząt, co pozwoliło mi zdobyć doświadczenie w ich pielęgnacji i opiece. Pracuję w branży kreatywnej, co daje mi dużą elastyczność w organizowaniu mojego czasu, dzięki czemu mogę poświęcić odpowiednią ilość czasu dla Twojego pupila. W wolnym czasie zajmuję się swoimi zwierzętami, które są dla mnie prawdziwą pasją. Potrafię zadbać o różnorodne gatunki, od psów, kotów, przez gryzonie, aż po płazy i gady. Moje podejście do opieki nad zwierzętami jest pełne pasji, zaangażowania i troski. Zajmowanie się zwierzętami to dla mnie coś więcej niż tylko praca – to sposób na życie. Każdy z moich podopiecznych czuje się bezpiecznie, a ja dokładam wszelkich starań, by spełnić wszystkie ich potrzeby. Dbam o to, by zwierzęta miały odpowiednie warunki do życia, zarówno fizyczne, jak i emocjonalne. Moje doświadczenie pozwala mi na opiekę nad różnymi zwierzętami, dzięki czemu potrafię dostosować swoją opiekę do ich indywidualnych potrzeb. Staram się zapewnić zwierzętom odpowiednią ilość ruchu, zabawy i odpoczynku. Mam także duże doświadczenie w karmieniu i pielęgnacji, co pozwala mi na dbanie o zdrowie moich podopiecznych. Jestem cierpliwa i uważna, co pozwala mi na skuteczną komunikację z zwierzętami i ich bezpieczne wychowanie. Uwielbiam spędzać czas z moimi pupilami, tworząc z nimi silną więź opartą na zaufaniu i miłości. Moje podejście jest indywidualne, ponieważ wiem, że każde zwierzę ma swoje unikalne potrzeby i temperament. Jeśli szukasz opiekuna, który nie tylko zadba o Twoje zwierzęta, ale także stworzy z nimi silną więź, jestem odpowiednią osobą do tej roli.",
                    AddressId = 6,
                    Host = "petsitter@gmail.com",
                    Price = 160.00m,
                    PhotoId = 6,
                    Facilities = FetchFacilityList(new List<int> {1, 2, 3, 4, 5}),

                    //Facilities = [1, 2, 3, 4, 5],
                    OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 6, AnimalTypeId = 1 },
                            new OfferAnimalType { OfferId = 6, AnimalTypeId = 2 },
                            new OfferAnimalType { OfferId = 6, AnimalTypeId = 3 },
                            new OfferAnimalType { OfferId = 6, AnimalTypeId = 4 },
                            new OfferAnimalType { OfferId = 6, AnimalTypeId = 5 },
                            new OfferAnimalType { OfferId = 6, AnimalTypeId = 6 },
                    },
                    //OfferAnimalTypes = ["Pies", "Kot", "Gryzoń", "Gad", "Płaz", "Rybki"],
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Name = "Michał",
                    OfferDescription  = "Cześć, jestem Michał! Mam dużą pasję do gadów i płazów, którymi zajmuję się od wielu lat. Moje zainteresowanie tymi zwierzętami zaczęło się, gdy byłem dzieckiem, a przez lata zgłębiałem wiedzę na temat ich naturalnych potrzeb. Jeśli potrzebujesz osoby, która zadba o Twoje gady lub płazy, jestem idealnym wyborem. Oferuję kompleksową opiekę, która obejmuje zapewnienie odpowiednich warunków życia, karmienie oraz monitorowanie zdrowia Twoich pupili. Gady i płazy wymagają specjalistycznej opieki, którą mogę im zapewnić, dostosowując temperaturę, wilgotność i rodzaj pożywienia do ich potrzeb. Dzięki mojej wiedzy mogę monitorować zdrowie zwierząt, sprawdzając, czy nie mają problemów zdrowotnych, i zapewniając im komfortowe warunki życia. Zajmuję się także czyszczeniem terrarium oraz dbam o odpowiednie środowisko w którym żyją. W przypadku gadów, takich jak jaszczurki, czy węże, wiem, jak ważna jest odpowiednia dieta oraz aktywność, którą zapewnię. Płazy, takie jak żaby czy salamandry, także będą miały dostosowane warunki, dbając o odpowiednią temperaturę i wilgotność w ich terrarium. Każde zwierzę traktuję indywidualnie, co pozwala mi zapewnić im najlepszą możliwą opiekę. Jeśli zależy Ci na profesjonalnej opiece nad Twoimi gadami i płazami, chętnie się nimi zaopiekuję. Dzięki mojemu doświadczeniu, Twoje zwierzęta będą w pełni bezpieczne i zadbane.",
                    PetSitterDescription = "Jestem uczniem technikum, specjalizującym się w biologii, co pozwala mi posiadać wiedzę i doświadczenie w opiece nad płazami i gadami. Moje zainteresowania i pasja do tych zwierząt sprawiają, że opieka nad nimi to dla mnie czysta przyjemność. Od najmłodszych lat fascynowały mnie gady i płazy, dlatego postanowiłem zgłębiać temat ich potrzeb oraz hodowli. Moje wykształcenie daje mi solidną podstawę teoretyczną, którą łączę z praktycznym doświadczeniem zdobytym przy opiece nad moimi własnymi zwierzętami. Zajmuję się zarówno pielęgnacją, jak i monitorowaniem zdrowia swoich podopiecznych, dbając o odpowiednie warunki życia. Jestem odpowiedzialny i dokładny, co pozwala mi skutecznie dbać o szczególne potrzeby gadów i płazów. Dzięki swojej pasji, potrafię stworzyć odpowiednie środowisko dla każdego zwierzęcia, zapewniając im komfort i bezpieczeństwo. Uwielbiam spędzać czas z tymi zwierzętami, co sprawia, że czuję się spełniony w tej roli. Moje podejście do opieki jest pełne zrozumienia i empatii, ponieważ wiem, jak ważna jest cierpliwość przy pracy z takimi zwierzętami. Zajmowanie się gadami i płazami to dla mnie coś więcej niż tylko obowiązek – to prawdziwa pasja. Jeśli szukasz opiekuna, który połączy wiedzę z miłością do tych wyjątkowych zwierząt, jestem idealnym wyborem.",
                    AddressId = 7,
                    Host = "petsitter@gmail.com",
                    Price = 129.99m,
                    PhotoId = 7,
                    Facilities = FetchFacilityList(new List<int> {3, 4, 5, 6}),

                    //Facilities = [3, 4, 5, 6],
                    OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 7, AnimalTypeId = 3 },
                            new OfferAnimalType { OfferId = 7, AnimalTypeId = 4 },
                            new OfferAnimalType { OfferId = 7, AnimalTypeId = 5 },
                    },
                    //OfferAnimalTypes = ["Gryzoń", "Gad", "Płaz"],
                    DateAdd = DateTime.Now,

                },
                new Offer()
                {
                    Name = "Krzysztof",
                    OfferDescription  = "Cześć, jestem Krzysztof! Posiadam kilka akwariów, a moją pasją jest opieka nad rybkami i gryzoniami. Zajmuję się akwarystyką od wielu lat i mam doświadczenie w dbaniu o zdrowie oraz dobre samopoczucie moich podopiecznych. Jeśli szukasz osoby, która zadba o Twoje ryby i gryzonie, jestem idealnym wyborem. Oferuję kompleksową opiekę, która obejmuje nie tylko karmienie, ale także zapewnienie odpowiednich warunków do życia. Moje doświadczenie pozwala mi na dostosowanie parametrów wody w akwarium, co jest kluczowe dla zdrowia ryb. Znam się na utrzymaniu odpowiedniej temperatury, pH i twardości wody, aby stworzyć idealne warunki dla Twoich ryb. Dodatkowo dbam o czystość akwarium, usuwając osady i zmieniając wodę, co zapewnia zdrowe i bezpieczne środowisko. Jeśli chodzi o gryzonie, posiadam własne, więc dobrze znam ich potrzeby. Zajmuję się ich codzienną opieką, zapewniając im odpowiednią dietę, aktywność i czystość klatki. Gryzonie to bardzo ciekawe stworzenia, które wymagają sporej uwagi, a ja z pełnym zaangażowaniem oferuję im to, czego potrzebują. Jeśli chcesz, by Twoje ryby i gryzonie były w dobrych rękach, chętnie się nimi zaopiekuję!\r\n\r\n",
                    PetSitterDescription = "Jestem profesjonalnym akwarystą, który posiada wiedzę na temat potrzeb ryb oraz gryzoni. Mam doświadczenie w dbaniu o parametry wody i zdrowie ryb, co pozwala mi skutecznie zadbać o każdy aspekt ich życia. Od lat zajmuję się akwarystyką, dlatego doskonale rozumiem, jak ważne jest utrzymanie odpowiednich warunków w akwarium. Monitoruję pH, temperaturę i twardość wody, aby stworzyć optymalne środowisko dla ryb. Moje doświadczenie pozwala mi również na precyzyjne karmienie ryb, co jest kluczowe dla ich zdrowia i witalności. Oprócz ryb, posiadam także gryzonie, dlatego wiem, jak zadbać o ich potrzeby. Codziennie dbam o ich komfort, zapewniając im odpowiednią przestrzeń, aktywność oraz właściwą dietę. Gryzonie, takie jak chomiki, świnki morskie czy myszy, potrzebują nie tylko jedzenia, ale również odpowiedniej interakcji i zabawy, co sprawia, że ich życie jest pełne radości. Moje podejście do opieki nad zwierzętami jest odpowiedzialne i profesjonalne, co pozwala mi zagwarantować pełną opiekę. Zawsze staram się zapewnić im jak najlepsze warunki, aby mogły cieszyć się zdrowiem i komfortem.",
                    AddressId = 8,
                    Host = "petsitter@gmail.com",
                    Price = 112.00m,
                    PhotoId = 8,
                    Facilities = FetchFacilityList(new List<int> {3, 5, 6}),

                    //Facilities = [3, 5, 6],
                    OfferAnimalTypes = new List<OfferAnimalType>
                    {
                            new OfferAnimalType { OfferId = 8, AnimalTypeId = 3 },
                            new OfferAnimalType { OfferId = 8, AnimalTypeId = 6 },
                    },
                    //OfferAnimalTypes = ["Rybki", "Gryzoń"],
                    DateAdd = DateTime.Now,

                },
            };

            return offers;
        }

        private List<Animal> FetchAnimalsList(List<int> ids)
        {
            return _context.Animals
                .Where(f => ids.Contains(f.Id))
                .ToList();
        }
        private IEnumerable<Application> GetApplications()
        {
            var applications = new List<Application>()
            {
                new Application()
                {
                    DateStart = new DateTime(2025, 3, 14),
                    DateEnd = new DateTime(2025, 3, 15),
                    OfferId = 1,
                    Applicant = "petowner@gmail.com",
                    Note  = "Milo to przyjazny kot, który uwielbia wylegiwać się na parapecie i obserwować świat za oknem. Lubi drapanie za uszkiem, ale nie przepada za gwałtownymi ruchami ani głośnymi dźwiękami. Jego ulubioną zabawką jest mała piłeczka, którą chętnie gania po mieszkaniu. Ma wrażliwy żołądek, dlatego powinien dostawać wyłącznie swoją karmę. Nie lubi, gdy ktoś dotyka jego łapek, dlatego lepiej unikać tego miejsca. Jest kotem przyjaznym, ale potrzebuje chwili, by zaufać nowym osobom. Uwielbia długie drzemki na miękkim kocyku. W nocy jest dość aktywny, więc warto zostawić mu kilka zabawek.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 5 }),
                    //Animals = [5],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 3, 16),
                    DateEnd = new DateTime(2025, 3, 17),
                    OfferId = 2,
                    Applicant = "petowner@gmail.com",
                    Note  = "Milo to bardzo towarzyski kot, który lubi spędzać czas z ludźmi, ale też ceni sobie chwilę samotności. Jego ulubione miejsce do odpoczynku to kanapa, gdzie potrafi przespać kilka godzin. Nie przepada za dziećmi, ponieważ hałas go stresuje. Ma skłonności do alergii pokarmowych, dlatego jego dieta powinna być ściśle przestrzegana. Lubi głaskanie, ale tylko wtedy, gdy sam do kogoś podejdzie. Drapak to jego ulubiona zabawka, dlatego dobrze jest mu zapewnić dostęp do niego. Nie toleruje innych zwierząt i może być zazdrosny o uwagę opiekuna. Potrzebuje codziennej dawki zabawy, aby rozładować energię.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 5 }),
                    //Animals = [5],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 3, 18),
                    DateEnd = new DateTime(2025, 3, 19),
                    OfferId = 3,
                    Applicant = "petowner@gmail.com",
                    Note  = "Max to energiczny pies, który uwielbia długie spacery i zabawę na świeżym powietrzu. Z kolei Milo jest bardziej spokojny i lubi leniwe popołudnia na kanapie. Max potrzebuje dużo ruchu, inaczej może stać się niespokojny i niszczyć rzeczy. Milo uwielbia smakołyki, ale nie może jeść kurczaka, ponieważ ma na niego alergię. Oba psy są przyjazne wobec ludzi, ale Max bywa nieufny wobec obcych psów. Milo nie lubi głośnych dźwięków, więc warto unikać miejsc z dużym hałasem. Oba psy są nauczone podstawowych komend, ale Max czasem wymaga dodatkowej motywacji. Lubią wspólne zabawy, ale potrzebują też chwili spokoju.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 1, 5 }),
                    //Animals = [1, 5],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 3, 20),
                    DateEnd = new DateTime(2025, 3, 21),
                    OfferId = 4,
                    Applicant = "petowner@gmail.com",
                    Note  = "Mars to duży, ale bardzo łagodny pies, który uwielbia pieszczoty i bliskość człowieka. Jest bardzo inteligentny i szybko uczy się nowych komend. Nie przepada za wodą, dlatego kąpiele bywają dla niego stresujące. Ma bardzo wrażliwy żołądek, więc powinien dostawać tylko swoją karmę. Lubi długie spacery, ale nie jest fanem biegania – preferuje spokojniejsze tempo. Jest bardzo przyjacielski wobec ludzi, ale nie toleruje nachalnych psów. Ma swoje ulubione miejsce do spania i nie lubi, gdy ktoś mu je zajmuje. Jest bardzo lojalny i uwielbia być w centrum uwagi.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 3 }),
                    //Animals = [3],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 3, 27),
                    DateEnd = new DateTime(2025, 3, 28),
                    OfferId = 5,
                    Applicant = "petowner@gmail.com",
                    Note  = "Max to wulkan energii, który uwielbia zabawę piłką i bieganie po parku. Milo to bardziej spokojny pies, który preferuje relaks w domu. Max potrzebuje dużo uwagi i aktywności, bo inaczej zaczyna się nudzić. Milo z kolei uwielbia długie drzemki i głaskanie po brzuchu. Oba psy są przyjazne, ale Max czasem jest nieco zbyt entuzjastyczny wobec nowych osób. Milo ma alergię na kurczaka, więc należy pilnować jego diety. Nie lubią zostawać same na długo, bo wtedy stają się niespokojne. Ich relacja jest bardzo dobra, ale każdy ma swoje potrzeby.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 1, 5 }),
                    //Animals = [1, 5],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 3, 28),
                    DateEnd = new DateTime(2025, 3, 29),
                    OfferId = 6,
                    Applicant = "petowner@gmail.com",
                    Note  = "Koko to mały królik, który uwielbia chrupać świeże warzywa i biegać po mieszkaniu. Jest bardzo towarzyski, ale nie lubi być podnoszony. Luna to spokojna suczka, która zaakceptowała Koko i nie wykazuje wobec niego agresji. Koko nie może jeść owoców cytrusowych, bo ma na nie uczulenie. Luna lubi długie spacery, ale po powrocie do domu najchętniej odpoczywa na swoim posłaniu. Królik potrzebuje codziennego dostępu do świeżego siana i wody. Oba zwierzaki dobrze się dogadują, ale Koko potrzebuje swojego własnego, bezpiecznego miejsca. Luna nie lubi głośnych dźwięków, dlatego najlepiej unikać hałaśliwych miejsc.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 3, 4 }),
                    //Animals = [3, 4],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 4, 1),
                    DateEnd = new DateTime(2025, 4, 2),
                    OfferId = 7,
                    Applicant = "petowner@gmail.com",
                    Note  = "Koko to energiczny królik, który uwielbia swobodnie biegać po pokoju, ale nie lubi być głaskany po grzbiecie. Spike, agama brodata, potrzebuje ciepłego terrarium i regularnego dostępu do świeżych warzyw oraz owadów. Tito, płaz wodno-lądowy, wymaga odpowiednio nawilżonego środowiska i spokojnej atmosfery. Koko jest bardzo ciekawski, ale nie powinien mieć kontaktu z innymi zwierzętami bez nadzoru. Agama Spike lubi się wygrzewać pod lampą i nie przepada za nagłymi ruchami. Tito jest wrażliwy na zmiany temperatury, dlatego ważne jest utrzymanie odpowiednich warunków. Każde ze zwierząt ma swoje specyficzne potrzeby, ale wszystkie wymagają troski i uwagi.",                  
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 6, 7, 8 }),
                    //Animals = [3, 4, 5],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 4, 2),
                    DateEnd = new DateTime(2025, 4, 3),
                    OfferId = 8,
                    Applicant = "petowner@gmail.com",
                    Note  = "Bąbel to złota rybka, która mieszka w przestronnym akwarium i potrzebuje regularnej pielęgnacji wody. Nie może być przekarmiana, bo łatwo nabiera nadwagi. Mars, duży i łagodny pies, jest bardzo spokojny i uwielbia leżeć na swoim ulubionym posłaniu. Nie przepada za kąpielami, ale uwielbia pieszczoty. Nie jest agresywny wobec innych zwierząt, ale może być zazdrosny o uwagę opiekuna. Bąbel nie powinien mieć kontaktu z głośnymi dźwiękami, ponieważ może to powodować u niego stres. Mars ma delikatny żołądek, więc jego dieta powinna być dobrze kontrolowana. Oba zwierzęta potrzebują stałej opieki, ale każde w inny sposób.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 3, 9 }),
                    //Animals = [3, 9],
                    ApplicationDateAdd = DateTime.Now,
                },
                new Application()
                {
                    DateStart = new DateTime(2025, 4, 3),
                    DateEnd = new DateTime(2025, 4, 4),
                    OfferId = 5,
                    Applicant = "petowner@gmail.com",
                    Note  = "Max to wesoły, energiczny pies, który uwielbia zabawę i długie spacery. Milo jest bardziej spokojny i preferuje odpoczynek w domu. Max potrzebuje dużo ruchu, ponieważ szybko się nudzi i może zacząć rozrabiać. Milo nie lubi głośnych dźwięków, więc najlepiej unikać hałaśliwych miejsc. Oba psy są przyjazne wobec ludzi, ale Max czasami bywa zbyt entuzjastyczny w kontaktach z innymi psami. Milo nie może jeść kurczaka, więc jego dieta musi być ściśle przestrzegana. Uwielbiają wspólne zabawy, ale każdy z nich ma inne potrzeby. Są bardzo lojalne i przywiązane do swoich opiekunów.",
                    ApplicationStatus = "Oczekująca",
                    Animals = FetchAnimalsList(new List<int> { 1, 5 }),
                    //Animals = [1, 5],
                    ApplicationDateAdd = DateTime.Now,
                },
            };
          
          

            return applications;
        }


        private IEnumerable<Opinion> GetOpinions()
        {
            var opinions = new List<Opinion>()
            {
                new Opinion()
                {
                    Rating = 4,
                    Comment = "Adam to świetny opiekun! Mój kot od razu polubił jego towarzystwo, a codzienne zdjęcia i raporty sprawiły, że czułam się spokojna o jego bezpieczeństwo. Na pewno skorzystam ponownie!",
                    ApplicationId = 1,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,

                },
                new Opinion()
                {
                    Rating = 4,
                    Comment = "Bardzo cenię spokój i delikatność Marty. Mój kot to dość lękliwe zwierzę, ale u Marty czuł się komfortowo. Wracał do domu zrelaksowany i szczęśliwy!",
                    ApplicationId = 2,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
                new Opinion()
                {
                    Rating = 4,
                    Comment = "Julia jest niezastąpiona! Opiekowała się moim kotem i psem przez tydzień, a do tego dbała o moje akwarium. Wszystko na najwyższym poziomie!",
                    ApplicationId = 3,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
                new Opinion()
                {
                    Rating = 2,
                    Comment = "Mój piesek jest trochę wybredny, jeśli chodzi o jedzenie, i niestety Ania nie do końca wiedziała, jak go zachęcić do jedzenia. Powinnam była wcześniej dokładniej wyjaśnić jego wymagania, ale liczyłam na większą inicjatywę ze strony opiekunki.",
                    ApplicationId = 4,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
                new Opinion()
                {
                    Rating = 5,
                    Comment = " Kasia jest świetna! Doskonale radzi sobie z opieką nad kotami i psami jednocześnie, co nie jest łatwe. Zwierzaki były w doskonałych rękach! Mój pies, który zazwyczaj ma problem z akceptacją nowych osób, od razu poczuł się swobodnie i bezpiecznie w jej towarzystwie. Kot również nie wykazywał żadnego stresu, a wręcz przeciwnie – był zrelaksowany i chętnie się bawił. Codziennie otrzymywałam zdjęcia i krótkie raporty o ich samopoczuciu, co dało mi ogromny spokój. Wróciłam do domu, a moje zwierzaki były szczęśliwe, zadbane i pełne energii. Z pewnością ponownie skorzystam z usług Kasi!",
                    ApplicationId = 5,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
                new Opinion()
                {
                    Rating = 4,
                    Comment = "Ewa ma ogromną wiedzę o gadach i płazach. Mój żółw i kot byli pod doskonałą opieką, polecam każdemu!",
                    ApplicationId = 6,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
                new Opinion()
                {
                    Rating = 3,
                    Comment = "Michał dobrze zna się na gadach, ale zapomniał podać mojemu gekonowi witamin zgodnie z harmonogramem. Trochę mnie to zaniepokoiło, ale poza tym wszystko było w porządku.",
                    ApplicationId = 7,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
                new Opinion()
                {
                    Rating = 4,
                    Comment = "Krzysztof to prawdziwy specjalista! Dbał o moje rybki i parametry wody, dzięki czemu wróciłem do zdrowego akwarium. Świetna opieka!",
                    ApplicationId = 8,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
                new Opinion()
                {
                    Rating = 4,
                    Comment = "Kasia świetnie poradziła sobie z opieką nad moim kotem i psem. Oboje byli zadbani, dostawali regularnie jedzenie i uwagę. Widać, że Kasia ma doświadczenie i kocha zwierzęta. Kot czuł się swobodnie, a pies miał zapewnione długie spacery i zabawy.",
                    ApplicationId = 5,
                    UserEmail = "petsitter@gmail.com",
                    OpinionDateAdd = DateTime.Now,
                },
            };

            return opinions;
        }


    }
}

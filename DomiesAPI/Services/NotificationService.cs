using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using static System.Net.Mime.MediaTypeNames;
using MailKit.Net.Smtp;




namespace DomiesAPI.Services
{
    public class NotificationService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificationService> _logger;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationService(
            IConfiguration configuration,
            ILogger<NotificationService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NotificationService wystartował.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now;
                _logger.LogInformation($"Aktualny czas: {currentTime}");

                if (currentTime.Hour == 7 && currentTime.Minute == 00)
                {
                    _logger.LogInformation("Przed wysłaniem powiadomień.");
                    await SendDailyNotifications();
                    _logger.LogInformation("Po wysłaniu powiadomień.");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        private async Task SendDailyNotifications()
        {
            using (var scope = _serviceScopeFactory.CreateScope()) 
            {
                try
                {
                    var _context = scope.ServiceProvider.GetRequiredService<DomiesContext>(); 
                    var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var today = DateTime.UtcNow.Date;
                    Console.WriteLine(today);
                    var applications = await _context.Applications

                    //.Where(a => a.Applicant == userEmail)
                    .Where(a => a.DateEnd.Date == today)
                    .Include(o => o.Animals)

                        .Select(a => new ApplicationDtoRead
                        {
                            Id = a.Id,
                            DateEnd = a.DateEnd,
                            OfferId = a.OfferId,
                            Applicant = a.Applicant,
                            Animals = a.Animals != null
                            ? a.Animals
                            .AsEnumerable()
                            .Select(
                            an => new AnimalDto
                            {
                                PetName = an.PetName,
                                SpecificDescription = an.SpecificDescription,
                                AnimalType = an.AnimalType,
                                Type = an.AnimalTypeNavigation.Type,

                            }).ToList()
                            : null
                        })
                    .ToListAsync();

                    if (applications == null)
                    {
                        Console.WriteLine("Brak aplikacji.");
                        throw new ArgumentException("Brak aplikacji.");
                    }
                    Console.WriteLine(applications);
                    //return applications;

                    foreach (var application in applications)
                    {
                        var user = application.Applicant;

                        string formattedDateEnd = application.DateEnd.ToString("dd.MM.yyyy");
                        string formattedDateStart = application.DateStart.ToString("dd.MM.yyyy");
                        var animalsNamesList = string.Join(", ", application.Animals
                            .Select(animal => animal.PetName).ToList());

                        var emailSubject = "📢 Przypomnienie – Odbiór Twojego pupila";
                        var emailBody = $@"
                        <!DOCTYPE html>
                <html lang='pl'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <title>Weryfikacja konta</title>
                    <style>
                    body {{
                        font-family: Arial, sans-serif;
                        line-height: 1.6;
                        color: #333333;
                        margin: 0px;
                        padding: 0px;
                        width: 91%;
                        max-width: 610px;
                        background-color: #f9f9f9;
                        border: 2px solid #e0e0e0;
                        border-radius: 3px;
                    }}
                    .container {{
                        width: 90%;
                        max-width: 600px;
                        margin: 20px auto;
                        border: 2px solid #e0e0e0;
                        border-radius: 16px;
                        padding: 20px;
                        font-size: 17px;
                        background-color: #ffffff;
                    }}
                    .header {{
                        text-align: center;
                        background-color: #f3ecdb;
                        border: 1px solid #c3b091;
                        color: black;
                        padding: 20px 0;
                        border-top-left-radius: 10px;
                        border-top-right-radius: 10px;
                    }}
                    .content {{
                        text-align: left;
                        color: black;
                        margin: 10px 0px;
                    }}
                    .button {{
                        display: inline-block;
                        border: 1px solid #c3b091;
                        background-color: #f3ecdb;
                        color: black;
                        text-decoration: none;
                        padding: 10px 20px;
                        font-size: 19px;
                        border-radius: 5px;
                        margin-top: 20px;
                        margin-bottom: 10px;
                    }}
                    .button:hover {{
                        background-color: #c3b091;
                    }}
                    .advice {{
                        background-color: #f9f9f9;
                        border: 1px solid #d1d1d1;
                        text-align: left;
                        color: #333;
                        padding: 15px;
                        border-radius: 8px;
                        margin-bottom: 20px;
                    }}
                    .footer {{
                        text-align: center;
                        margin-top: 30px;
                        font-size: 12px;
                        color: #777777;
                    }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                    <div class='header'>
                        <h1>Domies</h1>
                    </div>
                    <div class='content'>
                        <p>Drogi Kliencie,</p>
                        <p>Przypominamy, że masz do odbioru swojego pupila w dniu {formattedDateEnd}. To już dzisiaj! Prosimy o odbiór w wyznaczonym terminie. </p>
                        <h3 style=""color: #906030;"">Szczegóły odbioru:</h3>
                       <ul>
                            <li>🐾 <strong>Imię pulila/ Imiona pupili:</strong> {animalsNamesList}</li>

                            <li>📅 <strong>Data rozpoczęcia opieki:</strong> {formattedDateStart}</li>
                            <li>📅 <strong>Data odbioru zwierzaka:</strong> {formattedDateEnd}</li>
                        </ul>
                        <br/>
                        <p>Aby zapoznać się ze szczegółami oferty, kliknij poniższy link:</p>
                        <p><a href=""http://localhost:3000/offerDetails/{application.OfferId}"" style=""color: #906030;"">Zobacz ofertę</a></p>

                        <p>W razie jakichkolwiek pytań lub potrzebnych informacji, prosimy o kontakt z naszym działem obsługi klienta.</p>

                        <p>Z poważaniem,</p>
                        <p>🐶 <strong>Zespół Domies</strong></p>
                        <p>📧 <a href=""mailto:domies.680@gmail.com"" style=""color: #906030;"">domies.680@gmail.com</a></p>
                        <p>📞 +48 600 482 148</p>
                        <p>🌐 <a href=""http://localhost:3000/"" style=""color: #906030;"">Odwiedź ponownie naszą stronę!</a></p>
                    </div>
                    <div class='footer'>
                        <p>© 2025 Domies. Wszelkie prawa zastrzeżone.</p>
                    </div>
                    </div>
                </body>
                </html>";

                        await _emailService.SendEmailAsync(user, emailSubject, emailBody);

                        _logger.LogInformation($"Wysłano powiadomienie do {user} o {DateTime.Now}");
                    }

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                    throw new ApplicationException("Błąd podczas pobierania szczegółowych informacji", ex);

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Błąd podczas wysyłania powiadomień: {ex.Message}");
                }
            }


        }


    }

}

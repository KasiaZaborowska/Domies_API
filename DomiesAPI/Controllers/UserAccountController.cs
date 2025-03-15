using Azure;
using DomiesAPI.Models;
using DomiesAPI.Models.ModelsDto;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DomiesAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {

        //private readonly DomiesContext _context;
        //private ApiResponse _response;
        private IUserAccountService _userAccountService;
        private IEmailService _emailService;
        public UserAccountController(IUserAccountService userAccountService, IEmailService emailService)
        {
            //_context = context;
            //_response = apiResponse;
            _userAccountService = userAccountService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userdto)
        {       
            try
            {
                var verificationToken = Guid.NewGuid().ToString();
                string result = await _userAccountService.RegisterUser(userdto, verificationToken);

                if (result == "Użytkownik z danym mailem już istnieje.")
                {
                    return BadRequest(new { message = result });
                }
                else
                {
                    var verificationLink = $"http://localhost:3000/verify?token={verificationToken}";
                    Console.WriteLine("Rejestracja w toku..");
                    var emailSubject = "Zweryfikuj swój adres e-mail w serwisie Domies";
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
                        text-align: center;
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
                        <h1>Witamy w aplikacji Domies!</h1>
                    </div>
                    <div class='content'>
                        <p>Witaj,</p>
                        <p>Dziękujemy za utworzenie konta w <strong>Domies</strong>. Aby zakończyć proces rejestracji, kliknij poniższy przycisk, aby zweryfikować swój adres e-mail:</p>
                        <a href='{verificationLink}' class='button'>Zweryfikuj swój e-mail</a>
                        <p class='content'>Jeśli nie zakładałeś/aś konta w naszej aplikacji, zignoruj tę wiadomość.</p>
                    </div>
                    <div>
                        <br>
                        <div class='advice'>
                        <h4 class='content'>Ważne wskazówki dotyczące bezpieczeństwa:</h4>
                        <ol>
                            <li>Zawsze trzymaj dane do Twojego konta w bezpiecznym miejscu.</li>
                            <li>Nigdy nie ujawniaj nikomu swojego loginu i hasła.</li>
                            <li>Regularnie zmieniaj swoje hasło.</li>
                            <li>Jeśli podejrzewasz, że ktoś używa Twojego konta nielegalnie, prosimy o natychmiastowe poinformowanie nas o tym.</li>
                        </ol>
                        </div>
                    </div>
                    <div class='footer'>
                        <p>© 2025 Domies. Wszelkie prawa zastrzeżone.</p>
                    </div>
                    </div>
                </body>
                </html>";
                    await _emailService.SendEmailAsync(userdto.Email, emailSubject, emailBody);

                    return Ok(new { token = result, user = userdto.Email });
                }
            }
            //catch (ArgumentException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return BadRequest(new { message = ex.Message });
            //}
            catch (Exception ex)
            {
                return BadRequest(new { message = "Wystąpił błąd w rejestracji." });
            }
        
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyUserEmail(string token)
        {
            string result = await _userAccountService.VerifyUserEmail(token);
            if(result == "Niepoprawny token")
            {
                return BadRequest(new { message = result });
            }
            else
            {
                return Ok("Twoje konto zostało zweryfikowane!");
            }
           
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {        
            try
            {
                string result = await _userAccountService.Login(loginDto);
                return Ok(new { token = result, user = loginDto.Email });
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Wystąpił błąd w logowaniu." });
            }
        }
    }
}

using DomiesAPI;
using DomiesAPI.Models;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DomiesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultDbConnection"));
});


var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var key = Encoding.UTF8.GetBytes(jwtSettings["JwtSecretKey"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key), // U¿ycie klucza z appsettings.json
        ValidateIssuer = true, // Walidacja Issuer
        ValidateAudience = true, // Walidacja Audience
        ValidIssuer = jwtSettings["JwtIssuer"], // Issuer z konfiguracji
        ValidAudience = jwtSettings["JwtIssuer"], // Audience z konfiguracji
        RequireExpirationTime = true,
        ValidateLifetime = true // Walidacja, czy token nie wygas³
    };
});





// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// database seed 
builder.Services.AddScoped<DomiesSeeder>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IOfferService, OfferService>(); 
builder.Services.AddScoped<IAnimalTypeService, AnimalTypeService>(); 
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IOpinionService, OpinionService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

builder.Services.AddCors(options =>
{
    // first policy
    options.AddPolicy("frontApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });
});



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DomiesSeeder>();
    seeder.Seed();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("frontApp");

app.Run();

using DomiesAPI;
using DomiesAPI.Models;
using DomiesAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var connectionString = Env.GetString("CONNECTION_STRING");
var frontendUrl = Env.GetString("FRONTEND_URL");



builder.Services.AddDbContext<DomiesContext>(options =>
{
    options.UseSqlServer(connectionString);
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
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true, 
        ValidateAudience = true,
        ValidIssuer = jwtSettings["JwtIssuer"], 
        ValidAudience = jwtSettings["JwtIssuer"],
        RequireExpirationTime = true,
        ValidateLifetime = true, 
        RoleClaimType = "Role"
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id= JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[]{}
        }
    });
});


builder.Services.AddScoped<DomiesSeeder>();

builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IOfferService, OfferService>(); 
builder.Services.AddScoped<IAnimalTypeService, AnimalTypeService>(); 
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IOpinionService, OpinionService>();
builder.Services.AddScoped<IAnimalService, AnimalService>(); 
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IEmailService, EmailService>(); 
builder.Services.AddScoped<IFacilityService, FacilityService>();

builder.Services.AddHostedService<NotificationService>();
builder.Services.AddLogging(); // Rejestracja logowania


builder.Services.AddCors(options =>
{
    options.AddPolicy("frontApp",
        builder => builder
            .WithOrigins(frontendUrl)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DomiesSeeder>();
    seeder.Seed();
}


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

using System.Text;
using Divination.Application;
using Divination.Application.Manager;
using Divination.Application.Managers;
using Divination.Application.Services;
using Divination.Domain.Entities;
using Divination.Domain.Interfaces;
using Divination.Infrastructure;
using Divination.Infrastructure.Data;
using Divination.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS ayarları
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// JWT ayarları
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ClockSkew = TimeSpan.Zero,

    };
});

// Identity yapılandırması
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

// Veritabanı ayarlarıƒ
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(9, 0, 0))));



// builder.Services.AddAuthorization(options =>
// {
//     options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
//         .RequireAuthenticatedUser()
//         .Build();
// });

// Controller ve Swagger ayarları



builder.Services.Configure<FormOptions>(options=> options.MultipartBodyLengthLimit = 10 * 1024 * 1024);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DivinationProject API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});


builder.Services.Configure<FormOptions>(options=>{
    options.MultipartBodyLengthLimit=104857600;
});

// Servisleri ekle
builder.Services.AddScoped<IAppUserService, AppUserManager>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationManager>();

builder.Services.AddScoped<IFortuneTellerService, FortuneTellerManager>();
builder.Services.AddScoped<IFortuneTellerRepository,FortuneTellerRepository>();

builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();

builder.Services.AddScoped<IAnswerRepository,AnswerRepository>();

builder.Services.AddScoped<IEmailService, EmailManager>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var app = builder.Build();

// Swagger ayarları
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware'leri kullan
app.UseCors("AllowAll");
// app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// app.UseStaticFiles(new StaticFileOptions
//         {
//             FileProvider = new PhysicalFileProvider(
//                 Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles")),
//             RequestPath = "/UploadedFiles"
//         });

app.MapControllers();

app.Run();

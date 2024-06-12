using Application.ContactBook.Common.Interfaces;
using Application.ContactBook.Common.Models;
using ContactBook.Application.Common.Interfaces;
using Infrastructure.ContactBook.Identity;
using Infrastructure.ContactBook.Services;
using Infrastructure.ContactBook.Services.SendEmail;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyPlatform.Infrastructure.Services;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the IDateTime service
            services.AddScoped<IDateTime, DateTimeService>();
            
            // Register the ICurrentUserService
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // Register the interceptor with dependencies
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            // Register ApplicationDbContext
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            
            // Register MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IGenerateToken, GenerateToken>();

            services.AddLogging(builder =>
            {
                builder.AddConsole(); 
            });


            services.AddScoped<IUserRepositorySignIn, UserRepositorySignIn>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyProfileRepository, CompanyProfileRepository>();

            services.AddAuthentication("Bearer")
             .AddJwtBearer(options =>
             {
                 var validIssuer = configuration["Authentication:Issuer"];
                 var validAudience = configuration["Authentication:Audience"];
                 var secretKey = configuration["Authentication:SecretKey"];

                 if (string.IsNullOrEmpty(validIssuer) || string.IsNullOrEmpty(validAudience) || string.IsNullOrEmpty(secretKey))
                 {
                     throw new InvalidOperationException("Authentication configuration values are missing or invalid.");
                 }

                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = validIssuer,
                     ValidAudience = validAudience,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                 };
             });

            services.AddAuthorization();





            // Configure SmtpSettings using the configuration section
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

            // Register the SmtpClient and use the SmtpSettings
            services.AddTransient<SmtpClient>(sp =>
            {
                var smtpSettings = sp.GetRequiredService<IOptions<SmtpSettings>>().Value;
                return new SmtpClient(smtpSettings.Server, smtpSettings.Port)
                {
                    Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
                    EnableSsl = smtpSettings.EnableSsl
                };
            });


            services.AddTransient<IEmailService, SmtpEmailService>();

            // Register the SmtpEmailService with IEmailService
            services.AddTransient<IEmailService, SmtpEmailService>();
            return services;
        }
    }
}

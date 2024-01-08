using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SocialMemin.Application.Activities;
using SocialMemin.Application.Core;
using SocialMemin.Persistence;
using SocialMemin.Application.Interfaces;
using SocialMemin.Infrastructure.Security;
using SocialMemin.Infrastructure.Photos;

namespace SocialMemin.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(opt =>
                                opt.AddPolicy("CustomCorsPolicy", policy =>
                                                                    policy.AllowAnyMethod()
                                                                          .AllowAnyHeader()
                                                                          .AllowCredentials()
                                                                          .WithOrigins("http://localhost:3000")));

            services.AddMediatR(cfg =>
                                    cfg.RegisterServicesFromAssemblyContaining<List>());

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();

            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();

            services.Configure<CloudinarySettings>(config.GetSection("Cloudinary"));

            services.AddSignalR();

            return services;
        }
    }
}

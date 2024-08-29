using CA_Final_Regia.Domain.Interfaces.Repository;
using CA_Final_Regia.Infrastructure.Repositories;
using CA_Final_Regia.Services.Interfaces;
using CA_Final_Regia.Services.Services.AdminServices;
using CA_Final_Regia.Services.Services.JwtServices;
using CA_Final_Regia.Services.Services.LocationServices;
using CA_Final_Regia.Services.Services.PersonServices;
using CA_Final_Regia.Services.Services.PictureServices;
using CA_Final_Regia.Services.Services.UserServices;
using CA_Final_Regia.Services.Services.ValidationService;
using Microsoft.Extensions.DependencyInjection;
namespace CA_Final_Regia.IoC.ServiceCollectionExtensions
{
    public static class DependencyInjections
    {
        public static void ConfigureDependencyInjections(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IUserLogInService, UserLogInService>();
            services.AddScoped<IPictureResizeService, PictureResizeService>();
            services.AddScoped<IPersonAddInfoService, PersonAddInfoService>();
            services.AddScoped<ILocationAddService, LocationAddService>();
            services.AddScoped<IJwtExtractService, JwtExtraxtService>();
            services.AddScoped<IGetUsersService, GetUsersService>();
            services.AddScoped<IDeleteUserService, DeleteUserService>();
            services.AddScoped<IPersonGetInfoService, PersonGetInfoService>();
            services.AddScoped<ILocationGetService, LocationGetService>();
            services.AddScoped<IPersonUpdateService, PersonUpdateService>();
            services.AddScoped(typeof(IDtoValidation<>), typeof(DtoValidation<>));
            services.AddScoped<ILocationUpdateService, LocationUpdateService>();

            // Repositories
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}

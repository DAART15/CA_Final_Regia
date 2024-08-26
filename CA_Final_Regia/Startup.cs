using CA_Final_Regia.IoC.ServiceCollectionExtensions;
using CA_Final_Regia.Web.API.StartupConfiguration;
namespace CA_Final_Regia.Web.API
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;
        public string ConnectionString
        {
            get
            {
                string regiaDBString = Configuration.GetConnectionString("DefaultConnection");
                return regiaDBString;
            }
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.ConfigureSwagger();
            services.ConfigureJWT(Configuration);
            services.AddDatabaseServices(ConnectionString);
            services.ConfigureDependencyInjections();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(SecurityConfiguration.DevelopmentCorsPolicy);
            }
            else
            {
                app.UseCors(SecurityConfiguration.ProductionCorsPolicy);
            }
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}

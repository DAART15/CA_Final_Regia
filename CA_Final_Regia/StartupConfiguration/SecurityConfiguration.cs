namespace CA_Final_Regia.Web.API.StartupConfiguration
{
    internal static class SecurityConfiguration
    {
        internal const string DevelopmentCorsPolicy = "DevelopmentCorsPolicy";
        internal const string ProductionCorsPolicy = "ProductionCorsPolicy";
        internal static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(p => p.AddPolicy(DevelopmentCorsPolicy, builder =>
            {
                builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            services.AddCors(p => p.AddPolicy(ProductionCorsPolicy, builder =>
            {
                builder.WithOrigins("https://www.google.com")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));
        }
    }
}

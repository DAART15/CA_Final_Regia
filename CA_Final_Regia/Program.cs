using CA_Final_Regia.Infrastructure.Extensions;
using CA_Final_Regia.Interfaces;
using CA_Final_Regia.Services.AdminServices;
using CA_Final_Regia.Services.JwtServices;
using CA_Final_Regia.Services.LocationServices;
using CA_Final_Regia.Services.PersonServices;
using CA_Final_Regia.Services.PictureServices;
using CA_Final_Regia.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CA_Final_Regia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(p => p.AddPolicy("corsfordevelopment", builder =>
            {
                builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            builder.Services.AddCors(p => p.AddPolicy("ProductionCorsPolicy", builder =>
            {
                builder.WithOrigins("https://www.google.com")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            }));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            });
            });

            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"]
                };
            });


            // custom services from CA_Final_Regia.Services
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();
            builder.Services.AddScoped<IUserLogInService, UserLogInService>();
            builder.Services.AddScoped<IPictureResizeService, PictureResizeService>();
            builder.Services.AddScoped<IPersonAddInfoService, PersonAddInfoService>();
            builder.Services.AddScoped<ILocationAddService, LocationAddService>();
            builder.Services.AddScoped<IJwtExtraxtService, JwtExtraxtService>();
            builder.Services.AddScoped<IGetUsersService, GetUsersService>();
            builder.Services.AddScoped<IDeleteUserService, DeleteUserService>();
            builder.Services.AddScoped<IPersonGetInfoService, PersonGetInfoService>();


            // from Infrastructure.Extensions
            builder.Services.AddDatabaseServices(builder.Configuration.GetConnectionString("DefaultConnection"));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("corsfordevelopment");
            }
            else
            {
                app.UseCors("ProductionCorsPolicy");
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

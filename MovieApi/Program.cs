using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MovieApi.Contexts;
using MovieApi.Contracts.Contracts;
using MovieApi.Core.Interfaces;
using MovieApi.Data.Services;
using MovieApi.Interfaces;
using MovieApi.Services;
using MovieApi.Services.Services;
using System.Text;

namespace MovieApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("MovieApiContext") ?? throw new InvalidOperationException("Connection string 'MovieApiContext' not found.");

            builder.Services.AddDbContext<MovieApiContext>(options => options.UseSqlServer(connectionString));

            // Add services to the container.

            builder.Services.AddCors(options => {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<MovieApiContext>());

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.AddControllers();

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);	// anger att standardversionen är v1.0.
                options.AssumeDefaultVersionWhenUnspecified = true;	// gör att anrop utan angiven version använder standardversionen.

                options.ReportApiVersions = true;			        // gör att API: t skickar versionsinformation i responsheaders.
            })
            .AddApiExplorer(options =>                              // Med AddApiExplorer(...) kan Swagger hitta och gruppera varje version korrekt.
            {
                options.GroupNameFormat = "'v'VVV";                 // hur versionsgruppen ska heta.
                options.SubstituteApiVersionInUrl = true;           // ersätt version-placeholdern i URL:en med riktig version.
            });							                            // Tillsammans gör de att Swagger kan visa v1 och v2 snyggt och separat.




            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                // 1. Detta är oförändrat och definierar Bearer-knappen
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT-1234567890abcdefghijklmnopqrstuvxz"
                });

                // 2. DETTA ÄR NYTT FÖR .NET 10: Vi använder en lambda-funktion med det nya schemareferensobjektet
                options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
                });


                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Products API",
                    Version = "v1"
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Products API",
                    Version = "v2"
                });

            });

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            }
            );

            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseCors("AllowFrontend");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API v1");
                    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Products API v2");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

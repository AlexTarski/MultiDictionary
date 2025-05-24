using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MultiDictionary.App.Interfaces;
using MultiDictionary.App.Services;
using MultiDictionary.Domain;
using MultiDictionary.Domain.Entities;
using MultiDictionary.Infrastructure;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace MultiDictionary.WebAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("config.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var connectionString = builder.Configuration.GetConnectionString("MultiDictionaryContextDb");

            builder.Services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; //without that option all possibly nullable types would be marked as requiered
            });
            builder.Services.AddDbContext<MultiDictionaryContext>(options =>
            {
                options.UseSqlServer(connectionString,
                            x => x.MigrationsAssembly("MultiDictionary.Infrastructure"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddScoped<MultiDictionarySeeder>();
            builder.Services.AddScoped<IMultiDictionaryRepository, MultiDictionaryRepository>();
            builder.Services.AddScoped<IGlossaryService, GlossaryService>();
            builder.Services.AddScoped<IWordService, WordService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(cfg =>
                    cfg.SerializerSettings
                    .ReferenceLoopHandling = ReferenceLoopHandling.Ignore); //ignore nested loops (Glossary has Words which have a Glossary prop in them)

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    cors => cors.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader());
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MultiDictionaryContext>();
                // Apply migrations only if database does NOT exist
                if (!await dbContext.Database.CanConnectAsync())
                {
                    await dbContext.Database.MigrateAsync();
                    //Seed data to Db
                    var seeder = scope.ServiceProvider.GetRequiredService<MultiDictionarySeeder>();
                    await seeder.SeedAsync();
                }
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors("AllowAll"); // Enables CORS to establish connection with Blazor WebAssembly

            await app.RunAsync();
        }
    }
}
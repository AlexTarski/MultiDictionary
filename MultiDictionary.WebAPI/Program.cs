using Microsoft.EntityFrameworkCore;
using MultiDictionary.App.Services;
using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain;
using MultiDictionary.Domain.Entities;
using MultiDictionary.Infrastructure;

namespace MultiDictionary.WebAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("config.json").AddEnvironmentVariables();


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<MultiDictionaryContext>();
            builder.Services.AddScoped<IMultiDictionaryRepository, MultiDictionaryRepository>();
            builder.Services.AddScoped<IGlossaryService, GlossaryService>();
            builder.Services.AddScoped<IWordService, WordService>();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MultiDictionaryContext>();

                // Check if the app was started with a migration flag
                if (args.Contains("/applyMigration"))
                {
                    await dbContext.Database.MigrateAsync(); // Apply migrations explicitly
                }
                else
                {
                    await dbContext.Database.EnsureCreatedAsync(); // Ensure database exists
                }
            }


            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}

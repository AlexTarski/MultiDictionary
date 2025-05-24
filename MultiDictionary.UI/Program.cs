using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MultiDictionary.UI.Interfaces;
using MultiDictionary.UI.Services;

namespace MultiDictionary.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(config["ApiSettings:BaseUrl"]) });

            builder.Services.AddScoped<IWordService, WordService>();
            builder.Services.AddScoped<IGlossaryService, GlossaryService>();

            await builder.Build().RunAsync();
        }
    }
}

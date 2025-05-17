using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MultiDictionary.App.Interfaces;
using MultiDictionary.App.Services;

namespace MultiDictionary.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
            builder.Services.AddScoped<IWordService, WordService>();
            builder.Services.AddScoped<IGlossaryService, GlossaryService>();

            await builder.Build().RunAsync();
        }
    }
}

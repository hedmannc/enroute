using Blazored.SessionStorage;
using EnrouteUI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnrouteUI
{
    public class Startup
    {

        private static async Task Main(string[] args)
        {



            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddScoped<AuthenticationService>();
            builder.Services.AddScoped<TokenProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider,
                CustomAuthenticationStateProvider>();

            builder.Services.AddHttpClient("Auth", op =>
            {
                op.BaseAddress = new Uri(new Config().BackendURL);

            });

            builder.Services.AddBlazoredSessionStorage(config => {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            }
);
            builder.RootComponents.Add<App>("app");
            await builder.Build().RunAsync();

        }

    }
}

using Blazored.LocalStorage;
using EnrouteUI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrouteUI
{
    public class Startup
    {

        private static async Task Main(string[] args)
        {



            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddAuthorizationCore();


            builder.Services.AddScoped<MyAuthenticationStateProvider>();
            builder.Services.AddScoped<MyAuthenticationStateProvider.TokenProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<MyAuthenticationStateProvider>());
            builder.Services.AddScoped<Locations>();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddHttpClient("Auth", op =>
            {
                op.BaseAddress = new Uri(new Config().BackendURL);

            });

            builder.Services.AddBlazoredLocalStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
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

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace EnrouteUI
{
    public class Startup
    {

        private static async Task Main(string[] args)
        {

            var builder = WebAssemblyHostBuilder.CreateDefault(args);


            builder.RootComponents.Add<App>("app");




            await builder.Build().RunAsync();

        }

    }
}

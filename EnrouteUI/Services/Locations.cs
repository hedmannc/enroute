using EnrouteAppLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using static MyAuthenticationStateProvider;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System;

namespace EnrouteUI.Services
{
    public class Locations
    {

        public HttpClient httpclient = new();
        public Locations(IHttpClientFactory http)
        {

            httpclient = http.CreateClient("Auth");

        }


        public async Task<List<Location>> getlLocations()
        {
            List<Location> locations = new List<Location>();
            var response = await httpclient.GetAsync("Locations/getlocations");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                if (data != null)
                {
                    var d = JsonSerializer.Deserialize<List<Location>>(data, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (d != null)
                    {
                        locations.AddRange(d);
                    }

                }



            }

            return locations;
        }


        public async Task<List<Building>> getBuildings()
        {
            List<Building> buildings = new List<Building>();
            var response = await httpclient.GetAsync("Locations/getbuildings");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                if (data != null)
                {
                    var d = JsonSerializer.Deserialize<List<Building>>(data, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (d != null)
                    {
                        buildings.AddRange(d);
                    }

                }



            }

            return buildings;
        }
    }
}

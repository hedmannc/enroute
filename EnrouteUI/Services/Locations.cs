using EnrouteAppLibrary.Models;
using System.Collections.Generic;
using System.Net.Http;
using static MyAuthenticationStateProvider;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text;

namespace EnrouteUI.Services
{
    public class Locations
    {

        public HttpClient httpclient = new();


        public ILocalStorageService _sessionStorageService;


        public Locations(IHttpClientFactory http, ILocalStorageService storageStorage)
        {

            httpclient = http.CreateClient("Auth");

            _sessionStorageService = storageStorage;



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


        public async Task setBuilding( Building building)
        {
            try
            {
                var token = await _sessionStorageService.GetItemAsStringAsync("token");


                if (token != null)
                {

                    httpclient.DefaultRequestHeaders.Remove("Authorization");
                    httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    var content = new StringContent(JsonSerializer.Serialize<Building>(building), Encoding.UTF8, "application/json");

                    var response = await httpclient.PostAsync("Locations/AddBuilding", content);
                    if (!response.IsSuccessStatusCode)
                    {

                        throw new Exception($"{await response.Content.ReadAsStringAsync()}");

                    }


                }
            }
            catch(Exception) {

                throw;
            
            
            }



        }
    }
}

using Blazored.LocalStorage;
using EnrouteAppLibrary.Models;
using GoogleMapsComponents.Maps;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnrouteUI.Services
{
    public class Users
    {

        public HttpClient httpclient = new();

        public ILocalStorageService _sessionStorageService;
        public Users(IHttpClientFactory http, ILocalStorageService sessionStorageService)
        {

            httpclient = http.CreateClient("Auth");
            _sessionStorageService = sessionStorageService;

        }
        public async void setUserLocation(LatLngLiteral location)
        {
            var token = await _sessionStorageService.GetItemAsStringAsync("token");

            var content = new StringContent("");

            if(token is not null)
            {
                httpclient.DefaultRequestHeaders.Remove("Authorization");
                httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                await httpclient.PostAsync($"UserMaintenance/setUserLastLocation?longitude={location.Lng}&latitude={location.Lat}",content);
            }



           

        }

        public async Task<UserLocationHistory?> getUserLocation(string email)
        {
            var token = await _sessionStorageService.GetItemAsStringAsync("token");

       

            if (token is not null)
            {
                httpclient.DefaultRequestHeaders.Remove("Authorization");
                httpclient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                var response = await httpclient.GetAsync($"UserMaintenance/getUserLastLocation?email={email}");

                if (response != null)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    if(data != null)
                    {
                        return  JsonSerializer.Deserialize<UserLocationHistory>(data);
                    }
                }


            }

            return null;





        }
    }
}

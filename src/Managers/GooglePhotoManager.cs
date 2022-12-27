using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PhotoFrameSharp
{
    class GooglePhotosManager
    {
        public void Authenticate {

            // Load the configuration file
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Read the client ID and secret from the config file
            string clientId = config.GetValue<string>("GooglePhotos:ClientId");
            string clientSecret = config.GetValue<string>("GooglePhotos:ClientSecret");

            // Request an access token from the Google OAuth 2.0 server
            string accessToken = await GetAccessTokenAsync(clientId, clientSecret);

            // Use the access token to authenticate requests to the Google Photos API
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
        }

        public void GetAlbums {
            // Use the access token to authenticate requests to the Google Photos API
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            // Get a list of all the albums in the user's Google Photos library
            HttpResponseMessage response = await httpClient.GetAsync("https://photoslibrary.googleapis.com/v1/albums");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
        }

        
    }
}
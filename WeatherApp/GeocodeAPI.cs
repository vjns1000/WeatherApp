using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherApp
{
    internal class GeocodeAPI
    {
        const string _gecodoing_url = "https://api.api-ninjas.com/";
        HttpClient _gecodingHTTP;

        public readonly static GeocodeAPI _geoInstance = new GeocodeAPI();
        public GeocodeAPI()
        {
            _gecodingHTTP = new HttpClient() { BaseAddress = new Uri(_gecodoing_url) };

            // Add an Accept header for "application/json" format
            _gecodingHTTP.DefaultRequestHeaders.Accept.Clear();
            _gecodingHTTP.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Add any other headers you need, for example, an API key
            _gecodingHTTP.DefaultRequestHeaders.Add("X-Api-Key", "E+l98iX52w9UuMuHrxrI7A==Pehd2okKKSNF8zLE");
        }

        public async Task<string> GetCoords(string city, string country)
        {
            using HttpResponseMessage response = await _gecodingHTTP.GetAsync($"v1/geocoding?city={city}&country={country}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task <GeocodeInfo> GetGeographyInfo(string city, string country)
        {
            // Parse the JSON string into a JsonDocument
            using (JsonDocument document = JsonDocument.Parse(await _geoInstance.GetCoords(city, country)))
            {
                // Convert Jason string to C# object
                JsonElement root = document.RootElement[0];
                GeocodeInfo GeoInfo = new GeocodeInfo();
                GeoInfo.Name = root.GetProperty("name").ToString();
                GeoInfo.Latitude = root.GetProperty("latitude").GetDouble();
                GeoInfo.Longitude = root.GetProperty("longitude").GetDouble();
                GeoInfo.Country = root.GetProperty("country").ToString();
                return GeoInfo;
                
            };
        }

    }
}

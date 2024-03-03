using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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

        /// <summary>
        /// Returns list of cities with their names, longitude and latitude
        /// </summary>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task <List<GeocodeInfo[]>> GetGeographyInfo(List<string> cities, List<string> countries)
        {
            List<GeocodeInfo[]> geocodeInfoList = new List<GeocodeInfo[]>();
            for (int i = 0; i < cities.Count; i++)
            {
                GeocodeInfo?[] geocodeInfos = JsonSerializer.Deserialize<GeocodeInfo[]>(await _geoInstance.GetCoords(cities[i], countries[i]));
                geocodeInfoList.Add(geocodeInfos);
            }
            
            return geocodeInfoList;
        }

        public async Task<GeocodeInfo[]> GetGeographyInfo(string city, string country)
        {
            GeocodeInfo?[] geocodeInfos = JsonSerializer.Deserialize<GeocodeInfo[]>(await _geoInstance.GetCoords(city, country));
            return geocodeInfos;
        }
    }
}

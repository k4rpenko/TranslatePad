using System.Net.Http.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Client.api
{
    internal class Http_Send
    {
        private HttpClient _client;

        public Http_Send()
        {
            _client = new HttpClient();
        }

        /*public async Task<string> Get(string url)
        {

        }*/

        public async Task<HttpResponseMessage> PostAuth(string url, string email, string password)
        {
            try
            {
                var data = new { email = email, password = password };
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP POST request failed: {ex.Message}");
                return null;
            }

        }



        public async Task<HttpResponseMessage> PostAddDictionary(string url, string token, string H1, string P1)
        {
            try
            {
                var data = new { token = token, title = H1, content = P1 };
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP POST request failed: {ex.Message}");
                return null;
            }

        }

        public async Task<HttpResponseMessage> PostChangeNotes(string url, int id, string H1, string P1, string date)
        {
            try
            {
                var data = new { id = id, title = H1, content = P1, updated_at = date };
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP POST request failed: {ex.Message}");
                return null;
            }
        }

        public async Task<HttpResponseMessage> GetShowNotes(string url, string token)
        {
            try
            {
                var data = new { token = token };
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP POST request failed: {ex.Message}");
                return null;
            }
        }

        public async Task<HttpResponseMessage> GetOpenNotes(string url, int id)
        {
            try
            {
                var data = new { id = id };
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP POST request failed: {ex.Message}");
                return null;
            }
        }

    }
}

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


        public async Task<HttpResponseMessage> PostAdd_translate(string url, string token, string lang_orig_words, string orig_words, string lang_trans_words, string trans_words)
        {
            try
            {
                var data = new { token = token, lang_orig_words = lang_orig_words, orig_words = orig_words, lang_trans_words = lang_trans_words, trans_words = trans_words };
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                return response;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP POST request failed: {ex.Message}");
                return null;
            }

        }

        public async Task<HttpResponseMessage> GetShow_translate(string url, string token)
        {
            try
            {
                var data = new { token = token, };
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

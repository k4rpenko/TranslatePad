﻿using System.Net.Http.Json;
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

    }
}

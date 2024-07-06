using System.Net.Http.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Windows.Forms;
using System.Text;
using System;

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
        
        #region Authoruzation
        public async Task<string> PostAuthLogin(string email, string password, Label label_in)
        {
            try
            {
                var data = new { email = email, password = password };
                string url = "https://translate-pad.vercel.app/api/auth/Login"; // URL для відправлення запиту на вхід
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                if (response != null)
                {
                    if ((int)response.StatusCode == 200)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        JObject _JO = JObject.Parse(responseBody);
                        string refreshToken = _JO["refreshToken"].ToString();

                        label_in.ForeColor = Color.Green;
                        label_in.Text = "Вхід пройшов успішно";


                        return refreshToken;
                    }
                    else if ((int)response.StatusCode == 404)
                    {
                        label_in.ForeColor = Color.Red;
                        label_in.Text =
                            "За вказаною адресою електронної пошти немає облікового запису. Ви можете зареєструвати обліковий запис на цю адресу електронної пошти";
                    }
                    else if ((int)response.StatusCode == 401)
                    {
                        label_in.ForeColor = Color.Red;
                        label_in.Text = "Невірні облікові дані";
                    }
                }
                else
                {
                    label_in.ForeColor = Color.Red;
                    label_in.Text = "NULL";
                }
            }
            catch
            {
                label_in.Text = "error occurred";
            }

            return null;
        }

        public async Task<string> PostAuthRegister(string email, string password, Label label_in)
        {
            try
            {
                var data = new { email = email, password = password };
                string url = "https://translate-pad.vercel.app/api/auth/Regists"; // URL для відправлення запиту на вхід
                HttpResponseMessage response = await _client.PostAsJsonAsync(url, data);
                if (response != null)
                {
                    if ((int)response.StatusCode == 200)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        JObject _JO = JObject.Parse(responseBody);
                        string refreshToken = _JO["refreshToken"].ToString();

                        label_in.ForeColor = Color.Green;
                        label_in.Text = "Вхід пройшов успішно";


                        return refreshToken;
                    }
                    else if ((int)response.StatusCode == 404)
                    {
                        label_in.ForeColor = Color.Red;
                        label_in.Text =
                            "За вказаною адресою електронної пошти немає облікового запису. Ви можете зареєструвати обліковий запис на цю адресу електронної пошти";
                    }
                    else if ((int)response.StatusCode == 401)
                    {
                        label_in.ForeColor = Color.Red;
                        label_in.Text = "Невірні облікові дані";
                    }
                }
                else
                {
                    label_in.ForeColor = Color.Red;
                    label_in.Text = "NULL";
                }
            }
            catch
            {
                label_in.Text = "error occurred";
            }

            return null;
        }
        #endregion
        
       
        
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

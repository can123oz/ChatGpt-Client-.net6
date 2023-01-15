using ChatGptCloneBackend.Models;
using System.Text.Json;
using System.Text;

namespace ChatGptCloneBackend.Helpers
{
    public static class ChatGptHelper
    {
        public static async Task<ResponseBody> ConnectApi(string question, string apiKey)
        {
            RequestBody content = new()
            {
                max_tokens = 500,
                temperature = 0,
                prompt = question,
                model = "text-davinci-003"
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/completions"))
                    {
                        request.Headers.Add("Authorization", $"{apiKey}");
                        string requestString = JsonSerializer.Serialize(content);
                        request.Content = new StringContent(requestString, Encoding.UTF8, "application/json");
                        using (HttpResponseMessage? httpResponse = await httpClient.SendAsync(request))
                        {
                            ResponseBody completionResponse = new ResponseBody();
                            if (httpResponse != null)
                            {
                                if (httpResponse.IsSuccessStatusCode)
                                {
                                    string responseString = await httpResponse.Content.ReadAsStringAsync();
                                    var responseString1 = await httpResponse.Content.ReadFromJsonAsync<ResponseBody>();
                                    {
                                        if (!string.IsNullOrWhiteSpace(responseString))
                                        {
                                            completionResponse = JsonSerializer.Deserialize<ResponseBody>(responseString);
                                            completionResponse.status = true;
                                            return completionResponse;
                                        }
                                    }
                                }
                            }
                            completionResponse.status = false;
                            return completionResponse;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
using Newtonsoft.Json;
using System.Text;

namespace DiscordBot.Models
{
    public class OpenAI
    {
        private readonly HttpClient _httpClient;

        public CompletionRequest? CompletionRequest { get; set; }

        public OpenAI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CompletionResult> GetCompletion()
        {
            var json = JsonConvert.SerializeObject(CompletionRequest, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("completions", content);
            response.EnsureSuccessStatusCode();

            var stringifiedContent = await response.Content.ReadAsStringAsync();
            var deserializedObject = JsonConvert.DeserializeObject<CompletionResult>(stringifiedContent);

            return await Task.FromResult(deserializedObject);
        }
    }
}

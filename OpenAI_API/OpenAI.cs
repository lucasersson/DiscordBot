using Newtonsoft.Json;
using System.Text;
using OpenAI_API.Completion;
using OpenAI_API.Image;

namespace OpenAI_API
{
    public class OpenAI
    {
        private readonly HttpClient _httpClient;

        public CompletionRequest? CompletionRequest { get; set; }
        public ImageRequest? ImageRequest { get; set; }

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

        public async Task<ImageResult> GetImage()
        {
            throw new NotImplementedException();
        }
    }
}

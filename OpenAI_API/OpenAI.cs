using Newtonsoft.Json;
using System.Text;
using OpenAI_API.Completion;
using OpenAI_API.Image;
using OpenAI_API.Models;

namespace OpenAI_API
{
    public class OpenAI : IOpenAI
    {
        private readonly HttpClient _httpClient;

        public CompletionRequest? CompletionRequest { get; set; }
        public ImageRequest? ImageRequest { get; set; }

        public OpenAI(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CompletionModels> GetCompletionModels()
        {
            var response = await _httpClient.GetAsync("models");
            response.EnsureSuccessStatusCode();

            var stringifiedContent = await response.Content.ReadAsStringAsync();
            var deserializedObject = JsonConvert.DeserializeObject<CompletionModels>(stringifiedContent);

            if (deserializedObject == null)
            {
                throw new Exception("Bad request.");
            }

            return await Task.FromResult(deserializedObject);
        }

        public async Task<CompletionResult> GetCompletion()
        {
            var json = JsonConvert.SerializeObject(CompletionRequest, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("completions", content);
            response.EnsureSuccessStatusCode();

            var stringifiedContent = await response.Content.ReadAsStringAsync();
            var deserializedObject = JsonConvert.DeserializeObject<CompletionResult>(stringifiedContent);

            if(deserializedObject == null)
            {
                throw new Exception("Bad request.");
            }

            return await Task.FromResult(deserializedObject);
        }

        public async Task<ImageResult> GetImage()
        {
            var json = JsonConvert.SerializeObject(ImageRequest, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("images/generations", content);
            if(!response.IsSuccessStatusCode)
            {
                switch(response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new Exception("Unauthorized, OpenAI api token most likely expired.");
                    case System.Net.HttpStatusCode.BadRequest:
                        throw new Exception("Bad request, prompt was most likely flagged for profanity.");
                    case System.Net.HttpStatusCode.InternalServerError:
                        throw new Exception("Server error, try again later.");
                    default:
                        throw new Exception("Unknown error occured.");
                }
            }

            var stringifiedContent = await response.Content.ReadAsStringAsync();
            var deserializedObject = JsonConvert.DeserializeObject<ImageResult>(stringifiedContent);

            if (deserializedObject == null)
            {
                throw new Exception("Could not process image.");
            }

            return await Task.FromResult(deserializedObject);
        }
    }
}

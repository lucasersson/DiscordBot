using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Completion
{
    public class CompletionRequest
    {
        [JsonProperty("model")]
        public string? Model { get; set; }
        [JsonProperty("prompt")]
        public object? CompiledPrompt
        {
            get
            {
                if (MultiplePrompts?.Length == 1)
                    return Prompt;
                else
                    return MultiplePrompts;
            }
        }
        [JsonIgnore]
        public string[]? MultiplePrompts { get; set; }
        [JsonIgnore]
        public string Prompt
        {
            get => MultiplePrompts.FirstOrDefault();
            set
            {
                MultiplePrompts = new string[] { value };
            }
        }
        [JsonProperty("suffix")]
        public string? Suffix { get; set; }
        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; set; }
        [JsonProperty("temperature")]
        public double? Temperature { get; set; }
        [JsonProperty("top_p")]
        public double? TopP { get; set; }
        [JsonProperty("presence_penalty")]
        public double? PresencePenalty { get; set; }
        [JsonProperty("frequency_penalty")]
        public double? FrequencyPenalty { get; set; }
        [JsonProperty("n")]
        public int? NumChoicesPerPrompt { get; set; }
        [JsonProperty("stream")]
        public bool Stream { get; internal set; } = false;
        [JsonProperty("logprobs")]
        public int? Logprobs { get; set; }
        [JsonProperty("echo")]
        public bool? Echo { get; set; }
        [JsonProperty("best_of")]
        public int? BestOf { get; set; }
        [JsonProperty("user")]
        public string? User { get; set; }
    }

    public class Model
    {
        public const string DaVinci = "text-davinci-003";
        public const string Curie = "text-curie-001";
        public const string Babbage = "text-babbage-001";
        public const string Ada = "text-ada-001";
    }
}

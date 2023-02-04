using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Completion
{
    public class CompletionResult
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("created")]
        public int CreatedUnixTime { get; set; }
        [JsonIgnore]
        public DateTime? Created => DateTimeOffset.FromUnixTimeSeconds(CreatedUnixTime).DateTime;
        [JsonProperty("model")]
        public string? Model { get; set; }
        [JsonProperty("choices")]
        public List<Choice>? Completions { get; set; }
        [JsonIgnore]
        public TimeSpan? ProcessingTime { get; set; }
        [JsonIgnore]
        public string? Organization { get; set; }
        [JsonIgnore]
        public string? RequestId { get; set; }

        public override string ToString()
        {
            if (Completions != null && Completions.Count > 0)
                return Completions[0].Text;
            else
                return $"CompletionResult {Id} has no valid output";
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Models
{
    public class Choice
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
        [JsonProperty("index")]
        public int? Index { get; set; }
        [JsonProperty("logprobs")]
        public Logprobs? Logprobs { get; set; }
        [JsonProperty("finish_reason")]
        public string? FinishReason { get; set; }
    }
}

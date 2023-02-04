using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Completion
{
    public class Logprobs
    {
        [JsonProperty("tokens")]
        public List<string>? Tokens { get; set; }

        [JsonProperty("token_logprobs")]
        public List<double>? TokenLogprobs { get; set; }

        [JsonProperty("top_logprobs")]
        public IList<IDictionary<string, double>>? TopLogprobs { get; set; }

        [JsonProperty("text_offset")]
        public List<int>? TextOffsets { get; set; }
    }
}

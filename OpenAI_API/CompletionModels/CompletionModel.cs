using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Models
{
    public class CompletionModel
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("object")]
        public string? Object { get; set; }
        [JsonIgnore]
        public string? OwnedBy { get; set; }
        [JsonIgnore]
        public List<dynamic>? Permission { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Models
{
    public class CompletionModels
    {
        [JsonProperty("data")]
        public List<CompletionModel>? Data { get; set; }
        [JsonIgnore]
        public string? Object { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Image
{
    public class Image
    {
        [JsonProperty("url")]
        public string? Url { get; set; }
    }
}

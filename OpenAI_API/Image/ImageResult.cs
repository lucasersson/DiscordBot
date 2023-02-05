using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Image
{
    public class ImageResult
    {
        [JsonProperty("created")]
        public string? Created { get; set; }
        [JsonProperty("data")]
        public List<Image>? Images { get; set; }

        public override string ToString()
        {
            return Images[0].Url;
        }
    }
}

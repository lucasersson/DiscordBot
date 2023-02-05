using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API.Image
{
    public class ImageRequest
    {
        [Required]
        [JsonProperty("prompt")]
        public string? Prompt { get; set; }
        [JsonProperty("n")]
        public int ImagesCount { get; set; }
        [JsonProperty("size")]
        public string? Resolution { get; set; }
        [JsonIgnore]
        public string? ResponseFormat { get; set; }
        [JsonIgnore]
        public string? User { get; set; }
    }

    public static class Resolution
    {
        public const string r256 = "265x265";
        public const string r512 = "512x512";
        public const string r1024 = "1024x1024";
    }
}

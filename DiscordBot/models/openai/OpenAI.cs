using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.models.openai
{
    public class OpenAI
    {
        private readonly string _baseUrl = "https://openai.com/api/";
        private readonly string? _apiKey;

        public Completion Completion { get; set; }

        public OpenAI(OpenAIIonfiguration apiConfiguration)
        {
            _apiKey = apiConfiguration.ApiKey;
        }
    }
}

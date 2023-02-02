using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API
{
    public class OpenAI
    {
        //private readonly string _baseUrl = "https://openai.com/api/";
        private readonly OpenAIConfiguration _configuration;

        public Completion Completion { get; set; }

        public OpenAI(OpenAIConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.models.openai
{
    public record Completion(string engine, string prompt)
    {

        public Completion Create(string engine, string prompt)
        {
           throw new NotImplementedException();
        }

        private Task<Completion> Request(Completion completion)
        {
            throw new NotImplementedException();
        }
    }
}

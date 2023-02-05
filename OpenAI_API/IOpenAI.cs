using OpenAI_API.Completion;
using OpenAI_API.Image;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAI_API
{
    public interface IOpenAI
    {
        Task<CompletionModels> GetCompletionModels();
        Task<CompletionResult> GetCompletion();
        Task<ImageResult> GetImage();
    }
}

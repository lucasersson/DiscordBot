using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Models
{
    public class CompletionModal : IModal
    {
        public string Title => "Completion";

        [InputLabel("Model")]
        [ModalTextInput("model", placeholder: Model.DaVinci)]
        public string? PromptModel { get; set; }

        [InputLabel("Max tokens")]
        [ModalTextInput("max_tokens", placeholder: "2048")]
        public string? MaxTokens { get; set; }

        [InputLabel("Prompt")]
        [ModalTextInput("prompt", placeholder: "Type your prompt here.")]
        public string? Prompt { get; set; }
    }

}

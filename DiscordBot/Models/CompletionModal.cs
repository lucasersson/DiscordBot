using Discord;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API.Completion;
using System.ComponentModel;

namespace DiscordBot.Models
{
    public class CompletionModal : IModal
    {
        public string Title => "Completion";

        [InputLabel("Model")]
        [ModalTextInput("model", placeholder: "Enter model", initValue: Model.DaVinci)]
        [RequiredInput]
        public string? PromptModel { get; set; }

        [InputLabel("Max tokens")]
        [ModalTextInput("max_tokens", placeholder: "Enter max tokens", minLength: 1, maxLength: 4, initValue: "2048")]
        [RequiredInput]
        public string? MaxTokens { get; set; }

        [InputLabel("Temperature sampling")]
        [ModalTextInput("temperature", placeholder: "Enter a sampling value between 1 - 10", minLength: 1, maxLength: 2, initValue: "10")]
        [RequiredInput]
        public string? Temperature { get; set; }

        [InputLabel("Prompt")]
        [ModalTextInput("prompt", placeholder: "Type your prompt here.", minLength: 10, maxLength: 400, style: TextInputStyle.Paragraph)]
        [RequiredInput]
        public string? Prompt { get; set; }
    }

}

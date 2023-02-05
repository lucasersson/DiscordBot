using Discord.Interactions;
using OpenAI_API.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Models
{
    public class ImageModal : IModal
    {
        public string Title => "Image generation";

        [InputLabel("Resolution")]
        [ModalTextInput("resolution", placeholder: "Enter a compatible resolution", initValue: Resolution.r512)]
        [RequiredInput]
        public string? ImageResolution { get; set; }

        [InputLabel("Prompt")]
        [ModalTextInput("prompt", placeholder: "Enter a prompt", minLength: 5, maxLength: 1000)]
        [RequiredInput]
        public string? ImagePrompt { get; set; }
    }
}

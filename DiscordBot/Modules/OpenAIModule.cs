using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class OpenAIModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly OpenAI _openAI;

        public OpenAIModule(OpenAI openAI)
        {
            _openAI = openAI; 
        }

        [SlashCommand("completion", "returns text completion from chat-gpt model")]
        public async Task Complete() => await Context.Interaction.RespondWithModalAsync<CompletionModal>("prompt");
        //{
        //    _openAI.CompletionRequest = new CompletionRequest
        //    {
        //        Model = Model.DaVinci,
        //        MaxTokens = 2048,
        //        //Prompt = prompt
        //    };

        //    var result = await _openAI.GetCompletion();
        //    var textContent = result.ToString();

        //    var mb = new ModalBuilder
        //    {
        //        Title = "Completion",
        //        Components = new ModalComponentBuilder
        //        {

        //        }
        //    };

        //    await ReplyAsync(textContent);
        //}

        [ModalInteraction("completion")]
        public async Task ModalResponse(CompletionModal modal)
        {

            var parsedSuccess = int.TryParse(modal.MaxTokens, out var maxTokens);
            if(!parsedSuccess)
            {
                await RespondAsync("Max tokens needs to a number.");
                return;
            }

            if(string.IsNullOrWhiteSpace(modal.PromptModel))
            {
                await RespondAsync("You need to specify a ai model.");
                return;
            }

            if (string.IsNullOrWhiteSpace(modal.Prompt))
            {
                await RespondAsync("You need to write a prompt.");
                return;
            }

            _openAI.CompletionRequest = new CompletionRequest
            {
                Model = modal.PromptModel,
                Prompt = modal.Prompt,
                MaxTokens = maxTokens,
            };

            var result = await _openAI.GetCompletion();


            await RespondAsync(result.ToString());
        }
    }
}

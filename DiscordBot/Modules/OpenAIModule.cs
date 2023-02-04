using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI_API;
using OpenAI_API.Completion;
using DiscordBot.Models;

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
        public async Task Complete() => await Context.Interaction.RespondWithModalAsync<CompletionModal>("completion");

        [ModalInteraction("completion")]
        public async Task ModalResponse(CompletionModal modal)
        {
            //await RespondAsync("Processing operation");
            await DeferAsync();

            var parsedSuccess = int.TryParse(modal.MaxTokens, out var maxTokens);
            if(!parsedSuccess)
            {
                await FollowupAsync("Max tokens needs to a number.");
                return;
            }
            else
            {
                if(maxTokens > 2048)
                {
                    await FollowupAsync("Max tokens can't be greater than 2048.");
                }
            }

            if(string.IsNullOrWhiteSpace(modal.PromptModel))
            {
                await FollowupAsync("You need to specify an ai model.");
                return;
            }

            if (string.IsNullOrWhiteSpace(modal.Prompt))
            {
                await FollowupAsync("You need to write a prompt.");
                return;
            }

            _openAI.CompletionRequest = new CompletionRequest
            {
                Model = modal.PromptModel,
                Prompt = modal.Prompt,
                MaxTokens = maxTokens,
            };

            //try
            //{
                var result = await _openAI.GetCompletion();
            //}
            //catch (Exception ex)
            //{
            //    await RespondAsync(ex.ToString());
            //}

            var embed = new EmbedBuilder
            {
                Author = new EmbedAuthorBuilder
                {
                    Name = Context.Interaction.User.Username,
                    IconUrl = Context.Interaction.User.GetAvatarUrl() ?? Context.Interaction.User.GetDefaultAvatarUrl(),
                },
                Title = Format.Italics(modal.Prompt),
                Description = result.ToString(),
                Timestamp = DateTime.UtcNow,
                Color = Color.Orange
            }.Build();

            await FollowupAsync(embed: embed);
        }
    }
}

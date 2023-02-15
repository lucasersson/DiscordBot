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
using OpenAI_API.Image;

namespace DiscordBot.Modules
{
    public class OpenAIModule : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly OpenAI _openAI;

        public OpenAIModule(OpenAI openAI)
        {
            _openAI = openAI; 
        }

        [SlashCommand("completionmodels", "returns all available OpenAI completion models")]
        public async Task GetCompletionModels()
        {
            await DeferAsync();

            try
            {
                var models = await _openAI.GetCompletionModels();
                if(models == null)
                {
                    await FollowupAsync("No models found.", ephemeral: true);
                }

                var sb = new StringBuilder();

                models?.Data?.ForEach(model => sb.AppendLine(Format.Code(model.Id)));

                var description = sb.ToString();

                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder { Name = "OpenAI completion models" },
                    Description = description,
                    Color = Color.Green,
                    Timestamp = DateTime.UtcNow,
                }.Build();
                await FollowupAsync(embed: embed);
            }
            catch (Exception ex)
            {
                await FollowupAsync(ex.Message, ephemeral: true);
            }
        }

        [SlashCommand("completion", "returns text completion from gpt-3 model")]
        public async Task Complete() => await Context.Interaction.RespondWithModalAsync<CompletionModal>("completion");

        [ModalInteraction("completion")]
        public async Task ModalResponse(CompletionModal modal)
        {
            await DeferAsync();

            var parsedTokens = int.TryParse(modal.MaxTokens, out var maxTokens);
            if(!parsedTokens)
            {
                await FollowupAsync("Max tokens needs to a number.", ephemeral: true);
            }
            else
            {
                if(!Enumerable.Range(1, 2048).Contains(maxTokens))
                {
                    await FollowupAsync("Max tokens needs to be a number between 1 - 2048", ephemeral: true);
                }
            }

            var parsedTemperature = double.TryParse(modal.Temperature, out var temperature);
            if(!parsedTemperature || temperature < 1 || temperature > 10)
            {
                await FollowupAsync("Temperature needs to be a number between 1 - 10.", ephemeral: true);
            }
            else
            {
                temperature /= 10;
            }

            if(string.IsNullOrWhiteSpace(modal.PromptModel))
            {
                await FollowupAsync("You need to specify an ai model.", ephemeral: true);
                return;
            }

            if (string.IsNullOrWhiteSpace(modal.Prompt))
            {
                await FollowupAsync("You need to write a prompt.", ephemeral: true);
                return;
            }

            _openAI.CompletionRequest = new CompletionRequest
            {
                Model = modal.PromptModel,
                Prompt = modal.Prompt,
                MaxTokens = maxTokens,
                Temperature = temperature,
            };

            try
            {
                var result = await _openAI.GetCompletion();

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
            catch (Exception ex)
            {
                await FollowupAsync(ex.Message, ephemeral: true);
            }
        }

        [SlashCommand("generateimage", "returns a generated image from the dall-e model")]
        public async Task Image() => await Context.Interaction.RespondWithModalAsync<ImageModal>("image");

        [ModalInteraction("image")]
        public async Task ImageResponse(ImageModal modal)
        {
            await DeferAsync();

            _openAI.ImageRequest = new ImageRequest
            {
                Resolution = modal.ImageResolution,
                Prompt = modal.ImagePrompt,
                ImagesCount = 1
            };

            try
            {
                var result = await _openAI.GetImage();
                if(result != null && result.Images != null)
                {
                    var embed = new EmbedBuilder
                    {
                        Author = new EmbedAuthorBuilder
                        {
                            Name = Context.Interaction.User.Username,
                            IconUrl = Context.Interaction.User.GetAvatarUrl() ?? Context.Interaction.User.GetDefaultAvatarUrl(),
                        },
                        Description = Format.Bold(Format.Italics(modal.ImagePrompt)),
                        ImageUrl = result.Images[0].Url,
                        Timestamp = DateTime.UtcNow,
                        Color = Color.Blue
                    }.Build();

                    await FollowupAsync(embed: embed);
                }
            }
            catch (Exception ex)
            {
                await FollowupAsync(ex.Message, ephemeral: true);
            }
        }
    }
}

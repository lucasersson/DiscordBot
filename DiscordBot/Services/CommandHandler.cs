using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBot.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Services
{
    public record CommandHandler(DiscordSocketClient Client, InteractionService Commands, IServiceProvider Services)
    {
        public async Task InitializeAsync()
        {
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);

            Client.Ready += RegisterInteractions;
            Client.InteractionCreated += HandleInteraction;

            Commands.SlashCommandExecuted += HandleSlashCommand;
            Commands.ContextCommandExecuted += HandleContextCommand;
            Commands.ComponentCommandExecuted += HandleComponentCommand;
        }

        private async Task RegisterInteractions()
        {
            var configuration = Services.GetRequiredService<IConfiguration>();
            if (Debugger.IsAttached)
            {
                await Commands.RegisterCommandsToGuildAsync(ulong.Parse(configuration.GetValue<string>("Guilds:private")));
                await Commands.RegisterCommandsToGuildAsync(ulong.Parse(configuration.GetValue<string>("Guilds:3/3")));
                //_ = configuration.GetSection("Guilds").GetChildren().Select(async x =>
                //{
                //    await Commands.RegisterCommandsToGuildAsync(ulong.Parse(x.Value));
                //});
            }
            else
            {
                await Commands.RegisterCommandsGloballyAsync();
            }
        }

        private async Task HandleInteraction(SocketInteraction arg)
        {
            try
            {
                var ctx = new SocketInteractionContext(Client, arg);
                await Commands.ExecuteCommandAsync(ctx, Services);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());

                if(arg.Type == InteractionType.ApplicationCommand)
                {
                    await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
                }
            }
        }

        private Task HandleSlashCommand(SlashCommandInfo arg1, IInteractionContext arg2, IResult arg3)
        {
            if (!arg3.IsSuccess)
            {
                switch (arg3.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // implement
                        break;
                    case InteractionCommandError.BadArgs:
                        // implement
                        break;
                    case InteractionCommandError.Exception:
                        // implement
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // implement
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task HandleContextCommand( ContextCommandInfo arg1, IInteractionContext arg2, IResult arg3)
        {
            if (!arg3.IsSuccess)
            {
                switch (arg3.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // implement
                        break;
                    case InteractionCommandError.BadArgs:
                        // implement
                        break;
                    case InteractionCommandError.Exception:
                        // implement
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // implement
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task HandleComponentCommand(ComponentCommandInfo arg1, IInteractionContext arg2, IResult arg3)
        {
            if (!arg3.IsSuccess)
            {
                switch (arg3.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // implement
                        break;
                    case InteractionCommandError.BadArgs:
                        // implement
                        break;
                    case InteractionCommandError.Exception:
                        // implement
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // implement
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }
    }
}

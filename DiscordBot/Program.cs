using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.models.openai;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    static async Task Main()
    {
        var builder = new HostBuilder()
               .ConfigureAppConfiguration(x =>
               {
                   var configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", false, true)
                       .Build();

                   x.AddConfiguration(configuration);
               })
               .ConfigureLogging(x =>
               {
                   x.AddConsole();
                   x.SetMinimumLevel(LogLevel.Debug);
               })
               .ConfigureDiscordHost((context, config) =>
               {
                   config.SocketConfig = new DiscordSocketConfig
                   {
                       LogLevel = LogSeverity.Debug,
                       AlwaysDownloadUsers = false,
                       MessageCacheSize = 200,
                   };

                   var token = Environment.GetEnvironmentVariable("DiscordToken");
                   if (string.IsNullOrEmpty(token))
                   {
                       throw new Exception("Token not found.");
                   }

                   config.Token = token;
               })
               .ConfigureServices((context, services) =>
               {
                   //services.AddHttpClient("OpenAIClient", httpClient =>
                   //{
                   //    httpClient.BaseAddress = new Uri("https://api.tarkov.dev/graphql");
                   //});
                   services.AddHttpClient();

                   services.AddSingleton(new OpenAI(new OpenAIIonfiguration
                   {
                       ApiKey = Environment.GetEnvironmentVariable("OpenAI_API_Key")
                   })); ;
                   //.AddHostedService<CommandHandler>();
               })
               .UseCommandService((context, config) =>
               {
                   config.CaseSensitiveCommands = false;
                   config.LogLevel = LogSeverity.Debug;
                   config.DefaultRunMode = RunMode.Sync;
               })
               .UseConsoleLifetime();

    }
}

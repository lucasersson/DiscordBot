using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBot.Models;
using DiscordBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using OpenAI_API;
using RunMode = Discord.Commands.RunMode;

public class Program
{
    static void Main(string[] args)
        => RunAsync(args).GetAwaiter().GetResult();

    static async Task RunAsync(string[] args)
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
                   services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
                   services.AddSingleton<CommandHandler>();
                   services.AddHttpClient("OpenAI_API_Client", httpClient =>
                   {
                       httpClient.BaseAddress = new Uri(context.Configuration.GetValue<string>("BaseURLs:OpenAI"));
                       httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("OpenAI_API_Key"));
                       httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                       httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("lucasersson_discordbot");
                   });
                   services.AddTransient(sp =>
                   {
                       var factory = sp.GetService<IHttpClientFactory>();
                       var httpClient = factory?.CreateClient("OpenAI_API_Client") ?? new HttpClient();

                       return new OpenAI(httpClient);
                   });
               })
               .UseCommandService((context, config) =>
               {
                   config.CaseSensitiveCommands = false;
                   config.LogLevel = LogSeverity.Debug;
                   config.DefaultRunMode = RunMode.Sync;
               })
               .UseConsoleLifetime();

        var host = builder.Build();
        using (host)
        {
            var commandHandler = host.Services.GetRequiredService<CommandHandler>();
            await commandHandler.InitializeAsync();

            await host.RunAsync();
        }
    }
}
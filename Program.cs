
using Microsoft.Extensions.DependencyInjection;
using CommandLine;
using LacExporter.Services;

namespace LacExporter
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton<HttpService>()
                .AddSingleton<ExportService>()
                .BuildServiceProvider();

            await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(async opts => await serviceProvider.GetService<ExportService>()!.Export(opts));
        }
    }
}
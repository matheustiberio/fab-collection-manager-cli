using CommandLine;
using Exporter.Models;
using Exporter.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Exporter;

public static class Program
{
    private static async Task Main(string[] args)
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
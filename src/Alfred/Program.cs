using Alfred.Commands;
using Alfred.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;

namespace Alfred;

public static class Program
{
    private static readonly IServiceProvider Services = ServiceProviderBuilder.Build<ServiceConfiguration>();

    public static async Task<int> Main(string[] args)
    {
        return await Services.GetRequiredService<AppCommand>().InvokeAsync(args);
    }
}
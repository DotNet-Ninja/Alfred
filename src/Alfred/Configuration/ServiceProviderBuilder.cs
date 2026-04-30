using Microsoft.Extensions.DependencyInjection;

namespace Alfred.Configuration;

public static class ServiceProviderBuilder
{
    public static IServiceProvider Build<TConfiguration>() where TConfiguration: ServiceConfigurationBase, new()
    {
        var configuration = new TConfiguration();
        return configuration.Configure().BuildServiceProvider();
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Alfred.Configuration;

public abstract class ServiceConfigurationBase
{
    protected IServiceCollection Services { get; } = new ServiceCollection();

    public IServiceCollection Configure()
    {
        ConfigureServices();
        return Services;
    }

    public abstract void ConfigureServices();

    protected IServiceCollection AddSingleton<TService, TImplementation>() where TService: class where TImplementation : class, TService
    {
        return Services.AddSingleton<TService, TImplementation>();
    }

    protected IServiceCollection AddSingleton<TImplementation>() where TImplementation: class
    {
        return Services.AddSingleton<TImplementation>();
    }

    protected IServiceCollection AddTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        return Services.AddTransient<TService, TImplementation>();
    }

    protected IServiceCollection AddScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        return Services.AddScoped<TService, TImplementation>();
    }

    protected IServiceCollection AddSingleton<TService>(TService instance) where TService : class
    {
        return Services.AddSingleton(instance);
    }
}
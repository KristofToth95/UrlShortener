using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Features;
using UrlShortener.Features.Data;

namespace UrlShortener.Tests;

public class TestFixture
{
    public ServiceProvider ServiceProvider { get; }

    public TestFixture()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IUrlMapDb, InMemoryUrlMapDb>();
        services.AddTransient<IUrlShortener, Features.Features.UrlShortener>();
        ServiceProvider = services.BuildServiceProvider();
    }
}
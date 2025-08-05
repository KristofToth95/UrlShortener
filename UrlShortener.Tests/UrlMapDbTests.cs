using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Features.Data;
using Xunit;

namespace UrlShortener.Tests;

public class UrlMapDbTests : IClassFixture<TestFixture>
{
    private readonly IUrlMapDb _db;
    public UrlMapDbTests(TestFixture fixture)
    {
        _db = fixture.ServiceProvider.GetRequiredService<IUrlMapDb>();
    }
    
    [Fact]
    public void SaveAndGet_ReturnsCorrectUrl()
    {
        // Can be moved to a seperate file, for this simple project I will leave the test data here.
        string shortUrl = "abc123";
        string longUrl = "https://example.com";

        _db.SaveUrlMapping(shortUrl, longUrl);
        var result = _db.GetLongUrl(shortUrl);

        Assert.Equal(longUrl, result);
    }
    
    [Fact]
    public void SaveUrlMapping_OverwritesExistingShortUrl()
    {
        // Can be moved to a seperate file, for this simple project I will leave the test data here.
        string shortUrl = "abc123";
        string firstUrl = "https://first.com";
        string updatedUrl = "https://second.com";

        _db.SaveUrlMapping(shortUrl, firstUrl);
        _db.SaveUrlMapping(shortUrl, updatedUrl);
        var result = _db.GetLongUrl(shortUrl);

        Assert.Equal(updatedUrl, result);
    }
    
    [Fact]
    public void ExpandUrl_WithUnknownShortCode_ThrowsInvalidOperationException()
    {
        Assert.Throws<KeyNotFoundException>(() => _db.GetLongUrl("notfound"));
    }
}
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Features;
using UrlShortener.Features.Data;
using UrlShortener.Features.Infrastructure;
using Xunit;

namespace UrlShortener.Tests;

public class UrlShortenerTests : IClassFixture<TestFixture>
{
    private readonly IUrlShortener _shortener;
    private readonly IUrlMapDb _db;
    
    public UrlShortenerTests(TestFixture fixture)
    {
        _shortener = fixture.ServiceProvider.GetService<IUrlShortener>();
        _db = fixture.ServiceProvider.GetRequiredService<IUrlMapDb>();
    }
    
    [Fact]
    public void ShortenUrl_ShouldReturnShortenedUrl()
    {
        var result = _shortener.ShortenUrl("https://example.com");
        
        Assert.True(result.IsSuccess);
        Assert.False(string.IsNullOrWhiteSpace(result.Value));
        Assert.Equal(6, result.Value.Length);
    }
    
    [Fact]
    public void ExpandUrl_ShouldReturnOriginalUrl()
    {
        var longUrl = "https://example.com";
        var shortUrl = _shortener.ShortenUrl(longUrl);
        var result = _shortener.ExpandUrl(shortUrl.Value);
        
        Assert.True(result.IsSuccess);
        Assert.False(string.IsNullOrWhiteSpace(result.Value));
        Assert.True(result.Value == longUrl);
    }
    
    [Fact]
    public void ShortenUrl_ShouldReturnArgumentErrorIfInputEmpty()
    {
        var result = _shortener.ShortenUrl("");
        
        Assert.False(result.IsSuccess);
        Assert.True(result.Error != null);
        Assert.False(result.Error?.Type == ErrorType.Argument);
    }
    
    [Fact]
    public void ExpandUrl_ShouldReturnArgumentErrorIfInputEmpty()
    {
        var result = _shortener.ExpandUrl("");
        
        Assert.False(result.IsSuccess);
        Assert.True(result.Error != null);
        Assert.False(result.Error?.Type == ErrorType.Argument);
    }
    
    [Fact]
    public void ExpandUrl_ShouldReturnNotFoundErrorWhenNotFoundInDb()
    {
        var result = _shortener.ExpandUrl("123");
        
        Assert.False(result.IsSuccess);
        Assert.True(result.Error != null);
        Assert.False(result.Error?.Type == ErrorType.NotFound);
    }
    
    [Fact]
    public void ShortenUrl_ShouldReturnTheShortenedUrlFromTheDb()
    {
        var longUrl = "https://example.com";
        var shortUrl = _shortener.ShortenUrl(longUrl);
        
        var stored = _db.GetLongUrl(shortUrl.Value);
        
        Assert.Equal(longUrl, stored);
    }
}

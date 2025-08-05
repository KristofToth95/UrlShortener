using UrlShortener.Features.Infrastructure;

namespace UrlShortener.Features;

public interface IUrlShortener
{
    public Result<string> ShortenUrl(string longUrl);
    public Result<string> ExpandUrl(string shortUrl);
}
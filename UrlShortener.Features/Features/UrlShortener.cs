using UrlShortener.Features.Data;
using UrlShortener.Features.Infrastructure;

namespace UrlShortener.Features.Features;

public class UrlShortener : IUrlShortener
{
    private readonly IUrlMapDb _db;

    public UrlShortener(IUrlMapDb db)
    {
        _db = db;
    }

    public Result<string> ShortenUrl(string longUrl)
    {
        if (string.IsNullOrWhiteSpace(longUrl))
            return Result<string>.Failure(Error.Argument("The longUrl cannot be null, empty, or whitespace."));

        var shortUrl = Guid.NewGuid().ToString("N")[..6];
        _db.SaveUrlMapping(shortUrl, longUrl);
        return Result<string>.Success(shortUrl);
    }

    public Result<string> ExpandUrl(string shortUrl)
    {
        if (string.IsNullOrWhiteSpace(shortUrl))
            return Result<string>.Failure(Error.Argument("Short URL must not be null or empty."));

        try
        {
            var longUrl = _db.GetLongUrl(shortUrl);
            return Result<string>.Success(longUrl);
        }
        catch (KeyNotFoundException ex)
        {
            return Result<string>.Failure(Error.NotFound(ex.Message));
        }
    }
}
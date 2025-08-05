using UrlShortener.Features.Data;

public class InMemoryUrlMapDb : IUrlMapDb
{
    private readonly Dictionary<string, string> _map = new();

    public void SaveUrlMapping(string shortUrl, string longUrl)
        => _map[shortUrl] = longUrl;

    public string GetLongUrl(string shortUrl)
    {
        var longUrl = _map.GetValueOrDefault(shortUrl);
        if(longUrl == null)
            throw new KeyNotFoundException("There is no corresponding URL available for this shortUrl.");
        
        return longUrl;
    }
}
using System.Net;

namespace MusicPlayer.Bussines.Results;

public class Response
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public HttpStatusCode? HttpStatusCode { get; set; }
    public object? Data { get; set; }
}

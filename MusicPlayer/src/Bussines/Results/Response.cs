using Microsoft.AspNetCore.Identity;
using System.Net;

namespace MusicPlayer.Bussines.Results;

public record Response(string? title, HttpStatusCode statusCode, IdentityResult? data, object? obj);

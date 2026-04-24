namespace ShatteredRealms.Application.DTOs.Auth;

public sealed record ConfirmEmailRequest(string UserId, string Token);

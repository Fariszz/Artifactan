namespace Artifactan.Dto.Response;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
}
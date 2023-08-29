using Artifactan.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Artifactan.Entities.Master;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseEntity
{

    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public string? Otp { get; set; } = string.Empty;

}
using System.ComponentModel.DataAnnotations;

namespace Data.DTO_s;

public class RegisterDto
{
    public string Username { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;
}

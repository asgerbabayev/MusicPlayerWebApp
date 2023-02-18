using System.ComponentModel.DataAnnotations;

namespace Data.DTO_s;

public class LoginDto
{
    public string Username { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}

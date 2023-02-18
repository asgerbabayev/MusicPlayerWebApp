using Data.DTO_s;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using MusicPlayer.Bussines.Abstract;
using MusicPlayer.Bussines.Results;

namespace MusicPlayer.Bussines.Concrete;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;

    public AuthManager(UserManager<AppUser> userManager,
                       SignInManager<AppUser> signInManager,
                       RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public Task<Response> Login(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> Register(RegisterDto registerDto)
    {
        AppUser? existsUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existsUser != null) throw new Exception("Neynirsen bashin xarabdi");
        AppUser user = new()
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) throw new Exception(result.Errors.Select(x => x.Description).ToString());

        var roleResult = await _userManager.AddToRoleAsync(user, "Manager");
        if (!roleResult.Succeeded) throw new Exception(result.Errors.Select(x => x.Description).ToString());
        return new Response()
        {
            HttpStatusCode = System.Net.HttpStatusCode.OK,
            Title = "Success",
            Description = "",
            Data = ""
        };

    }

    public async Task CreateRole()
    {
        await _roleManager.CreateAsync(new AppRole() { Name = "Admin" });
        await _roleManager.CreateAsync(new AppRole() { Name = "Manager" });
        await _roleManager.CreateAsync(new AppRole() { Name = "Member" });
    }
}

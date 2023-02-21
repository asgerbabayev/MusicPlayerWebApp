using Data.DTO_s;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using MusicPlayer.Bussines.Abstract;
using MusicPlayer.Bussines.Results;
using System.Net;
using System.Security.Claims;

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

    public async Task<Response> Login(LoginDto loginDto)
    {
        var userExists = await _userManager.FindByNameAsync(loginDto.Username);
        if (userExists == null) return new Response("Invalid Credentials", HttpStatusCode.Unauthorized, null, null);
        bool result = await _userManager.CheckPasswordAsync(userExists, loginDto.Password);
        if (!result) return new Response("Invalid Credentials", HttpStatusCode.Unauthorized, null, null);

        IList<string> roles = await _userManager.GetRolesAsync(userExists);

        IList<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userExists.Id.ToString()),
            new Claim(ClaimTypes.Name,userExists.UserName),
            new Claim(ClaimTypes.Email,userExists.Email)
        };

        foreach (string role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        await _signInManager.SignInAsync(userExists, true);
        return new Response("error", HttpStatusCode.OK, null, null);
    }

    public async Task<Response> Register(RegisterDto registerDto)
    {
        AppUser? existsUser = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existsUser != null) throw new Exception();
        AppUser user = new()
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return new Response("error", HttpStatusCode.BadRequest, result, null);

        var roleResult = await _userManager.AddToRoleAsync(user, "Manager");
        if (!roleResult.Succeeded) return new Response("error", HttpStatusCode.BadRequest, roleResult, null);
        return new Response("success", HttpStatusCode.OK, roleResult, null);
    }

    public async Task CreateRole()
    {
        await _roleManager.CreateAsync(new AppRole() { Name = "Admin" });
        await _roleManager.CreateAsync(new AppRole() { Name = "Manager" });
        await _roleManager.CreateAsync(new AppRole() { Name = "Member" });
    }
}

using Data.DTO_s;
using Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicPlayer.Bussines.Abstract;
using MusicPlayer.Bussines.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MusicPlayer.Bussines.Concrete;

public class AuthManager : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    //private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthManager(UserManager<AppUser> userManager,
                       RoleManager<AppRole> roleManager,
                       IConfiguration configuration)
    {
        _userManager = userManager;
        //_signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<Response> Login(LoginDto loginDto)
    {
        var userExists = await _userManager.FindByNameAsync(loginDto.Username);
        if (userExists == null) return new Response("Invalid Credentials", HttpStatusCode.Unauthorized, null, null);
        bool result = await _userManager.CheckPasswordAsync(userExists, loginDto.Password);
        if (!result) return new Response("Invalid Credentials", HttpStatusCode.Unauthorized, null, null);

        IList<string> roles = await _userManager.GetRolesAsync(userExists);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, userExists.Id.ToString()),
            new Claim(ClaimTypes.Name,userExists.UserName),
            new Claim(ClaimTypes.Email,userExists.Email)
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:securityKey"]));

        SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        DateTime expires = DateTime.Now.AddSeconds(30);

        JwtSecurityToken securityToken = new(
            issuer: _configuration["Jwt:issuer"],
            audience: _configuration["Jwt:audience"],
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
            );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new Response("success", HttpStatusCode.OK, null, token);
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

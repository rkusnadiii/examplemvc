using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using examplemvc.Models; 
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class JWTController : ControllerBase
{
    // private readonly UserManager<Login> _userManager; 
    private readonly IConfiguration _configuration;

    public JWTController(IConfiguration configuration)
    {
        // _userManager = userManager;
        _configuration = configuration;
    }


    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login([FromBody] Login loginModel)
    {
        if (ModelState.IsValid)
        {
            // var log = await _userManager.FindByNameAsync(loginModel.Username);

            // if (log != null && await _userManager.CheckPasswordAsync(log, loginModel.Password))
            // {
            var log = loginModel.Username;
            var token = GenerateJWTToken(log, loginModel.Role);
            return Ok(new { Token = token });
            //}
        }

        return Unauthorized();
    }

    private string GenerateJWTToken(string user, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sid, user),
            new Claim("Role", role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

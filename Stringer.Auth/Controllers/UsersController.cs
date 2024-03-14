using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stringer.Auth.Db.Repository.Users;
using Stringer.Auth.Domain.Models;

namespace Stringer.Auth.Controllers;

[Produces("application/json")]
[Route("api/users/")]
[ApiController]
[Authorize]
public class UsersController: ControllerBase
{
    private IUsersRepository _usersRepository;

    public UsersController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [HttpGet("token")]
    [AllowAnonymous]
    public async Task<IActionResult> GetToken([FromQuery]string login, [FromQuery]string password)
    {
        if (!await _usersRepository.Login(login, password))
            return Unauthorized();

        var user = await _usersRepository.GetUser(login, password);
        var issuer = "https://stringer.com/";
        var audience = "https://stringer.com/";
        var key = Encoding.ASCII.GetBytes("ThisIsSecretKeyFromSecretPlaceThisIsSecretKeyFromSecretPlaceThisIsSecretKeyFromSecretPlaceThisIsSecretKeyFromSecretPlace");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", user.Id),

            }),
            Expires = DateTime.UtcNow.AddHours(3),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return Ok(stringToken);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _usersRepository.GetUsers();
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromQuery]string login, [FromQuery]string password)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            return BadRequest("Bad login or password");
        var result = await _usersRepository.CreateUser(login, password);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromQuery]string login, [FromQuery]string password, [FromQuery]string id)
    {
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            return BadRequest("Bad login or password");
        await _usersRepository.UpdateUser(new User { Id = id, Login = login, Password = password });
        return Ok("OK");
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromQuery]string id)
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest("Empty id");
        await _usersRepository.DeleteUser(id);
        return Ok("OK");
    }
}
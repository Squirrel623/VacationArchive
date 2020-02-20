using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using server.Services;
using server.Controllers.ApiModels;


namespace server.Controllers 
{
  public class Test {
    public int Id {get;set;}
  }

  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  [ApiController]
  [Route("[controller]/[action]")]
  public class UserController : ControllerBase 
  {
    private IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult<LoginResponse> Login([FromBody]LoginRequest request)
    {
      var id = 1;
      var username = "Squirrel623";

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes("1234567891234567");
      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new Claim[] {
          new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
          new Claim(JwtRegisteredClaimNames.UniqueName, username)
        }),
        Expires = DateTime.UtcNow.AddDays(1),
        NotBefore = DateTime.UtcNow,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return new LoginResponse(id: 1, username: "Squirrel623", token: tokenHandler.WriteToken(token));
    }

    [HttpGet]
    public ActionResult<Test> Test()
    {
      var identity = HttpContext.User.Identity as ClaimsIdentity;
      if (identity == null) {
        return Unauthorized();
      }

      Console.WriteLine("Claims for test:");
      foreach(var claim in identity.Claims)
      {
        Console.WriteLine("\nType: " + claim.Type);
        Console.WriteLine("\nValue: " + claim.Value);
      }

      var idClaimStr = identity.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
      if (!int.TryParse(idClaimStr, out int idClaim)) {
        return Unauthorized();
      }

      return new Test() {Id = idClaim};
    }

    [HttpPost]
    public IActionResult Register()
    {
      return Ok();
    }
  }
}
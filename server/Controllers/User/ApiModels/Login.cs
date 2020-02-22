using System;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.User.ApiModels
{
  public class LoginRequest
  {
    [Required]
    public string Username {get; set;} = null!;

    [Required]
    public string Password {get; set;} = null!;
  }

  public class LoginResponse
  {
    public LoginResponse(int id, string username, string token) {
      Id = id;
      Username = username;
      Token = token;
    }

    public int Id {get;}
    public string Username {get;}
    public string Token {get;}
  }
}
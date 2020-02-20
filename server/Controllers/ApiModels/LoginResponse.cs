using System;

namespace server.Controllers.ApiModels
{
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
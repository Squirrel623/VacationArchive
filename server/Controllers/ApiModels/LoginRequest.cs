using System;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.ApiModels
{
  public class LoginRequest
  {
    [Required]
    public string Username {get; set;} = null!;

    [Required]
    public string Password {get; set;} = null!;
  }
}
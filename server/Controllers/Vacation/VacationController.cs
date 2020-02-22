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
using server.Controllers.Vacation.ApiModels;


namespace server.Controllers 
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class VacationController : ControllerBase 
  {
    private IUserService _userService;

    public VacationController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost]
    public ActionResult<AddResponse> Add([FromBody]AddRequest request)
    {
      Console.WriteLine(request.Title);
      return new AddResponse(request.Title);
    }
  }
}
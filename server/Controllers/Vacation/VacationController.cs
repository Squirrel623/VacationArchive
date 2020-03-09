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
  [Route("vacations")]
  public class VacationController : ControllerBase 
  {
    private IUserService _userService;
    private IVacationService _vacationService;

    public VacationController(IUserService userService, IVacationService vacationService)
    {
      _userService = userService;
      _vacationService = vacationService;
    }

    [HttpPost]
    public ActionResult<CreateResponse> Create([FromBody]CreateRequest request)
    {
      //Right now we don't have the user's id from the request. That will be supplied by the JWT
      //infrastructure. For now just use a hard coded user ID.
      var vacation = _vacationService.Create(1, request.Title, request.StartDate, request.EndDate);
      if (vacation is null)
      {
        return BadRequest();
      }
      
      return Created($"Vacation/{vacation.Id}", vacation);
    }

    [HttpGet]
    public ActionResult<GetAllResponse> GetAll()
    {
      return new GetAllResponse(vacations: _vacationService.GetAll(userId: 1).Select(modelVacation => new Controllers.Vacation.ApiModels.Vacation {
          Id = modelVacation.Id,
          CreatedBy = modelVacation.CreatedBy,
          Title = modelVacation.Title,
          StartDate = modelVacation.StartDate,
          EndDate = modelVacation.EndDate ?? DateTime.Now
        }) 
      );
    }

    [HttpGet("{id}")]
    public ActionResult<GetResponse> Get(int id)
    {
      var vacation = _vacationService.Get(id);
      if (vacation is null)
      {
        return NotFound();
      }

      return Ok(vacation);
    }

  }
}
using System;
using System.IO;
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
using Microsoft.AspNetCore.Http;

using server.Services;
using server.Controllers.Vacation.ApiModels;
using server.Storage;

using ApiModels = server.Controllers.Vacation.ApiModels;
using Activites = server.Controllers.Vacation.ApiModels.Activites;
using ActivityMedia = server.Controllers.Vacation.ApiModels.Activites.Media;

namespace server.Controllers 
{
  [ApiController]
  [Route("vacations")]
  public class VacationController : ControllerBase 
  {
    private IUserService _userService;
    private IVacationService _vacationService;
    private IActivityService _activityService;

    public VacationController(IUserService userService, IVacationService vacationService, IActivityService activityService)
    {
      _userService = userService;
      _vacationService = vacationService;
      _activityService = activityService;
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
      return new GetAllResponse(vacations: _vacationService.GetAll(userId: 1).Select(modelVacation => ApiModels.Vacation.FromModelVacation(modelVacation)));
    }

    [HttpGet("{id}")]
    public ActionResult<GetResponse> Get(int id)
    {
      var modelVacation = _vacationService.Get(id);
      if (modelVacation is null)
      {
        return NotFound();
      }

      return GetResponse.FromApiModelVacation(ApiModels.Vacation.FromModelVacation(modelVacation));
    }

    [HttpGet("{id}/activities")]
    public ActionResult<Activites.GetAllResponse> GetAllActivities(int id)
    {
      return new Activites.GetAllResponse(vacationActivities: _vacationService.GetAllActivities(id).Select(modelActivity => Activites.VacationActivity.FromModel(modelActivity)));
    }

    [HttpGet("{vacationId}/activities/{activityId}")]
    public ActionResult<Activites.GetResponse> GetActivity(int vacationId, int activityId)
    {
      var modelActivity = _vacationService.GetActivity(activityId);
      if (modelActivity is null)
      {
        return NotFound();
      }

      return Activites.GetResponse.FromApiModelActivity(Activites.VacationActivity.FromModel(modelActivity));
    }

    [HttpGet("{vacationId}/activities/{activityId}/media")]
    public async Task<ActionResult<ActivityMedia.GetAllResponse>> GetAllActivityMedia(int vacationId, int activityId)
    {
      try
      {
        var result = await _activityService.GetAllActivityMedia(vacationId: vacationId, activityId: activityId);

        if (result is null)
        {
          return NotFound();
        }

        return Ok(new ActivityMedia.GetAllResponse() {
          Items = result.Select((mediaRecord) => ActivityMedia.VacationActivityMedia.FromModel(mediaRecord))
        });
      }
      catch(Exception e)
      {
        return StatusCode(500, e.ToString());
      }
    }
  
    [HttpPost("{vacationId}/activities/{activityId}/media"), DisableRequestSizeLimit]
    public async Task<ActionResult<ActivityMedia.CreateResponse>> SaveMedia(int vacationId, int activityId)
    {
      Console.WriteLine("In Save Media");
      try
      {
        IFormFile file = Request.Form.Files[0];
        Stream fileStream = file.OpenReadStream();

        Models.VacationActivityMedia? savedMediaRecord = await _activityService.SaveMediaContents(
          vacationId: vacationId,
          activityId: activityId,
          contents: fileStream,
          name: file.FileName,
          contentType: file.ContentType
        );

        return savedMediaRecord is null ? 
          StatusCode(500, "Save unsuccessful") :
          Ok(ActivityMedia.CreateResponse.FromModel(savedMediaRecord));
      }
      catch(Exception e)
      {
        Console.WriteLine(e.ToString());
        return StatusCode(500, e.ToString());
      }
    }

    [HttpGet("{vacationId}/activities/{activityId}/media/{mediaId}")]
    public async Task<ActionResult<ActivityMedia.GetResponse>> GetMedia(int vacationId, int activityId, int mediaId)
    {
      
      try
      {
        var result = await _activityService.GetMedia(
          vacationId: vacationId,
          activityId: activityId,
          mediaId: mediaId
        );

        if (result is null)
        {
          return NotFound();
        }

        return Ok(ActivityMedia.VacationActivityMedia.FromModel(result));
      }
      catch(Exception e)
      {
        return StatusCode(500, e.ToString());
      }
    }

    [HttpGet("{vacationId}/activities/{activityId}/media/{mediaId}/contents")]
    public async Task<ActionResult> GetMediaContents(int vacationId, int activityId, int mediaId)
    {
      try
      {
        var result = await _activityService.GetMediaContents(
          vacationId: vacationId,
          activityId: activityId,
          mediaId: mediaId
        );

        if (result is null)
        {
          return NotFound();
        }

        result.Contents.Seek(0, SeekOrigin.Begin);
        return new FileStreamResult(result.Contents, result.ContentType);
      }
      catch(Exception e)
      {
        return StatusCode(500, e.ToString());
      }
    }
  }
}
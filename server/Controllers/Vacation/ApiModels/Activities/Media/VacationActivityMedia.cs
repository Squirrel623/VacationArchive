using System;
using Models = server.Models;

namespace server.Controllers.Vacation.ApiModels.Activites.Media
{
  public class VacationActivityMedia
  {
    public int Id {get;set;}
    public int ActivityId {get;set;}
    public int VacationId {get;set;}
    public string ContentType {get;set;} = "";

    public static VacationActivityMedia FromModel(Models.VacationActivityMedia mediaRecord)
    {
      return new VacationActivityMedia
      {
        Id = mediaRecord.Id,
        VacationId = mediaRecord.VacationId,
        ActivityId = mediaRecord.ActivityId,
        ContentType = mediaRecord.ContentType
      };
    }
  }
}
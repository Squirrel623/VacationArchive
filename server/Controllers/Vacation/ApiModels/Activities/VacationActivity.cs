using System;
using Models = server.Models;

namespace server.Controllers.Vacation.ApiModels.Activites
{
  public class VacationActivity
  {
    public int Id {get;set;}
    public int VacationId {get;set;}
    public string Title {get;set;} = "";
    public DateTime Date {get;set;}

    public static VacationActivity FromModel(Models.VacationActivity activity)
    {
      return new VacationActivity
      {
        Id = activity.Id,
        VacationId = activity.VacationId,
        Title = activity.Title,
        Date = activity.Date,
      };
    }
  }
}
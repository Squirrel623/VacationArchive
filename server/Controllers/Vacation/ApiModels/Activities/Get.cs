using System;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.Vacation.ApiModels.Activites
{
  public class GetResponse : VacationActivity 
  {
    public static GetResponse FromApiModelActivity(VacationActivity activity)
    {
      return new GetResponse()
      {
        Id = activity.Id,
        VacationId = activity.VacationId,
        Title = activity.Title,
        Date = activity.Date
      };
    }
  }
}
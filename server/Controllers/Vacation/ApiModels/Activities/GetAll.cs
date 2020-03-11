using System;
using System.Collections.Generic;

namespace server.Controllers.Vacation.ApiModels.Activites
{
  public class GetAllResponse
  {
    public GetAllResponse(IEnumerable<VacationActivity> vacationActivities)
    {
      VacationActivities = vacationActivities;
    }
    public IEnumerable<VacationActivity> VacationActivities {get;}
  }
}
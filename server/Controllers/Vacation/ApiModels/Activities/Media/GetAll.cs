using System;
using System.Collections.Generic;

namespace server.Controllers.Vacation.ApiModels.Activites.Media
{
  public class GetAllResponse
  {
    public IEnumerable<VacationActivityMedia> Items {get;set;} = new List<VacationActivityMedia>();
  }
}
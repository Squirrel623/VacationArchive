using System;
using System.Collections.Generic;

namespace server.Controllers.Vacation.ApiModels
{
  public class GetAllResponse
  {
    public GetAllResponse(IEnumerable<Vacation> vacations)
    {
      Vacations = vacations;
    }
    public IEnumerable<Vacation> Vacations {get;}
  }
}
using System;
using System.Collections.Generic;

using server.Models;

namespace server.Controllers.Vacation.ApiModels
{
  public class Vacation
  {
    public int Id {get;set;}
    public int CreatedBy {get;set;}
    public string Title {get;set;} = "";
    public DateTime StartDate {get;set;}
    public DateTime EndDate {get;set;}
  }

  public class GetAllResponse
  {
    public GetAllResponse(IEnumerable<Vacation> vacations)
    {
      Vacations = vacations;
    }
    public IEnumerable<Vacation> Vacations {get;}
  }
}
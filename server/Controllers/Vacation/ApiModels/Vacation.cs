using System;

namespace server.Controllers.Vacation.ApiModels
{
  public class Vacation
  {
    public int Id {get;set;}
    public int CreatedBy {get;set;}
    public string Title {get;set;} = "";
    public DateTime StartDate {get;set;}
    public DateTime EndDate {get;set;}

    public static Vacation FromModelVacation(Models.Vacation vacation)
    {
      return new Vacation()
      {
        Id = vacation.Id,
        CreatedBy = vacation.CreatedBy,
        Title = vacation.Title,
        StartDate = vacation.StartDate,
        EndDate = vacation.EndDate ?? DateTime.Now
      };
    }
  }
}
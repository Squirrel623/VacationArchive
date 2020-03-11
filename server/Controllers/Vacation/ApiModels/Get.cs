using System;
using System.ComponentModel.DataAnnotations;
using Models = server.Models;

namespace server.Controllers.Vacation.ApiModels
{
  public class GetResponse : Vacation 
  {
    public static GetResponse FromApiModelVacation(Vacation vacation)
    {
      return new GetResponse
      {
        Id = vacation.Id,
        CreatedBy = vacation.CreatedBy,
        Title = vacation.Title,
        StartDate = vacation.StartDate,
        EndDate = vacation.EndDate,
      };
    }
  }
}
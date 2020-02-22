using System;
using System.ComponentModel.DataAnnotations;

namespace server.Controllers.Vacation.ApiModels
{
  public class AddRequest
  {
    [Required]
    public string Title {get; set;} = null!;

    [Required]
    public DateTime StartDate {get; set;}

    [Required]
    public DateTime EndDate {get;set;}
  }

  public class AddResponse
  {
    public AddResponse(string title)
    {
      Title = title;
    }
    public string Title {get;}
  }
}
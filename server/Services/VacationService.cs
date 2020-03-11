using System;
using System.Collections.Generic;
using System.Linq;
using server.Models;
using server.Models.Context;

namespace server.Services
{
  public interface IVacationService
  {
    IEnumerable<Vacation> GetAll(int userId);
    Vacation? Create(int user, string title, DateTime startDate, DateTime endDate);
    Vacation? Get(int id);
    IEnumerable<VacationActivity> GetAllActivities(int vacationId);
    VacationActivity? GetActivity(int activityId);
  }

  public class VacationService : IVacationService
  {
    private AppDbContext _context;
    public VacationService(AppDbContext context)
    {
      _context = context;
    }

    public IEnumerable<Vacation> GetAll(int userId)
    {
      return _context.Vacation.Where(vacation => vacation.CreatedBy == userId).AsEnumerable();
    }

    public Vacation? Get(int id)
    {
      return _context.Vacation.FirstOrDefault(vacation => vacation.Id == id);
    }

    public Vacation? Create(int user, string title, DateTime startDate, DateTime endDate) 
    {
      Vacation newVacation = new Vacation()
      {
        Id = default,
        CreatedBy = user,
        Title = title,
        StartDate = startDate,
        EndDate = endDate
      };

      try {
        var result = _context.Vacation.Add(newVacation);
        result.Context.SaveChanges();

        return result.Entity;
      } catch {
        return null;
      }
    }
  
    public IEnumerable<VacationActivity> GetAllActivities(int vacationId)
    {
      return _context.VacationActivity.Where(activity => activity.VacationId == vacationId).AsEnumerable();
    }

    public VacationActivity? GetActivity(int activityId)
    {
      return _context.VacationActivity.FirstOrDefault(activity => activity.Id == activityId);
    }
  }
}
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
  }
}
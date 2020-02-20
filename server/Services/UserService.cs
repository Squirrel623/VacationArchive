using System;
using System.Collections.Generic;
using System.Linq;
using server.Models;
using server.Models.Context;

namespace server.Services
{
  public interface IUserService
  {
    User? Authenticate(string username, string password);
    User? Create(User user, string password);
    User? GetById(int id);
  }

  public class UserService : IUserService
  {
    private AppDbContext _context;
    public UserService(AppDbContext context)
    {
      _context = context;
    }

    public User? Authenticate(string username, string password)
    {
      return null;
    }
    public User? Create(User user, string password)
    {
      return null;
    }
    public User? GetById(int id)
    {
      return null;
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models.Context;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacationController : ControllerBase
    {
        private readonly ILogger<VacationController> _logger;
        private readonly AppDbContext db;

        public VacationController(ILogger<VacationController> logger, AppDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(
              db.Vacation.Include(vacation => vacation.VacationActivity).ToArray()
            );
        }
    }
}

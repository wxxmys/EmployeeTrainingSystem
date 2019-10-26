using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTrainingSystem.Controllers
{
    /// <summary>
    /// 学校
    /// </summary>
    public class SchoolController : Controller
    {
        public readonly EntityDbContext _context;
        public SchoolController(EntityDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
          
            return View();
        }
      
    }
}
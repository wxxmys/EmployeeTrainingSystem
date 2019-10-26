using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTrainingSystem.Controllers
{
    public class CurriculumController : Controller
    {
        private readonly EntityDbContext _context = new EntityDbContext();
        public IActionResult Index()
        {
            var entity = _context.ClassSchedule.ToList();
            return View(entity);
        }
        public IActionResult Front()
        {
            return View();
        }
        public IActionResult BackEnd()
        {
            return View();
        }
    }
}
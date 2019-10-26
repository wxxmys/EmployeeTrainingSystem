using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EmployeeTrainingSystem.ViewComponents
{
    public class CourselistViewComponent:ViewComponent
    {
        private  EntityDbContext _context { get; }
        public CourselistViewComponent (EntityDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(Guid id)
        {
            var course =  _context.TeachingPlans.Include(t => t.Courseware)
                .Where(t => t.Parent.ID == id || t.ID == id).OrderBy(x => x.CreateDate).ToList();
            return View(course);
        }
    }
}

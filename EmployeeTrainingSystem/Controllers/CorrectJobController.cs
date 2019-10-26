using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
    /// <summary>
    /// 批改作业
    /// </summary>
    public class CorrectJobController : Controller
    {
        private readonly EntityDbContext _context;
        public CorrectJobController(EntityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var CorrectJobSelect = _context.CorrectJob.AsNoTracking().ToList();
            return View(CorrectJobSelect);
        }
        /// <summary>
        /// 批改
        /// </summary>
        /// <returns></returns>
        public IActionResult Update()
        {
            var InformationValue = _context.Information.ToList();
            ViewBag.InformationValue = InformationValue;//通过ViewBag传递值

            var TeacherValue = _context.Teacher.ToList();
            ViewBag.TeacherValue = TeacherValue;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeTrainingEntity.CorrectJob correctJob,Guid id)
        {
            var CorrectJobUpdate = _context.CorrectJob.Find(id);
           // CorrectJobUpdate.User = _context.user.Find(correctJob.User.UserID);
            CorrectJobUpdate.Grade = correctJob.Grade;
            CorrectJobUpdate.Teacher = _context.Teacher.Find(correctJob.Teacher.Id);
            CorrectJobUpdate.Publishjob = _context.Publishjob.Find(correctJob.Publishjob.ID);


            await _context.SaveChangesAsync();
            return View();
        }
        /// <summary>
        /// 查
        /// </summary>
        /// <returns></returns>
        public IActionResult Select()
        {
            var CorrectJobSelect = _context.CorrectJob.ToList();
            //var CorrectJobSelect = _context.CorrectJob.AsQueryable().Where();
            return View(CorrectJobSelect);
        }

        public IActionResult SelectID(Guid id)
        {
            var CorrectJobSelectID = _context.CorrectJob.AsQueryable().Where(t => t.ID == id);
            return View(CorrectJobSelectID);
        }
    }
}
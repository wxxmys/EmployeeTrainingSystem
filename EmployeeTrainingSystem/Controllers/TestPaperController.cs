using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
   
    public class TestPaperController : Controller
    {
        private readonly EntityDbContext _context = new EntityDbContext();
        public IActionResult Index()
        {
            var entity = _context.TestPaper.ToList();
            return View(entity);
        }
        public IActionResult Add()
        {
            var TeacherValue = _context.Teacher.ToList();
            ViewBag.TeacherValue = TeacherValue;//通过ViewBag传递值
            var CollegeClassValue = _context.CollegeClass.ToList();
            ViewBag.CollegeClassValue = CollegeClassValue;
            return View();
        }
        [HttpPost]
        public IActionResult Add(TestPaper testPaper)
        {
            var t = testPaper.Time.ToString();
            var entity = new TestPaper()
            {
                ID = Guid.NewGuid(),
                Name=testPaper.Name,
                //Teacher = _context.Teacher.Find(testPaper.Teacher.Id),
                //CollegeClass=_context.CollegeClass.Find(testPaper.CollegeClass.ID),
                Time = t != "" ? Convert.ToDateTime(t) : DateTime.Now,
                TestTime=testPaper.TestTime,
                Performance=testPaper.Performance

            };

            _context.Add(entity);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            var entity = _context.TestPaper.Find(id);
            return View(entity);
        }
        [HttpPost]
        public IActionResult Edit(Guid id, TestPaper testPaper)
        {
            var entity = _context.TestPaper.Find(id);
            entity.Teacher = testPaper.Teacher;
            entity.CollegeClass = testPaper.CollegeClass;
            entity.Name = testPaper.Name.ToString().Trim();
            entity.Time = testPaper.Time;
            entity.TestTime = testPaper.TestTime;
            entity.Performance = testPaper.Performance;
            _context.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid id)
        {
            var entity = _context.TestPaper.Find(id);
            _context.Remove(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detail(Guid id)
        {
            var entity = _context.TestPaper.Find(id);

            return View(entity);
        }

    }
}
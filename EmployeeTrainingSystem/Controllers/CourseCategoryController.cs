using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using EmployeeTrainingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
    //[Authorize(Roles = "Teacher")]
    public class CourseCategoryController : Controller
    {
        private readonly EntityDbContext _context = new EntityDbContext();
        //课程分类
        public IActionResult Index()
        {
            var entity = _context.ClassSchedule.ToList();
            return View(entity);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(IFormCollection add)//增
        {
            var t = add["Time"].ToString();
            var entity = new ClassSchedule
            {
                ID = Guid.NewGuid(),
                ClassScheduleName = add["ClassScheduleName"].ToString(),
                Time = t != "" ? Convert.ToDateTime(t) : DateTime.Now,                    
                Site = add["Site"].ToString(),
            };
            _context.ClassSchedule.Add(entity);
            _context.SaveChanges();
            return View();
        }
        public IActionResult Delete(Guid id)//删
        {
            var delete = _context.ClassSchedule.Find(id);
            _context.Remove(delete);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public IActionResult Update(Guid id)
        {
            var Updata = _context.ClassSchedule.Find(id);

            return View(Updata);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, ClassSchedule collection)//改
        {
            var Updata = _context.ClassSchedule.Find(id);
            Updata.ClassScheduleName = collection.ClassScheduleName.ToString();
            Updata.Time = collection.Time;
            Updata.Site = collection.Site.ToString();
            _context.Update(Updata);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Select()//查
        {
            return View();
        }
        public IActionResult ceshi()
        {
            return View();
        }
    }
}
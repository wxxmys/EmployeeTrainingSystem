using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using EmployeeTrainingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
    public class InformationController : Controller
    {
        /// <summary>
        /// 题库
        /// </summary>
        private readonly EntityDbContext _context = new EntityDbContext();
        public IActionResult Index()
        {

            var entity =_context.Information.Include(t => t.ClassSchedule).ToList();
            return View(entity);
        }
        public IActionResult Add()
        {
            var ClassScheduleValue = _context.ClassSchedule.ToList();
            ViewBag.ClassScheduleValue = ClassScheduleValue;//通过ViewBag传递值
            return View();
        }
       [HttpPost]
        public IActionResult Add(Information information)
        {
            var entity = new Information()
            {
                ID = Guid.NewGuid(),
                Title = information.Title,
                Subject = information.Subject,
                Answer=information.Answer,
                ClassSchedule =_context.ClassSchedule.Find(information.ClassSchedule.ID)
            };
          
            _context.Add(entity);
           
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            var entity = _context.Information.Find(id);
           
            return View(entity);
        }
        [HttpPost]
        public IActionResult Edit(Guid id, Information information)
        {
            
            var entity = _context.Information.Find(id);           
            entity.Title = information.Title.ToString().Trim();
            entity.Subject = information.Subject.ToString().Trim();
            entity.Answer = information.Answer.ToString().Trim();
            entity.ClassSchedule = information.ClassSchedule;
            _context.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid id)
        {
            var entity = _context.Information.Find(id);
            _context.Remove(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
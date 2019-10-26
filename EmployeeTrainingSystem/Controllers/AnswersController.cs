using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTrainingSystem.Controllers
{
    public class AnswersController : Controller
    {
        private readonly EntityDbContext _context = new EntityDbContext();
        public IActionResult Index()
        {
            var entity = _context.Answers.ToList();
            return View(entity);
            
        }
        public IActionResult Add()
        {
         
            return View();
        }
        [HttpPost]
        public IActionResult Add(Answer answers)
        {
            var entity = new Answer()
            {
                ID = Guid.NewGuid(),  
                answer=answers.answer
            };
            _context.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid id)
        {
            var entity = _context.Answers.Find(id);

            return View(entity);
            
        }
        [HttpPost]
        public IActionResult Edit(Guid id, Answer answers)
        {

            var entity = _context.Answers.Find(id);

            entity.answer = answers.answer.ToString().Trim();


            _context.Update(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(Guid id)
        {
            var entity = _context.Answers.Find(id);
            _context.Remove(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
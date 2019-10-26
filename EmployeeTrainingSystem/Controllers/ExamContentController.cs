using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
    public class ExamContentController : Controller
    {
        private readonly EntityDbContext _context = new EntityDbContext();
        public IActionResult Index(Guid id)
        {
           //var entity = _context.TestPaper.Find(id);
           var  entity= _context.examContents.Include(t => t.TestPaper).ToList();
            var InformationValue = _context.Information.ToList();
            ViewBag.InformationValue = InformationValue;//通过ViewBag传递值

            return View(entity);
        }
        public IActionResult Add()
        {
            var TestPaperValue = _context.TestPaper.ToList();
            ViewBag.TestPaperValue = TestPaperValue;//通过ViewBag传递值
            var InformationValue = _context.Information.ToList();
            ViewBag.InformationValue = InformationValue;//通过ViewBag传递值
            return View();
        }
        [HttpPost]
        public IActionResult Add(ExamContent examContent)
        {
            var entity = new ExamContent()
            {
                ID = Guid.NewGuid(),
                TestPaper=_context.TestPaper.Find(examContent.TestPaper.ID),
                Information=_context.Information.Find(examContent.Information.ID)
            };
            _context.Add(entity);

            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public IActionResult Edit(Guid id)
        {
            var entity = _context.examContents.Find(id);
            return View(entity);
        }
        [HttpPost]
        public IActionResult Edit(Guid id,ExamContent examContent)
        {
            var entity = _context.examContents.Find(id);
            entity.TestPaper = examContent.TestPaper;
            entity.Information = examContent.Information;
            return RedirectToAction("Index");

        }
        public IActionResult Delete(Guid id)
        {
            var entity = _context.examContents.Find(id);
            _context.Remove(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
 }
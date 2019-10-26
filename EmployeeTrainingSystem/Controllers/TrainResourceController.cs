using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
    //培训资源
    public class TrainResourcetController : Controller
    {
        public readonly EntityDbContext _context;

        public TrainResourcetController(EntityDbContext context)
        {
            _context = context;
        }
        // GET: TrainResource
        public ActionResult Index()
        {
            var list = _context.TrainResource.ToList();
            return View(list);
        }

        // GET: TrainResource/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainResource/Create
        public ActionResult Create()
        {
            ViewBag.TeachingDirection = _context.Teacher.ToString();
            ViewBag.ClassScheduleName = _context.ClassSchedule.ToString();
            return View();
        }

        // POST: TrainResource/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> Create(IFormCollection collection,EmployeeTrainingEntity.TrainResource trainResource)
        {
            if (ModelState.IsValid)
            {
                var newTrainResource = new TrainResource()
                {
                    ID = new Guid(),
                    Person = collection["Person"].ToString(),
                    DateTime = DateTime.Now,
                    TeachingDirection = _context.Teacher.Find(trainResource.TeachingDirection.TeachingDirection),
                    ClassScheduleName = _context.ClassSchedule.Find(trainResource.ClassScheduleName.ID),
                    Resourcecontent = collection["Resourcecontent"].ToString()
                };
                _context.TrainResource.Add(newTrainResource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: TrainResource/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrainResource/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(include: "ID,Person,DateTime,TeachingDirection,ClassScheduleName,Resourcecontent")]TrainResource trainResource)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(trainResource).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(trainResource);
        }

        // GET: TrainResource/Delete/5
       
        public async Task<ActionResult> Delete(Guid ID)
        {
            var TrainResourceDelete = _context.TrainResource.Find(ID);
            _context.Remove(TrainResourceDelete);
            await _context.SaveChangesAsync();
            return Json("ok");
        }
    }
}
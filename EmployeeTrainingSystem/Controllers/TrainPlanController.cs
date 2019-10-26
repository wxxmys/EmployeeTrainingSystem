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
    public class TrainPlanController : Controller
    {//培训计划
        public readonly EntityDbContext _context;
        public TrainPlanController(EntityDbContext context)
        {
            _context = context;
        }
        // GET: TrainPlan
        public ActionResult Index()
        {
            var list = _context.TrainPlan.ToList();
            return View(list);
        }

        // GET: TrainPlan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainPlan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainPlan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var newTrainPlan = new TrainPlan()
                {
                    ID = Guid.NewGuid(),
                    Person = collection["Person"].ToString(),
                    Time = DateTime.Now,
                    Content = collection["Content"].ToString()
                };
                _context.TrainPlan.Add(newTrainPlan);
                // _context.SaveChanges();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: TrainPlan/Edit/5
        public ActionResult Edit(Guid? ID)
        {
            var result = _context.TrainPlan.Find(ID);
            return View(result);
        }

        // POST: TrainPlan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(include:"ID,Person,Time,Content")]TrainPlan trainPlan)
        {
            if(ModelState.IsValid)
            {
                _context.Entry(trainPlan).State = EntityState.Modified;
                //_context.SaveChanges();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainPlan);
        }

        // GET: TrainPlan/Delete/5
        public async  Task<ActionResult> Delete(Guid ID)
        {
            var TrainPlanDelete = _context.TrainPlan.Find(ID);
            _context.Remove(TrainPlanDelete);
            await _context.SaveChangesAsync();
           // _context.SaveChanges();
            return Json("ok");
        }
    }
}
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
    //培训记录
    public class TrainRecordController : Controller
    {
        public readonly EntityDbContext _context;
        public TrainRecordController (EntityDbContext context)
        {
            _context = context;
        }
        // GET: TrainRecord
        public ActionResult Index()
        {
            //var list = _context.TrainRecord.ToList();
            var TrainRecordSelect = _context.TrainRecord.Include(t => t.ClassScheduleName).OrderBy(x => x.DateTime).ToList();
            return View(TrainRecordSelect);
        }

        // GET: TrainRecord/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainRecord/Create
        public ActionResult Create()//新增
        {
            ViewBag.ClassScheduleName = _context.ClassSchedule.ToList();
            return View();
        }

        // POST: TrainRecord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection,EmployeeTrainingEntity.TrainRecord trainRecord)
        {
            if(ModelState.IsValid)
            {
            var newTrainRecord = new TrainRecord()
            {
                ID = Guid.NewGuid(),
                Person = collection["Person"].ToString(),
                DateTime = DateTime.Now,
                ClassScheduleName = _context.ClassSchedule.Find(trainRecord.ClassScheduleName.ID),
                Score = collection["Score"].ToString(),
                TrainContent = collection["TrainContent"].ToString()
            };
            _context.TrainRecord.Add(newTrainRecord);
            await _context.SaveChangesAsync();
            //_context.SaveChanges();
            return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: TrainRecord/Edit/5
        public ActionResult Edit(Guid? ID)//修改
        {
            var result = _context.TrainRecord.Find(ID);
            ViewBag.ClassScheduleName = _context.ClassSchedule.ToList();
            return View(result);
        }

        // POST: TrainRecord/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(include: "ID,Person,DateTime,ClassScheduleName,Score,TrainContent")]TrainRecord trainRecord)
        {
            if(ModelState.IsValid)
            {
                _context.Entry(trainRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                //_context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(trainRecord);
        }

        // GET: TrainRecord/Delete/5
        public async Task<ActionResult> Delete(Guid ID)//删除
        {
            var TrainRecordDelete = _context.TrainRecord.Find(ID);
            _context.Remove(TrainRecordDelete);
            await _context.SaveChangesAsync();
            //_context.SaveChanges();
            return Json("ok");
        }

        // POST: TrainRecord/Delete/5
    }
}
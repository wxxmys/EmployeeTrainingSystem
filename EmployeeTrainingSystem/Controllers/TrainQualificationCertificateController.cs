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
    //培训资格证明
    public class TrainQualificationCertificateController : Controller
    {
        public readonly EntityDbContext _context;
        public TrainQualificationCertificateController (EntityDbContext context)
        {
            _context = context;
        }
        // GET: TrainQualificationCertificate
        public ActionResult Index()
        {
            var list = _context.TrainQualificationCertificate.ToList();
            return View(list);
        }

        // GET: TrainQualificationCertificate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TrainQualificationCertificate/Create
        public ActionResult Create()//新增
        {
            return View();
        }

        // POST: TrainQualificationCertificate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            if(ModelState.IsValid)
            { 
            var newTrainQualificationCertificate = new TrainQualificationCertificate()
            {
                ID = Guid.NewGuid(),
                Person = collection["Person"].ToString(),
                CertificateNumber = collection["CertificateNumber"].ToString(),
                DateTime = DateTime.Now,
                TrainingContent = collection["TrainingContent"].ToString()
            };
            _context.TrainQualificationCertificate.Add(newTrainQualificationCertificate);
            //_context.SaveChanges();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: TrainQualificationCertificate/Edit/5
        public ActionResult Edit(Guid? ID)
        {
            var result = _context.TrainQualificationCertificate.Find(ID);
            return View(result);
        }

        // POST: TrainQualificationCertificate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(include: "ID,Person,CertificateNumber,DateTime,TrainingContent")]TrainQualificationCertificate trainQualificationCertificate)
        {
            if(ModelState.IsValid)
            {
                _context.Entry(trainQualificationCertificate).State = EntityState.Modified;
                //_context.SaveChanges();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainQualificationCertificate);
        }

        // GET: TrainQualificationCertificate/Delete/5
        public async Task<ActionResult> Delete(Guid ID)//删除
        {
            var TrainQualificationCertificateDelete = _context.TrainQualificationCertificate.Find(ID);
            _context.Remove(TrainQualificationCertificateDelete);
            await _context.SaveChangesAsync();
            //_context.SaveChanges();
            return Json("ok");
        }
    }
}
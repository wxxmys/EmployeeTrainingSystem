using System;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
    [Authorize(Roles = "Teacher")]
    /// <summary>
    /// 发布作业
    /// </summary>
    public class PublishjobController : Controller
    {
        
        private readonly EntityDbContext _context;
        public PublishjobController(EntityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //  var PublishjobSelect = _context.Publishjob.ToList();
            //多表关联查询  （AsNoTracking()==非跟踪状态）
            var PublishjobSelect = _context.Publishjob.AsNoTracking().Include(t => t.CollegeClass).Include(t=>t.Information).Include(t => t.Teacher).OrderBy(x => x.Releasetime).ToList();
            return View(PublishjobSelect);
        }
        /// <summary>
        /// 增
        /// </summary>
        /// <returns></returns>
        /// 
        public  IActionResult Insert()
        {
            var InformationValue = _context.Information.ToList();
            ViewBag.InformationValue = InformationValue;//通过ViewBag传递值

            var TeacherValue = _context.Teacher.ToList();
            ViewBag.TeacherValue = TeacherValue;//通过ViewBag传递值

            //var EmployeeValue = _context.Employee.ToList();
            //ViewBag.EmployeeValue = EmployeeValue;//通过ViewBag传递值


            var ClassValue = _context.CollegeClass.ToList();
            ViewBag.ClassValue = ClassValue;//通过ViewBag传递值

            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Insert(Publishjob publishjob)
        {
            if(ModelState.IsValid)
            { 
                var PublishjobInadex = new Publishjob()
                {
                    CollegeClass = _context.CollegeClass.Find(publishjob.CollegeClass.ID),
                    Teacher = _context.Teacher.Find(publishjob.Teacher.Id),
                    Content = publishjob.Content,
                    Information = _context.Information.Find(publishjob.Information.ID),
                };
                _context.Publishjob.Add(PublishjobInadex);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Publishjob");
            }
            return View();
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Delete(Guid id)
        {
            //var PublishjobDelete = _context.Publishjob.SingleOrDefault();
            var PublishjobDelete = _context.Publishjob.Find(id);
            _context.Publishjob.Remove(PublishjobDelete);
            await _context.SaveChangesAsync();

            return Json("ok");
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <returns></returns>
        public IActionResult Update(Guid id)
        {
            var InformationValue = _context.Information.ToList();
            ViewBag.InformationValue = InformationValue;//通过ViewBag传递值

            var TeacherValue = _context.Teacher.ToList();
            ViewBag.TeacherValue = TeacherValue;//通过ViewBag传递值

            //var EmployeeValue = _context.Employee.ToList();
            //ViewBag.EmployeeValue = EmployeeValue;//通过ViewBag传递值

            var ClassValue = _context.CollegeClass.ToList();
            ViewBag.ClassValue = ClassValue;//通过ViewBag传递值

            Publishjob publishjob = _context.Publishjob.Find(id);
            return View(publishjob);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeTrainingEntity.Publishjob publishjob,Guid id)
        {
            if (ModelState.IsValid) { 
            var PublishjobUpdate = _context.Publishjob.Find(id);

            PublishjobUpdate.CollegeClass = _context.CollegeClass.Find(publishjob.CollegeClass.ID);
            PublishjobUpdate.Teacher = _context.Teacher.Find(publishjob.Teacher.Id);
            PublishjobUpdate.Content = publishjob.Content;
            PublishjobUpdate.Information = _context.Information.Find(publishjob.Information.ID);
           // PublishjobUpdate.Employee = _context.Employee.Find(publishjob.Employee.ID);
          
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Publishjob");
            }
            return View();
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <returns></returns>
        public IActionResult Select()
        {
            var PublishjobSelect = _context.Publishjob.ToList();
           // var PublishjobSelect2 = _context.Publishjob.AsQueryable().Where(t => t.ID = id);
            return View(PublishjobSelect);
        }
        /// <summary>
        /// 通过ID查询
        /// </summary>
        /// <returns></return
        public IActionResult SelectID(Guid id)
        {
            var PublishjobSelectID = _context.Publishjob.AsNoTracking().AsQueryable().Where(t=>t.ID==id);
            return View(PublishjobSelectID);
        }

        /// <summary>
        /// 提交作业
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IActionResult SubmitPublishjob()
        {

            return View();
        }
    }
}
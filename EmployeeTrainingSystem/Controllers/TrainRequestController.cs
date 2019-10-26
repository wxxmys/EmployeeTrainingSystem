using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeTrainingEntity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace EmployeeTrainingSystem.Controllers
{
    public class TrainRequestController : Controller
    {
        //培训申请
        public readonly EntityDbContext _context;
        public TrainRequestController(EntityDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page=1,int pageSize = 10)// pageSize默认每页最多显示10行记录
        {
            //var list = _context.TrainRequest.ToList();
            //分页
            //一定要先排序才能跳过
            //var employee = _context.Employee.AsNoTracking().OrderBy(t => t.Name).Skip((page - 1) * 20).Take(20).ToListAsync();//非跟踪（AsNoTracking()），分页

            // var RequestPage = await _context.Member.AsNoTracking().OrderBy(t => t.Name).ToPagedListAsync(page, pageSize);
            //ViewData["PageSize"] = pageSize; //每页最多显示的记录数的返回给视图。以便在下次查询和排序的时候，每页也显示相同的记录数。
            //var RequestPage = await _context.Employee.AsNoTracking().OrderBy(t => t.Name).ToPagedListAsync(page, pageSize);
            var Requestpage =await _context.TrainRequest.ToListAsync();
            return View(Requestpage.ToPagedList(page, pageSize));
            // return View(RequestPage);
            //var TrainRequestSelect =  _context.TrainRequest.Include(t => t.DepartmentName).Include(+t => t.ClassScheduleName).OrderBy(x => x.Time).ToList();//多表关联查询
            // return View(TrainRequestSelect);
        }
       

        public IActionResult Insert()
        {
            //ViewBag.StateEnum = Enum.GetValues(typeof(State)).Cast<State>();
            ViewBag.DepartmentName = _context.Department.ToList();//部门下拉框
            ViewBag.ClassScheduleName = _context.ClassSchedule.ToList();
            ViewBag.DepartmentName = _context.Department.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert(IFormCollection collection, EmployeeTrainingEntity.TrainRequest trainRequest)//增
        {
            if(ModelState.IsValid)
            { 
            var newTrainRequest = new TrainRequest()
            {
                ID = Guid.NewGuid(),
                Person = collection["Person"].ToString(),
                Time = DateTime.Now,
               DepartmentName = _context.Department.Find(trainRequest.DepartmentName.ID),//部门下拉框
                ClassScheduleName = _context.ClassSchedule.Find(trainRequest.ClassScheduleName.ID),
                ApplicationContent = collection["ApplicationContent"].ToString(),
                State=trainRequest.State//枚举状态
            };
            _context.TrainRequest.Add(newTrainRequest);
           // _context.SaveChanges();
            await _context.SaveChangesAsync();//异步
            return RedirectToAction(nameof(Index));
            }
            return View();

        }



        public async Task<IActionResult> Delete(Guid ID)//删
        {
            var TrainRequestDelete = _context.TrainRequest.Find(ID);
            _context.Remove(TrainRequestDelete);
           // _context.SaveChanges();
           await _context.SaveChangesAsync();//异步
            return Json("ok");
        }

        public IActionResult Update(Guid? ID)
        {
            var result = _context.TrainRequest.Find(ID);
            ViewBag.DepartmentName = _context.Department.ToList();//部门下拉框
            ViewBag.ClassScheduleName = _context.ClassSchedule.ToList();
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind(include: "ID,Person,Time,DepartmentName,ClassScheduleName,ApplicationContent,State")]TrainRequest trainRequest)//使用模型绑定进行修改
        {
            if (ModelState.IsValid)
            {
                _context.Entry(trainRequest).State = EntityState.Modified;
               // _context.SaveChanges();
                await _context.SaveChangesAsync();//异步
                return RedirectToAction(nameof(Index));
            }
            return View(trainRequest);

            //var result = _context.TrainRequest.Find(trainRequest.ID);
            //var TrainRequestUpdate = new TrainRequest()
            //{
            //    Person = trainRequest.Person,
            //Time = trainRequest.Time,
            //ApplicationContent=trainRequest.ApplicationContent
            //};
            //_context.Update(result);
            //_context.SaveChanges();
            //return View(result);
        }
   
    }
}
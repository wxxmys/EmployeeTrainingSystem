using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTrainingSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly EntityDbContext _entityDbContext;
        public DepartmentController(EntityDbContext entityDbContext)
        {
            _entityDbContext = entityDbContext;
        }
        //部门
        public IActionResult Index()
        {
          var department =  _entityDbContext.Department.ToList();
            return View(department);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(EmployeeTrainingEntity.Department department)//分类
        {
            var department1 = new Department()
            {
                  DepartmentName = department.DepartmentName,
                  Phone = department.Phone,
                  CreationTime = department.CreationTime
            };
            _entityDbContext.Add(department1);
            _entityDbContext.SaveChanges();
            return Ok();
        }
        public IActionResult Update(EmployeeTrainingEntity.Department department)
        {
            _entityDbContext.Update(department);
            return Ok();
        }
      }
}
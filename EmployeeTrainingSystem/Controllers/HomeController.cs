using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeTrainingSystem.Models;
using EmployeeTrainingEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EmployeeTrainingSystem.Controllers
{
    public class HomeController : Controller
    {
        private EntityDbContext _context { get; }
        public HomeController(EntityDbContext context)
        {
            _context = context;
        }
        //首页
        public IActionResult Index()
        {
            //计划集合
            var detail = _context.TeachingPlans.Include(t => t.Courseware)
                .Where(x=>x.Parent==null).OrderBy(x => x.CreateDate).ToList();
            return View(detail);
        }
        /// <summary>
        /// 首页单击视频展示详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detail(Guid id)
        {
            var detail = _context.TeachingPlans.Include(t => t.Courseware)
                .Where(t => t.Parent.ID == id || t.ID == id).OrderBy(x => x.CreateDate).ToList(); 
            return View(detail);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTrainingSystem.Controllers
{
    public class LiveController : Controller
    {
        //直播
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Insert()//增
        {
            return View();
        }
        public IActionResult Delete()//删
        {
            return View();
        }
        public IActionResult Update()//改
        {
            return View();
        }
        public IActionResult Select()//查
        {
            return View();
        }
    }
}
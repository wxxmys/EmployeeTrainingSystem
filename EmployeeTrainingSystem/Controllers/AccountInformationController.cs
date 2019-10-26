                                                                                                                                                                                                                                                                                                                                                                                                                        using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTrainingSystem.Controllers
{
    public class AccountInformationController : Controller
    {
        //账户信息
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()//登陆
        {
            return View();
        }
        public IActionResult Register()//注册
        {
            return View();
        }
        public IActionResult CangePassword()//修改密码
        {
            return View();
        }
        public IActionResult Information()//个人信息
        {
            return View();
        }
         
    }
}
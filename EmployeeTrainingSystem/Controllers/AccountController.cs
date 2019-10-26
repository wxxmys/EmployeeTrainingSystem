using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using EmployeeTrainingSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Collections.Specialized.BitVector32;

namespace EmployeeTrainingSystem.Controllers
{
    /// <summary>
    /// 成员
    /// </summary>
    public class AccountController : Controller
    {
        private readonly EntityDbContext _dataContext; //数据上下文


        public AccountController(EntityDbContext dataConten)
        {

            _dataContext = dataConten;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public IActionResult SignIn()
        //{
        //    if (User.Identity.IsAuthenticated) //当用户存在，跳转到主页
        //        return RedirectToAction("Index", "Home");
        //    return View();
        //}
        [HttpPost]
        public async Task<ActionResult> SignIn([FromServices]ILogger<AccountController> logger, LoginViewsModel loginViewsModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewsModel);
            }
            
            var  member =  _dataContext.Member.AsNoTracking().FirstOrDefault(t => t.Username.Trim() == loginViewsModel.UserName.Trim());
            if (member == null)
            {
                logger.LogWarning("用户输入了错误的用户名");
                return NotFound();
            }
            if (member.Password != loginViewsModel.Password)
            {
                logger.LogError("用户输入了错误的密码");

                return Unauthorized();
            }
          
            //添加声明
            ClaimsIdentity identity = new ClaimsIdentity("Overview", ClaimTypes.Name, ClaimTypes.Role);
           
            //添加用户Id
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()));
            //添加用户名
            identity.AddClaim(new Claim(ClaimTypes.Name, loginViewsModel.UserName));
            
 
            if (member.Accredit.HasFlag(Accredit.Administrators))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, Accredit.Affairs .ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, Accredit.Resource.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, Accredit.Student.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, Accredit.Teacher.ToString()));
            }
          
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal, new AuthenticationProperties
            { 
                //持久化存储Cookie
                IsPersistent = true,
                //过期时间
                ExpiresUtc = DateTime.Now.AddMinutes(30)
            });
            logger.LogInformation("用户登录成功");
            return RedirectToAction("Index", "Home");

        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            ViewBag.enums = Enum.GetValues(typeof(Accredit)).Cast<Accredit>();
            return View();
        }
 
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// 没有权限的页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Denied()
        {
            return View();
        }
    }
}
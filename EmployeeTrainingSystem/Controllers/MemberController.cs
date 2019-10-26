using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using EmployeeTrainingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.Controllers
{
    //[Authorize(Roles = "Student,Teacher")]
    public class MemberController : Controller
    {
        //人员
        private readonly EntityDbContext _entityDbContext;
        public MemberController(EntityDbContext entityDbContext)
        {
            _entityDbContext = entityDbContext;
        }
       
        public IActionResult Index()
        {
            var member = _entityDbContext.Member.AsNoTracking().FirstOrDefault(t => t.Username.Trim() ==User.Identity.Name);
            
            return View(member);
        }
        [HttpGet]
        public IActionResult AddMember()
        {
            //if (User.Identity.IsAuthenticated)
            //    return RedirectToAction("Index", "Home");
            ViewBag.enums = Enum.GetValues(typeof(Accredit)).Cast<Accredit>();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMember(RegisterViewsModel registerViewsModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewsModel);
            }
            Member member;
            Guid id;
            try
            {

                member = _entityDbContext.Member.AsNoTracking().FirstOrDefault(t => t.Username.Trim() == registerViewsModel.UserName.Trim());
                if (member != null)
                    return Forbid();

                member = new Member
                {
                    Username = registerViewsModel.UserName,
                    Password = registerViewsModel.Password,
                    Id = Guid.NewGuid(),
                    Sex = registerViewsModel.Sex,
                    Name = registerViewsModel.Name,
                    Accredit = registerViewsModel.Accredit,
                 
                    Dateofbirth = registerViewsModel.Dateofbirth,
                    Email = registerViewsModel.Email,
                    MobileNumber = registerViewsModel.MobileNumber,
                };

                id = member.Id;
                _entityDbContext.Member.Add(member);
                await _entityDbContext.SaveChangesAsync();
            }
            catch (SqlException)
            {
                return View("DbLost");
            }

            return Ok();
          
        }
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Avatar(IFormFile files)
        {
            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            filePath = @"wwwroot\img\MyImg\" + files.FileName;
            if (files.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        files.CopyTo(stream);
                    }
                }
            var member = _entityDbContext.Member.FirstOrDefault(x => x.Username == User.Identity.Name);
            member.Avatar = @"img\MyImg\" + files.FileName;
            _entityDbContext.SaveChanges();
            return RedirectToAction("Index","Member");
        }
    }
}
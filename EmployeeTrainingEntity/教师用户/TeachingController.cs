using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingEntity;
using EmployeeTrainingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTrainingSystem.测试
{
    /// <summary>
    /// 教学计划控制器
    /// </summary>
    [Authorize(Roles = "Teacher")]
    public class TeachingController : Controller
    {
        /// <summary>
        /// 课件列表
        /// </summary>
        private List<Courseware> LisCos = new List<Courseware>();
        private readonly EntityDbContext _context;
        public TeachingController(EntityDbContext context)
        {
            _context = context;
        }
        // GET: Teaching
        public async Task<ActionResult> Index()
        {
            var user = _context.Member.AsNoTracking().FirstOrDefault(t => t.Username.Trim() == User.Identity.Name);
            var list = await _context.TeachingPlan.Include(t => t.Teacher).Include(t => t.Courseware).Where(x => x.Teacher == user).OrderByDescending(x => x.StartDate).ToListAsync();
            return View(list);
        }

        //GET: Teaching/Details/5
        public ActionResult Details(Guid id)
        {
            var listC = _context.Courseware.Where(x => x.TeachingPlan.ID == id).OrderBy(x => x.DateTime).ToList();
            var html = _GetHtmlModal(listC);
            return Json(html);
        }

        //        // GET: Teaching/Create
        public ActionResult Create()
        {
            //查询当前登陆对象
            var user = _context.Member.AsNoTracking().FirstOrDefault(t => t.Username.Trim() == User.Identity.Name);
            if (user == null)
                return RedirectToAction("Index", "Home");
            //控件显示时间
            var Com = new TeachingViewModel { StartDate = DateTime.Now, Teacher = user.Name };
            return View(Com);
        }

        // POST: Teaching/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeachingViewModel tcp, List<IFormFile> files)
        {
            //判断实体是否校验通过
            if (!ModelState.IsValid)
                return View();
            //查询当前登陆对象
            var user = _context.Member.AsNoTracking().FirstOrDefault(t => t.Username.Trim() == User.Identity.Name);
            if (user == null)
                return RedirectToAction("Index", "Home");

            try
            {
                //保存文件，liscos
                await UploadFiles(files);

                var tp = new TeachingPlan()
                {
                    Title = tcp.Title,
                    Content = tcp.Content.Trim() != "" ? tcp.Content : "没有内容",
                    CourseSort = tcp.CourseSort,
                    StartDate = Convert.ToDateTime(tcp.StartDate),
                    Teacher = await _context.Member.FindAsync(user.Id)
                };
                _context.TeachingPlan.Add(tp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// 上传文件本地保存的方法
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private async Task UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();
            foreach (var formFile in files)
            {
                filePath = @"Upload\教职工文件\" + formFile.FileName.Replace(" ", "");
                //添加课件
                var com = new Courseware
                {
                    DateTime = DateTime.Now,
                    Titile = formFile.FileName,
                    LinkAddress = filePath,
                };
                LisCos.Add(com);

                //保存文件
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
        }

        // GET: Teaching/Edit/5
        //[ValidateInput(false)]
        public ActionResult Edit(Guid id)
        {
            var result = _context.TeachingPlan.Include(t => t.Teacher).Where(t => t.ID == id).FirstOrDefault();
            var tc = new TeachingViewModel
            {
                Title = result.Title,
                Content = result.Content,
                CourseSort = result.CourseSort,
                StartDate = result.StartDate,
                Teacher = result.Teacher.Name
            };
            return View(tc);
        }

        // POST: Teaching/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeachingViewModel tcp, Guid id)
        {
            try
            {
                //从数据库查找当前数据
                var result = await _context.TeachingPlan.FindAsync(id);
                //查询当前登陆对象
                var user = _context.Member.AsNoTracking().FirstOrDefault(t => t.Username.Trim() == User.Identity.Name && t.Name == tcp.Teacher);

                result.Title = tcp.Title;
                result.Content = tcp.Content.Trim() != "" ? tcp.Content :
                    "前端的发展太快了，应该怎么去追求深度学习而不是一味追求广度？当讨论前端 UI 工程师困境的时候，会立足以个体为主，还是以这个岗位群体为主？我将与你一起探讨。";

                result.CourseSort = tcp.CourseSort;
                result.StartDate = tcp.StartDate.Year != 1 ? Convert.ToDateTime(tcp.StartDate) : DateTime.Now;
                result.Teacher = await _context.Member.FindAsync(user.Id);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tcp);
            }
        }

        // GET: Teaching/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            //教学计划
            var re = await _context.TeachingPlan.FindAsync(id);
            //删除课件
            var listC = await _context.Courseware.Where(x => x.TeachingPlan.ID == id).ToListAsync();
            foreach (var item in listC)
                _context.Remove(item);
            _context.Remove(re);
            //物理文件删除
            if (DeleteFile(listC))
            {
                _context.SaveChanges();
                return Json("ok");
            }
            return Json("delete fail");//失败
        }
        /// <summary>
        /// 删除文件夹以及文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public bool DeleteFile(List<Courseware> files)
        {
            foreach (var item in files)
                if (System.IO.File.Exists(item.LinkAddress))
                {
                    //删除文件
                    System.IO.File.Delete(item.LinkAddress);
                }
            return true;
        }

        /// <summary>
        /// 生成模态框内容
        /// </summary>
        /// <param name="cmts"></param>
        /// <returns></returns>
        public string _GetHtmlModal(List<Courseware> cmts)
        {
            var htmlString = "";

            foreach (var item in cmts)
            {
                htmlString += "<p><a href=" + item.LinkAddress + " target='_blank'>" + item.Titile + "</a></p>";
            }

            return htmlString;
        }
    }
}
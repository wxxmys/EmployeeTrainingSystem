using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeTrainingSystem;
using EmployeeTrainingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace EmployeeTrainingEntity.Controllers

{
    /// <summary>
    /// 教学计划控制器
    /// 用户界面进入
    /// </summary>
    //[Authorize(Roles = "Teacher")]
    public class TeachingController : Controller
    {
        private readonly EntityDbContext _context;
        public TeachingController(EntityDbContext context)
        {
            _context = context;
        }

        // GET: Teaching
        public ActionResult Index()
        {
            var ms = _context.TeachingPlans.Include(t => t.Courseware).OrderBy(x => x.CreateDate).ToList();
            return View(ms);
        }

        #region 计划

        /// <summary>
        /// 创建计划
        /// </summary>
        /// <param name="tcp"></param>
        /// <returns></returns>
        public async Task<ActionResult> Creplan(TeachingViewModel tcp, IFormFile files)
        {
            //去除指定验证
            ModelState.Remove("StartDate");
            ModelState.Remove("EndDate");
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            var address = "";
            if (files != null)
                try
                {
                    //保存文件取地址
                    address = await UploadFiles(files);
                }
                catch { }
            var t = new TeachingPlan
            {
                Title = tcp.Title,
                Cover = address,
                Content = tcp.Content,
                StartDate = tcp.StartDate,
                EndDate = tcp.EndDate,
                CourseSort = tcp.CourseSort,
                CreateDate = DateTime.Now,
                Teacher = null,
                Parent = null
            };
            await _context.TeachingPlans.AddAsync(t);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 删除计划
        /// </summary>
        public async Task<ActionResult> Delplan(Guid id)
        {
            //查询对象包含的内容
            var tcp = await _context.TeachingPlans.FindAsync(id);
            var tcm = _context.TeachingPlans.Where(x => x.Parent.ID == id);//多个
            var tcow = _context.Coursewares.Where(x => x.TeachingPlan == tcp);//当前的课件

            if (tcow.Count() != 0)
            {
                foreach (var c in tcow)
                {
                    _context.Coursewares.Remove(c);
                    //物理文件
                    DeleteFile(c.LinkAddress);
                }
            }

            if (tcm.Count() != 0)//是否有子集
            {
                //删除章节和包含的课件
                foreach (var m in tcm)
                {
                    //多个
                    var mcow = await _context.Coursewares.Where(x => x.TeachingPlan == m).ToListAsync();
                    if (tcow.Count() != 0)
                        foreach (var c in mcow)
                        {
                            _context.Coursewares.Remove(c);
                            //物理文件
                            DeleteFile(c.LinkAddress);
                        }
                    _context.TeachingPlans.Remove(m);
                }
            }
            //当前计划/章节
            if (tcp != null)
            {
                _context.TeachingPlans.Remove(tcp);
                //物理文件
                DeleteFile(tcp.Cover);
            }
            else
            {
                return Content("删除失败!");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region 章节
        /// <summary>
        /// 创建章节
        /// </summary>
        /// <param name="tcp"></param>
        /// <returns></returns>
        public async Task<ActionResult> Crematerial(TeachingViewModel tcp)
        {
            var Parent = await _context.TeachingPlans.SingleAsync(x => x.ID == Guid.Parse(tcp.pid) && x.Parent == null);
            var t = new TeachingPlan
            {
                Title = tcp.Title,
                CreateDate = DateTime.Now,
                Parent = Parent
            };

            await _context.TeachingPlans.AddAsync(t);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// 删除章节
        /// </summary>
        public async Task<ActionResult> Delmaterial(Guid id)
        {
            var tcm = await _context.TeachingPlans.FindAsync(id);
            var cow = _context.Coursewares.Where(x => x.TeachingPlan == tcm);
            //删除包含的课件
            foreach (var c in cow)
            {
                _context.Coursewares.Remove(c);
                //物理文件
                DeleteFile(c.LinkAddress);
            }
            //删除当前章节
            _context.TeachingPlans.Remove(tcm);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        /// <summary>
        /// 修改计划、章节
        /// </summary>
        public async Task<ActionResult> Edit(TeachingViewModel tcp, IFormFile files)
        {
            var result = await _context.TeachingPlans.FindAsync(Guid.Parse(tcp.pid));
            if (result.Parent == null)
            {
                if (files != null)
                {
                    //删除物理文件
                    DeleteFile(result.Cover);
                    //保存文件取地址
                    result.Cover = await UploadFiles(files);
                }
                result.Title = tcp.Title;
                result.Content = tcp.Content;
                result.StartDate = tcp.StartDate;
                result.EndDate = tcp.EndDate;
                result.CourseSort = tcp.CourseSort;
                result.Parent = null;
            }
            else//章节
            {
                result.Title = tcp.Title;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        #region 文件        
        //取文件名
        public string getfileName(ref string fileN)
        {
            //取后缀名          
            var fileLastName = fileN.Substring(fileN.LastIndexOf("."),
                (fileN.Length - fileN.LastIndexOf(".")));

            fileN = fileN.Replace(fileLastName, "");//去后zui的文件名
            return fileLastName;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="cos"></param>
        /// <returns></returns>
        public async Task<ActionResult> CreCows(CowViewModel cos, IFormFile files)
        {

            List<Courseware> clis = new List<Courseware>();
            var plan = await _context.TeachingPlans.FindAsync(Guid.Parse(cos.pid));
            if (cos.Category == IMethod.录播)
            {
                if (files == null)
                    return Content("<script>alert('文件不能为空!');location.href='" + Url.Action("index", "teaching") + "'</script>");
                //保存文件
                var address = await UploadFiles(files);

                //文件名 //取后缀名
                var fileN = files.FileName.ToString();
                var type = getfileName(ref fileN);
                var cows = new Courseware
                {
                    Title = cos.Title ?? fileN,
                    LinkAddress = address,
                    CreateDate = DateTime.Now,
                    Duration = type == "AVI" || type == "mp4" ? type : null,//时长
                    IMethod = cos.Category,
                    Type = type,
                    Size = files.Length,
                };
                //添加文件关联计划
                clis.Add(cows);
                plan.Courseware = clis;
            }
            if (cos.Category == IMethod.直播)
            {
                var cows = new LiveFiles
                {
                    Title = cos.Title,
                    LinkAddress = cos.LinkAddress,
                    StartDate = cos.StartDate,
                    EndDate = cos.EndDate,
                    CreateDate = DateTime.Now,
                    IMethod = cos.Category,
                };
                clis.Add(cows);
                plan.Courseware = clis;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 修改文件
        /// </summary>
        /// <param name="cos"></param>
        /// <returns></returns>
        public async Task<ActionResult> EditCows(CowViewModel cos, IFormFile files)
        {
            var cow = await _context.Coursewares.FindAsync(Guid.Parse(cos.pid));
            var livecow = await _context.LiveFiles.FindAsync(Guid.Parse(cos.pid));
            //修改文件地址
            if (files != null && cow.IMethod == IMethod.录播)//录播
            {
                //删除物理文件
                DeleteFile(cow.LinkAddress);
                //保存文件
                var address = await UploadFiles(files);

                //文件名 //取后缀名
                var fileN = files.FileName.ToString();
                var type = getfileName(ref fileN);

                cow.Title = cos.Title ?? fileN;
                cow.LinkAddress = address;//视频地址
                cow.Duration = type == "AVI" || type == "mp4" ? type : null;//时长
                cow.Type = type;
                cow.Size = files.Length;

            }
            if (livecow != null && livecow.IMethod == IMethod.直播)
            {
                livecow.Title = cos.Title;
                livecow.LinkAddress = cos.LinkAddress;
                livecow.StartDate = cos.StartDate;
                livecow.EndDate = cos.EndDate;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCows(Guid id)
        {
            var cu = _context.Coursewares.Find(id);
            _context.Coursewares.Remove(cu);
            //物理文件
            var result = DeleteFile(cu.LinkAddress);
            _context.SaveChanges();
            if (!result)
                return Content("文件不存在!");
            else
                return RedirectToAction(nameof(Index));

        }

        //文件操作

        #endregion

        #region 物理文件操作
        ///<summary>
        /// 上传文件本地保存的方法
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private async Task<string> UploadFiles(IFormFile files)
        {

            var filePath = Path.GetTempFileName();

            var fileN = files.FileName.ToString();
            var filelastN = getfileName(ref fileN);

            try
            {
                filePath = @"Upload\教职工文件\" + fileN + "-s" + DateTime.Now.Second + filelastN;//去空格等不正常的命名

                //保存文件
                if (filePath.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await files.CopyToAsync(stream);
                    }
                }
            }
            catch { return null; }
            return "\\" + filePath;
        }

        /// <summary>
        /// 删除文件夹以及文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public bool DeleteFile(string LinkAddress)
        {
            try
            {
                var address = LinkAddress.Remove(0, 1);

                if (System.IO.File.Exists(address))
                {
                    //删除文件
                    System.IO.File.Delete(address);
                    return true;
                }
            }
            catch { }
            return false;
        }
        #endregion

        /// <summary>
        /// 获取模态框 创建
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CretModal(Guid id, string sta)
        {
            var tea = await _context.TeachingPlans.FindAsync(id);

            string htmlString = "";
            htmlString += "<div class='modal-header'>";
            htmlString += "<h5 class='modal-title' id=\"myModalLabel\">";
            if (tea != null)//内容
                htmlString += "新增-" + tea.Title;
            else//计划
                htmlString += "新增-空计划";
            htmlString += " </h5>";

            htmlString += "<button type = \"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>";
            htmlString += "</div>";
            htmlString += "<div class=\"container mb-lg-3\">";

            if (tea != null)
            {
                if (tea != null && sta == "z")
                {
                    htmlString += "<form action=\"/Teaching/Crematerial\" method=\"post\">";

                    htmlString += "<div class=\"form-group\">";
                    htmlString += "<label for=\"validationDefault01\" class=\"col-form-label\">标题</label>";
                    htmlString += "<input type=\"text\" class=\"form-control\" id=\"validationDefault01\"  name=\"Title\"  required>";
                    htmlString += "<div class=\" valid-feedback\"></div>";
                    htmlString += "</div>";
                    htmlString += " <input type = \"hidden\" name = \"pid\" value='" + tea.ID + "' />";
                }
                else
                {
                    htmlString += "<form action=\"/Teaching/CreCows\"  enctype=\"multipart/form-data\"  method=\"post\">";
                    htmlString += "<a href='#' onclick='cowrecord()' >录播</a><a href='#'  onclick='cowslive()'>直播</a>";
                    htmlString += "<div class=\"form-group\" id='Cowsbox'>";
                    htmlString += "<div class=\"form-group\">";
                    htmlString += "<label for=\"validationDefault01\" class=\"col-form-label\">文件名</label>";
                    htmlString += "<input type=\"text\" class=\"form-control\" id=\"validationDefault01\"  name=\"Title\" required>";
                    htmlString += "<div class=\" valid-feedback\"></div>";
                    htmlString += "</div>";
                    htmlString += "<label class=\"col-form-label\">上传文件</label>";
                    htmlString += "<div class=\"custom-file\">";
                    htmlString += "<input type=\"file\" name=\"files\" required accept=\"video/*\" /><br />";
                    htmlString += "</div>";
                    htmlString += "</div>";
                    if (sta != "z")
                        htmlString += " <input type = \"hidden\" name = \"pid\" value='" + tea.ID + "' />";
                }
            }
            //创建计划
            else
            {
                htmlString += "<form action=\"/Teaching/Creplan\" enctype=\"multipart/form-data\" method=\"post\">";

                htmlString += "<div class=\"form-group\">";
                htmlString += "<img id=\"imgCover\" src=\"\" class='img-thumbnail' alt='封面' /><br/>";
                htmlString += "<label class=\"col-form-label\">上传封面</label>";
                htmlString += "<div class=\"custom-file\">";
                htmlString += "<input type=\"file\" name=\"files\" onchange='Immediate()' id='file' accept=\"image/*\" />";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"validationDefault01\" class=\"col-form-label\">标题</label>";
                htmlString += "<input type=\"text\" class=\"form-control\" id=\"validationDefault01\"  name=\"Title\"  required>";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"exampleFormControlSelect1\" class=\"col-form-label\">选择分类</label>";
                htmlString += "<select name=\"CourseSort\" class=\"form-control\" id='exampleFormControlSelect1'>";
                var category = Enum.GetValues(typeof(EnumCategory));//分类
                foreach (var item in category)
                {
                    htmlString += "<option value =" + item + ">" + item.ToString() + "</option>";
                }
                htmlString += "</select>";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"message-text\" class=\"col-form-label\">添加描述</label>";
                htmlString += "<textarea class=\"form-control\" id=\"message-text\" name=\"Content\" required></textarea>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"validationDefault02\" class=\"col-form-label\">设置开始时间</label>";
                htmlString += "<input type=\"datetime-local\" class=\"form-control\" id=\"validationDefault02\"  name=\"StartDate\">";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"validationDefault03\" class=\"col-form-label\">设置结束时间</label>";
                htmlString += "<input type=\"datetime-local\" class=\"form-control\" id=\"validationDefault03\"  name=\"EndDate\">";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
            }
            htmlString += "<div class=\"modal-footer\">";
            htmlString += "<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">关</button>";
            htmlString += "<button type=\"submit\" class=\"btn btn-primary\">确定</button>";
            htmlString += "</div>";
            htmlString += "</form>";
            htmlString += "</div>";

            return Json(htmlString);
        }

        /// <summary>
        /// 获取模态框 修改
        /// </summary>
        [HttpPost]
        public ActionResult EditModal(Guid id)
        {
            #region 局部视图方式（易维护）
            //if (tea != null && tea.ParentID != null || cow != null)//课程
            //{
            //    if (tea != null)
            //    {
            //        var teav = new TeachingViewModel
            //        {
            //            Title = tea.Title
            //        };
            //        return PartialView("_Materials", teav);
            //    }
            //    else//上传文件
            //    {
            //        var c = new CowViewModel
            //        {
            //            LinkAddress = cow.LinkAddress
            //        };
            //        return PartialView("_Cows", c);
            //    }
            //}
            //else//计划
            //{
            //    var teav = new TeachingViewModel
            //    {
            //        Title = tea.Title,
            //        Content = tea.Content
            //    };
            //    return PartialView("_plan", teav);
            //}


            //return PartialView("_plan");
            #endregion

            _context.TeachingPlans.Include(t => t.Courseware).ToList();
            var tea = _context.TeachingPlans.Find(id);
            var cow = _context.Coursewares.Find(id);

            string htmlString = "";
            htmlString += "<div class='modal-header'>";
            htmlString += "<h5 class='modal-title' id=\"myModalLabel\">";
            if (tea != null)//内容
                htmlString += "修改-" + tea.Title;
            else//计划else if (cow != null)
                htmlString += "修改-" + cow.Title;
            htmlString += " </h5>";

            htmlString += "  <button type = \"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button>";
            htmlString += "  </div>";
            htmlString += "<div class=\"container mb-lg-3\">";
            if (tea != null && tea.Parent != null || cow != null)
            {
                if (tea != null)
                {
                    htmlString += "<form action=\"/Teaching/Edit\" method=\"post\">";

                    htmlString += "<div class=\"form-group\">";
                    htmlString += "<label for=\"validationDefault01\" class=\"col-form-label\">标题</label>";
                    htmlString += "<input type=\"text\" class=\"form-control\" id=\"validationDefault01\"  name=\"Title\"  required  value='" + tea.Title + "'>";
                    htmlString += "<div class=\" valid-feedback\"></div>";
                    htmlString += "</div>";
                    htmlString += " <input type = \"hidden\" name = \"pid\" value='" + tea.ID + "' />";
                }
                else//修改文件
                {

                    htmlString += "<form action=\"/Teaching/EditCows\"  enctype=\"multipart/form-data\"  method=\"post\">";
                    if (cow.IMethod != IMethod.直播)
                    {
                        htmlString += "<div class=\"form-group\">";
                        htmlString += "<label for=\"validationDefault01\" class=\"col-form-label\">文件名</label>";
                        htmlString += "<input type=\"text\" class=\"form-control\" id=\"validationDefault01\"  name=\"Title\" " +
                            "required value='" + cow.Title + "'>";
                        htmlString += "<div class=\" valid-feedback\"></div>";
                        htmlString += "</div>";
                        htmlString += "<label class=\"col-form-label\">上传文件</label>";
                        htmlString += "<div class=\"custom-file\">";                        
                        htmlString += "<input type=\"file\" name=\"files\"  value='" + cow.LinkAddress + "' required accept=\"video/*\" /><br />";
                        htmlString += "</div>";
                        htmlString += " <input type = \"hidden\" name = \"pid\" value='" + cow.ID + "' />";
                    }
                    else
                    {
                        var livecow = _context.LiveFiles.Find(id);
                        var sdate = Convert.ToInt32(livecow.StartDate.Year) > 1 ? livecow.StartDate.ToString("s") : null;
                        var edate = Convert.ToInt32(livecow.EndDate.Year) > 1 ? livecow.EndDate.ToString("s") : null;

                        htmlString += "<div class=\"form-group\">";
                        htmlString += "<label class=\"col-form-label\">标题</label>";
                        htmlString += "<input name=\"Title\" class=\"form-control\"  required value='" + livecow.Title + "' />";
                        htmlString += "</div>";
                        htmlString += "<div class=\"form-group\">";
                        htmlString += "<label  class=\"col-form-label\">直播地址</label>";
                        htmlString += "<input name=\"LinkAddress\" class=\"form-control\" required value='" + livecow.LinkAddress + "' />";
                        htmlString += "</div>";
                        htmlString += "<div class=\"form -group\">";
                        htmlString += "<label class=\"col-form-label\">选择开始时间</label>";
                        htmlString += "<input name=\"StartDate\" class=\"form-control\" type=\"datetime-local\" " +
                            " value=" + sdate + " >";
                        htmlString += "</div>";
                        htmlString += "<div class=\"form -group\">";
                        htmlString += "<label class=\"col-form-label\">选择结束时间</label>";
                        htmlString += "<input name=\"EndDate\" class=\"form-control\" type=\"datetime-local\" " +
                            " value=" + edate + " >";
                        htmlString += "</div>";
                        htmlString += " <input type = \"hidden\" name = \"pid\" value='" + livecow.ID + "' />";
                    }
                }

            } //修改计划
            else
            {
                var sdate = Convert.ToInt32(tea.StartDate.Year) > 1 ? tea.StartDate.ToString("s") : null;
                var edate = Convert.ToInt32(tea.EndDate.Year) > 1 ? tea.EndDate.ToString("s") : null;

                htmlString += "<form action=\"/Teaching/Edit\" enctype=\"multipart/form-data\" method=\"post\">";

                htmlString += "<div class=\"form-group\">";
                htmlString += "<img id=\"imgCover\" class='img-thumbnail' alt='封面' src='" + tea.Cover + "' > <br/>";
                htmlString += "<label class=\"col-form-label\">上传封面</label>";
                htmlString += "<div class=\"custom-file\">";
                htmlString += "<input type=\"file\" name=\"files\" onchange='Immediate()'  id='file'  accept=\"image/*\" />";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"validationDefault01\" class=\"col-form-label\">标题</label>";
                htmlString += "<input type=\"text\" class=\"form-control\" id=\"validationDefault01\"  name=\"Title\" " +
                    " required  value='" + tea.Title + "' >";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"exampleFormControlSelect1\" class=\"col-form-label\">选择分类</label>";
                htmlString += "<select name=\"CourseSort\" class=\"form-control\" id='exampleFormControlSelect1'>";
                var category = Enum.GetValues(typeof(EnumCategory));
                foreach (var item in category)
                {
                    if (item.ToString() == tea.CourseSort.ToString())
                        htmlString += "<option value =" + item + " selected =\"selected\">" + item.ToString() + "</option>";
                    else
                        htmlString += "<option value =" + item + ">" + item.ToString() + "</option>";
                }
                htmlString += "</select>";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"message-text\" class=\"col-form-label\">添加描述</label>";
                htmlString += "<textarea class=\"form-control\" id=\"message-text\" name=\"Content\" " +
                    " required >" + tea.Content + "</textarea>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"validationDefault02\" class=\"col-form-label\">设置开始时间</label>";
                htmlString += "<input type=\"datetime-local\" class=\"form-control\" id=\"validationDefault02\"  name=\"StartDate\"" +
                    "  value='" + sdate + "'>";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
                htmlString += "<div class=\"form-group\">";
                htmlString += "<label for=\"validationDefault03\" class=\"col-form-label\">设置结束时间</label>";
                htmlString += "<input type=\"datetime-local\" class=\"form-control\" id=\"validationDefault03\"  name=\"EndDate\"" +
                    " value='" + edate + "'>";
                htmlString += "<div class=\" valid-feedback\"></div>";
                htmlString += "</div>";
                htmlString += " <input type = \"hidden\" name = \"pid\" value='" + tea.ID + "' />";
            }

            htmlString += "<div class=\"modal-footer\">";
            htmlString += "<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">关</button>";
            htmlString += "<button type=\"submit\" class=\"btn btn-primary\">确定</button>";
            htmlString += "</div>";
            htmlString += "</form>";
            htmlString += "</div>";

            return Json(htmlString);
        }

        // 创建视图
        public ActionResult CowsRecord(Guid id)
        {
            var cows = _context.Coursewares.Find(id);
            var cow = new CowViewModel { Category = IMethod.录播 };
            return PartialView("_CowsRecord", cow);
        }

        public ActionResult CowsLive(Guid id)
        {
            var cow = new CowViewModel
            {
                Category = IMethod.直播,
            };
            return PartialView("_CowsLive", cow);
        }
    }
}
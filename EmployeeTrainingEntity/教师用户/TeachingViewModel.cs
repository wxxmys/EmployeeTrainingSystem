//using EmployeeTrainingEntity;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EmployeeTrainingSystem.Models.测试
//{
//    public class TeachingViewModel
//    {
//        [Required(ErrorMessage = "标题不能为空")]//字段是必填的
//        [Display(Name = "标题")]//定义字段的呈现名称
//        public string Title { get; set; }//教学计划名称

//        [Required(ErrorMessage = "教学内容不能为空")]//字段是必填的
//        [Display(Name = "教学内容")]//定义字段的呈现名称
//        public string Content { get; set; }//教学内容

//        [Display(Name = "教学方式")]//定义字段的呈现名称
//        public virtual IMethod Method { get; set; }//上课方式

//        [Display(Name = "课件")]//定义字段的呈现名称
//        public virtual ICollection<Courseware> Courseware { get; set; }//课件

//        [Display(Name = "课程分类")]//定义字段的呈现名称
//        public virtual EnumCategory CourseSort { get; set; }//课程分类

//        [Display(Name = "开始时间")]//定义字段的呈现名称
//        [DataType(DataType.Date)]
//        public DateTime StartDate { get; set; }//开始时间

//        [Required]
//        [Display(Name = "制定人")]
//        public virtual string Teacher { get; set; }
//    }
//}

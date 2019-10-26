using EmployeeTrainingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTrainingSystem.Models
{
    public class TeachingViewModel
    {
        //HttpPostedFileBase 
        public string Cover { get; set; }//封面
        [Required]
        public string Title { get; set; }//计划名称
        public string Content { get; set; }//描述
        [Required]
        public EnumCategory CourseSort { get; set; }//分类
        public DateTime StartDate { get; set; }//创建时间
        public DateTime EndDate { get; set; }//结束日期
        public virtual ICollection<Courseware> Courseware { get; set; }//课件
        public string pid { get; set; }//章
    }
}

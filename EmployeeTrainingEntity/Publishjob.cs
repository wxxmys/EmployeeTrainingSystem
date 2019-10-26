using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 作业布置
    /// </summary>
   public class Publishjob
    {
        public Guid ID { get; set; }
       
        /// <summary>
        /// 班级
        /// </summary>
        public CollegeClass CollegeClass { get; set; }

        /// <summary>
        /// 教师类
        /// </summary>
        public Teacher Teacher { get; set; }
        
        /// <summary>
        /// 作业内容
        /// </summary>
        [Required(ErrorMessage ="此字段不能为空")]
        public string Content { get; set; }

        /// <summary>
        /// 作业布置时间
        /// </summary>
        public DateTime Releasetime { get; set; }

        /// <summary>
        /// 题库
        /// </summary>
        public Information Information { get; set; }

        public Publishjob()
        {
            ID = Guid.NewGuid();
            Releasetime = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingSystem.Models
{
    public class EmployeeViewModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        [Display(Name = "姓名")]

        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Genre { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// 入职时间
        /// </summary>
        public DateTime DatesEmployed { get; set; }
        /// <summary>
        /// 所属学院
        /// </summary>
        public Guid College { get; set; }
        /// <summary>
        /// 所属班级
        /// </summary>
      
        public Guid Class { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MobileNumber { get; set; }
    }
}

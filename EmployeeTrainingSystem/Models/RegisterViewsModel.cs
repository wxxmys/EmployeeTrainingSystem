using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EmployeeTrainingEntity;

namespace EmployeeTrainingSystem.Models
{
    public class RegisterViewsModel
    {

        [Required(ErrorMessage = "用户名必填")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "性别")]
        public bool Sex { get; set; }
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "选择身份")]
        public Accredit Accredit { get; set; } 
      
       
        /// <summary>
        /// 出生日期
        /// </summary>
        [Required]
        [Display(Name = "出生日期")]
        public DateTime Dateofbirth { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Required]
        [Display(Name = "手机")]
        public string MobileNumber { get; set; }
    }
}

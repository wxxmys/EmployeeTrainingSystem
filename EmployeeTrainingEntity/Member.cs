using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeTrainingEntity
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; }

         /// <summary>
         /// 用户名
         /// </summary>
        [Required(ErrorMessage = "用户名必填")]
        [Display(Name = "用户名")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "性别")]
        public bool Sex { get; set; }
        /// <summary>
        /// 真是姓名
        /// </summary>
        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; } //手机号码
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime Dateofbirth { get; set; }
        [Required]
        [Display(Name = "邮件")]
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "选择头像")]
        public string Avatar { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        [Required]
        [Display(Name = "选择身份")]
        public virtual Accredit Accredit { get; set; }

        public string Discriminator { get; set; }
    }
}

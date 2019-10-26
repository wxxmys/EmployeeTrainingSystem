using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingSystem.Models
{
    public class UserAddViveMode
    {

        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression()]
        public string Email { get; set; }
    }
}

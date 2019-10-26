using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EmployeeTrainingEntity;

namespace EmployeeTrainingSystem.Models
{
    public class LoginViewsModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [Display(Name="密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

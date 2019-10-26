using EmployeeTrainingEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingSystem.Models
{
    /// <summary>
    /// 我的课程表
    /// </summary>
    public class MySchedule
    {
        public Guid ID { get; set; }
        //public EnumCategory category { get; set; }
        public virtual ICollection<ClassSchedule> ClassSchedule { get; set; } //课程
    }
}

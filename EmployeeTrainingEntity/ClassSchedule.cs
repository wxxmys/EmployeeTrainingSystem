using EmployeeTrainingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 课程
    /// </summary>
    public class ClassSchedule
    {
        
        public Guid ID { get; set; }
        public string ClassScheduleName { get; set; }
        public DateTime Time { get; set; }//时间
        public string Site { get; set; } //地点
        //public virtual Teacher Teacher { get; set; }//老师
        public ClassSchedule()
        {
            ID = Guid.NewGuid();
        }
    }
}

using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeTrainingEntity
{
    public class TrainRequest//培训申请
    {
        public Guid ID { get; set; }
        public string Person { get; set; }
        public DateTime Time { get; set; }
        public Department DepartmentName { get; set; }//部门名称
        public ClassSchedule ClassScheduleName { get; set; }//培训课程名称
        public string ApplicationContent { get; set; }//申请内容
        public State State { get; set; }//状态

    }
}

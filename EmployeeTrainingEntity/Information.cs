using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 题库
    /// </summary>
    public class Information
    {
        public Guid ID { get; set; }
        public string Title { get; set; }//标题
        public string Subject { get; set; }//题目内容

        public string Answer { get; set; }//答案
        public virtual ClassSchedule ClassSchedule { get; set; }//课程分类

        public Information()
        {
            ID = Guid.NewGuid();
        }
    }
}

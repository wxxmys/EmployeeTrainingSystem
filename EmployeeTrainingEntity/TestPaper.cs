using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 试卷
    /// </summary>
   public class TestPaper
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 试卷名称
        /// </summary>
        public string Name { get; set; }
        public virtual Teacher Teacher { get; set; }//老师
        public virtual CollegeClass CollegeClass { get; set; }//班级
      
        public DateTime Time { get; set; }//试卷创建时间

        public string TestTime { get; set; }//考试时间
        public string Performance { get; set; }//成绩
        public TestPaper()
        {
            ID = Guid.NewGuid();
            Time = DateTime.Now;
        }
    }
}

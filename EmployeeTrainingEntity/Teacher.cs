using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 教师类
    /// </summary>
    public class Teacher:Member
    {
        /// <summary>
        /// 学历
        /// </summary>
        public string Degree { get; set; }
        /// <summary>
        /// 授课方向
        /// </summary>
      
        public string TeachingDirection { get; set; }
        //  public  TestPaper TestPaper{ get; set; }
      
    }
   
}

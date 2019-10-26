using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
   public class Students:Member
    {
        /// <summary>
        /// 班级学院 为空就是学院
        /// </summary>
        public CollegeClass CollegeClass { get; set; }
    }
}

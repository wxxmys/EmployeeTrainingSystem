using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    [Flags]
   public enum Accredit:byte
    {
       
       /// <summary>
       /// 学生用户
       /// </summary>
        Student = 0,
        /// <summary>
        /// 教师
        /// </summary>
        Teacher = 2<<1, //4
        /// <summary>
        /// 教务
        /// </summary>
        Affairs = 2<<2, //8
        /// <summary>
        /// 资源管理
        /// </summary>
        Resource = 2<<3, //16
        /// <summary>
        /// 超级管理员
        /// </summary>
        Administrators = Student | Teacher | Affairs | Resource,//28
    }
}

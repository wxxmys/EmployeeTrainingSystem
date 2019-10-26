using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 批改作业
    /// </summary>
   public class CorrectJob
    {
        public Guid ID { get; set; }

        /// <summary>
        /// 作业主人的名称
        /// </summary>
        public Member Member { get; set; }
        /// <summary>
        /// 成绩
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// 批改作业的时间
        /// </summary>
        public DateTime Markhomeworktime { get; set; }

        /// <summary>
        /// 批改人的名称
        /// </summary>
        public Teacher Teacher { get; set; }

        /// <summary>
        /// 作业ID
        /// </summary>
        public  Publishjob Publishjob { get; set; }

        /// <summary>
        /// 批改状态
        /// </summary>
        public bool Correctingstate { get; set; } =false;

      
        public CorrectJob()
        {
            ID = Guid.NewGuid();
            Markhomeworktime = DateTime.Now;
        }
    }
}

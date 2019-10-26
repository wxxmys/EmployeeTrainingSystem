using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
   /// <summary>
   /// 考试结果管理
   /// </summary>
    public class Examination
    {
        public Guid ID { get; set; }
        public Member Member { get; set; }
        public  virtual ICollection<Information> Information { get; set; }//试卷包含的题目
        public DateTime Time { get; set; }
    }
}

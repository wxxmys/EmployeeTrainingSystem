using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    //培训记录
  public  class TrainRecord
    {
        public Guid ID { get; set; }
        public string Person { get; set; }
        public DateTime DateTime { get; set; }
        public ClassSchedule ClassScheduleName { get; set; }//培训课程名称
        public string Score { get; set; }//成绩
        public string TrainContent { get; set; }//培训内容
    }
}

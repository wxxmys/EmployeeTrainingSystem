using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    //培训计划
  public  class TrainPlan
    {
       public Guid ID { get; set;}
        public string Person { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }//培训计划内容 
    }
}

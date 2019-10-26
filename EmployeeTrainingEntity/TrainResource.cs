using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    //培训资源
   public class TrainResource
    {
        public Guid ID { get; set; }
        public string Person { get; set; }
        public DateTime DateTime { get; set; }
        public  Teacher TeachingDirection { get; set; }//授课方向
        public  ClassSchedule ClassScheduleName { get; set; }//所教的课程
        public  string Resourcecontent { get; set; }//资源内容 
    }
}

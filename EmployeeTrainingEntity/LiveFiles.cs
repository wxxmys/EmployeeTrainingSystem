using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    public class LiveFiles : Courseware
    {
        public DateTime StartDate { get; set; }//开始时间
        public DateTime EndDate { get; set; }//截至日期
        public bool State { get; set; }//直播有效状态
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 答案
    /// </summary>
   public class Answer
    {
        public Guid ID { get; set; }

        public Information Information { get;set;}

        public string answer { get; set; }//答案
    }
}

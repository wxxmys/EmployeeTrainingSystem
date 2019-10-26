using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 试卷内容
    /// </summary>
    public class ExamContent
    {
        public Guid ID { get; set; }
        public virtual TestPaper TestPaper { get; set; }

        public virtual Information Information{get;set;}

    }
}

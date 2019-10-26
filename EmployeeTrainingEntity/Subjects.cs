using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 科目
    /// </summary>
   public class Subjects
    {
        public Guid ID { get; set; }
        public string SubjectsName { get; set; } //科目
        public List<Teacher> Teachers { get; set; } //都有什么老师在上这个课

        public Subjects()
        {
            this.ID = Guid.NewGuid();
           
        }
    }
}

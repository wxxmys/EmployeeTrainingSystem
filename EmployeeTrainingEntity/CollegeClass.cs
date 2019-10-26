using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 虚拟学院,与班级 ParentID空为学院
    /// </summary>
    public class CollegeClass
    {
        [Key]
        public Guid ID { get; set; }
        public virtual CollegeClass ParentID { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; } //创建时间
        public CollegeClass()
        {
            this.ID = Guid.NewGuid();
            this.CreationTime = DateTime.Now;
        }
    }
}

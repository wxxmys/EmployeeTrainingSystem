using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingEntity
{
   /// <summary>
   /// 部门
   /// </summary>
    public class Department
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 部门电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 部门创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } 
        public Department()
        {
            this.ID = Guid.NewGuid();
            //this.CreationTime = DateTime.Now;
        }
    }
}

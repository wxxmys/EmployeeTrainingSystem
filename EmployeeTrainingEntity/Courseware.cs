using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingEntity
{
    public class Courseware
    {
        public Guid ID { get; set; }

        public string Title { get; set; }//标题
        public DateTime CreateDate { get; set; } //创建时间
        public string LinkAddress { get; set; }//文件地址
        public string Duration { get; set; }//时长
        public IMethod IMethod { get; set; }//方式
        public double Size { get; set; }//文件大小
        public string Type { get; set; }//类型
        public virtual TeachingPlan TeachingPlan { get; set; }//属于
        public Courseware()
        {
            ID = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}

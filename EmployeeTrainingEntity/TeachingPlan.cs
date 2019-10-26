using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingEntity
{
    public class TeachingPlan
    {
        public Guid ID { get; set; }

        public string Cover { get; set; }//封面
        public string Title { get; set; }//标题
        public string Content { get; set; }//描述
        public virtual EnumCategory CourseSort { get; set; }//课程分类
        public DateTime StartDate { get; set; }//开始时间
        public DateTime EndDate { get; set; }//截至日期
        public DateTime CreateDate { get; set; }//创建时间
        public virtual TeachingPlan Parent { get; set; }//父id
        public virtual Member Teacher { get; set; }//制定计划的老师
        public ICollection<Courseware> Courseware { get; set; }//课程内容
        public TeachingPlan()
        {
            ID = Guid.NewGuid();
            StartDate = DateTime.Now;
        }
    }
}

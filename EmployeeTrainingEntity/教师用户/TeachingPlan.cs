//using EmployeeTrainingEntity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EmployeeTrainingEntity.测试
//{
//    /// <summary>
//    /// 教学计划
//    /// </summary>
//    public class TeachingPlan
//    {
//        public Guid ID { get; set; }

//        public string Cover { get; set; }//封面
//        public string Title { get; set; }//教学计划名称
//        public string Content { get; set; }//教学描述     
//        public virtual EnumCategory CourseSort { get; set; }//课程分类
//        public DateTime StartDate { get; set; }//创建时间
//        public DateTime EndDate { get; set; }//结束日期
//        public virtual Member Teacher { get; set; }//制定计划的老师
//        public ICollection<TeachingMaterial> TeachingMaterials { get; set; }//课程内容

//        public TeachingPlan()
//        {
//            ID = Guid.NewGuid();
//            StartDate = DateTime.Now;
//        }
//    }
//}

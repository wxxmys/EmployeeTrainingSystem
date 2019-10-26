//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace EmployeeTrainingEntity.测试
//{
//    /// <summary>
//    /// 教学内容
//    /// </summary>
//   public class TeachingMaterial
//    {
//        public Guid ID { get; set; }
//        public string Title { get; set; }
//        public string Content { get; set; }//课程描述
//        public virtual TeachingMaterial ParentID { get; set; }//节
//        public virtual IMethod Method { get; set; }//上课方式
//        public DateTime DateTime { get; set; }//创建时间
//        public virtual ICollection<Courseware> Courseware { get; set; }//课件
//        public virtual TeachingPlan TeachingPlan { get; set; }

//        public TeachingMaterial()
//        {
//            ID = Guid.NewGuid();
//            DateTime = new DateTime();
//        }
//    }
//}

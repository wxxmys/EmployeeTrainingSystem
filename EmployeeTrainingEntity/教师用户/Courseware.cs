//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EmployeeTrainingEntity.测试
//{
//    /// <summary>
//    /// 课件
//    /// </summary>
//    public class Courseware
//    {
//        public Guid ID { get; set; }

//        public string Titile { get; set; }//标题
//        public DateTime DateTime { get; set; } //创建时间
//        public string LinkAddress { get; set; }//文件地址
//        public string Duration { get; set; }//时长
//        public virtual IMethod Method { get; set; }//教学方式
//        public virtual TeachingMaterial TeachingMaterial { get; set; }
//        public Courseware()
//        {
//            ID = Guid.NewGuid();
//            DateTime = new DateTime();
//        }
//    }
//}

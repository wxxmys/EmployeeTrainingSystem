using EmployeeTrainingSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 评论回复
    /// </summary>
    public class CommentReply
    {
        public Guid ID { get; set; }
        public string RemoteAnswering { get; set; }//远程答疑
        public DateTime Time { get; set; }//时间
        public virtual ICollection<ClassSchedule> ClassSchedule { get; set; }//培训课程
        //public virtual Employee Employee { get; set; }//人
        public virtual CommentReply ParentReply { get; set; }   //上级回复
        public CommentReply()
        {
            ID = Guid.NewGuid();
            Time = DateTime.Now;
        }
    }
}

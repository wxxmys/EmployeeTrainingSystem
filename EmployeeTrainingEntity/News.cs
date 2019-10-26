using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 消息
    /// </summary>
   public class News
    {
        public Guid ID { get; set; }
        public string Headline { get; set; }//标题
        public string Content { get; set; }//内容
        public DateTime DateTime { get; set; }

    }
}

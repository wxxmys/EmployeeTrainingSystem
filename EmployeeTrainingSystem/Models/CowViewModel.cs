using EmployeeTrainingEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingSystem.Models
{
    public class CowViewModel
    {
        public string Title { get; set; }//标题
        public DateTime DateTime { get; set; } //创建时间
        public string LinkAddress { get; set; }//文件地址
        [DataType(DataType.Time)]
        public string Duration { get; set; }//时长
        public IMethod Category { get; set; }//类别
        public string pid { get; set; }//操作内容
        public DateTime StartDate { get; set; }//开始时间
        public DateTime EndDate { get; set; }//截至日期
        public string State { get; set; }//直播有效状态
    }
}

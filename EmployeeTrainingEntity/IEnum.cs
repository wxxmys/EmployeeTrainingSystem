using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTrainingEntity
{
    /// <summary>
    /// 课程科目枚举
    /// </summary>
    public enum EnumCategory
    {
        计算机科 = 0,
        美术科 = 1,
    }

    /// <summary>
    /// 教学分类
    /// </summary>
    public enum IMethod
    {
        录播=0,
        直播=1
    }
    /// <summary>E
    /// 培训申请审核状态
    /// </summary>
    public enum State
    {
        已通过,
        审核中,
        未通过
    }
}

using System;
using System.ComponentModel;

namespace HangFireTest
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：Class1.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Class1
    /// 创建标识：yjq 2017/6/25 19:22:37
    /// </summary>
    public  class Class1
    {
        [DisplayName("每分钟执行的日志")]
        public static void NameTest()
        {
            NLog.LogManager.GetLogger("11").Info("每分钟执行的日志");
        }
    }
}
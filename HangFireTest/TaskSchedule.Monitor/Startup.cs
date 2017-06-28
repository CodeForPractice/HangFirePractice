using Hangfire;
using Hangfire.Redis;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(TaskSchedule.Monitor.Startup))]

namespace TaskSchedule.Monitor
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：Startup.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：Startup
    /// 创建标识：yjq 2017/6/25 16:51:33
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new RedisStorageOptions
            {
                Prefix = "hangfire:",
                InvisibilityTimeout = TimeSpan.FromHours(3)
            };
            GlobalConfiguration.Configuration
              //.UseColouredConsoleLogProvider()
              .UseRedisStorage("localhost,password=yjq", options: options)
              //.UseSqlServerStorage("Server=.;User ID=sa;Password=123456;database=HangFireTest;Connection Reset=False;")
              ;

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
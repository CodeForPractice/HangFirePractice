using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TaskSchedule.Monitor.Startup))]

namespace TaskSchedule.Monitor
{
    /// <summary>
    /// Copyright (C) 2017 yjq ��Ȩ���С�
    /// ������Startup.cs
    /// �����ԣ������ࣨ�Ǿ�̬��
    /// �๦��������Startup
    /// ������ʶ��yjq 2017/6/25 16:51:33
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("Server=.;User ID=sa;Password=123456;database=HangFireTest;Connection Reset=False;");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
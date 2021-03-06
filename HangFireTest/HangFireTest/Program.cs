﻿using Hangfire;
using Hangfire.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HangFireTest
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var options = new RedisStorageOptions
            {
                Prefix = "hangfire:",
                InvisibilityTimeout = TimeSpan.FromHours(3)
            };
            GlobalConfiguration.Configuration
              .UseColouredConsoleLogProvider()
              .UseRedisStorage("localhost,password=yjq", options: options)
              //.UseSqlServerStorage("Server=.;User ID=sa;Password=123456;database=HangFireTest;Connection Reset=False;")
              ;

            //支持基于队列的任务处理：任务执行不是同步的，而是放到一个持久化队列中，以便马上把请求控制权返回给调用者。
            //   BackgroundJob.Enqueue(() => Console.WriteLine("Simple!"));
            //延迟任务执行：不是马上调用方法，而是设定一个未来时间点再来执行。
            //  BackgroundJob.Schedule(() => Console.WriteLine("Reliable!"), TimeSpan.FromSeconds(5));
            //循环任务执行：一行代码添加重复执行的任务，其内置了常见的时间循环模式，也可基于CRON表达式来设定复杂的模式。
            // RecurringJob.AddOrUpdate(() => Console.WriteLine("Transparent!"), Cron.Minutely);//注意最小单位是分钟

            using (var server = new BackgroundJobServer())
            {
                //List<string> recurringJobIdList = new List<string>();
                var jobId1 = Guid.NewGuid().ToString();
                string jobId2 = BackgroundJob.Enqueue(() => ConsoleWhile());
                RecurringJob.AddOrUpdate(jobId1, () => Class1.NameTest(), Cron.Minutely);
                //for (int i = 0; i < 100; i++)
                //{
                //    BackgroundJob.Enqueue(() => ConsoleWhile());
                //}
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
                RecurringJob.RemoveIfExists(jobId1);
                BackgroundJob.Delete(jobId2);
                server.Dispose();
            }
        }

        [DisplayName("循环输出")]
        public static void ConsoleWhile()
        {
            while (true)
            {
                NLog.LogManager.GetLogger("0").Info("WHERE循环输出" + DateTime.Now.ToString("yyyyMMdd HH:mm:sss"));
                System.Threading.Thread.Sleep(6000);
            }
            
        }
    }
}
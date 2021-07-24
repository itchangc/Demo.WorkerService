using Demo.WorkerService.Application;
using Demo.WorkerService.WorkJob;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApplication;
using MyInfrastructure;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    ConfigurationHelper.Configuration = hostContext.Configuration;

                    services.AddTransient<MyContext>();
                    services.AddTransient<IUserInfoApp, UserInfoApp>();
                    services.AddTransient<Icontainer, MyContainer>();//配置Icontainer接口和Mycontainer类的依赖注入的关系
                    services.AddHostedService<Worker>();
                    //services.AddHostedService<WorkerOne>();
                });
    }
}

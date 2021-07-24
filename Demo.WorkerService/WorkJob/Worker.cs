using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demo.WorkerService.Application;
using Demo.WorkerService.Helper;
using MyApplication;

namespace Demo.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Icontainer _icontainer;
        private readonly IUserInfoApp _userInfo;

        public Worker(ILogger<Worker> logger, Icontainer icontainer, IUserInfoApp userInfoApp)
        {
            _logger = logger;
            _icontainer = icontainer;
            _userInfo = userInfoApp;
        }
        //重写BackgroundService.SatrtAsync方法，在开始服务的时候，执行一些处理逻辑，这里我们仅输出一条日志
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            if (1!=2)
            {
                Console.WriteLine("3213"); 
            }
            LoggerHelper._.Info("开始执行");
            LoggerHelper._.Debug("开始执行");
            LoggerHelper._.Error("开始执行");
            LoggerHelper._.Fatal("开始执行");
            LoggerHelper._.Warn("开始执行");
            var data = _userInfo.GetUserInfo();
            var num = _userInfo.GetUserInfoConts();
            Console.WriteLine(data.Result.Count + "-----" + num);


            _logger.LogInformation("work  staring  at :{time}", DateTimeOffset.Now);

            await base.StartAsync(cancellationToken);

        }
        //第一个 windows服务或linux守护程序 的处理逻辑，由RunTaskOne方法内部启动的Task任务线程进行处理，同样可以从参数CancellationToken stoppingToken中的IsCancellationRequested属性，得知Worker Service服务是否已经被停止
        protected Task RunTaskOne(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                //如果服务被停止，那么下面的IsCancellationRequested会返回true，我们就应该结束循环
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("RunTaskOne running at: {time}", DateTimeOffset.Now);
                    Thread.Sleep(1000);
                }
            }, stoppingToken);
        }
        //第二个 windows服务或linux守护程序 的处理逻辑，由RunTaskTwo方法内部启动的Task任务线程进行处理，同样可以从参数CancellationToken stoppingToken中的IsCancellationRequested属性，得知Worker Service服务是否已经被停止
        protected Task RunTaskTwo(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                //如果服务被停止，那么下面的IsCancellationRequested会返回true，我们就应该结束循环
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("RunTaskTwo running at: {time}", DateTimeOffset.Now);
                    Thread.Sleep(1000);
                }
            }, stoppingToken);
        }
        //第三个 windows服务或linux守护程序 的处理逻辑，由RunTaskThree方法内部启动的Task任务线程进行处理，同样可以从参数CancellationToken stoppingToken中的IsCancellationRequested属性，得知Worker Service服务是否已经被停止
        protected Task RunTaskThree(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                //如果服务被停止，那么下面的IsCancellationRequested会返回true，我们就应该结束循环
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("RunTaskThree running at: {time}", DateTimeOffset.Now);
                    Thread.Sleep(1000);
                }
            }, stoppingToken);
        }

        //重写BackgroundService.ExecuteAsync方法，封装windows服务或linux守护程序中的处理逻辑
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Task taskOne = RunTaskOne(stoppingToken);
                Task taskTwo = RunTaskTwo(stoppingToken);
                Task taskThree = RunTaskThree(stoppingToken);

                await Task.WhenAll(taskOne, taskTwo, taskThree);//使用await关键字，异步等待RunTaskOne、RunTaskTwo、RunTaskThree方法返回的三个Task对象完成，这样调用ExecuteAsync方法的线程会立即返回，不会卡在这里被阻塞
            }
            catch (Exception ex)
            {
                //RunTaskOne、RunTaskTwo、RunTaskThree方法中，异常捕获后的处理逻辑，这里我们仅输出一条日志
                _logger.LogError(ex.Message);
            }
            finally
            {
                //Worker Service服务停止后，如果有需要收尾的逻辑，可以写在这里
            }
        }
        //重写BackgroundService.StopAsync方法，在结束服务的时候，执行一些处理逻辑，这里我们仅输出一条日志
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);

            await base.StopAsync(cancellationToken);
        }


    }

}

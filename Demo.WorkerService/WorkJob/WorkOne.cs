using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.WorkerService.WorkJob
{
    public class WorkerOne : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public WorkerOne(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        //重写BackgroundService.StartAsync方法，在开始服务的时候，执行一些处理逻辑，这里我们仅输出一条日志
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("WorkerOne starting at: {time}", DateTimeOffset.Now);

            await base.StartAsync(cancellationToken);
        }

        //重写BackgroundService.ExecuteAsync方法，封装windows服务或linux守护程序中的处理逻辑
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //如果服务被停止，那么下面的IsCancellationRequested会返回true，我们就应该结束循环
            while (!stoppingToken.IsCancellationRequested)
            {
                //模拟服务中的处理逻辑，这里我们仅输出一条日志，并且等待1秒钟时间
                _logger.LogInformation("WorkerOne running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        //重写BackgroundService.StopAsync方法，在结束服务的时候，执行一些处理逻辑，这里我们仅输出一条日志
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("WorkerOne stopping at: {time}", DateTimeOffset.Now);

            await base.StopAsync(cancellationToken);
        }
    }
}

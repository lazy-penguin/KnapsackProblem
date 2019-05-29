using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnapsackProblem.Utils
{
    public class FindUnfinishedService : IHostedService
    {
        private readonly TaskService TaskService; 

        public FindUnfinishedService(TaskService taskService)
        {
            TaskService = taskService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(() => TaskService.FindUnfinished());
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

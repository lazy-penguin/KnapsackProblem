using DataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnapsackProblem.Utils
{
    public class TaskService
    {
        private Dictionary<int, CancellationTokenSource> TokenSources { get; set; } = new Dictionary<int, CancellationTokenSource>();
        private readonly ContextFactoryService ContextFactory;

        public TaskService(ContextFactoryService contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public void Interrupt(KnapsackContext context, int taskId)
        {
            if (TokenSources.TryGetValue(taskId, out CancellationTokenSource tokenSource))
            {
                var task = context.Tasks.Find(taskId);
                task.Status = null;
                context.SaveChanges();
                tokenSource.Cancel();
                TokenSources.Remove(taskId);
            }
        }

        public void Start(KnapsackTask task, List<Item> itemList)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var solver = new KnapsackProblemSolver
            {
                Items = itemList,
                Token = token,
                Context = ContextFactory.GetContext(),
            };

            int taskId = solver.CreateNewTask(task);
            Task.Factory.StartNew(() => solver.Start());
            TokenSources.Add(taskId, tokenSource);
        }

        public void FindUnfinished()
        {
            var context = ContextFactory.GetContext();
            foreach (var task in context.Tasks.ToList())
            {
                if (task.Status != true)
                {
                    Restart(task.Id);
                }
            }
        }

        public void Restart(int taskId)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var solver = new KnapsackProblemSolver
            {
                Token = token,
                Context = ContextFactory.GetContext(),
            };

            solver.InitTask(taskId);

            var process = Task.Factory.StartNew(() => solver.Start());
            if(!TokenSources.TryAdd(taskId, tokenSource))
            {
                TokenSources.Remove(taskId);
                TokenSources.Add(taskId, tokenSource);
            }
        }
    }
}

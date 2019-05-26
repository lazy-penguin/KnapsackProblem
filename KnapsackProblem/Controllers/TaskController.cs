using DataManagers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KnapsackProblem.Controllers
{
    public static class TaskController
    {
        private static Dictionary<int, CancellationTokenSource> TokenSources { get; set; } = new Dictionary<int, CancellationTokenSource>();

        public static void Interrupt(int taskId)
        {
            CancellationTokenSource tokenSource;
            if(TokenSources.TryGetValue(taskId, out tokenSource))
            {
                tokenSource.Cancel();
                TokenSources.Remove(taskId);
            }
        }

        public static void Start(KnapsackContext context, KnapsackTask task, List<Item> itemList)
        {
            var solver = new KnapsackProblemSolver
            {
                Items = itemList,
                Context = context,
            };

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            int taskId = solver.CreateNewTask(task);
            Task.Factory.StartNew(() => solver.DynamicSolve(token));
            TokenSources.Add(taskId, tokenSource);
        }

        public static void FindUnfinished(KnapsackContext context)
        {
            foreach(var task in context.Tasks.ToList())
            {
                if (task.Status == false)
                {
                    Restart(context, task.Id);
                }
            }
        }

        public static void Restart(KnapsackContext context, int taskId)
        {
            var solver = new KnapsackProblemSolver
            {
                Context = context,
            };
            solver.InitContext();
            solver.InitTask(taskId);

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var process = Task.Factory.StartNew(() => solver.DynamicSolve(token));
            if(!TokenSources.TryAdd(taskId, tokenSource))
            {
                TokenSources.Remove(taskId);
                TokenSources.Add(taskId, tokenSource);
            }
        }
    }
}
